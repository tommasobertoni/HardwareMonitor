using HardwareMonitor.Client.Domain.Entities;
using HardwareMonitor.Client.Domain.Utils;
using Microsoft.Win32;
using System.Windows.Forms;

namespace HardwareMonitor.Client.Settings.Utils
{
    public class ClientSettingsHandler
    {
        // The path to the key where Windows looks for startup applications
        private const string _STARTUP_APPLICATIONS_KEY = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        private const string _STARTUP_APPLICATION_NAME = "HardwareMonitorCLient";
        private const string _DEFAULT_CLIENT_SETTINGS_KEY = "hardwaremonitorclientsettings";

        private const string _RUN_AT_STARTUP_KEY = "runatstartup";
        private const bool _DEFAULT_RUN_AT_STARTUP = false;

        private const string _STARTUP_NOTIFICATION_KEY = "startupnotification";
        private const bool _DEFAULT_STARTUP_NOTIFICATION = true;

        private const string _START_PROGRAM_AS_ADMIN_KEY = "startprogramasadmin";
        private const bool _DEFAULT_START_PROGRAM_AS_ADMIN = false;

        private const string _THEME_KEY = "theme";
        private const Theme _DEFAULT_THEME = Theme.LIGHT;

        private bool _runAtStartup;
        public bool RunAtStartup
        {
            get
            {
                return _runAtStartup;
            }
            set
            {
                _runAtStartup = value;
                _settings.Set(_RUN_AT_STARTUP_KEY, _runAtStartup);
                SetAutorun(_runAtStartup);
            }
        }

        private bool _startupNotification;
        public bool StartupNotification
        {
            get
            {
                return _startupNotification;
            }
            set
            {
                _startupNotification = value;
                _settings.Set(_STARTUP_NOTIFICATION_KEY, _startupNotification);
            }
        }

        private bool _startProgramAsAdmin;
        public bool StartProgramAsAdmin
        {
            get
            {
                return _startProgramAsAdmin;
            }
            set
            {
                _startProgramAsAdmin = value;
                _settings.Set(_START_PROGRAM_AS_ADMIN_KEY, _startProgramAsAdmin);
            }
        }

        private Theme _theme;
        public Theme Theme
        {
            get
            {
                return _theme;
            }
            set
            {
                _theme = value;
                _settings.Set(_THEME_KEY, (int)_theme);
            }
        }

        private SettingsStorage _settings;

        public ClientSettingsHandler(string key = _DEFAULT_CLIENT_SETTINGS_KEY)
        {
            _settings = new SettingsStorage(key);
            Update();
        }

        public void Update()
        {
            string stringValue;
            if (!bool.TryParse(stringValue = _settings.Get(_RUN_AT_STARTUP_KEY)?.ToString(), out _runAtStartup))
                _runAtStartup = _DEFAULT_RUN_AT_STARTUP;

            if (!bool.TryParse(stringValue = _settings.Get(_STARTUP_NOTIFICATION_KEY)?.ToString(), out _startupNotification))
                _startupNotification = _DEFAULT_STARTUP_NOTIFICATION;

            if (!bool.TryParse(stringValue = _settings.Get(_START_PROGRAM_AS_ADMIN_KEY)?.ToString(), out _startProgramAsAdmin))
                _startProgramAsAdmin = _DEFAULT_START_PROGRAM_AS_ADMIN;

            object value;
            _theme = (value = _settings.Get(_THEME_KEY)) != null ? (Theme)value : _DEFAULT_THEME;
        }

        private void SetAutorun(bool autorun)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(_STARTUP_APPLICATIONS_KEY, true))
            {
                if (autorun) key.SetValue(_STARTUP_APPLICATION_NAME, "\"" + Application.ExecutablePath + "\"");
                else if (key.GetValue(_STARTUP_APPLICATION_NAME) != null) key.DeleteValue(_STARTUP_APPLICATION_NAME);
            }
        }

        public void Close()
        {
            _settings.Close();
        }
    }
}
