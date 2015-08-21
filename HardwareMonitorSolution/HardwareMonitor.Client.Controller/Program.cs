using HardwareMonitor.Client.Domain.Contracts;
using HardwareMonitor.Client.Temperature;
using HardwareMonitor.Client.TemperatureWCF;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Security.Permissions;
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

            Console.WriteLine("CreateController");
            try
            {
                IController controller = new HardwareMonitorController();
                controller.TemperatureUI = new TemperatureUI();
                Application.Run();
            }
            catch (AdminRightsRequiredException)
            {
                Console.WriteLine("AdminRightsRequiredException");
                ElevateProcess(Process.GetCurrentProcess());
                Application.Exit();
            }

            Console.WriteLine("End");
        }

        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
        public static Process ElevateProcess(Process source)
        {
            //Create a new process
            Process target = new Process();
            target.StartInfo = source.StartInfo;
            target.StartInfo.FileName = source.MainModule.FileName;
            target.StartInfo.WorkingDirectory = Path.GetDirectoryName(source.MainModule.FileName);

            //Required for UAC to work
            target.StartInfo.UseShellExecute = true;
            target.StartInfo.Verb = "runas";

            try
            {
                if (!target.Start())
                    return null;
            }
            catch (Win32Exception e)
            {
                //Cancelled
                if (e.NativeErrorCode == 1223)
                    return null;
                throw;
            };
            return target;
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
