using HardwareMonitor.Client.Controller.Monitors;
using HardwareMonitor.Client.Domain.Contracts;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using HardwareMonitor.Client.Controller.Utils;
using HardwareMonitor.Client.Settings.Utils;
using System.Reflection;
using static HardwareMonitor.Client.Domain.Utils.LogsManager;

namespace HardwareMonitor.Client.Controller
{
    public class HardwareMonitorController : IController
    {
        private const string _APPLICATION_NAME = "Hardware Monitor Client";
        private const string _MONITORS_ICON_NAME = "Monitors";
        private const int _NOTIFICATION_TIMEOUT = 10000;

        #region Settings
        private const string _SETTINGS_ICON_NAME = "Settings";
        private readonly static string _SETTINGS_UI_NAME = $"{_APPLICATION_NAME} - {_SETTINGS_ICON_NAME}";

        private IClientSettingsUI _clientSettingsUI;
        public IClientSettingsUI SettingsUI
        {
            private get { return _clientSettingsUI; }
            set
            {
                _clientSettingsUI?.Close();
                _clientSettingsUI = value;

                var settingsItem = _notifyIcon.ContextMenuStrip.Items[_notifyIcon.ContextMenuStrip.Items.IndexOfKey(_SETTINGS_ICON_NAME)];

                if (_clientSettings != null)
                {
                    _clientSettingsUI.Name = _SETTINGS_UI_NAME;
                    _clientSettingsUI.OnSavedSettings += (s, e) => _clientSettings?.Update();
                    _clientSettingsUI.OnForceTheme += (s, theme) =>
                    {
                        _clientSettingsUI.ForceTheme(theme);
                        TemperatureUI?.ForceTheme(theme);
                    };
                    settingsItem.Image = _clientSettingsUI.Icon;
                    _clientSettingsUI.OnLog += (s, message) => Log(message, LogLevel.VERBOSE);
                }
                
                settingsItem.Visible = _clientSettingsUI != null;
            }
        }
        #endregion

        #region Temperature
        private const string _TEMPERATURE_ICON_NAME = "Temperature";
        private readonly static string _TEMPERATURE_UI_NAME = $"{_APPLICATION_NAME} - {_TEMPERATURE_ICON_NAME}";

        private RemoteTemperatureMonitor _remoteTemperatureMonitor;
        private List<ITemperatureObserver> _temperatureObservers = new List<ITemperatureObserver>();

        public void AddObserver(ITemperatureObserver temperatureObserver)
        {
            _temperatureObservers.Add(temperatureObserver);
            NotifyTemperature(temperatureObserver);
        }

        public bool RemoveObserver(ITemperatureObserver temperatureObserver)
        {
            if (temperatureObserver == null) return false;
            return _temperatureObservers.Remove(temperatureObserver);
        }

        private ITemperatureUI _temperatureUI;
        public ITemperatureUI TemperatureUI {
            private get { return _temperatureUI; }
            set
            {
                RemoveObserver(_temperatureUI);
                _temperatureUI?.Close();
                _temperatureUI = value;
                RemoveTrayItemByKey(_TEMPERATURE_ICON_NAME);
                
                if (_temperatureUI != null)
                {
                    _temperatureUI.Name = _TEMPERATURE_UI_NAME;

                    _temperatureUI.OnUpdateTimeChanged += (s, e) =>
                    {
                        _remoteTemperatureMonitor.Settings.Update();
                    };

                    _temperatureUI.OnNotification += (s, message) =>
                    {
                        if (!_isShowingNotification)
                        {
                            _isShowingNotification = true;
                            _notifyIcon.ShowBalloonTip(_NOTIFICATION_TIMEOUT, _APPLICATION_NAME, message, ToolTipIcon.Warning);
                        }
                    };
                    
                    _temperatureUI.OnLog += (s, message) => Log(message, LogLevel.VERBOSE);
                    _temperatureUI.OnRequestUpdate += (s, e) => NotifyTemperature(_temperatureUI);

                    AddTrayItem(_TEMPERATURE_ICON_NAME, _temperatureUI.Icon,
                        (s, e) => _temperatureUI?.Show(true), _TEMPERATURE_ICON_NAME);

                    AddObserver(_temperatureUI);
                }
            }
        }
        #endregion

        private ToolStripItem AddTrayItem(string text, System.Drawing.Image image, EventHandler onClick, string key)
        {
            var monitorItems = GetMonitorsToolStripMenuItem();
            var item = monitorItems.DropDown.Items.Add(text, image, onClick);
            item.Name = key;
            monitorItems.Visible = true;
            return item;
        }

