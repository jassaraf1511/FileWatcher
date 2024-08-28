using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.WindowsServices;
using Serilog;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace CFSB.FedWireMqService.Services
{
    public class TriggerMQService : BackgroundService
    {
        private readonly ILogger<TriggerMQService> _logger;

        private readonly AppSettings _appSettings;
        private readonly ConnectionStrings _connectionString;
        private MqFundWireConnectivity mqConnectivity;
        private MqFundWireQueues mqQueues;
       // private MqQueueParameters[] mqIncomingQueues;
        public TriggerMQService(ILogger<TriggerMQService> logger, IOptions<AppSettings> appSettings, IOptions<ConnectionStrings> connectionString)
        {
            _logger = logger;

            _appSettings = appSettings.Value;
            
            _connectionString = connectionString.Value;
            mqConnectivity = _appSettings.MqFundWireConnectivity;
            mqQueues = _appSettings.MqFundWireQueues;

            
            Console.WriteLine(mqQueues.MqIncomingFundMessageSetting.MqQueue);
            //   customFolderList = appSettings.Value.CustomFolderSettings;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            MqQueueParameters[] mqIncomingQueues = { mqQueues.MqIncomingFundMessageSetting, mqQueues.MqIncomingStatementSetting, mqQueues.MqIncomingBroadCastSetting, mqQueues.MqIncomingUnsolicitedSetting};
            while (!stoppingToken.IsCancellationRequested)
            {

                foreach (MqQueueParameters queuParm  in mqIncomingQueues)
                {
                    startMqServiceGet(mqConnectivity, queuParm);
                }
                startMqServicePut(mqConnectivity, mqQueues.MqOutgoingFundMessageSetting);
                // _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                // startMqServiceGet(_appSettings.MqFundWireConnectivity);
                    await Task.Delay(1000, stoppingToken);
            }
            stopProcess();
        }
        /// <summary>Event automatically fired when the service is stopped by Windows</summary>
        private void stopProcess()
        {

        }
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            stopProcess();
            _logger.LogInformation("Stopping Service");

            await base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            stopProcess();
            _logger.LogInformation("Disposing Service");

            base.Dispose();
        }

        private void startMqServiceGet(MqFundWireConnectivity mqFundWireSetting, MqQueueParameters mqQueueParam)
        {

            MQTriggerIcomingMessages mqTriggerIncoming = new MQTriggerIcomingMessages(mqFundWireSetting, mqQueueParam,  _logger);

          //  mqTriggerIncoming.GetMessagesFromQueue();
            Thread T1 = new Thread(new ThreadStart(mqTriggerIncoming.GetMessagesFromQueue));
            T1.Name = mqQueueParam.MqQueue;
           T1.Start();
           
        }

        private void startMqServicePut(MqFundWireConnectivity mqFundWireSetting, MqQueueParameters mqQueueParam)
        {

            // mqConnectivity, queuParm
            string folder = mqQueueParam.SaveMessageFolder;
            string fileFilters = mqQueueParam.SaveMessageFileName +"*." +  mqQueueParam.SaveMessageFileExtension;
            string[] files = Directory.GetFiles(folder, fileFilters, SearchOption.TopDirectoryOnly);

            foreach (string file in files)
            {
                try
                {

                    MQTriggerIcomingMessages mqTriggerIncoming = new MQTriggerIcomingMessages(mqFundWireSetting, mqQueueParam, _logger, file);

                    //  mqTriggerIncoming.GetMessagesFromQueue();
                    Thread T1 = new Thread(new ThreadStart(mqTriggerIncoming.PutMessageToQueue));
                    T1.Name = Path.GetFileNameWithoutExtension(file);// mqQueueParam.MqQueue;
                    T1.Start();

                }
                catch (Exception ex)
                {
                    continue;
                }
            }
        }

    }
}
