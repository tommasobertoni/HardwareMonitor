using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using static System.Reflection.Assembly;
using System.Diagnostics;
using System.IO;

namespace HardwareMonitor.Client.Setup.CustomActions
{
    [RunInstaller(true)]
    public partial class CustomInstaller : Installer
    {
        public CustomInstaller() : base()
        {
            InitializeComponent();
        }

        protected override void OnAfterInstall(IDictionary savedState)
        {
            var location = Path.GetDirectoryName(GetExecutingAssembly().Location).ToString();
            Directory.SetCurrentDirectory(location);
            Process.Start("explorer.exe", $"{location}\\HardwareMonitorClient.exe");

            base.OnAfterInstall(savedState);
        }
    }
}
