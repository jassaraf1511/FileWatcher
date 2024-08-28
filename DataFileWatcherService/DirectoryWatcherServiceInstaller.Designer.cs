namespace CFSB.FileWatcher.Service
{
    partial class DirectoryWatcherServiceInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.WatcherServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            this.WatcherServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            // 
            // WatcherServiceInstaller
            // 
            this.WatcherServiceInstaller.Description = "First Data Payment File Watcher";
            this.WatcherServiceInstaller.DisplayName = "First Data Payment File Watcher";
            this.WatcherServiceInstaller.ServiceName = "FirsDataDirectoryWtacherService";
            this.WatcherServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.WatcherServiceInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.WatcherServiceInstaller_AfterInstall);
            // 
            // WatcherServiceProcessInstaller
            // 
            this.WatcherServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.WatcherServiceProcessInstaller.Password = null;
            this.WatcherServiceProcessInstaller.Username = null;
            this.WatcherServiceProcessInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.WatcherServiceProcessInstaller_AfterInstall);
            // 
            // DirectoryWatcherServiceInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.WatcherServiceInstaller,
            this.WatcherServiceProcessInstaller});

        }

        #endregion
        private System.ServiceProcess.ServiceProcessInstaller WatcherServiceProcessInstaller;
        public System.ServiceProcess.ServiceInstaller WatcherServiceInstaller;
    }
}