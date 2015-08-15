using HardwareMonitor.Client.Domain.Contracts;
using HardwareMonitor.Client.Temperature;
using HardwareMonitor.Client.TemperatureWCF;
using System;
using System.ServiceModel;
using System.Windows.Forms;

namespace HardwareMonitor.Client.Controller
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IController controller = new HardwareMonitorController();
            controller.TemperatureUI = new TemperatureUI();
            controller.AddObserver(TemperatureObserver.Instance);

            var temperatureBroadcastService = new ServiceHost(typeof(TemperatureWCFClientBroadcast));
            temperatureBroadcastService.Open();

            Application.ApplicationExit += (s, e) => temperatureBroadcastService.Close();
            Application.Run();
        }
    }
}
