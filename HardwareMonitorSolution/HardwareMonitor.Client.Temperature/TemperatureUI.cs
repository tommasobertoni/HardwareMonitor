using HardwareMonitor.Client.Domain.Contracts;
using HardwareMonitor.Client.Domain.Entities;
using System;
using System.Windows.Forms;
using System.Drawing;
using HardwareMonitor.Client.Temperature.Utils;
using System.Media;

namespace HardwareMonitor.Client.Temperature
{
    public partial class TemperatureUI : Form, ITemperatureUI
    {
        private Bitmap _iconBitmap;
        Image IView.Icon
        {
            get
            {
                return _iconBitmap;
            }
        }

        public event EventHandler<ViewValueChangedEventArgs> OnTemperatureAlertLevelChanged;
        public event EventHandler<ViewValueChangedEventArgs> OnUpdateTimeChanged;
        public event EventHandler<ViewValueChangedEventArgs> OnObserversCountChanged;
        public event EventHandler<ViewValueChangedEventArgs> OnNotificationMethodChanged;
        public event EventHandler<string> OnNotification;
        public event EventHandler<string> OnLog;
        public event EventHandler OnViewExit;
        public event EventHandler OnRequestUpdate;

        private bool _alert;
        private SoundPlayer _sound;
        private float _lastAvgCPUsTemperature;
        private int _lastSavedTemperatureAlertLevel,
                    _lastSavedUpdateTime,
                    _lastSavedObserversCount;

        private TemperatureUISettingsHandler _settings;

