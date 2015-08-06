using HardwareMonitor.Client.Domain.Contracts;
using HardwareMonitor.Client.Temperature;
using System;
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
            controller.TemperatureUI.Show();

            Application.Run();
        }
    }
}
