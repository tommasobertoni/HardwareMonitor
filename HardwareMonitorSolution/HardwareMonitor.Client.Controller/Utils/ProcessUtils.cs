using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Security.Permissions;
using static System.Reflection.Assembly;

namespace HardwareMonitor.Client.Controller.Utils
{
    class ProcessUtils
    {
        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
        public static Process RerunCurrentProcess(bool administratorRights = false)
        {
            if (administratorRights) return RerunProcessWithAdminPrivileges(Process.GetCurrentProcess());
            else return Process.Start("explorer.exe", GetEntryAssembly().Location);
        }

        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
        public static Process RerunProcessWithAdminPrivileges(Process source)
        {
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
                if (e.NativeErrorCode == 1223) return null;
                throw;
            };

            return target;
        }
    }
}
