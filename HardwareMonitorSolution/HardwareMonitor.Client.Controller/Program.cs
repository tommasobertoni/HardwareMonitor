using HardwareMonitor.Client.Domain.Contracts;
using HardwareMonitor.Client.Temperature;
using HardwareMonitor.Client.TemperatureWCF;
using System;
using System.IO.Pipes;
using System.Security.Principal;
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

            if (args?.Length > 0) controller.LauncherPipe = new AnonymousPipeClientStream(PipeDirection.Out, args[0]);
            controller.TemperatureUI = new TemperatureUI();
            
            Application.Run();
        }
    }

    static class BroadcastServices
    {
        public static bool IsUserAdministrator()
        {
            //bool value to hold our return value
            bool isAdmin;
            try
            {
                //get the currently logged in user
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException)
            {
                isAdmin = false;
            }
            catch
            {
                isAdmin = false;
            }
            return isAdmin;
        }

        public static class Temperature
        {
            private static ServiceHost _temperatureBroadcastService;
            public static bool IsRunning { get; private set; }

            public static bool Start(IController controller)
            {
                if (_temperatureBroadcastService == null && IsUserAdministrator())
                {
                    controller.AddObserver(TemperatureObserver.Instance);
                    _temperatureBroadcastService = new ServiceHost(typeof(TemperatureWCFClientBroadcast));
                    _temperatureBroadcastService.Open();
                    Application.ApplicationExit += CloseTemperatureService;
                    IsRunning = true;
                    return true;
                }

                return false;
            }

            private static void CloseTemperatureService(object s, EventArgs e)
            {
                _temperatureBroadcastService.Close();
            }

            public static bool Stop(IController controller)
            {
                if (_temperatureBroadcastService != null)
                {
                    controller.RemoveObserver(TemperatureObserver.Instance);
                    _temperatureBroadcastService.Close();
                    Application.ApplicationExit -= CloseTemperatureService;
                    IsRunning = false;
                    return true;
                }

                return false;
            }
        }
    }
}
