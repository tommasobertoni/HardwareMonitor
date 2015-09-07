using HardwareMonitor.Client.Controller.Utils;
using HardwareMonitor.Client.Domain.Contracts;
using System;
using static System.Diagnostics.Process;
using System.Windows.Forms;
using static HardwareMonitor.Client.Domain.Utils.LogsManager;
using HardwareMonitor.Client.Temperature;
using HardwareMonitor.Client.Settings;
using HardwareMonitor.Client.Settings.Utils;
using System.ServiceProcess;
using HardwareMonitor.Client.Controller.Monitors;
using System.Linq;

namespace HardwareMonitor.Client.Controller
{
    class Program
    {
        private static void InitComponents()
        {
            IController controller = new HardwareMonitorController();
            controller.SettingsUI = new SettingsForm();
            controller.TemperatureUI = new TemperatureUI();
        }

        [STAThread]
        static void Main(string[] args)
        {
            ServiceController hardwareMonitorService = ServiceController.GetServices().FirstOrDefault(
                s => s.ServiceName == RemoteAbstractMonitor.SERVICE_NAME);

            if (hardwareMonitorService != null)
            {
                hardwareMonitorService.WaitForStatus(ServiceControllerStatus.Running);

                try
                {
                    var settings = new ClientSettingsHandler();

                    if (settings.StartProgramAsAdmin && !UACUtils.IsUserAdministrator)
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
                    Log($"Program Main exit => {ex}", LogLevel.ERROR);
                    var result = MessageBox.Show(
                        "The Hardware Monitor Client crashed. More information can be found in the log file in the installation directory.\n\nDo you want to open it now?",
                        "Hardware Monitor Client error",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    if (result == DialogResult.Yes) Start(LogFilePath(LogLevel.ERROR));
                }
            }
            else
            {
                Log("Service not found", LogLevel.ERROR);
                MessageBox.Show(
                    $"Can't find \"{RemoteAbstractMonitor.SERVICE_NAME}\" service. Please, ensure that it's installed and running on the local machine.",
                    "Hardware Monitor Client error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
