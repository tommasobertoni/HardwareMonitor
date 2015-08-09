using System.ServiceProcess;

namespace HardwareMonitor.WindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new HardwareMonitorWinService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
