﻿using HardwareMonitor.Client.Domain.Contracts;
using System;
using System.Windows.Forms;
using System.Drawing;
using HardwareMonitor.Client.Temperature.Utils;
using HardwareMonitor.Client.Domain.Entities;

namespace HardwareMonitor.Client.Temperature
{
    public partial class TemperatureUI : Form, ITemperatureUI
    {
        string IView.Name
        {
            get
            {
                return Text;
            }

            set
            {
                Text = value;
            }
        }
        
        Image IView.Icon { get; } = Properties.Resources.temperatureIcon.ToBitmap();

        public event EventHandler<int> OnUpdateTimeChanged;
        public event EventHandler<string> OnNotification;
        public event EventHandler OnViewExit;
        public event EventHandler OnRequestUpdate;

        private bool _alert;
        private float _lastAvgCPUsTemperature;
        private int _lastSavedTemperatureAlertLevel,
                    _lastSavedUpdateTime;

        private TemperatureUISettingsHandler _settings;
        private IView _changeSoundUI;

        public TemperatureUI()
        {
            InitializeComponent();

            _settings = new TemperatureUISettingsHandler();

            FormClosed += (s, e) =>
            {
                if ((e as FormClosedEventArgs).CloseReason == CloseReason.UserClosing) OnViewExit(s, e);
            };
            
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

            SetNotificationMethod(_settings.Notification);

            if (SoundResourcesManager.Instance.SelectedSound != null) UpdateSoundName();
            else btnChangeSound.Enabled = false; //no resource available
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
            nupUpdateTime.ValueChanged += (s, e) => UpdateUpdateTimeValue((int)nupUpdateTime.Value * 1000);
            nupUpdateTime.MouseUp += (s, e) => UpdateUpdateTimeValue((int)nupUpdateTime.Value * 1000, true);
            nupUpdateTime.LostFocus += (s, e) => SetUpdateTime(_lastSavedUpdateTime);
            trackbarUpdateTime.MouseWheel += DoNothing_MouseWheel;
            trackbarUpdateTime.ValueChanged += (s, e) => UpdateUpdateTimeValue(trackbarUpdateTime.Value * 1000);
            trackbarUpdateTime.MouseUp += (s, e) => UpdateUpdateTimeValue(trackbarUpdateTime.Value * 1000, true);
            trackbarUpdateTime.LostFocus += (s, e) => SetUpdateTime(_lastSavedUpdateTime);

            rbMessageAndSoundNotif.CheckedChanged += RB_CheckedChanged;
            rbMessageNotif.CheckedChanged += RB_CheckedChanged;
            rbNoNotif.CheckedChanged += RB_CheckedChanged;
            #endregion

            FormClosing += (s, e) =>
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    Hide();
                    e.Cancel = true;
                }
            };

