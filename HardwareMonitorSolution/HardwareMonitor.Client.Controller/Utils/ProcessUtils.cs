using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Security.Permissions;

namespace HardwareMonitor.Client.Controller.Utils
{
    class ProcessUtils
    {
        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
        public static Process RerunCurrentProcess(bool administratorRights = false)
        {
            return RerunProcess(Process.GetCurrentProcess(), administratorRights);
        }

        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
        public static Process RerunProcess(Process source, bool administratorRights = false)
        {
            Process target = new Process();
            target.StartInfo = source.StartInfo;
            target.StartInfo.FileName = source.MainModule.FileName;
            target.StartInfo.WorkingDirectory = Path.GetDirectoryName(source.MainModule.FileName);

            //Required for UAC to work
            target.StartInfo.UseShellExecute = true;
            if (administratorRights) target.StartInfo.Verb = "runas";

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
