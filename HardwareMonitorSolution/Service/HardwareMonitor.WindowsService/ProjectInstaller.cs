using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace HardwareMonitor.WindowsService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private void serviceInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            new ServiceController(serviceInstaller.ServiceName).Start();
        }

        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            ServiceController service = new ServiceController(serviceInstaller.ServiceName);
            
            try
            {
                if (service.Status == ServiceControllerStatus.Running)
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 0, 10));
                    service.Close();
                }
            }
            finally
            {
                base.OnBeforeUninstall(savedState);
            }
        }
    }
}
