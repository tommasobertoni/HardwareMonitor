using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureAlertSystem.TrayUI.Utils
{
    public class SettingsStorage
    {
        private const string USER_SETTINGS_KEY = "tassettingsuserkey";
        private const string ALERT_TEMPERATURE_KEY = "alerttemperaturekey";
        private const string TIMEOUT_KEY = "timeoutkey";
        private const string NOTIFICATION_MODE_KEY = "norificationmodekey";

        public enum NotificationMode
        {
            MESSAGE_BOX,
            TRAY_NOTIFICATION,
            NONE
        }

        private RegistryKey _rk;

        public SettingsStorage()
        {
            _rk =
                Registry.CurrentUser.OpenSubKey(USER_SETTINGS_KEY,
                    RegistryKeyPermissionCheck.ReadWriteSubTree)
                ??
                Registry.CurrentUser.CreateSubKey(USER_SETTINGS_KEY,
                    RegistryKeyPermissionCheck.ReadWriteSubTree);
        }

        public int GetAlertTemperature()
        {
            var saved = _rk.GetValue(ALERT_TEMPERATURE_KEY);
            return saved == null ? -1 : (int)saved;
        }

        public void SetAlertTemperature(int temperature)
        {
            _rk.SetValue(ALERT_TEMPERATURE_KEY, temperature);
        }

        public int GetTimeoutMillis()
        {
            var saved = _rk.GetValue(TIMEOUT_KEY);
            return saved == null ? -1 : (int)saved;
        }

        public void SetTimeoutMillis(int timeout)
        {
            _rk.SetValue(TIMEOUT_KEY, timeout);
        }

        public NotificationMode GetNotificationMode()
        {
            var saved = _rk.GetValue(NOTIFICATION_MODE_KEY);
            return saved == null ? NotificationMode.MESSAGE_BOX : (NotificationMode) Enum.Parse(typeof(NotificationMode), saved as string);
        }

        public void SetNotificationMode(NotificationMode mode)
        {
            _rk.SetValue(NOTIFICATION_MODE_KEY, mode);
        }
    }
}
