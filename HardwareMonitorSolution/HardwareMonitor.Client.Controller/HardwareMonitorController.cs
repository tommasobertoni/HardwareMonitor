using HardwareMonitor.Client.Controller.Monitors;
using HardwareMonitor.Client.Domain.Contracts;
using HardwareMonitor.Client.Temperature.Utils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.ServiceModel;
using System.Windows.Forms;
using static System.Threading.Thread;

namespace HardwareMonitor.Client.Controller
{
    public class HardwareMonitorController : IController
    {
        private const string _APPLICATION_NAME = "HardwareMonitor";
        private const string _MONITORS_ICON_NAME = "Monitors";
        private const string _SETTINGS_ICON_NAME = "Settings";

        #region Temperature
        private const string _TEMPERATURE_ICON_NAME = "Temperature";
        private readonly static string _TEMPERATURE_UI_NAME = $"{_APPLICATION_NAME} - {_TEMPERATURE_ICON_NAME}";

        private TemperatureMonitorServiceReference.HardwareMonitorTemperatureWCFContractClient _temperatureService;
        private TemperatureMonitor _temperatureMonitor;
        
        private ITemperatureUI _temperatureUI;
        public ITemperatureUI TemperatureUI {
            get
            {
                return _temperatureUI;
            }

            set
            {
                InitTemperatureMonitorIfNull();

                _temperatureUI = value;
                GetMonitorsToolStripItemCollection().RemoveByKey(_TEMPERATURE_ICON_NAME);

                if (_temperatureMonitor != null)
                {
                    _temperatureUI.Name = _TEMPERATURE_UI_NAME;
                    _temperatureUI.OnTemperatureAlertLevelChanged += (s, e) => { };
                    _temperatureUI.OnUpdateTimeChanged += (s, e) => { };
                    _temperatureUI.OnObserversCountChanged += (s, e) => { };
                    _temperatureUI.OnNotificationMethodChanged += (s, e) => { };
                    _temperatureUI.OnViewExit += (s, e) => { };
                    _temperatureUI.OnRequestUpdate += (s, e) => { };

                    GetMonitorsToolStripItemCollection().Add(_TEMPERATURE_ICON_NAME, _temperatureUI.Icon,
                        (s, e) => TemperatureUI?.Show(true)).Name = _TEMPERATURE_ICON_NAME;

                    UpdateTemperatureUI();
                    _temperatureUI.Show();
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
        private TemperatureUISettingsHandler _settings;

        public HardwareMonitorController()
        {
            _settings = new TemperatureUISettingsHandler();

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
            #endregion

            _temperatureService = new TemperatureMonitorServiceReference.HardwareMonitorTemperatureWCFContractClient();

            Application.ApplicationExit += (s, e) =>
            {
                _trayIcon?.Dispose();
                _temperatureService?.Close();
            };
        }

        private void UpdateTemperatureUI()
        {
            TemperatureUI?.SetTemperatureAlertLevel(_settings.TemperatureAlertLevel);
            TemperatureUI?.SetUpdateTime(_settings.UpdateTime);
            TemperatureUI?.SetObserversCount(_settings.ObserversCount);
            TemperatureUI?.SetNotificationMethod(_settings.Notification);
        }

        private void InitTemperatureMonitorIfNull()
        {
            _temperatureMonitor = new TemperatureMonitor()
            {
                Service = _temperatureService
            };
            _temperatureMonitor.OnEventTriggered += () => TemperatureUI?.SetAvgCPUsTemperature(
                (int) (_temperatureMonitor.GetAvgCPUsTemperature() ?? 0));
            _temperatureMonitor.Start();
        }
    }
}
