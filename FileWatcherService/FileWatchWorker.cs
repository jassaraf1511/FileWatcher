using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.WindowsServices;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace CFSB.FileWatcherService
{
    public class FileWatchWorker : BackgroundService
    {
        private readonly ILogger<FileWatchWorker> _logger;
       
        private readonly AppSettings _appSettings;
      
        private CustomFolderSetting[] customFolderList;
        private List<FileSystemWatcher> listFileSystemWatcher;

        public FileWatchWorker(ILogger<FileWatchWorker> logger, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            customFolderList = appSettings.Value.CustomFolderSettings;
            _appSettings = appSettings.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            listFileSystemWatcher = new List<FileSystemWatcher>();
            foreach (CustomFolderSetting customFolder in customFolderList)
            {
                startFileSystemWatcher(customFolder);
            }
            // customFolderSetting = customFolderList[0];
            // startFileSystemWatcher(customFolderSetting);
           
            
            while (!stoppingToken.IsCancellationRequested)
            {
               // _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
            stopProcess();
        }

        /// <summary>Event automatically fired when the service is stopped by Windows</summary>
        private void stopProcess()
        {
            if (listFileSystemWatcher != null)
            {
                foreach (FileSystemWatcher fsw in listFileSystemWatcher)
                {
                    // Stop listening
                    fsw.EnableRaisingEvents = false;
                    // Dispose the Object
                    fsw.Dispose();
                }
                // Clean the list
                listFileSystemWatcher.Clear();
            }
        }
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            stopProcess();
            _logger.LogInformation("Stopping Service");

            await base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            stopProcess();
            _logger.LogInformation("Disposing Service");

            base.Dispose();
        }
        private void startFileSystemWatcher(CustomFolderSetting customFolder)
        {

            if (customFolder == null) return;

            if (string.IsNullOrEmpty(customFolder.FolderEnabled)) return;
            string folderStatus = customFolder.FolderEnabled.ToLower().Trim();

            if (!(folderStatus.Equals("true") || folderStatus.Equals("false"))) return;

            bool folderEnable = Convert.ToBoolean(folderStatus);
       
            DirectoryInfo dir = new DirectoryInfo(customFolder.FolderPath);

            if (!folderEnable || !dir.Exists) return;

            FileSystemWatcher fileSWatch = new FileSystemWatcher();
            fileSWatch.Filter = customFolder.FolderFilter;
            fileSWatch.Path = customFolder.FolderPath;
            fileSWatch.IncludeSubdirectories = false;
            if (customFolder.FolderIncludeSub.ToLower().Equals("true"))
            {
                fileSWatch.IncludeSubdirectories = true;
            }

            // Sets the action to be executed
            string executable = Path.Combine(customFolder.ExecutablePath, customFolder.ExecutableFile);
            StringBuilder actionToExecute = new StringBuilder(executable);

            // List of arguments
            StringBuilder actionArguments = new StringBuilder(customFolder.ExecutableArguments);

            fileSWatch.NotifyFilter = NotifyFilters.LastAccess |
                NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            // Associate the event that will be triggered when a new file
            // is added to the monitored folder, using a lambda expression                   
            fileSWatch.Created += (senderObj, fileSysArgs) => fileSWatch_Created(senderObj, fileSysArgs,
                actionToExecute.ToString(), actionArguments.ToString());

            fileSWatch.Changed += (senderObj, fileSysArgs) => fileSWatch_Created(senderObj, fileSysArgs,
      actionToExecute.ToString(), actionArguments.ToString());

            // Begin watching
            fileSWatch.EnableRaisingEvents = true;
            // Add the systemWatcher to the list
            listFileSystemWatcher.Add(fileSWatch);
            // Record a log entry into Windows Event Log
            _logger.LogInformation(String.Format("Starting to monitor files with extension ({0}) in the folder ({1})",fileSWatch.Filter, fileSWatch.Path));
              
               
            if (customFolder.TouchFiles) touchFiles(customFolder.FolderPath, customFolder.FolderFilter);
        }

        /// <summary>This event is triggered when a file with the specified
        /// extension is created on the monitored folder</summary>
        /// <param name="sender">Object raising the event</param>
        /// <param name="e">List of arguments - FileSystemEventArgs</param>
        /// <param name="action_Exec">The action to be executed upon detecting a change in the File system</param>
        /// <param name="action_Args">arguments to be passed to the executable (action)</param>
        void fileSWatch_Created(object sender, FileSystemEventArgs e,
         string action_Exec, string action_Args)
        {
            string fileName = e.FullPath;
            if (fileName.Contains(" "))
            {
                fileName="\""+ e.FullPath + "\"";
            }
            // Adds the file name to the arguments. The filename will be placed in lieu of {0}
            string newStr = string.Format(action_Args, fileName);
            // Executes the command from the DOS window
            ExecuteCommandLineProcess(action_Exec, newStr);
        }

        /// <summary>Executes a set of instructions through the command window</summary>
        /// <param name="executableFile">Name of the executable file or program</param>
        /// <param name="argumentList">List of arguments</param>
        private void ExecuteCommandLineProcess(string executableFile, string argumentList)
        {
            // Use ProcessStartInfo class

            string processDetail = string.Format(" --> Executable: {0} --> Arguments: {1}", executableFile, argumentList);

            //  if (IsFileLocked(argumentList)) return;
            CustomFolderSetting folder = getCustomFolder(executableFile);

            if (!string.IsNullOrEmpty(folder.ExecutableFile) && folder.WaitExecutionToComplete)
            {

            }
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = executableFile;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = argumentList;
           

            try
            {
                // Start the process with the info specified
                // Call WaitForExit and then the using-statement will close
                using (Process exeProcess = Process.Start(startInfo))
                {
                    
                    exeProcess.WaitForExit();
                    // Register a log of the successful operation
                    _logger.LogInformation("Succesful operation " + processDetail);

                }
            }
            catch (Exception exc)
            {
               
                _logger.LogError(exc, " Failled operation " + processDetail);
            }

           
        }

        private void touchFiles(string folder, string fileFilters)
        {
            string[] files = Directory.GetFiles(folder, fileFilters, SearchOption.TopDirectoryOnly);

            foreach (string file in files)
            {
                try
                {


                    //  _logger.LogInformation(file);
                    System.IO.File.SetLastWriteTimeUtc(file, DateTime.UtcNow);
                    System.IO.File.SetLastAccessTimeUtc(file, DateTime.UtcNow);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
        }

        public bool IsFileLocked(string fileToCheck)
        {
            bool Locked = false;
            //int attempts = 5;
            string filename = fileToCheck;
            _logger.LogInformation(filename);
            try
            {
                FileStream fs =
                    File.Open(filename, FileMode.Append,
                    FileAccess.Write, FileShare.None);

                fs.Close();
            }
            catch (IOException ex)
            {
                _logger.LogError(ex, " Failled operation " + filename);
                Locked = true;
            }
            return Locked;
        }

        private CustomFolderSetting getCustomFolder(string excutableFile)
        {

            CustomFolderSetting customeFolder= new CustomFolderSetting();
            foreach(CustomFolderSetting folder in customFolderList)
            {
                //WaitExecutionToComplete
                if (folder.ExecutableFile.Equals(excutableFile))
                {
                    customeFolder = folder;
                    break;
                }
            }
            return customeFolder;
        }
        private void checkProcess()
        {
            Process[] processlist = Process.GetProcesses();
            foreach (Process theprocess in processlist)
            {
                _logger.LogError("Process: {0} ID: {1}", theprocess.ProcessName, theprocess.Id);
              //  _logger.LogError(theprocess.Modules.ToString());
            }
        }
    }
}
