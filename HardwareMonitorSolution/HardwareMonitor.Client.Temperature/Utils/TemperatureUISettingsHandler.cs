using HardwareMonitor.Client.Domain.Utils;
using System;

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
        public const int MIN_UPDATE_TIME = 2000;
        public const int MAX_UPDATE_TIME = 120000;

        private const string _THEME_KEY = "theme";
        private const Theme _DEFAULT_THEME = Theme.DEFAULT;

        private const string _NOTIFICATION_KEY = "notification";
        private const NotificationMethod _DEFAULT_NOTIFICATION = NotificationMethod.SOUND_AND_MESSAGE;

        private const string _SOUND_RESOURCE_NAME_KEY = "soundresourcename";

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
                _settings.Set(_NOTIFICATION_KEY, (int)_notification);
            }
        }

        private string _soundResourceName;
        public string SoundResourceName
        {
            get
            {
                return _soundResourceName;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("The name for the resource must be valid");

                _soundResourceName = value;
                _settings.Set(_SOUND_RESOURCE_NAME_KEY, _soundResourceName);
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
            _notification = (value = _settings.Get(_NOTIFICATION_KEY)) != null ? (NotificationMethod)value : _DEFAULT_NOTIFICATION;
            _theme = (value = _settings.Get(_THEME_KEY)) != null ? (Theme)value : _DEFAULT_THEME;
            _soundResourceName = (value = _settings.Get(_SOUND_RESOURCE_NAME_KEY)) as string;
        }

        public void Close()
        {
            _settings.Close();
        }
    }

    public enum Theme
    {
        DEFAULT, DARK
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
