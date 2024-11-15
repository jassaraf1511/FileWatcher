using System;
using System.Text;
using System.IO;
using System.Data;
using System.Xml;
using System.Xml.Serialization;
using CFSB.DataAccess.DataEntities;
using CFSB.LoggerWriter.Service;

/// <summary>
/// Message Formatter to be Publish to COS & ALMOND need to complete
/// </summary>
namespace CFSB.FileProcessing.Services
{
    public class MessageFormatter
    {
        public static DataTable XmlMessageTable;
        private static MessageFormatter msgFormatterinstance;

        static object lockObject = new object();
        static LogWriter loggerInstance;


        public static MessageFormatter MsgFormatterinstance
        {
            get
            {
                // If the instance is null then create one and init the Queue
                if (msgFormatterinstance == null)
                {
                    msgFormatterinstance = new MessageFormatter();
                    loggerInstance = LogWriter.Instance;
                    loggerInstance.WriteToLog(AppConfig.InterfaceName);

                }
                return msgFormatterinstance;
            }
        }

        /// <summary>
        /// Format Payment Message Base 
        /// </summary>
        /// <param name="sourceRow"></param>
        /// <returns></returns>
        public static PaymentMessages FormatMessage(DataRow sourceRow)
        {
           
            PaymentMessages paymentMessage = new PaymentMessages();
            try
            { 

                return paymentMessage;
            }
            catch (Exception ex)
            {
                loggerInstance.WriteToLog(ex);
                return null;
            }

        }

    }

}




