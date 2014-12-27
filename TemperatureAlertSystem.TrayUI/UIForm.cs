using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TemperatureAlertSystem.Domain;
using TemperatureAlertSystem.TrayUI.Utils;

namespace TemperatureAlertSystem.TrayUI
{
    public partial class UIForm : Form
    {
        private const string _APP_TITLE = "Temperature Alert System";

        private NotifyIcon  _trayIcon;

        private TrayIconTextUpdater _trayIconTextUpdater;

        private TemperatureWatcher _temperatureWatcher;

        private SettingsStorage _settings;

        //thread-safe call
        //http://msdn.microsoft.com/query/dev12.query?appId=Dev12IDEF1&l=EN-US&k=k(EHInvalidOperation.WinForms.IllegalCrossThreadCall);k(TargetFrameworkMoniker-.NETFramework,Version%3Dv4.5.1);k(DevLang-csharp)&rd=true
        private delegate void OnTemperatureCheckCallback();

        public UIForm()
        {
            #region Init tray icon

            _trayIcon = new NotifyIcon()
            {
                Text = _APP_TITLE,
                Visible = true,
                Icon = Properties.Resources.AppIcon
            };

            var trayMenuStrip = new ContextMenuStrip();
            
            trayMenuStrip.Items.Add("Settings", Properties.Resources.SettingsImage,
                (object sender, EventArgs e) => ShowSettings());

            trayMenuStrip.Items.Add(new ToolStripSeparator());
            trayMenuStrip.Items.Add("Exit", null, (object sender, EventArgs e) => Application.Exit());

            _trayIcon.ContextMenuStrip = trayMenuStrip;

            #endregion

            #region Attach temperature observer

            _temperatureWatcher = new TemperatureWatcher();

            _temperatureWatcher.OnTemperatureCheck += () =>
            {
                OnTemperatureCheckCallback callback = new OnTemperatureCheckCallback(OnTemperatureCheck);
                if (IsHandleCreated)
                    Invoke(callback);
            };

            _temperatureWatcher.OnHighTemperatureLevelDetected += () => DisplayHighTemperatureMessage();

            #endregion

            #region Init other components

            _settings = new SettingsStorage();
            _trayIconTextUpdater = new TrayIconTextUpdater();
            _trayIconTextUpdater.OnUpdateTextEvent += () =>
            {
                TrayIconTextUpdater.OnUpdateText callback = new TrayIconTextUpdater.OnUpdateText(OnUpdateTemperatureLabel);
                if (IsHandleCreated)
                    Invoke(callback);
            };

            Application.ApplicationExit += OnApplicationExit;

            #endregion

            #region Restore Settings and Init Form Controls

            InitializeComponent();
            
            //temperature
            var alertTemperature = _settings.GetAlertTemperature();
            if (alertTemperature == -1)
                alertTemperature = TemperatureWatcher.DEFAULT_TEMPERATURE_ALERT_LEVEL;
            _temperatureWatcher.TemperatureAlertLevel = alertTemperature;

            //timeout
            var timeoutMillis = _settings.GetTimeoutMillis();
            if (timeoutMillis == -1)
                timeoutMillis = TemperatureWatcher.DEFAULT_CHECK_TEMPERATURE_TIMEOUT_MILLIS;
            _temperatureWatcher.CheckTemperatureTimeoutMillis = timeoutMillis;

            //notification
            switch (_settings.GetNotificationMode())
            {
                case SettingsStorage.NotificationMode.MESSAGE_BOX:
                    rbMessageBoxNotif.Checked = true;
                    break;

                case SettingsStorage.NotificationMode.TRAY_NOTIFICATION:
                    rbTrayNotif.Checked = true;
                    break;

                case SettingsStorage.NotificationMode.NONE:
                    rbNoNotif.Checked = true;
                    break;
            }

            UpdateFormControls();

            #endregion

            #region UI events

            trackBarTemperature.MouseWheel += DoNothing_MouseWheel;
            trackBarTemperature.ValueChanged += (object sender, EventArgs e) =>
                UpdateAlertTemperatureLabel(trackBarTemperature.Value);
            trackBarTemperature.MouseUp += trackBarTemperature_MouseUp;

            trackBarTimeout.MouseWheel += DoNothing_MouseWheel;
            trackBarTimeout.ValueChanged += (object sender, EventArgs e) =>
                UpdateTimeoutLabel(trackBarTimeout.Value);
            trackBarTimeout.MouseUp += trackBarTimeout_MouseUp;

            rbMessageBoxNotif.CheckedChanged += radioButton_checkedChanged;
            rbTrayNotif.CheckedChanged += radioButton_checkedChanged;
            rbNoNotif.CheckedChanged += radioButton_checkedChanged;

            #endregion

            #region Start

            _temperatureWatcher.Start();
            _trayIconTextUpdater.StartUpdate();
            _trayIcon.ShowBalloonTip(2000, _APP_TITLE, "The program has started successfully", ToolTipIcon.None);

            #endregion
        }

