using System;
using System.Collections.Generic;
using System.Text;

namespace CFSB.FedWireMqService
{
    public class AppSettings
    {
        public string LogDirectory { get; set; }
        public string LogFileName { get; set; }
        public string InputFolder { get; set; }

        public string LoggerTemplate { get; set; }
      //  public ConnectionStrings DbConnectionStrings { get; set; }
        public CustomFolderSetting[] CustomFolderSettings { get; set; }

        public MqFundWireConnectivity MqFundWireConnectivity { get; set; }
        public MqFundWireQueues MqFundWireQueues { get; set; }
    }
    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
        public string FedWireDBConnectionString { get; set; }
        public string ExcelOleDBConnectionString { get; set; }

    }
    public class CustomFolderSetting
    {
        public bool FolderEnabled { get; set; }
        public string FolderDescription { get; set; }
        public string FolderFilter { get; set; }
        public string FolderPath { get; set; }
        public bool FolderIncludeSub { get; set; }
        public string ExecutablePath { get; set; }
        public string ExecutableFile { get; set; }
        public string ExecutableArguments { get; set; }

    }
    /* - QueueManagerName: Name of The Queue Manager                      */
    /* - keyRepository   : can be *SYSTEM or *USER                        */
    /* - cipherSpec      : CipherSpec value.                              */
    /* - host            : hostname                                       */
    /* - port            : port number                                    */
    /* - channel         : connection channel                             */
    /* - numberOfMsgs    : number of messages                             */
    /* - sslPeerName     : distinguished name of the server certificate   */
    /* - keyResetCount   : KeyResetCount value                            */
    /* - sslCertRevocationCheck  : Enable Certificate Revocation Check   */
    /*                                                                   */
    /*  "keyRepository" and "cipherSpec" values are required only for    */
    /*	SSL connection.                                                  */
    /*                                                                   */
    public class MqFundWireConnectivity
    {
        public string MqHost { get; set; }
        public string MqHostDescription { get; set; }
        public int MqPort { get; set; }
        public string MqUserId { get; set; }
        public string MqUserPassword { get; set; }
        public string QueueManagerName { get; set; }
        public string MqServer { get; set; }
        public string MqServerConnectionChannel { get; set; }
        public string MqClientConnectionChannel { get; set; }
        public string MqSNDRChannel { get; set; }
        public string MqListener { get; set; }
        public string KeyRepository { get; set; }
        public string CipherSpec { get; set; }
        public string SslCertRevocationCheck { get; set; }
        public string SslPeerName { get; set; }
        public int KeyResetCount { get; set; }

       

    }
    public class MqFundWireQueues
    {
        public MqQueueParameters MqOutgoingFundMessageSetting { get; set; }
        public MqQueueParameters MqIncomingFundMessageSetting { get; set; }
        public MqQueueParameters MqIncomingStatementSetting { get; set; }
        public MqQueueParameters MqIncomingBroadCastSetting { get; set; }
        public MqQueueParameters MqIncomingUnsolicitedSetting { get; set; }
        public MqQueueParameters MqBackupQueueSetting { get; set; }
    }
    public class MqQueueParameters
    {
        public bool MqQueueEnabled { get; set; }
        public string MqQueueType { get; set; }
        public string MqQueueDataType { get; set; }
        public string MqQueueDescription { get; set; }
        public string MqQueue { get; set; }
        public bool MqBrowsing { get; set; }
        public string SaveMessageFolder { get; set; }
        public string SaveMessageFileName { get; set; }
        public string SaveMessageFileExtension { get; set; }
        public bool AppendSaveMessageToFile { get; set; }
        public bool SaveMessageToFile { get; set; }

    }
}

