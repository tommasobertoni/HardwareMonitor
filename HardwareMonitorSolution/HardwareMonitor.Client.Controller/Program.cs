using HardwareMonitor.Client.Controller.Utils;
using HardwareMonitor.Client.Domain.Contracts;
using HardwareMonitor.Client.Temperature;
using System;
using static System.Diagnostics.Process;
using System.Windows.Forms;
using static HardwareMonitor.Client.Domain.Utils.LogsManager;

namespace HardwareMonitor.Client.Controller
{
    class Program
    {
        private static void InitComponents()
        {
            IController controller = new HardwareMonitorController();
            controller.TemperatureUI = new TemperatureUI();
        }

        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                var settings = new ClientSettingsHandler();

                if (settings.StartProgramAsAdmin && !BroadcastServices.IsUserAdministrator)
                {
                    ProcessUtils.RerunProcessWithAdminPrivileges(GetCurrentProcess());
                    Application.Exit();
                }
                else
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    InitComponents();
                    Application.Run();
                }
            }
            catch (Exception ex)
            {
                Log($"Program Main exit => {ex}");
            }
        }
    }
}
