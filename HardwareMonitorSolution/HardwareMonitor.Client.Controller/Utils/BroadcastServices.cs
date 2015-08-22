using HardwareMonitor.Client.Domain.Contracts;
using HardwareMonitor.Client.TemperatureWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HardwareMonitor.Client.Controller.Utils
{
    static class BroadcastServices
    {
        private static bool? _isUserAdministrator;

        public static bool IsUserAdministrator
        {
            get
            {
                if (!_isUserAdministrator.HasValue)
                {
                    try
                    {
                        //get the currently logged in user
                        WindowsIdentity user = WindowsIdentity.GetCurrent();
                        WindowsPrincipal principal = new WindowsPrincipal(user);
                        _isUserAdministrator = principal.IsInRole(WindowsBuiltInRole.Administrator);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        _isUserAdministrator = false;
                    }
                    catch
                    {
                        _isUserAdministrator = false;
                    }
                }

                return _isUserAdministrator.Value;
            }
        }

        public static class Temperature
        {
            private static ServiceHost _temperatureBroadcastService;
            public static bool IsRunning { get; private set; }

            public static bool Start(IController controller)
            {
                if (_temperatureBroadcastService == null && IsUserAdministrator)
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
