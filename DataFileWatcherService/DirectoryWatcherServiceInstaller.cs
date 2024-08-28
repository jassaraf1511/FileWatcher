using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace CFSB.FileWatcher.Service
{
    [RunInstaller(true)]
    public partial class DirectoryWatcherServiceInstaller : System.Configuration.Install.Installer
    {
        public DirectoryWatcherServiceInstaller()
        {
            InitializeComponent();
        }

        private void WatcherServiceProcessInstaller_AfterInstall(object sender, InstallEventArgs e)
        {

        }

        private void WatcherServiceInstaller_AfterInstall(object sender, InstallEventArgs e)
        {

        }
    }
}
