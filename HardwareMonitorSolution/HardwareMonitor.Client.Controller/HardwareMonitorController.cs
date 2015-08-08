using HardwareMonitor.Client.Controller.Monitors;
using HardwareMonitor.Client.Domain.Contracts;
using HardwareMonitor.Client.Temperature.Utils;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using static System.Threading.Thread;

namespace HardwareMonitor.Client.Controller
{
    public class HardwareMonitorController : IController
    {
        private const string _APPLICATION_NAME = "HardwareMonitor";

        #region Temperature
        private readonly static string _TEMPERATURE_UI_NAME = $"{_APPLICATION_NAME} - Temperature";

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

                _temperatureUI.Name = _TEMPERATURE_UI_NAME;
                _temperatureUI.OnTemperatureAlertLevelChanged += (s, e) => { };
                _temperatureUI.OnUpdateTimeChanged += (s, e) => { };
                _temperatureUI.OnObserversCountChanged += (s, e) => { };
                _temperatureUI.OnNotificationMethodChanged += (s, e) => { };
                _temperatureUI.OnViewExit += (s, e) => { };
                _temperatureUI.OnRequestUpdate += (s, e) => { };

                UpdateTemperatureUI();
            }
        }
        #endregion

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

            trayMenuStrip.Items.Add("Temperature Settings", Properties.Resources.Settings,
                (s, e) => TemperatureUI?.Show(true));

            trayMenuStrip.Items.Add(new ToolStripSeparator());
            trayMenuStrip.Items.Add("Exit", null, (object sender, EventArgs e) => /*Application.Exit()*/ TemperatureUI?.Hide());

            _trayIcon.ContextMenuStrip = trayMenuStrip;
            #endregion

            _temperatureService = new TemperatureMonitorServiceReference.HardwareMonitorTemperatureWCFContractClient();

            Application.ApplicationExit += (s, e) =>
            {
                _temperatureService.Close();
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
            _temperatureMonitor = new TemperatureMonitor();
            _temperatureMonitor.OnEventTriggered += () => TemperatureUI?.SetAvgCPUsTemperature(
                (int) (_temperatureMonitor.GetAvgCPUsTemperature() ?? 0));
            _temperatureMonitor.Start();
        }
    }
}
