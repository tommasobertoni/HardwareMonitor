using HardwareMonitor.Client.Domain.Entities;
using HardwareMonitor.Domain.Utils;

namespace HardwareMonitor.Client.Temperature.Utils
{
    public class TemperatureUISettingsHandler
    {
        private const string _DEFAULT_TEMPERATURE_SETTINGS_KEY = "temperaturesettings";

        private const string _TEMPERATURE_ALERT_LEVEL_KEY = "temperaturelertlevel";
        private const int _DEFAULT_TEMPERATURE_ALERT_LEVEL = 30;
        public const int MIN_TEMPERATURE_ALERT_LEVEL = 30;
        public const int MAX_TEMPERATURE_ALERT_LEVEL = 110;

        private const string _UPDATE_TIME_KEY = "updatetime";
        private const int _DEFAULT_UPDATE_TIME = 5000;
        public const int MIN_UPDATE_TIME = 1000;
        public const int MAX_UPDATE_TIME = 120000;

        private const string _OBSERVERS_COUNT_KEY = "observerscount";
        public const int _DEFAULT_OBSERVERS_COUNT = 5;
        public const int MIN_OBSERVERS_COUNT = 0;
        public const int MAX_OBSERVERS_COUNT = 30;

        private const string _NOTIFICATION_KEY = "notification";
        private const NotificationMethod _DEFAULT_NOTIFICATION = NotificationMethod.SOUND_AND_MESSAGE;
        
        private int _temperaturelertlevel;
        public int TemperatureAlertLevel {
            get
            {
                return _temperaturelertlevel;
            }
            set
            {
                if (value.Between(MIN_TEMPERATURE_ALERT_LEVEL, MAX_TEMPERATURE_ALERT_LEVEL, true))
                {
                    _temperaturelertlevel = value;
                    _settings.Set(_TEMPERATURE_ALERT_LEVEL_KEY, _temperaturelertlevel);
                }
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
                if (value.Between(MIN_UPDATE_TIME, MAX_UPDATE_TIME, true))
                {
                    _updatetime = value;
                    _settings.Set(_UPDATE_TIME_KEY, _updatetime);
                }
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
                if (value.Between(MIN_OBSERVERS_COUNT, MAX_OBSERVERS_COUNT, true))
                {
                    _observerscount = value;
                    _settings.Set(_OBSERVERS_COUNT_KEY, _observerscount);
                }
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
                _settings.Set(_NOTIFICATION_KEY, _notification);
            }
        }

        private SettingsStorage _settings;

        public TemperatureUISettingsHandler(string key = _DEFAULT_TEMPERATURE_SETTINGS_KEY)
        {
            _settings = new SettingsStorage(key);
            Update();
        }

        public void Update()
        {
            object value;
            _temperaturelertlevel = (value = _settings.Get(_TEMPERATURE_ALERT_LEVEL_KEY)) != null ? (int)value : _DEFAULT_TEMPERATURE_ALERT_LEVEL;
            _updatetime = (value = _settings.Get(_UPDATE_TIME_KEY)) != null ? (int)value : _DEFAULT_UPDATE_TIME;
            _observerscount = (value = _settings.Get(_OBSERVERS_COUNT_KEY)) != null ? (int)value : _DEFAULT_OBSERVERS_COUNT;
            _notification = (value = _settings.Get(_NOTIFICATION_KEY)) != null ? (NotificationMethod)value : _DEFAULT_NOTIFICATION;
        }
    }

    static class Utils
    {
        public static bool Between(this int num, int lower, int upper, bool inclusive = false)
        {
            return inclusive
                ? lower <= num && num <= upper
                : lower < num && num < upper;
        }
    }
}
