namespace CFSB.FileWatcher.Service
{
    partial class DirectoryWatcherService
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
            this.fileSystemWatcherComponent = new System.IO.FileSystemWatcher();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcherComponent)).BeginInit();
            // 
            // fileSystemWatcherComponent
            // 
            this.fileSystemWatcherComponent.EnableRaisingEvents = true;
            this.fileSystemWatcherComponent.Filter = "MCHE*.*";
            this.fileSystemWatcherComponent.Path = "E:\\CRB\\MCHEDataFile";
            this.fileSystemWatcherComponent.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcherComponent_Changed);
            this.fileSystemWatcherComponent.Created += new System.IO.FileSystemEventHandler(this.fileSystemWatcherComponent_Created);
            this.fileSystemWatcherComponent.Deleted += new System.IO.FileSystemEventHandler(this.fileSystemWatcherComponent_Deleted);
            this.fileSystemWatcherComponent.Renamed += new System.IO.RenamedEventHandler(this.fileSystemWatcherComponent_Renamed);
            this.fileSystemWatcherComponent.Error += new System.IO.ErrorEventHandler(OnError);
            // 
            // DirectoryWatcherService
            // 
            this.ServiceName = "FirsDataDirectoryWtacherService";
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcherComponent)).EndInit();

        }

        #endregion

        public System.IO.FileSystemWatcher fileSystemWatcherComponent;
    }
}
