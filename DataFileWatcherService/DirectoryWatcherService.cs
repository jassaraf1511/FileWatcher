using System;
using System.ServiceProcess;
using System.Configuration;
using CFSB.LoggerWriter.Service;
using CFSB.FileProcessing.Services;
using System.IO;

namespace CFSB.FileWatcher.Service
{
    public partial class DirectoryWatcherService : ServiceBase
    {
        private LogWriter loggerInstance;
        public DirectoryWatcherService()
        {
            //this.ServiceName = "FirsDataDirectoryWtacherService";
            InitializeComponent();
            this.loggerInstance = LogWriter.Instance;
            

        }

        protected override void OnStart(string[] args)
        {
            


            this.fileSystemWatcherComponent.EnableRaisingEvents = true;
            this.fileSystemWatcherComponent.Filter = ConfigurationManager.AppSettings["DataFileToProcess"];
            try
            {
                this.fileSystemWatcherComponent.Path = ConfigurationManager.AppSettings["DataFileWatchDirectory"];

            }
            catch (Exception ex)
            {
           
               loggerInstance.WriteToLog(ex);
            }
           
        }

        protected override void OnStop()
        {
       
        }

        private void fileSystemWatcherComponent_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
              loggerInstance.WriteToLog("starting ServivecChanged"+ e.FullPath);
        
            FirstDataFileParsing fileParsing = new FirstDataFileParsing(e.FullPath);
        }

        private void fileSystemWatcherComponent_Created(object sender, System.IO.FileSystemEventArgs e)
        {
            loggerInstance.WriteToLog("starting ServiveCreated" + e.FullPath);
            FirstDataFileParsing fileParsing = new FirstDataFileParsing(e.FullPath);
        }

        private void fileSystemWatcherComponent_Deleted(object sender, System.IO.FileSystemEventArgs e)
        {
            loggerInstance.WriteToLog("starting ServiveDeleted " + e.FullPath +"   - " + e.Name);
        }

        private void fileSystemWatcherComponent_Renamed(object sender, System.IO.RenamedEventArgs e)
        {
            loggerInstance.WriteToLog("starting Servivecrenamed");
        }
        // FileSystemWatcher – OnError Event Handler
        public void OnError(object sender, System.IO.ErrorEventArgs eErrorEventArgse)
        {
            
            
        }
        
}
}
