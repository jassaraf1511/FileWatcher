using System;
using System.IO;
using CFSB.LoggerWriter.Service;
namespace CFSB.DataFileManagement.Services
{
    public class DataFileManager
    {

        private static LogWriter loggerInstance = LogWriter.Instance;
        public static bool MoveFile(string sourceFile, string targetDirectory)
        {
            bool isSuccess = false;
            try
            {
                createDirectory(targetDirectory);
                string destinationFile = targetDirectory + @"\" + Path.GetFileName(sourceFile);
                if (File.Exists(sourceFile))
                {

                     File.Move(sourceFile, destinationFile);
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                    loggerInstance.WriteToLog(String.Format("Source file {0} Not Found!" , sourceFile));
                }
                return isSuccess;
            }
            catch(Exception ex)
            {
                loggerInstance.WriteToLog(ex);
                return false;
            }

        }


        private static void createDirectory(string directory)
        {
            try
            {

                if (!Directory.Exists(directory))
                {
                    System.IO.Directory.CreateDirectory(directory);
                }
            }
            catch (Exception ex)
            {

            }

        }
    }
}
