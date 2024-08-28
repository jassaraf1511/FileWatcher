using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using IBM.WMQ;
using System.Transactions;
//using Serilog;
using Microsoft.Extensions.Logging;
//using Serilog.Events;
//using Serilog.Core;

namespace CFSB.FedWireMqService.Services
{
    public class MQTriggerIcomingMessages
    {
        private MqFundWireConnectivity mqConnectivityParms;
        private MqQueueParameters mqQueueParm;
        private string messageFileName;

        /// <summary>
        /// Dictionary to store the properties
        /// </summary>
        private IDictionary<string, object> properties = null;

        private StreamWriter messageFile;
        private readonly ILogger<TriggerMQService> logger;
        private string outPutMessageFile;

        public MQTriggerIcomingMessages(MqFundWireConnectivity mqConnectivityParms, MqQueueParameters mqQueueParm, ILogger<TriggerMQService> logger)
        {
            this.mqConnectivityParms = mqConnectivityParms;
            this.mqQueueParm = mqQueueParm;
            this.logger = logger;
            properties = new Dictionary<string, object>();

            loadMqParameters();
        }

        public MQTriggerIcomingMessages(MqFundWireConnectivity mqConnectivityParms, MqQueueParameters mqQueueParm, ILogger<TriggerMQService> logger, string outPutMessageFile)
        {
            this.mqConnectivityParms = mqConnectivityParms;
            this.mqQueueParm = mqQueueParm;
            this.logger = logger;
            this.outPutMessageFile = outPutMessageFile;
            properties = new Dictionary<string, object>();

            loadMqParameters();
        }
        public MqFundWireConnectivity MqConnectivityParms { get => mqConnectivityParms; set => mqConnectivityParms = value; }
        public MqQueueParameters MqQueueParm { get => mqQueueParm; set => mqQueueParm = value; }
        public string MessageFileName { get => messageFileName; set => messageFileName = value; }


        public void GetMessagesFromQueue()
        {


            createDirectory(mqQueueParm.SaveMessageFolder);
            int count = 0;
            try
            {

                String queueName = Convert.ToString(properties["QueueName"]);
                int noOfMsgs = Convert.ToInt32(properties["MessageCount"]);
                messageFileName = getFileName();
                messageFile = new StreamWriter(messageFileName);

                logger.LogInformation("Connecting to queue manager.. ");
                logger.LogInformation("Accessing queue " + queueName + ".. ");
                   for (int i=0;i<5000;i++)
                   {
                       string message1 = "Accessing queue " + queueName +  "\t New Message Arrived at " + DateTime.Now.ToString() + "Sequence " + i;
                       storeMessageToFile(message1);
                      // logger.LogInformation(message1);
                   }
                   closeFile();
                string fileTxt = messageFileName.Replace(".tmp", ".txt");
                   renameFile(messageFileName, fileTxt);
                   return;
              
                // create connection

                logger.LogInformation("Connecting to queue manager.. ");

                using (var queueManager = CreateQMgrConnection())
                {


                    // accessing queue
                    logger.LogInformation("Accessing queue " + queueName + ".. ");


                    MQGetMessageOptions Gmo = new MQGetMessageOptions();
                    Gmo.WaitInterval = 30;
                    Gmo.Options |= MQC.MQGMO_WAIT + MQC.MQGMO_SYNCPOINT;
                    var queue = queueManager.AccessQueue(queueName, MQC.MQOO_INPUT_AS_Q_DEF + MQC.MQOO_FAIL_IF_QUIESCING);


                    // getting messages continuously


                    while (true)
                    {
                        // creating a message object
                        using (CommittableTransaction tx = new CommittableTransaction())
                        {
                            var message = new MQMessage();
                            count++;

                            try
                            {
                                // get message optionsf
                                MQGetMessageOptions mqGetMessageOptions = new MQGetMessageOptions();
                                mqGetMessageOptions.Options = MQC.MQGMO_FAIL_IF_QUIESCING + MQC.MQGMO_NO_WAIT + MQC.MQGMO_BROWSE_NEXT; // browse with no wait
                                mqGetMessageOptions.MatchOptions = MQC.MQMO_NONE; // no matching required



                                queue.Get(message, Gmo);
                                // decode timestamp of message when it was putted in source queue
                                var timestamp = DateTime.ParseExact(
                                    ASCIIEncoding.ASCII.GetString(message.MQMD.PutDate) +
                                    ASCIIEncoding.ASCII.GetString(message.MQMD.PutTime),
                                    "yyyyMMddHHmmssff", System.Globalization.CultureInfo.InvariantCulture);
                                var msgId = ASCIIEncoding.ASCII.GetString(message.MQMD.MsgId);

                                string mqMessage = message.ReadString(message.MessageLength);
                                logger.LogInformation("Message " + " Received. Data = " + message.ReadString(message.MessageLength));
                                logger.LogInformation("id " + System.Text.UTF8Encoding.UTF8.GetString(message.MessageId) + "QUEUE " + message.MessageSequenceNumber);
                                storeMessageToFile(mqMessage);
                                tx.Commit();
                                //   queueManager.Commit();
                                message.ClearMessage();

                            }
                            catch (MQException mqe)
                            {
                                switch (mqe.ReasonCode)
                                {
                                    case IBM.WMQ.MQC.MQRC_NO_MSG_AVAILABLE:
                                        logger.LogInformation("error" + "No message available.");
                                        break;
                                    case IBM.WMQ.MQC.MQRC_Q_MGR_QUIESCING:
                                    case IBM.WMQ.MQC.MQRC_Q_MGR_STOPPING:
                                        logger.LogInformation("error" + "Queue Manager Stopping: " + "\t" + mqe.Message);
                                        break;
                                    case IBM.WMQ.MQC.MQRC_Q_MGR_NOT_ACTIVE:
                                    case IBM.WMQ.MQC.MQRC_Q_MGR_NOT_AVAILABLE:
                                        logger.LogInformation("error" + "Queue Manager not available: " + "\t" + mqe.Message);
                                        break;
                                    default:
                                        logger.LogInformation("error" + " Error reading topic: " + "\t" + mqe.Message);
                                        tx.Rollback();
                                        queueManager.Backout();
                                        break;
                                }
                                if (mqe.ReasonCode == 2033)
                                    logger.LogInformation("No messages available at queue");
                                else
                                    logger.LogInformation("MQException received. Details: {0} - {1}", mqe.ReasonCode, mqe.Message);

                                break;
                            }
                            catch (Exception ex)
                            {

                                tx.Rollback();


                            }
                        }
                    }
                    // closing queue
                    logger.LogInformation("Closing queue.. ");
                    //  queue.Close();
                    logger.LogInformation("done");
                    queueManager.Commit();
                    queueManager.Close();
                    queueManager.Disconnect();
                    // disconnecting queue manager
                    logger.LogInformation("Disconnecting queue manager.. ");
                }
                logger.LogInformation("done");
            }
            catch (MQException mqe)
            {
                logger.LogInformation("");
                logger.LogInformation("MQException received. Details: {0} - {1}", mqe.ReasonCode, mqe.Message);
                logger.LogInformation(mqe.StackTrace);
            }
            closeFile();
            // return count;
        }

