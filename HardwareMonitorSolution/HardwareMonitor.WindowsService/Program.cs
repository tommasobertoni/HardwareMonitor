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
#if DEBUG
            var serv = new HardwareMonitorWinService();
            serv.OnDebug();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new HardwareMonitorWinService()
            };
            ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
