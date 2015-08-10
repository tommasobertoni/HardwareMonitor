using HardwareMonitor.Client.Controller.Monitors;
using HardwareMonitor.Client.Domain.Contracts;
using HardwareMonitor.Client.Domain.Entities;
using HardwareMonitor.Client.Temperature.Utils;
using System;
using System.Windows.Forms;

namespace HardwareMonitor.Client.Controller
{
    public class HardwareMonitorController : IController
    {
        private const string _APPLICATION_NAME = "HardwareMonitor";
        private const int _TRAY_NOTIFICATION_TIMEOUT = 1000;
        private const string _MONITORS_ICON_NAME = "Monitors";
        private const string _SETTINGS_ICON_NAME = "Settings";

        #region Temperature
        private const string _TEMPERATURE_ICON_NAME = "Temperature";
        private readonly static string _TEMPERATURE_UI_NAME = $"{_APPLICATION_NAME} - {_TEMPERATURE_ICON_NAME}";
        
        private TemperatureMonitor _temperatureMonitor;
        private bool _isMessageVisible;

        private ITemperatureUI _temperatureUI;
        public ITemperatureUI TemperatureUI {
            set
            {
                InitTemperatureMonitorIfNull();

                _temperatureUI = value;
                GetMonitorsToolStripItemCollection().RemoveByKey(_TEMPERATURE_ICON_NAME);

                if (_temperatureMonitor != null)
                {
                    _temperatureUI.Name = _TEMPERATURE_UI_NAME;
                    _temperatureUI.OnTemperatureAlertLevelChanged += (s, e) =>
                    {
                        if (e.Value != null && e.Save && _temperatureMonitor != null)
                            _temperatureMonitor.Settings.TemperatureAlertLevel = (int)e.Value;
                    };

                    _temperatureUI.OnUpdateTimeChanged += (s, e) =>
                    {
                        if (e.Value != null && e.Save && _temperatureMonitor != null)
                            _temperatureMonitor.Settings.UpdateTime = (int)e.Value;
                    };

                    _temperatureUI.OnObserversCountChanged += (s, e) =>
                    {
                        if (e.Value != null && e.Save && _temperatureMonitor != null)
                            _temperatureMonitor.Settings.ObserversCount = (int)e.Value;
                    };

                    _temperatureUI.OnNotificationMethodChanged += (s, e) =>
                    {
                        if (e.Value != null && e.Save && _temperatureMonitor != null)
                            _temperatureMonitor.Settings.Notification = (NotificationMethod)e.Value;
                    };

                    //_temperatureUI.OnLog += (s, e) => { };
                    //_temperatureUI.OnViewExit += (s, e) => { };
                    _temperatureUI.OnRequestUpdate += (s, e) => UpdateTemperatureUI();

                    GetMonitorsToolStripItemCollection().Add(_TEMPERATURE_ICON_NAME, _temperatureUI.Icon,
                        (s, e) => _temperatureUI?.Show(true)).Name = _TEMPERATURE_ICON_NAME;

                    UpdateTemperatureUI();
                }
            }
        }
        #endregion

        private ToolStripItemCollection GetMonitorsToolStripItemCollection()
        {
            var items = _trayIcon.ContextMenuStrip.Items;
            var monitorsItem = items[items.IndexOfKey(_MONITORS_ICON_NAME)] as ToolStripMenuItem;
            return monitorsItem.DropDown.Items;
        }

        private NotifyIcon _trayIcon;

        public HardwareMonitorController()
        {
            Application.ApplicationExit += (s, e) =>
            {
                _temperatureUI?.Close();
                _temperatureMonitor?.Stop();
                _trayIcon?.Dispose();
            };

            #region Init tray icon
            _trayIcon = new NotifyIcon()
            {
                Text = _APPLICATION_NAME,
                Visible = true,
                Icon = Properties.Resources.ohmlogo
            };

            var trayMenuStrip = new ContextMenuStrip();

            trayMenuStrip.Items.Add(_MONITORS_ICON_NAME).Name = _MONITORS_ICON_NAME;
            trayMenuStrip.Items.Add(_SETTINGS_ICON_NAME, Properties.Resources.Settings);
            trayMenuStrip.Items.Add(new ToolStripSeparator());
            trayMenuStrip.Items.Add("Exit", null, (s, e) => Application.Exit());

            _trayIcon.ContextMenuStrip = trayMenuStrip;
            _trayIcon.ShowBalloonTip(_TRAY_NOTIFICATION_TIMEOUT, _APPLICATION_NAME, "The program has started successfully", ToolTipIcon.None);
            #endregion
        }

        private void UpdateTemperatureUI()
        {
            if (_temperatureMonitor != null && _temperatureMonitor.IsServiceReady)
            {
                _temperatureUI?.SetTemperatureAlertLevel(_temperatureMonitor.Settings.TemperatureAlertLevel);
                _temperatureUI?.SetUpdateTime(_temperatureMonitor.Settings.UpdateTime / 1000);
                _temperatureUI?.SetObserversCount(_temperatureMonitor.Settings.ObserversCount);
                _temperatureUI?.SetNotificationMethod(_temperatureMonitor.Settings.Notification);
            }
        }

        private void InitTemperatureMonitorIfNull()
        {
            _temperatureMonitor = new TemperatureMonitor();
            _temperatureMonitor.OnServiceReady += () =>
            {
                if (_temperatureUI != null) UpdateTemperatureUI();
            };
            _temperatureMonitor.OnEventTriggered += () =>
            {
                float? temperature = _temperatureMonitor.GetAvgCPUsTemperature();

                if (temperature.HasValue)
                {
                    _temperatureUI?.SetAvgCPUsTemperature((int)temperature.Value);
                    if (_temperatureMonitor != null &&
                        temperature >= _temperatureMonitor.Settings.TemperatureAlertLevel &&
                        !_isMessageVisible)
                        DisplayHighTemperatureMessage(temperature.Value);
                }
            };
            _temperatureMonitor.Start();
        }

        private void DisplayHighTemperatureMessage(float temperature)
        {
            string message = $"AVG cpu temperature: {temperature} °C!";

            switch (_temperatureMonitor.Settings.Notification)
            {
                case NotificationMethod.MESSAGE_BOX:
                    _isMessageVisible = true;
                    new System.Threading.Thread(() =>
                    {
                        MessageBox.Show(_temperatureUI as IWin32Window, message, _APPLICATION_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        _isMessageVisible = false;
                    }).Start();
                    break;

                case NotificationMethod.TRAY_NOTIFICATION:
                    _trayIcon.ShowBalloonTip(_TRAY_NOTIFICATION_TIMEOUT, _APPLICATION_NAME, message, ToolTipIcon.Info);
                    break;

                case NotificationMethod.NONE:
                    break;
            }
        }
    }
}
