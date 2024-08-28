using System;
using System.Collections.Generic;
using System.Text;

namespace CFSB.FileWatcherService
{
    public class AppSettings
    {
        public string LogDirectory { get; set; }
        public string LogFileName { get; set; }
        public string InputFolder { get; set; }
      
        public string LoggerTemplate { get; set; }
        public CustomFolderSetting[] CustomFolderSettings { get; set; }

       
    }

   
    public class CustomFolderSetting
    {
        public string FolderEnabled { get; set; }
        public string FolderDescription { get; set; }
        public string FolderFilter { get; set; }
        public string FolderPath { get; set; }
        public string FolderIncludeSub { get; set; }
        public string ExecutablePath { get; set; }
        public string ExecutableFile { get; set; }
        public string ExecutableArguments { get; set; }
        public bool WaitExecutionToComplete { get; set; }
        public string FolderID { get; set; }
        public bool TouchFiles { get; set; }
    }


}