        public TemperatureUI()
        {
            InitializeComponent();

            _settings = new TemperatureUISettingsHandler();
            _iconBitmap = Properties.Resources.temperatureIcon.ToBitmap();
            _sound = new SoundPlayer();
            _sound.Stream = Properties.Resources.a_tone;
            _sound.Load();

            FormClosed += (s, o) => OnViewExit(s, o);

            #region Init ThermometerPictureBox
            thermometerPictureBox.MarginLeft = 20;
            thermometerPictureBox.MarginRight = 20;
            thermometerPictureBox.MarginTop = 5;
            thermometerPictureBox.MarginBottom = 35;
            thermometerPictureBox.Value = 0;
            #endregion

            #region Init controls values
            nupTemperatureAlertLevel.Minimum = TemperatureUISettingsHandler.MIN_TEMPERATURE_ALERT_LEVEL;
            nupTemperatureAlertLevel.Maximum = TemperatureUISettingsHandler.MAX_TEMPERATURE_ALERT_LEVEL;
            trackBarTemperatureAlertLevel.Minimum = TemperatureUISettingsHandler.MIN_TEMPERATURE_ALERT_LEVEL;
            trackBarTemperatureAlertLevel.Maximum = TemperatureUISettingsHandler.MAX_TEMPERATURE_ALERT_LEVEL;
            SetTemperatureAlertLevel(_settings.TemperatureAlertLevel);

            nupUpdateTime.Minimum = TemperatureUISettingsHandler.MIN_UPDATE_TIME / 1000;
            nupUpdateTime.Maximum = TemperatureUISettingsHandler.MAX_UPDATE_TIME / 1000;
            trackbarUpdateTime.Minimum = TemperatureUISettingsHandler.MIN_UPDATE_TIME / 1000;
            trackbarUpdateTime.Maximum = TemperatureUISettingsHandler.MAX_UPDATE_TIME / 1000;
            SetUpdateTime(_settings.UpdateTime);

            nupObservers.Minimum = TemperatureUISettingsHandler.MIN_OBSERVERS_COUNT;
            nupObservers.Maximum = TemperatureUISettingsHandler.MAX_OBSERVERS_COUNT;
            trackBarObservers.Minimum = TemperatureUISettingsHandler.MIN_OBSERVERS_COUNT;
            trackBarObservers.Maximum = TemperatureUISettingsHandler.MAX_OBSERVERS_COUNT;
            SetObserversCount(_settings.ObserversCount);

            SetNotificationMethod(_settings.Notification);
            #endregion

            #region Attach value change events
            labelAvgCPUsTemperature.BorderStyle = BorderStyle.None;
            labelAvgCPUsTemperature.Paint += (s, e) =>
            {
                if (labelAvgCPUsTemperature.BorderStyle != BorderStyle.None)
                    ControlPaint.DrawBorder(e.Graphics, labelAvgCPUsTemperature.DisplayRectangle, Color.Red, ButtonBorderStyle.Solid);
            };

            nupTemperatureAlertLevel.MouseWheel += DoNothing_MouseWheel;
            nupTemperatureAlertLevel.ValueChanged += (s, e) => UpdateTemperatureAlertValue((int)nupTemperatureAlertLevel.Value);
            nupTemperatureAlertLevel.MouseUp += (s, e) => UpdateTemperatureAlertValue((int)nupTemperatureAlertLevel.Value, true);
            nupTemperatureAlertLevel.LostFocus += (s, e) => SetTemperatureAlertLevel(_lastSavedTemperatureAlertLevel);
            trackBarTemperatureAlertLevel.MouseWheel += DoNothing_MouseWheel;
            trackBarTemperatureAlertLevel.ValueChanged += (s, e) => UpdateTemperatureAlertValue(trackBarTemperatureAlertLevel.Value);
            trackBarTemperatureAlertLevel.MouseUp += (s, e) => UpdateTemperatureAlertValue(trackBarTemperatureAlertLevel.Value, true);
            trackBarTemperatureAlertLevel.LostFocus += (s, e) => SetTemperatureAlertLevel(_lastSavedTemperatureAlertLevel);

            nupUpdateTime.MouseWheel += DoNothing_MouseWheel;
            nupUpdateTime.ValueChanged += (s, e) => UpdateUpdateTimeValue((int)nupUpdateTime.Value);
            nupUpdateTime.MouseUp += (s, e) => UpdateTemperatureAlertValue((int)nupUpdateTime.Value, true);
            nupUpdateTime.LostFocus += (s, e) => SetUpdateTime(_lastSavedUpdateTime);
            trackbarUpdateTime.MouseWheel += DoNothing_MouseWheel;
            trackbarUpdateTime.ValueChanged += (s, e) => UpdateUpdateTimeValue(trackbarUpdateTime.Value);
            trackbarUpdateTime.MouseUp += (s, e) => UpdateTemperatureAlertValue(trackBarTemperatureAlertLevel.Value, true);
            trackbarUpdateTime.LostFocus += (s, e) => SetUpdateTime(_lastSavedUpdateTime);

            nupObservers.MouseWheel += DoNothing_MouseWheel;
            nupObservers.ValueChanged += (s, e) => UpdateObserversCountValue((int)nupObservers.Value);
            nupObservers.MouseUp += (s, e) => UpdateTemperatureAlertValue((int)nupObservers.Value, true);
            nupObservers.LostFocus += (s, e) => SetObserversCount(_lastSavedObserversCount);
            trackBarObservers.MouseWheel += DoNothing_MouseWheel;
            trackBarObservers.ValueChanged += (s, e) => UpdateObserversCountValue(trackBarObservers.Value);
            trackBarObservers.MouseUp += (s, e) => UpdateTemperatureAlertValue(trackBarTemperatureAlertLevel.Value, true);
            trackBarObservers.LostFocus += (s, e) => SetObserversCount(_lastSavedObserversCount);

            rbMessageBoxNotif.CheckedChanged += RB_CheckedChanged;
            rbMessageBoxNotif.CheckedChanged += RB_CheckedChanged;
            rbMessageBoxNotif.CheckedChanged += RB_CheckedChanged;
            #endregion

            FormClosing += (s, e) =>
            {
                Hide();
                e.Cancel = true;
            };
        }

        private void DoNothing_MouseWheel(object sender, EventArgs e)
        {
            HandledMouseEventArgs ee = (HandledMouseEventArgs)e;
            ee.Handled = true;
        }

        private void UpdateTemperatureAlertValue(int temperature, bool saveSettings = false)
        {
            SetTemperatureAlertLevel(temperature);

            if (saveSettings)
            {
                _lastSavedTemperatureAlertLevel = temperature;
                OnTemperatureAlertLevelChanged?.Invoke(this, new ViewValueChangedEventArgs
                {
                    Value = temperature,
                    Save = saveSettings
                });
                _settings.Update();
            }
        }

        private void UpdateUpdateTimeValue(int updateTime, bool saveSettings = false)
        {
            SetUpdateTime(updateTime);

            if (saveSettings)
            {
                _lastSavedUpdateTime = updateTime;
                OnUpdateTimeChanged?.Invoke(this, new ViewValueChangedEventArgs
                {
                    Value = updateTime,
                    Save = saveSettings
                });
                _settings.Update();
            }
        }

        private void UpdateObserversCountValue(int observersCount, bool saveSettings = false)
        {
            SetObserversCount(observersCount);

            if (saveSettings)
            {
                _lastSavedObserversCount = observersCount;
                OnObserversCountChanged?.Invoke(this, new ViewValueChangedEventArgs
                {
                    Value = observersCount,
                    Save = saveSettings
                });
                _settings.Update();
            }
        }

