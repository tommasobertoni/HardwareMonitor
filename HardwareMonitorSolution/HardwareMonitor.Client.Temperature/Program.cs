using System;
using System.Windows.Forms;

namespace HardwareMonitor.Client.Temperature
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new TemperatureUI());
        }
    }
}