        private void RemoveTrayItemByKey(string key)
        {
            var monitorItems = GetMonitorsToolStripMenuItem();
            monitorItems.DropDown.Items.RemoveByKey(key);
            if (monitorItems.DropDown.Items.Count == 0)
                monitorItems.Visible = false;
        }

        private ToolStripMenuItem GetMonitorsToolStripMenuItem()
        {
            var items = _notifyIcon.ContextMenuStrip.Items;
            return items[items.IndexOfKey(_MONITORS_ICON_NAME)] as ToolStripMenuItem;
        }

        private ClientSettingsHandler _clientSettings;

        private NotifyIcon _notifyIcon;
        private bool _isShowingNotification;

        public HardwareMonitorController()
        {
            _clientSettings = new ClientSettingsHandler();

            Application.ApplicationExit += (s, e) =>
            {
                CloseAll();
            };

            #region Init tray icon
            _notifyIcon = new NotifyIcon()
            {
                Text = _APPLICATION_NAME,
                Visible = true,
                Icon = UACUtils.IsUserAdministrator ? Properties.Resources.ohmuaclogo : Properties.Resources.ohmlogo
            };
            
            _notifyIcon.BalloonTipClosed += (s, e) => _isShowingNotification = false;
            _notifyIcon.BalloonTipClicked += (s, e) => _isShowingNotification = false;

            var trayMenuStrip = new ContextMenuStrip();

            var monitorItems = trayMenuStrip.Items.Add(_MONITORS_ICON_NAME);
            monitorItems.Name = _MONITORS_ICON_NAME;
            monitorItems.Visible = false;

            var settingsItem = trayMenuStrip.Items.Add(_SETTINGS_ICON_NAME, null, (snd, evt) => SettingsUI?.Show(true));
            settingsItem.Name = _SETTINGS_ICON_NAME;
            settingsItem.Visible = false;

            trayMenuStrip.Items.Add(new ToolStripSeparator());

            trayMenuStrip.Items.Add("Restart", null, (s, e) => {

                //if the privileges requested are different that the ones acquired
                if (_clientSettings.StartProgramAsAdmin != UACUtils.IsUserAdministrator)
                {
                    ProcessUtils.RerunCurrentProcess(_clientSettings.StartProgramAsAdmin);
                    Application.Exit();
                }
                else Application.Restart();
            });

            trayMenuStrip.Items.Add("Exit", null, (s, e) =>
            {
                CloseAll();
                Application.Exit();
            });

            _notifyIcon.ContextMenuStrip = trayMenuStrip;
            _notifyIcon.MouseClick += NotifyIcon_click;
            #endregion

            #region Init Remote Temperature Monitor
            _remoteTemperatureMonitor = new RemoteTemperatureMonitor();
            _remoteTemperatureMonitor.OnEventTriggered += () =>
            {
                NotifyTemperature();
            };
            #endregion
            
            if (_clientSettings.StartupNotification)
            {
                _notifyIcon.ShowBalloonTip(_NOTIFICATION_TIMEOUT, _APPLICATION_NAME,
                    $"The program has been started successfully", ToolTipIcon.None);
            }
        }

        private void NotifyIcon_click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //http://stackoverflow.com/questions/2208690/invoke-notifyicons-context-menu
                MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
                mi.Invoke(_notifyIcon, null);
            }
        }

        private bool NotifyTemperature(ITemperatureObserver observer = null)
        {
            if (_remoteTemperatureMonitor == null) return false;

            if (_remoteTemperatureMonitor.IsServiceReady)
            {
                float temperature = _remoteTemperatureMonitor.GetAvgCPUsTemperature().GetValueOrDefault();
                if (observer == null)
                {
                    lock (_temperatureObservers)
                    {
                        _temperatureObservers.ForEach(obs =>
                            obs.OnAvgCPUsTemperatureChanged(temperature));
                    }
                }
                else observer.OnAvgCPUsTemperatureChanged(temperature);

                return true;
            }
            else return false;
        }

        private void CloseAll()
        {
            _notifyIcon?.Dispose();

            _remoteTemperatureMonitor?.StopWorker();
            TemperatureUI?.Close();
            _temperatureObservers?.Clear();

            SettingsUI.Close();
        }
    }

    class AdminRightsRequiredException : Exception
    {
        public AdminRightsRequiredException() : base() { }

        public AdminRightsRequiredException(string message) : base(message) { }
    }
}
