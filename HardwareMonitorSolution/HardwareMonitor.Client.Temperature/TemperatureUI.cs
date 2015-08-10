using HardwareMonitor.Client.Domain.Contracts;
using HardwareMonitor.Client.Domain.Entities;
using System;
using System.Windows.Forms;
using System.Drawing;

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
        public event EventHandler<string> OnLog;
        public event EventHandler OnViewExit;
        public event EventHandler OnRequestUpdate;

        private int _lastSavedTemperatureAlertLevel,
                    _lastSavedUpdateTime,
                    _lastSavedObserversCount;

        public TemperatureUI()
        {
            InitializeComponent();

            _iconBitmap = Properties.Resources.temperatureIcon.ToBitmap();

            FormClosed += (s, o) => OnViewExit(s, o);

            #region Init ThermometerPictureBox
            thermometerPictureBox1.MarginLeft = 20;
            thermometerPictureBox1.MarginRight = 20;
            thermometerPictureBox1.MarginTop = 5;
            thermometerPictureBox1.MarginBottom = 35;
            thermometerPictureBox1.Percentage = 0;
            #endregion

            #region Attach value change events
            nupTemperatureAlertLevle.MouseWheel += DoNothing_MouseWheel;
            nupTemperatureAlertLevle.ValueChanged += (s, e) => UpdateTemperatureAlertValue((int)nupTemperatureAlertLevle.Value);
            nupTemperatureAlertLevle.MouseUp += (s, e) => UpdateTemperatureAlertValue((int)nupTemperatureAlertLevle.Value, true);
            nupTemperatureAlertLevle.LostFocus += (s, e) => SetTemperatureAlertLevel(_lastSavedTemperatureAlertLevel);
            trackBarTemperature.MouseWheel += DoNothing_MouseWheel;
            trackBarTemperature.ValueChanged += (s, e) => UpdateTemperatureAlertValue(trackBarTemperature.Value);
            trackBarTemperature.MouseUp += (s, e) => UpdateTemperatureAlertValue(trackBarTemperature.Value, true);
            trackBarTemperature.LostFocus += (s, e) => SetTemperatureAlertLevel(_lastSavedTemperatureAlertLevel);

            nupUpdateTime.MouseWheel += DoNothing_MouseWheel;
            nupUpdateTime.ValueChanged += (s, e) => UpdateUpdateTimeValue((int)nupUpdateTime.Value);
            nupUpdateTime.MouseUp += (s, e) => UpdateTemperatureAlertValue((int)nupUpdateTime.Value, true);
            nupUpdateTime.LostFocus += (s, e) => SetUpdateTime(_lastSavedUpdateTime);
            trackbarUpdateTime.MouseWheel += DoNothing_MouseWheel;
            trackbarUpdateTime.ValueChanged += (s, e) => UpdateUpdateTimeValue(trackbarUpdateTime.Value);
            trackbarUpdateTime.MouseUp += (s, e) => UpdateTemperatureAlertValue(trackBarTemperature.Value, true);
            trackbarUpdateTime.LostFocus += (s, e) => SetUpdateTime(_lastSavedUpdateTime);

            nupObservers.MouseWheel += DoNothing_MouseWheel;
            nupObservers.ValueChanged += (s, e) => UpdateObserversCountValue((int)nupObservers.Value);
            nupObservers.MouseUp += (s, e) => UpdateTemperatureAlertValue((int)nupObservers.Value, true);
            nupObservers.LostFocus += (s, e) => SetObserversCount(_lastSavedObserversCount);
            trackBarObservers.MouseWheel += DoNothing_MouseWheel;
            trackBarObservers.ValueChanged += (s, e) => UpdateObserversCountValue(trackBarObservers.Value);
            trackBarObservers.MouseUp += (s, e) => UpdateTemperatureAlertValue(trackBarTemperature.Value, true);
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
            }
        }

        private void RB_CheckedChanged(object sender, EventArgs e)
        {
            NotificationMethod? value = null;
            if (sender == rbMessageBoxNotif && rbMessageBoxNotif.Checked) value = NotificationMethod.MESSAGE_BOX;
            else if (sender == rbTrayNotif && rbTrayNotif.Checked) value = NotificationMethod.TRAY_NOTIFICATION;
            else if (sender == rbNoNotif && rbNoNotif.Checked) value = NotificationMethod.NONE;

            if (value != null)
                OnTemperatureAlertLevelChanged?.Invoke(this, new ViewValueChangedEventArgs
                {
                    Value = value.Value,
                    Save = true
                });
        }

        public void SetAvgCPUsTemperature(int temperature)
        {
            labelLastMeasuredTemperature.Text = temperature.ToString();
            thermometerPictureBox1.Percentage = temperature;
        }

        public void SetTemperatureAlertLevel(int temperature)
        {
            if (temperature < trackBarTemperature.Minimum || temperature > trackBarTemperature.Maximum) return;

            labelTemperature.Text = $"{temperature} °C";
            trackBarTemperature.Value = temperature;
            nupTemperatureAlertLevle.Value = temperature;
        }

        public void SetUpdateTime(int updateTime)
        {
            if (updateTime < trackbarUpdateTime.Minimum || updateTime > trackbarUpdateTime.Maximum) return;

            string unit = updateTime > 99 ? "s" : "sec";
            labelUpdateTime.Text = $"{updateTime} {unit}";
            trackbarUpdateTime.Value = updateTime;
            nupUpdateTime.Value = updateTime;
        }

        public void SetObserversCount(int observersCount)
        {
            if (observersCount < trackBarObservers.Minimum || observersCount > trackBarObservers.Maximum) return;

            labelObservers.Text = $"{observersCount}";
            trackBarObservers.Value = observersCount;
            nupObservers.Value = observersCount;
        }

        public void SetNotificationMethod(NotificationMethod notification)
        {
            switch (notification)
            {
                case NotificationMethod.MESSAGE_BOX: rbMessageBoxNotif.Checked = true; break;
                case NotificationMethod.TRAY_NOTIFICATION: rbTrayNotif.Checked = true; break;
                case NotificationMethod.NONE: rbNoNotif.Checked = true; break;
            }
        }

        void IView.Show(bool resetPosition)
        {
            _lastSavedTemperatureAlertLevel = trackBarTemperature.Value;
            _lastSavedUpdateTime = trackbarUpdateTime.Value;
            _lastSavedObserversCount = trackBarObservers.Value;
            if (resetPosition) CenterToScreen(); //ensure that the form is in the center of the screen
            Show();
        }
    }
}
