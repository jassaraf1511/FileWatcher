using System;
using System.Data;
using System.Data.SqlClient;

namespace CFSB.FileProcessing.Services
{
    public class GetDataTableSchema
    {
        private string connectionString;
        public GetDataTableSchema(string connectionString)
        {
            this.connectionString = connectionString;
        }



        public DataTable GetDatatable(string tableName)
        {
          

            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionString);
                
                string query = "SELECT top 1*  FROM dbo." + tableName;

                SqlCommand cmd = new SqlCommand(query, sqlConn);
                sqlConn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                sqlConn.Close();

                return dt;

            }
            catch (Exception ex)
            {
                return null;
            }

        }

   }
}