            ApplyTheme();
        }

        private void DoNothing_MouseWheel(object sender, EventArgs e)
        {
            HandledMouseEventArgs ee = (HandledMouseEventArgs)e;
            ee.Handled = true;
        }

        private void ApplyTheme()
        {
            Color backgroundColor, foregroundColor;
            Bitmap thermometerImage;

            switch (_settings.Theme)
            {
                case Theme.Dark:
                    backgroundColor = SystemColors.ControlText;
                    foregroundColor = SystemColors.Control;
                    thermometerImage = Properties.Resources.ThermometerDark;
                    break;

                default:
                    backgroundColor = SystemColors.Control;
                    foregroundColor = SystemColors.ControlText;
                    thermometerImage = Properties.Resources.Thermometer;
                    break;
            }

            BackColor = backgroundColor;
            trackBarTemperatureAlertLevel.BackColor = backgroundColor;
            trackbarUpdateTime.BackColor = backgroundColor;
            labelAvgCPUsTemperature.ForeColor = foregroundColor;
            labelTemperatureAlertLevelTitle.ForeColor = foregroundColor;
            labelTemperature.ForeColor = foregroundColor;
            labelUpdateTimeTitle.ForeColor = foregroundColor;
            labelUpdateTime.ForeColor = foregroundColor;
            labelNotificationMethodTitle.ForeColor = foregroundColor;
            rbMessageAndSoundNotif.ForeColor = foregroundColor;
            rbMessageNotif.ForeColor = foregroundColor;
            rbNoNotif.ForeColor = foregroundColor;
            labelSoundName.ForeColor = foregroundColor;
            labelSoundName.BackColor = backgroundColor;
            btnChangeSound.ForeColor = foregroundColor;
            btnChangeSound.BackColor = backgroundColor;
            thermometerPictureBox.Image = thermometerImage;
        }

        void IView.ForceTheme(Theme theme)
        {
            if (_settings.Theme != theme)
            {
                _settings.Theme = theme;
                ApplyTheme();
                _changeSoundUI?.ForceTheme(theme);
            }
        }

        private void UpdateTemperatureAlertValue(int temperature, bool saveSettings = false)
        {
            SetTemperatureAlertLevel(temperature);

            if (saveSettings)
            {
                _lastSavedTemperatureAlertLevel = temperature;
                _settings.TemperatureAlertLevel = _lastSavedTemperatureAlertLevel;
            }
        }

        private void UpdateUpdateTimeValue(int updateTime, bool saveSettings = false)
        {
            SetUpdateTime(updateTime);

            if (saveSettings)
            {
                _lastSavedUpdateTime = updateTime;
                _settings.UpdateTime = _lastSavedUpdateTime;
                OnUpdateTimeChanged?.Invoke(this, updateTime);
            }
        }

        private void UpdateSoundName()
        {
            labelSoundName.Text = $"\"{SoundResourcesManager.Instance.SelectedSound?.Name ?? "No sound selected"}\"";
            _settings.SoundResourceName = SoundResourcesManager.Instance.SelectedSound.ResourceName;
        }

        private void RB_CheckedChanged(object sender, EventArgs e)
        {
            NotificationMethod? value = null;
            if (sender == rbMessageAndSoundNotif && rbMessageAndSoundNotif.Checked) value = NotificationMethod.SoundAndMessage;
            else if (sender == rbMessageNotif && rbMessageNotif.Checked) value = NotificationMethod.Message;
            else if (sender == rbNoNotif && rbNoNotif.Checked) value = NotificationMethod.None;

            if (value.HasValue) _settings.Notification = value.Value;
        }

        private delegate void ThreadSafeUpdateAvgCPUsTemperature(float temperature);

        public void OnAvgCPUsTemperatureChanged(float temperature)
        {
            if (labelAvgCPUsTemperature.InvokeRequired || thermometerPictureBox.InvokeRequired)
                Invoke(new ThreadSafeUpdateAvgCPUsTemperature(OnAvgCPUsTemperatureChanged), new object[] { temperature });
            
            labelAvgCPUsTemperature.Text = $"{temperature} °C";
            thermometerPictureBox.Value = (int)temperature;

            _lastAvgCPUsTemperature = temperature;
            CheckTemperature(_settings.TemperatureAlertLevel, true);
        }

        private void CheckTemperature(float temperatureAlertLevel, bool showAlert = false)
        {
            if (_lastAvgCPUsTemperature >= temperatureAlertLevel)
            {
                if (!_alert)
                {
                    _alert = true;
                    labelAvgCPUsTemperature.BorderStyle = BorderStyle.FixedSingle;
                    labelAvgCPUsTemperature.Invalidate();
                }

                if (showAlert)ShowAlertMessage(_lastAvgCPUsTemperature);
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

        private void SetNotificationMethod(NotificationMethod notification)
        {
            switch (notification)
            {
                case NotificationMethod.SoundAndMessage: rbMessageAndSoundNotif.Checked = true; break;
                case NotificationMethod.Message: rbMessageNotif.Checked = true; break;
                case NotificationMethod.None: rbNoNotif.Checked = true; break;
            }
        }

        private void btnChangeSound_Click(object sender, EventArgs e)
        {
            var dialog = new SoundChooserForm();
            _changeSoundUI = dialog as IView;
            _changeSoundUI?.ForceTheme(_settings.Theme);
            var result = dialog.ShowDialog(this);
            _changeSoundUI = null;
            dialog.Dispose();

            if (result == DialogResult.OK || result == DialogResult.Yes) UpdateSoundName();
        }

        void IView.Show(bool resetPosition)
        {
            _lastSavedTemperatureAlertLevel = trackBarTemperatureAlertLevel.Value;
            _lastSavedUpdateTime = trackbarUpdateTime.Value;
            if (resetPosition) CenterToScreen(); //ensure that the form is in the center of the screen
            Show();
        }

        private void ShowAlertMessage(float temperature)
        {
            switch (_settings.Notification)
            {
                case NotificationMethod.SoundAndMessage:
                    SoundResourcesManager.Instance.SelectedSound?.Play();
                    goto case NotificationMethod.Message;

                case NotificationMethod.Message:
                    OnNotification?.Invoke(this, $"AVG cpu temperature: {temperature} °C!");
                    break;

                case NotificationMethod.None:
                    break;
            }
        }

        void IView.Close()
        {
            SoundResourcesManager.Instance.DisposeAll();
            base.Close();
            base.Dispose();
            _settings.Close();
        }
    }
}
