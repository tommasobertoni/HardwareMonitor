using HardwareMonitor.WindowsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var sh = new ServiceHost(typeof(HardwareMonitorWinService));
            sh.Open();
            Console.WriteLine("Service running press any key to terminate...");
            Console.ReadKey();
            sh.Close();
        }
    }
}