        private void RB_CheckedChanged(object sender, EventArgs e)
        {
            NotificationMethod? value = null;
            if (sender == rbMessageBoxNotif && rbMessageBoxNotif.Checked) value = NotificationMethod.SOUND_AND_MESSAGE;
            else if (sender == rbTrayNotif && rbTrayNotif.Checked) value = NotificationMethod.MESSAGE;
            else if (sender == rbNoNotif && rbNoNotif.Checked) value = NotificationMethod.NONE;

            if (value != null)
            {
                OnTemperatureAlertLevelChanged?.Invoke(this, new ViewValueChangedEventArgs
                {
                    Value = value.Value,
                    Save = true
                });
                _settings.Update();
            }
        }

        void ITemperatureUI.SetAvgCPUsTemperature(float temperature)
        {
            labelAvgCPUsTemperature.Text = temperature.ToString();
            thermometerPictureBox.Value = (int)temperature;

            _lastAvgCPUsTemperature = temperature;
            _settings.Update();
            CheckTemperature(_settings.TemperatureAlertLevel, true);
        }

        private void CheckTemperature(float temperatureAlertLevel, bool showAlert = false)
        {
            if (_lastAvgCPUsTemperature >= temperatureAlertLevel)
            {
                if (showAlert)
                {
                    _alert = true;
                    ShowAlertMessage(_lastAvgCPUsTemperature);
                }
                labelAvgCPUsTemperature.BorderStyle = BorderStyle.FixedSingle;
                labelAvgCPUsTemperature.Invalidate();
            }
            else
            {
                if (_alert) _alert = false;
                labelAvgCPUsTemperature.BorderStyle = BorderStyle.None;
                labelAvgCPUsTemperature.Invalidate();
            }
        }

        private void SetTemperatureAlertLevel(float temperature)
        {
            if (temperature < TemperatureUISettingsHandler.MIN_TEMPERATURE_ALERT_LEVEL ||
                temperature > TemperatureUISettingsHandler.MAX_TEMPERATURE_ALERT_LEVEL) return;

            labelTemperature.Text = $"{(int)temperature} °C";
            trackBarTemperatureAlertLevel.Value = (int)temperature;
            nupTemperatureAlertLevel.Value = (int)temperature;

            CheckTemperature(temperature);
        }

        private void SetUpdateTime(int updateTime)
        {
            if (updateTime < TemperatureUISettingsHandler.MIN_UPDATE_TIME ||
                updateTime > TemperatureUISettingsHandler.MAX_UPDATE_TIME)
                return;

            updateTime /= 1000;
            string unit = updateTime > 99 ? "s" : "sec";
            labelUpdateTime.Text = $"{updateTime} {unit}";
            trackbarUpdateTime.Value = updateTime;
            nupUpdateTime.Value = updateTime;
        }

        private void SetObserversCount(int observersCount)
        {
            if (observersCount < TemperatureUISettingsHandler.MIN_OBSERVERS_COUNT ||
                observersCount > TemperatureUISettingsHandler.MAX_OBSERVERS_COUNT)
                return;

            labelObservers.Text = $"{observersCount}";
            trackBarObservers.Value = observersCount;
            nupObservers.Value = observersCount;
        }

        private void SetNotificationMethod(NotificationMethod notification)
        {
            switch (notification)
            {
                case NotificationMethod.SOUND_AND_MESSAGE: rbMessageBoxNotif.Checked = true; break;
                case NotificationMethod.MESSAGE: rbTrayNotif.Checked = true; break;
                case NotificationMethod.NONE: rbNoNotif.Checked = true; break;
            }
        }

        void IView.Show(bool resetPosition)
        {
            _lastSavedTemperatureAlertLevel = trackBarTemperatureAlertLevel.Value;
            _lastSavedUpdateTime = trackbarUpdateTime.Value;
            _lastSavedObserversCount = trackBarObservers.Value;
            if (resetPosition) CenterToScreen(); //ensure that the form is in the center of the screen
            Show();
        }

        private void ShowAlertMessage(float temperature)
        {
            switch (_settings.Notification)
            {
                case NotificationMethod.SOUND_AND_MESSAGE:
                    _sound.Play();
                    goto case NotificationMethod.MESSAGE;

                case NotificationMethod.MESSAGE:
                    OnNotification?.Invoke(this, $"AVG cpu temperature: {temperature} °C!");
                    break;

                case NotificationMethod.NONE:
                    break;
            }
        }
    }
}
