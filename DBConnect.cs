using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Cfsb.LoggerWriter.Services;
using Cfsb.Incoming.FedWires.DataEntities;


namespace Cfsb.Incoming.FedWires.ORM
{
    public class DBConnect
    {

        private readonly string connectionString;
        private LogWriter log;
        private InterfaceDetail appSettings;
        private static SqlConnection connection = null;

        public string ConnectionString => connectionString;
        public static string connStr;
        public LogWriter Log { get => log; set => log = value; }
        public InterfaceDetail AppSettings { get => appSettings; set => appSettings = value; }
        public static SqlConnection Connection { get => connection; set => connection = value; }

        public DBConnect(LogWriter log, InterfaceDetail appSettings, string connectionString)
        {
            this.connectionString = connectionString;
            this.log = log;
            this.appSettings = appSettings;
            connStr = this.connectionString;
            this.CreateConnection();
        }

        public static void CheckConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                return;
            }
            try
            {
                connection = new SqlConnection(connStr);
                connection.Open();
                return;
            }
            catch (Exception ex)
            {

                
                return;
            }

        }

        private void CreateConnection()
        {
            try
            {
                connection = new SqlConnection(this.connectionString);
                connection.Open();
                return ;
            }
            catch(Exception ex)
            {
              
                log.WriteToLog(ex.Message);
                log.WriteToLog(ex.StackTrace);
                return ;
            }
            
            
        }

        public void CloseConnection()
        {
            try
            {

                connection.Close();
                connection.Dispose();
                
            }
            catch (Exception ex)
            {

                log.WriteToLog(ex.Message);
                log.WriteToLog(ex.StackTrace);
                
            }


        }
        private void initialize()
        {

        }

    }

}