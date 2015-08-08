using HardwareMonitor.Client.Domain.Contracts;
using HardwareMonitor.Client.Domain.Entities;
using System;
using System.Windows.Forms;

namespace HardwareMonitor.Client.Temperature
{
    public partial class TemperatureUI : Form, ITemperatureUI
    {
        public class IntEventArgs : EventArgs
        {
            public int Value { get; set; }
        }

        public event EventHandler OnTemperatureAlertLevelChanged;
        public event EventHandler OnUpdateTimeChanged;
        public event EventHandler OnObserversCountChanged;
        public event EventHandler OnNotificationMethodChanged;
        public event EventHandler OnViewExit;
        public event EventHandler OnRequestUpdate;

        public TemperatureUI()
        {
            InitializeComponent();

            FormClosed += (s, o) => OnViewExit(s, o);

            #region Init ThermometerPictureBox
            thermometerPictureBox1.MarginLeft = 20;
            thermometerPictureBox1.MarginRight = 20;
            thermometerPictureBox1.MarginTop = 5;
            thermometerPictureBox1.MarginBottom = 35;
            thermometerPictureBox1.Percentage = 0;
            #endregion

            trackBarTemperature.MouseWheel += DoNothing_MouseWheel;
            trackbarUpdateTime.MouseWheel += DoNothing_MouseWheel;
            trackBarObservers.MouseWheel += DoNothing_MouseWheel;

            #region Attach value change events
            //trackBarTemperature.ValueChanged += (s, e) => UpdateTemperatureAlertValue(trackBarTemperature.Value);
            trackBarTemperature.MouseUp += (s, e) => UpdateTemperatureAlertValue(trackBarTemperature.Value);
            trackBarTemperature.LostFocus += (s, e) => UpdateTemperatureAlertValue(trackBarTemperature.Value);

            //trackbarUpdateTime.ValueChanged += (s, e) => UpdateUpdateTimeValue(trackbarUpdateTime.Value);
            trackbarUpdateTime.MouseUp += (s, e) => UpdateTemperatureAlertValue(trackBarTemperature.Value);
            trackbarUpdateTime.LostFocus += (s, e) => UpdateUpdateTimeValue(trackbarUpdateTime.Value);

            //trackBarObservers.ValueChanged += (s, e) => UpdateObserversCountValue(trackBarObservers.Value);
            trackBarObservers.MouseUp += (s, e) => UpdateTemperatureAlertValue(trackBarTemperature.Value);
            trackBarObservers.LostFocus += (s, e) => UpdateObserversCountValue(trackBarObservers.Value);

            rbMessageBoxNotif.CheckedChanged += RB_CheckedChanged;
            rbMessageBoxNotif.CheckedChanged += RB_CheckedChanged;
            rbMessageBoxNotif.CheckedChanged += RB_CheckedChanged;
            #endregion
        }

        private void DoNothing_MouseWheel(object sender, EventArgs e)
        {
            HandledMouseEventArgs ee = (HandledMouseEventArgs)e;
            ee.Handled = true;
        }

        private void UpdateTemperatureAlertValue(int temperature)
        {
            labelTemperature.Text = $"{temperature} °C";

            if (OnTemperatureAlertLevelChanged != null)
                OnTemperatureAlertLevelChanged(this, new IntEventArgs
                {
                    Value = temperature
                });
        }

        private void UpdateUpdateTimeValue(int updateTime, bool saveSettings = false)
        {
            labelUpdateTime.Text = $"{updateTime} sec";

            if (OnUpdateTimeChanged != null)
                OnUpdateTimeChanged(this, new IntEventArgs
                {
                    Value = updateTime
                });
        }

        private void UpdateObserversCountValue(int observersCount, bool saveSettings = false)
        {
            labelObservers.Text = $"{observersCount}";

            if (OnObserversCountChanged != null)
                OnObserversCountChanged(this, new IntEventArgs
                {
                    Value = observersCount
                });
        }

        private void RB_CheckedChanged(object sender, EventArgs e)
        {
            if (OnTemperatureAlertLevelChanged == null) return;

            NotificationMethod value;
            if (sender == rbMessageBoxNotif/* && rbMessageBoxNotif.Checked*/) value = NotificationMethod.MESSAGE_BOX;
            else if (sender == rbTrayNotif/* && rbTrayNotif.Checked*/) value = NotificationMethod.TRAY_NOTIFICATION;
            else/* (sender == rbNoNotif/* && rbNoNotif.Checked)*/ value = NotificationMethod.NONE;

            OnTemperatureAlertLevelChanged(this, new NotificationMethodEventArgs
            {
                Value = value
            });
        }

        void ITemperatureUI.SetAvgCPUsTemperature(int temperature)
        {
            labelLastMeasuredTemperature.Text = temperature.ToString();
            thermometerPictureBox1.Percentage = temperature;
        }

        void ITemperatureUI.SetTemperatureAlertLevel(int tal)
        {
            labelTemperature.Text = tal.ToString();
            trackBarTemperature.Value = tal;
        }

        void ITemperatureUI.SetUpdateTime(int updateTime)
        {
            labelUpdateTime.Text = updateTime.ToString();
            trackbarUpdateTime.Value = updateTime / 1000;
        }

        void ITemperatureUI.SetObserversCount(int observersCount)
        {
            labelObservers.Text = observersCount.ToString();
            trackBarObservers.Value = observersCount;
        }

        void ITemperatureUI.SetNotificationMethod(NotificationMethod notification)
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
            if (resetPosition) CenterToScreen(); //ensure that the form is in the center of the screen
            Show();
        }
    }
}
