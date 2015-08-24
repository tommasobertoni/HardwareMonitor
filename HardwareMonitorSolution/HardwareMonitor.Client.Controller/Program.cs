using HardwareMonitor.Client.Controller.Utils;
using HardwareMonitor.Client.Domain.Contracts;
using HardwareMonitor.Client.Temperature;
using System;
using static System.Diagnostics.Process;
using System.Windows.Forms;
using static HardwareMonitor.Client.Controller.Utils.LogsManager;

namespace HardwareMonitor.Client.Controller
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var settings = new ClientSettingsHandler();

                if ((settings.StartProgramAsAdmin || settings.StartupBroadcastServices) && !BroadcastServices.IsUserAdministrator)
                {
                    ProcessUtils.RerunProcessWithAdminPrivileges(GetCurrentProcess());
                    Application.Exit();
                }
                else
                {
                    IController controller = new HardwareMonitorController();
                    controller.TemperatureUI = new TemperatureUI();
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