        public void PutMessageToQueue()
        {

            String queueName = Convert.ToString(properties["QueueName"]);
            int noOfMsgs = Convert.ToInt32(properties["MessageCount"]);
            logger.LogInformation("Connecting to queue manager.. ");
            logger.LogInformation("Accessing queue " + queueName + ".. ");

            if (!File.Exists(outPutMessageFile))
            {
                logger.LogInformation("Could Not Process {0} , this file does not exist ", outPutMessageFile);
                return;
            }

            try
            {
              //  using (var queueManager = CreateQMgrConnection())
                {

                //    var queue = queueManager.AccessQueue(queueName, MQC.MQOO_OUTPUT + MQC.MQOO_FAIL_IF_QUIESCING);

                    // Process Message File To Send To FRB

                    string processingFile = outPutMessageFile.Replace("." + mqQueueParm.SaveMessageFileExtension, ".prc");
                    renameFile(outPutMessageFile, processingFile);
                    using (StreamReader reader = new StreamReader(processingFile))
                    {
                        try
                        {
                            string messageLine;
                            while ((messageLine = reader.ReadLine()) != null)
                            {
                                var message = new MQMessage();
                                try
                                {
                                    message.WriteString(messageLine);
                                //    queue.Put(message);
                                    logger.LogInformation(messageLine);
                                }
                                catch(MQException mqePut)
                                {
                                    logger.LogInformation("");
                                    logger.LogInformation("MQException received. Details: {0} - {1}", mqePut.ReasonCode, mqePut.Message);
                                    logger.LogInformation(mqePut.StackTrace);
                                }
                            }
                        }
                        catch (Exception exFile)
                        {
                            logger.LogInformation("");
                            logger.LogInformation("Outgoing File  Execption on file  : {0} - Message Error {1}", outPutMessageFile, exFile.Message);
                            logger.LogInformation(exFile.StackTrace);
                        }
                        reader.Close();
                        reader.Dispose();
                        renameFile(processingFile, processingFile.Replace(".prc",".done"));
                    }

                }
            }
            catch (MQException mqe)
            {
                logger.LogInformation("");
                logger.LogInformation("MQException received. Details: {0} - {1}", mqe.ReasonCode, mqe.Message);
                logger.LogInformation(mqe.StackTrace);

            }
            finally
            {
               
            }

            // return count;
        }
        private bool loadMqParameters()
        {
            try
            {

                properties.Add(MQC.MQPSC_Q_MGR_NAME, mqConnectivityParms.QueueManagerName);
                properties.Add(MQC.HOST_NAME_PROPERTY, mqConnectivityParms.MqHost);
                properties.Add(MQC.PORT_PROPERTY, mqConnectivityParms.MqPort);

                properties.Add(MQC.CHANNEL_PROPERTY, mqConnectivityParms.MqServerConnectionChannel);
                properties.Add(MQC.SSL_CERT_STORE_PROPERTY, "");
                properties.Add(MQC.SSL_CIPHER_SPEC_PROPERTY, "");
                properties.Add(MQC.SSL_PEER_NAME_PROPERTY, "");
                properties.Add(MQC.SSL_RESET_COUNT_PROPERTY, 0);
                properties.Add("QueueName", mqQueueParm.MqQueue);
                properties.Add("MessageCount", 1);
                properties.Add("sslCertRevocationCheck", false);
                properties.Add(MQC.USER_ID_PROPERTY, mqConnectivityParms.MqUserId);
                properties.Add(MQC.PASSWORD_PROPERTY, mqConnectivityParms.MqUserPassword);

            }
            catch (Exception e)
            {
                logger.LogInformation("Exeption caught while parsing command line arguments: " + e.Message);
                logger.LogInformation(e.StackTrace);
                return false;
            }

            return true;
        }
        /// <summary>
        /// Create a connection to the Queue Manager
        /// </summary>
        private MQQueueManager CreateQMgrConnection()
        {


            // mq properties

            var connectionProperties = new Hashtable {

                      { MQC.HOST_NAME_PROPERTY, properties[MQC.HOST_NAME_PROPERTY] },
                      { MQC.PORT_PROPERTY, properties[MQC.PORT_PROPERTY] },
                      { MQC.CHANNEL_PROPERTY, properties[MQC.CHANNEL_PROPERTY] }  };




            connectionProperties.Add(MQC.USER_ID_PROPERTY, properties[MQC.USER_ID_PROPERTY]);
            connectionProperties.Add(MQC.PASSWORD_PROPERTY, properties[MQC.PASSWORD_PROPERTY]);

            if ((String)properties[MQC.SSL_CERT_STORE_PROPERTY] != "") connectionProperties.Add(MQC.SSL_CERT_STORE_PROPERTY, properties[MQC.SSL_CERT_STORE_PROPERTY]);
            if ((String)properties[MQC.SSL_CIPHER_SPEC_PROPERTY] != "") connectionProperties.Add(MQC.SSL_CIPHER_SPEC_PROPERTY, properties[MQC.SSL_CIPHER_SPEC_PROPERTY]);
            if ((String)properties[MQC.SSL_PEER_NAME_PROPERTY] != "") connectionProperties.Add(MQC.SSL_PEER_NAME_PROPERTY, properties[MQC.SSL_PEER_NAME_PROPERTY]);
            if ((Int32)properties[MQC.SSL_RESET_COUNT_PROPERTY] != 0) connectionProperties.Add(MQC.SSL_RESET_COUNT_PROPERTY, properties[MQC.SSL_RESET_COUNT_PROPERTY]);
            if ((Boolean)properties["sslCertRevocationCheck"] != false) MQEnvironment.SSLCertRevocationCheck = true;



            return new MQQueueManager(mqConnectivityParms.QueueManagerName, connectionProperties);

        }

