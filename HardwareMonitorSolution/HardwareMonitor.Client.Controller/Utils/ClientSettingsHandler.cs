using HardwareMonitor.Domain.Utils;
using Microsoft.Win32;
using System.Windows.Forms;

namespace HardwareMonitor.Client.Controller.Utils
{
    class ClientSettingsHandler
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

        private const string _STARTUP_BROADCAST_SERVICES_KEY = "startupbroadcastservices";
        private const bool _DEFAULT_STARTUP_BROADCAST_SERVICES = false;

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
                SetAutorun(_startupNotification);
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

        private bool _startupBroadcastServices;
        public bool StartupBroadcastServices
        {
            get
            {
                return _startupNotification;
            }
            set
            {
                _startupBroadcastServices = value;
                _settings.Set(_STARTUP_BROADCAST_SERVICES_KEY, _startupBroadcastServices);
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
            string value;
            if (!bool.TryParse(value = _settings.Get(_RUN_AT_STARTUP_KEY)?.ToString(), out _runAtStartup))
                _runAtStartup = _DEFAULT_RUN_AT_STARTUP;

            if (!bool.TryParse(value = _settings.Get(_STARTUP_NOTIFICATION_KEY)?.ToString(), out _startupNotification))
                _startupNotification = _DEFAULT_STARTUP_NOTIFICATION;

            if (!bool.TryParse(value = _settings.Get(_START_PROGRAM_AS_ADMIN_KEY)?.ToString(), out _startProgramAsAdmin))
                _startProgramAsAdmin = _DEFAULT_START_PROGRAM_AS_ADMIN;

            if (!bool.TryParse(value = _settings.Get(_STARTUP_BROADCAST_SERVICES_KEY)?.ToString(), out _startupBroadcastServices))
                _startupBroadcastServices = _DEFAULT_STARTUP_BROADCAST_SERVICES;
        }

        private void SetAutorun(bool autorun)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(_STARTUP_APPLICATIONS_KEY, true))
            {
                if (autorun) key.SetValue(_STARTUP_APPLICATION_NAME, "\"" + Application.ExecutablePath + "\"");
                else if (key.GetValue(_STARTUP_APPLICATIONS_KEY) != null) key.DeleteValue(_STARTUP_APPLICATION_NAME);
            }
        }
    }
}
