using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareMonitor.Domain.Utils
{
    public class SettingsStorage
    {
        private RegistryKey _rk;

        public SettingsStorage(string key)
        {
            _rk =
                Registry.CurrentUser.OpenSubKey(key, RegistryKeyPermissionCheck.ReadWriteSubTree)
                ??
                Registry.CurrentUser.CreateSubKey(key, RegistryKeyPermissionCheck.ReadWriteSubTree);
        }

        public object Get(string propertyKey)
        {
            return _rk.GetValue(propertyKey);
        }

        public void Set(string propertyKey, object propertyValue)
        {
            _rk.SetValue(propertyKey, propertyValue);
        }
    }
}
