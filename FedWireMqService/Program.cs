using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Serilog;
using Serilog.Enrichers;
using Serilog.Events;

namespace CFSB.FedWireMqService
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }
        public static void Main(string[] args)
        {
            AppSettings appSettings = new AppSettings();
            ConnectionStrings connectionStrings = new ConnectionStrings();

            Configuration = new ConfigurationBuilder()
            .AddJsonFile("appSettings.json")
            .AddEnvironmentVariables()
            .Build();

            Configuration.GetSection("AppSettings").Bind(appSettings);
            Configuration.GetSection("DbConnectionStrings").Bind(connectionStrings);

            var baseDir = appSettings.LogDirectory;            
            var loggerTemplate = appSettings.LoggerTemplate;
            var logfile = Path.Combine(baseDir, appSettings.LogFileName + ".log");

            
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.With(new ThreadIdEnricher())
                .Enrich.FromLogContext()
                //     .WriteTo.Console(LogEventLevel.Information, loggerTemplate, theme: AnsiConsoleTheme.Literate)
                .WriteTo.File(logfile, LogEventLevel.Information, loggerTemplate,
                    rollingInterval: RollingInterval.Day, retainedFileCountLimit: 90)
                .CreateLogger();
            Log.Information("====================================================================");
            Log.Information($"Application Starts. Version: {System.Reflection.Assembly.GetEntryAssembly()?.GetName().Version}");
            Log.Information($"Application info: {appSettings.InputFolder}");
           

           // var dbConnectionStrings = Configuration.GetSection("DbConnectionStrings").Get<ConnectionStrings>();
          //  var connectionString = appSettings.DbConnectionStrings.FedWireDBConnectionString;
            

            int KeyResetCount = appSettings.MqFundWireConnectivity.KeyResetCount;
            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

                .ConfigureServices((hostContext, services) =>
                {

                   services.AddHostedService<Services.TriggerMQService>();

                    services.Configure<AppSettings>(hostContext.Configuration.GetSection("AppSettings"));
                   services.Configure<ConnectionStrings>(hostContext.Configuration.GetSection("DbConnectionStrings"));

                })
           .UseWindowsService()
           .UseSerilog();

    }
}
