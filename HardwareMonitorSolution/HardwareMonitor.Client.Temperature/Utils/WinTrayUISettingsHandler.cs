using HardwareMonitor.Client.Domain.Entities;
using HardwareMonitor.Domain.Utils;

namespace HardwareMonitor.Client.Temperature.Utils
{
    internal class WinTrayUISettingsHandler
    {
        private const string _TEMPERATURE_ALERT_LEVEL_KEY = "temperaturelertlevel";
        private const int _DEFAULT_TEMPERATURE_ALERT_LEVEL = 30;

        private const string _UPDATE_TIME_KEY = "updatetime";
        private const int _DEFAULT_UPDATE_TIME = 5;

        private const string _OBSERVERS_COUNT_KEY = "observerscount";
        private const int _DEFAULT_OBSERVERS_COUNT = 5;

        private const string _NOTIFICATION_KEY = "notification";
        private const NotificationMethod _DEFAULT_NOTIFICATION = NotificationMethod.MESSAGE_BOX;


        private int _temperaturelertlevel;
        public int TemperatureAlertLevel {
            get
            {
                return _temperaturelertlevel;
            }
            set
            {
                _temperaturelertlevel = value;
                _settings.Set(_TEMPERATURE_ALERT_LEVEL_KEY, _temperaturelertlevel);
            }
        }

        private int _updatetime;
        public int UpdateTime
        {
            get
            {
                return _updatetime;
            }
            set
            {
                _updatetime = value;
                //_settings.Set(_UPDATE_TIME_KEY, _updatetime);
            }
        }

        private int _observerscount;
        public int ObserversCount
        {
            get
            {
                return _observerscount;
            }
            set
            {
                _observerscount = value;
                //_settings.Set(_OBSERVERS_COUNT_KEY, _observerscount);
            }
        }

        private NotificationMethod _notification;
        public NotificationMethod Notification
        {
            get
            {
                return _notification;
            }
            set
            {
                _notification = value;
                //_settings.Set(_NOTIFICATION_KEY, _notification);
            }
        }

        private SettingsStorage _settings;

        public WinTrayUISettingsHandler(string key)
        {
            _settings = new SettingsStorage(key);

            object value;
            _temperaturelertlevel = (value = _settings.Get(_TEMPERATURE_ALERT_LEVEL_KEY)) != null ? (int)value : _DEFAULT_TEMPERATURE_ALERT_LEVEL;
            _updatetime = (value = _settings.Get(_UPDATE_TIME_KEY)) != null ? (int)value : _DEFAULT_UPDATE_TIME;
            _observerscount = (value = _settings.Get(_OBSERVERS_COUNT_KEY)) != null ? (int)value : _DEFAULT_OBSERVERS_COUNT;
            _notification = (value = _settings.Get(_NOTIFICATION_KEY)) != null ? (NotificationMethod)value : _DEFAULT_NOTIFICATION;
        }
    }
}
