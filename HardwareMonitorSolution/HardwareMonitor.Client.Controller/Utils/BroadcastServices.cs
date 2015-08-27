using System;
using System.Security.Principal;

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
    }
}
