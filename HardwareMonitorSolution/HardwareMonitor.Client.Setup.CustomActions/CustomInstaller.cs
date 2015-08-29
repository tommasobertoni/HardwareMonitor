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
        public CustomInstaller()
        {
            InitializeComponent();
        }

        public override void Commit(IDictionary savedState)
        {
            var location = Path.GetDirectoryName(GetExecutingAssembly().Location).ToString();
            Directory.SetCurrentDirectory(location);
            Process.Start("explorer.exe", $"{location}\\HardwareMonitor.Client.Controller.exe");

            base.Commit(savedState);
        }
    }
}
