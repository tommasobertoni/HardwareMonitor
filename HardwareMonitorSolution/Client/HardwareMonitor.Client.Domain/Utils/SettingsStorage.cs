using Microsoft.Win32;
using static Microsoft.Win32.Registry;
using static Microsoft.Win32.RegistryKeyPermissionCheck;

namespace HardwareMonitor.Client.Domain.Utils
{
    public class SettingsStorage
    {
        private RegistryKey _rk;

        public SettingsStorage(string key, bool localMachine = false)
        {
            RegistryKey rk = localMachine ? LocalMachine : CurrentUser;
            _rk = rk.OpenSubKey(key, ReadWriteSubTree)
                  ??
                  rk.CreateSubKey(key, ReadWriteSubTree);
        }

        public object Get(string propertyKey)
        {
            return _rk.GetValue(propertyKey);
        }

        public void Set(string propertyKey, object propertyValue)
        {
            _rk.SetValue(propertyKey, propertyValue);
        }

        public void Close()
        {
            _rk.Close();
            _rk.Dispose();
        }
    }
}