        private void OnTemperatureCheck()
        {
            UpdateLastMeasuredTemperatureLabel((int)_temperatureWatcher.LastMeasuredTemperature);
        }

        private void OnUpdateTemperatureLabel()
        {
            if (_temperatureWatcher.LastTemperatureMeasuredDatetime == default(DateTime)) return;

            int secondsAgo = (DateTime.Now - _temperatureWatcher.LastTemperatureMeasuredDatetime).Seconds;
            int secondsToNextTemperatureMeasurement =
                (_temperatureWatcher.CheckTemperatureTimeoutMillis / 1000) - secondsAgo;

            UpdateLastMeasuredTemperatureDescLabel(secondsAgo);
            _trayIcon.Text = String.Format("{0}\n{1} seconds to next temperature check",
                _APP_TITLE, secondsToNextTemperatureMeasurement);
        }

        private void DoNothing_MouseWheel(object sender, EventArgs e)
        {
            HandledMouseEventArgs ee = (HandledMouseEventArgs)e;
            ee.Handled = true;
        }

        private void trackBarTemperature_MouseUp(object sender, MouseEventArgs e)
        {
            int alertTemperature = trackBarTemperature.Value;
            _temperatureWatcher.TemperatureAlertLevel = alertTemperature;
            _settings.SetAlertTemperature(alertTemperature);
        }

        private void trackBarTimeout_MouseUp(object sender, MouseEventArgs e)
        {
            int timeoutSeconds = trackBarTimeout.Value;
            int timeoutMillis = timeoutSeconds * 1000;
            _temperatureWatcher.CheckTemperatureTimeoutMillis = timeoutMillis;
            _settings.SetTimeoutMillis(timeoutMillis);
        }

        private void UpdateAlertTemperatureLabel(float alertTemperature)
        {
            labelTemperature.Text = String.Format("{0} °C", alertTemperature);
        }

        private void UpdateTimeoutLabel(int timeout)
        {
            labelTimeout.Text = String.Format("{0} sec", timeout);
        }

        void radioButton_checkedChanged(object sender, EventArgs e)
        {
            if (sender == rbMessageBoxNotif && rbMessageBoxNotif.Checked)
                _settings.SetNotificationMode(SettingsStorage.NotificationMode.MESSAGE_BOX);
            else if (sender == rbTrayNotif && rbTrayNotif.Checked)
                _settings.SetNotificationMode(SettingsStorage.NotificationMode.TRAY_NOTIFICATION);
            else if (sender == rbNoNotif && rbNoNotif.Checked)
                _settings.SetNotificationMode(SettingsStorage.NotificationMode.NONE);
        }

        private void UpdateLastMeasuredTemperatureLabel(int temperature)
        {
            thermometerPictureBox.Percentage = temperature;
            labelLastMeasuredTemperature.Text = String.Format("{0} °C", temperature);
        }

        private void UpdateLastMeasuredTemperatureDescLabel(int secondsAgo)
        {
            labelLastMeasuredTemperatureDesc.Text = String.Format("Last measured temperature, {0} seconds ago", secondsAgo);
        }

        void OnApplicationExit(object sender, EventArgs e)
        {
            _trayIconTextUpdater.StopUpdate();
            _temperatureWatcher.Stop();
            _trayIcon.Dispose();
        }

        private void DisplayHighTemperatureMessage()
        {
            string message = String.Format(
                            "The cpu temperature is {0} °C!",
                            _temperatureWatcher.LastMeasuredTemperature);

            switch (_settings.GetNotificationMode())
            {
                case SettingsStorage.NotificationMode.MESSAGE_BOX:
                    MessageBox.Show(message, _APP_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case SettingsStorage.NotificationMode.TRAY_NOTIFICATION:
                    _trayIcon.ShowBalloonTip(1000, _APP_TITLE, message, ToolTipIcon.Info);
                    break;

                case SettingsStorage.NotificationMode.NONE:
                    break;
            }
        }

        private void ShowSettings()
        {
            CenterToScreen(); //ensure that the form is in the center of the screen
            UpdateFormControls();
            Show();
        }

        private void UpdateFormControls()
        {
            UpdateLastMeasuredTemperatureLabel((int)_temperatureWatcher.LastMeasuredTemperature);
            trackBarTemperature.Value = (int) _temperatureWatcher.TemperatureAlertLevel;
            UpdateAlertTemperatureLabel(_temperatureWatcher.TemperatureAlertLevel);
            var timeout = _temperatureWatcher.CheckTemperatureTimeoutMillis / 1000;
            trackBarTimeout.Value = timeout;
            UpdateTimeoutLabel(timeout);
        }
    }
}
