using System;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

namespace CFSB.FileProcessing.Services
{
    public class AppConfig
    {

        /// <summary>
        /// Property: 			DataSource
        ///</summary> 

        public static string SQLInterfaceConnectionString
        {
            get
            {

                try
                {
                    return ConfigurationManager.ConnectionStrings["SQLInterfaceConnectionString"].ToString();
                }
                catch (Exception e)
                {
                    return null;
                }


            }
        }

        /// <summary>
        /// Property: 			AppTempDirectory
        ///</summary> 

        public static string AppTempDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["AppTempDirectory"].ToString();
            }
        }
        /// <summary>
        /// Property: 			AppDataDirectory
        ///</summary> 

        public static string AppDataDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["AppDataDirectory"].ToString();
            }
        }
        /// <summary>
        /// Property: 			AppLogDirectory
        ///</summary> 

        public static string AppLogDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["AppLogDirectory"].ToString();
            }
        }

        /// <summary>
        /// Property: 			AppHomeDirectory
        ///</summary> 

        public static string AppHomeDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["AppHomeDirectory"].ToString();
            }
        }

        /// <summary>
        /// Property: 			AppConfigDirectory
        ///</summary> 

        public static string AppConfigDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["AppConfigDirectory"].ToString();
            }
        }

        /// <summary>
        /// Property: 			UsrDataDirectory
        ///</summary> 

        public static string UsrDataDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["UsrDataDirectory"].ToString();
            }
        }

        /// <summary>
        /// Property: 			UsrTempDirectory
        ///</summary> 

        public static string UsrTempDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["UsrTempDirectory"].ToString();
            }
        }

        /// <summary>
        /// Property: 			UsrLogDirectory
        ///</summary> 

        public static string UsrLogDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["UsrLogDirectory"].ToString();
            }
        }

        /// <summary>
        /// Property: 			UsrLogFileName
        ///</summary> 

        public static string UsrLogFileName
        {
            get
            {
                return ConfigurationManager.AppSettings["UsrLogFileName"].ToString();
            }
        }

        /// <summary>
        /// Property: 			UsrHomeDirectory
        ///</summary> 

        public static string UsrHomeDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["UsrHomeDirectory"].ToString();
            }
        }

        /// <summary>
        /// Property: 			UsrConfigDirectory
        ///</summary> 

        public static string UsrConfigDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["UsrConfigDirectory"].ToString();
            }
        }

        /// <summary>
        /// Property: 			FTPSourceServer
        ///</summary> 

        public static string FTPSourceServer
        {
            get
            {

                return ConfigurationManager.AppSettings["FTPSourceServer"].ToString();
            }
        }

        /// <summary>
        /// Property: 			FTPSourcePort
        ///</summary> 

        public static string FTPSourcePort
        {
            get
            {
                return ConfigurationManager.AppSettings["FTPSourcePort"].ToString();
            }
        }
        /// <summary>
        /// Property: 			InterfaceProviderName
        ///</summary> 

        public static string InterfaceProviderName
        {
            get
            {
                return ConfigurationManager.AppSettings["InterfaceProviderName"].ToString();
            }
        }
        /// <summary>
        /// Property: 			InterfaceProviderID
        ///</summary> 

        public static string InterfaceProviderId
        {
            get
            {
                return ConfigurationManager.AppSettings["InterfaceProviderId"].ToString();
            }
        }
        /// <summary>
        /// Property: 			FTPUSourceserName
        ///</summary> 

        public static string FTPUSourceUserName
        {
            get
            {
                return ConfigurationManager.AppSettings["FTPUSourceUserName"].ToString();
            }
        }

        /// <summary>
        /// Property: 			FTPSourcePassword
        ///</summary> 

        public static string FTPSourcePassword
        {
            get
            {
                return ConfigurationManager.AppSettings["FTPSourcePassword"].ToString();
            }
        }

        /// <summary>
        /// Property: 			FTPSourceRemoteDirectory
        ///</summary> 

        public static string FTPSourceRemoteDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["FTPSourceRemoteDirectory"].ToString();
            }
        }

        /// <summary>
        /// Property: 			FTPSourceFileName
        ///</summary> 

        public static string FTPSourceFileName
        {
            get
            {
                return ConfigurationManager.AppSettings["FTPSourceFileName"].ToString();
            }
        }

        /// <summary>
        /// Property: 			FTPSourceFileExtension
        ///</summary> 

        public static string FTPSourceFileExtension
        {
            get
            {
                return ConfigurationManager.AppSettings["FTPSourceFileExtension"].ToString();
            }
        }

        /// <summary>
        /// Property: 			FTPSourceTriggerFileExtension
        ///</summary> 

        public static string FTPSourceTriggerFileExtension
        {
            get
            {
                return ConfigurationManager.AppSettings["FTPSourceTriggerFileExtension"].ToString();
            }
        }

        /// <summary>
        /// Property: 			FTPDownloadDirectory
        ///</summary> 

        public static string FTPDownloadDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["FTPDownloadDirectory"].ToString();
            }
        }

        /// <summary>
        /// Property: 			FDArchiveDirectory
        ///</summary> 

        public static string ArchiveDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["ArchiveDirectory"].ToString();
            }
        }

        /// <summary>
        /// Property: 			ArchiveFileExtension
        ///</summary> 

        public static string ArchiveFileExtension
        {
            get
            {
                return ConfigurationManager.AppSettings["ArchiveFileExtension"].ToString();
            }
        }

        /// <summary>
        /// Property: 			DataDictionaryPath
        ///</summary> 

        public static string DataDictionaryPath
        {
            get
            {
                return ConfigurationManager.AppSettings["DataDictionaryPath"].ToString();
            }
        }

        /// <summary>
        /// Property: 			DataDictionaryBook
        ///</summary> 

        public static string DataDictionaryBook
        {
            get
            {
                return ConfigurationManager.AppSettings["DataDictionaryBook"].ToString();
            }
        }

        /// <summary>
        /// Property: 			DataDictionarySheet
        ///</summary> 

        public static string DataDictionarySheet
        {
            get
            {
                return ConfigurationManager.AppSettings["DataDictionarySheet"].ToString();
            }
        }

        /// <summary>
        /// Property: 			DataDictionaryXML
        ///</summary> 

        public static string DataDictionaryXML
        {
            get
            {
                return ConfigurationManager.AppSettings["DataDictionaryXML"].ToString();
            }
        }


        /// <summary>
        /// Property: 			ExcelOleDbConnectionString
        ///</summary> 

        public static string ExcelOleDbConnectionString(string dataSourceName)
        {


            // return string.Format(ConfigurationManager.AppSettings["ExcelOleDbConnectionString"].ToString(),dataSourceName);
            return string.Format(ConfigurationManager.ConnectionStrings["ExcelOleDBConnectionString"].ToString(), dataSourceName);

        }

        /// <summary>
        /// Property: 			SHKeyPP
        ///</summary> 

        public static string SHKeyPP
        {
            get
            {
                return ConfigurationManager.AppSettings["SHKeyPP"].ToString();
            }
        }

        /// <summary>
        /// Property: 			SHSaltValue
        ///</summary> 

        public static string SHSaltValue
        {
            get
            {
                return ConfigurationManager.AppSettings["SHSaltValue"].ToString();
            }
        }
        /// <summary>
        /// Property: 			FTP Access Method
        /// 
        /// StandardFTP = 1,
        /// FtpSViaSSLImplicit = 2,
        /// FtpSViaSSLExplicit = 3,
        /// TcpClientSSL = 5,
        /// TcpClient = 4,
        /// SftpViaSSH = 6,
        ///</summary> 
        public static string FTPFtpAccessMethod
        {
            get
            {
                return ConfigurationManager.AppSettings["FTPAccessMethod"].ToString();
            }
        }
        /// <summary>
        /// Property: 			SHKeyPP
        ///</summary> 

        public static string InterfaceName
        {
            get
            {
                return ConfigurationManager.AppSettings["InterfaceName"].ToString();
            }
        }
        /// <summary>
        /// Property: 			InterfaceID
        ///</summary> 

        public static string InterfaceId
        {
            get
            {
                return ConfigurationManager.AppSettings["InterfaceId"].ToString();
            }
        }
        public static string MessageDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["MessageDirectory"].ToString();
            }
        }


        /// <summary>
        /// Property: 			DataDictionaryId
        ///</summary> 

        public static int DataDictionaryId
        {
            get
            {
                int intRersult;
                string stringToConvert = ConfigurationManager.AppSettings["DataDictionaryId"].ToString().Trim();

                return int.TryParse(stringToConvert, out intRersult) ? (int)intRersult : 0;

            }
        }
        /// <summary>
        /// Property: 			DataDictionaryName
        ///</summary> 
        public static string DataDictionaryName
        {
            get
            {

                return ConfigurationManager.AppSettings["DataDictionaryName"].ToString().Trim();

            }
        }
        /// <summary>
        /// Property: 			AppDataDirectory
        ///</summary> 

        private static string createDirectory(string directory)
        {

            string createdDirectory = directory;

            try
            {



                if (directory == null || directory.Trim().Length == 0)
                {
                    // appDataDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
                    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory);

                    createdDirectory = (new System.IO.DirectoryInfo(di.Parent.FullName)).Parent.FullName + @"\NewCreated\";
                }
                if (!System.IO.Directory.Exists(directory))
                    System.IO.Directory.CreateDirectory(directory);
            }
            catch (Exception e)
            {
                System.Console.Write(e.Message);
                System.Console.Write(e.StackTrace.ToString());
            }
            return createdDirectory;


        }



        //End Class
    }
}