        private string getFileName()
        {
            DateTime dt = DateTime.Now;
            string messageFileName = mqQueueParm.SaveMessageFileName + string.Format("{0:yyyyMMddHHmmss}", dt) + ".tmp";
          //  Console.WriteLine(messageFileName);
            return Path.Combine(mqQueueParm.SaveMessageFolder, messageFileName);

        }
        //
        // Create Directory
        //
        private bool createDirectory(string dirName)
        {

            if (Directory.Exists(dirName)) return true;
            try
            {

                Directory.CreateDirectory(dirName);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogInformation("Cfreate Directory error :" + dirName + " Error Message : " + ex.Message);
                return false;
            }


        }

        private void renameFile(string sourceFile, string targetFile)
        {
            try
            {

                File.Move(sourceFile, targetFile);
            }
            catch (Exception ex)
            {

                return;
            }
        }

        private string moveFile(string sourceFile, string targetDirectory)
        {
            string targeFile = string.Empty;
            try
            {
                targeFile = Path.Combine(targetDirectory, Path.GetFileName(sourceFile));
                File.Move(sourceFile, targeFile);
            }
            catch (Exception ex)
            {

                return targeFile;
            }
            return targeFile;
        }
        private void storeMessageToFile(string message)
        {
            try
            {


                messageFile.WriteLine(message);
                messageFile.Flush();

            }
            catch { }

        }

        private void closeFile()
        {


            try
            {
                messageFile.Flush();
                messageFile.Close();
                messageFile.Dispose();
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);
            }

        }
    }



}



