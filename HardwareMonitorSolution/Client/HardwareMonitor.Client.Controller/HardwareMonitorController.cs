using HardwareMonitor.Client.Controller.Monitors;
using HardwareMonitor.Client.Domain.Contracts;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using HardwareMonitor.Client.Controller.Utils;
using HardwareMonitor.Client.Settings.Utils;

namespace HardwareMonitor.Client.Controller
{
    [Flags]
    public enum HardwareMonitorType
    {
        Temperature
    }

    public class HardwareMonitorController : IController
    {
        #region Settings
        private readonly static string _SETTINGS_UI_NAME = $"{ContextMenuController.APPLICATION_NAME} - {ContextMenuController.SETTINGS_ICON_NAME}";

        private IClientSettingsUI _clientSettingsUI;
        public IClientSettingsUI SettingsUI
        {
            private get { return _clientSettingsUI; }
            set
            {
                _clientSettingsUI?.Close();
                _clientSettingsUI = value;

                if (_clientSettings != null)
                {
                    _clientSettingsUI.Name = _SETTINGS_UI_NAME;
                    _clientSettingsUI.OnSavedSettings += (s, e) => _clientSettings?.Update();
                    _clientSettingsUI.OnForceTheme += (s, theme) =>
                    {
                        _clientSettingsUI.ForceTheme(theme);
                        TemperatureUI?.ForceTheme(theme);
                    };
                    
                    _menuController.SetSettingsItem(_clientSettingsUI.Icon, (s, e) => SettingsUI?.Show(true));
                }

                _menuController.SetSettingsItemVisible(_clientSettingsUI != null);
            }
        }
        #endregion

        #region Temperature
        private readonly static string _TEMPERATURE_UI_NAME = $"{ContextMenuController.APPLICATION_NAME} - {ContextMenuController.TEMPERATURE_ICON_NAME}";

        private RemoteTemperatureMonitor _remoteTemperatureMonitor;
        private List<ITemperatureObserver> _temperatureObservers = new List<ITemperatureObserver>();

        private ITemperatureUI _temperatureUI;
        public ITemperatureUI TemperatureUI {
            private get { return _temperatureUI; }
            set
            {
                RemoveObserver(_temperatureUI);
                _temperatureUI?.Close();
                _temperatureUI = value;
                
                if (_temperatureUI != null)
                {
                    _temperatureUI.Name = _TEMPERATURE_UI_NAME;
                    _temperatureUI.OnUpdateTimeChanged += (s, e) => _remoteTemperatureMonitor.Settings.Update();
                    _temperatureUI.OnNotification += (s, message) => _menuController.ShowNotification(message, ToolTipIcon.Warning);
                    _temperatureUI.OnRequestUpdate += (s, e) => NotifyTemperature(_temperatureUI);
                    _menuController.SetTemperatureItem(_temperatureUI.Icon, (s, e) => _temperatureUI?.Show(true));

                    AddObserver(_temperatureUI);
                }

                _menuController.SetTemperatureItemVisible(_temperatureUI != null);
            }
        }

        public void AddObserver(ITemperatureObserver temperatureObserver)
        {
            if (temperatureObserver != null)
            {
                _temperatureObservers.Add(temperatureObserver);
                NotifyTemperature(temperatureObserver);
            }
        }

        public bool RemoveObserver(ITemperatureObserver temperatureObserver)
        {
            if (temperatureObserver == null) return false;
            return _temperatureObservers.Remove(temperatureObserver);
        }
        #endregion

        private ClientSettingsHandler _clientSettings = new ClientSettingsHandler();
        private ContextMenuController _menuController;

        public HardwareMonitorController()
        {
            Application.ApplicationExit += (s, e) => CloseAll();

            _menuController = new ContextMenuController(this);
            _menuController.OnRestartClicked += () =>
            {
                //if the privileges requested are different that the ones acquired
                if (_clientSettings.StartProgramAsAdmin != UACUtils.IsUserAdministrator)
                {
                    ProcessUtils.RerunCurrentProcess(_clientSettings.StartProgramAsAdmin);
                    Application.Exit();
                }
                else Application.Restart();
            };

            _menuController.OnExitClicked += () =>
            {
                CloseAll();
                Application.Exit();
            };
            
            _remoteTemperatureMonitor = new RemoteTemperatureMonitor();
            _remoteTemperatureMonitor.OnEventTriggered += () =>
            {
                NotifyTemperature();
            };

            if (_clientSettings.StartupNotification)
                _menuController.ShowNotification($"The program has been started successfully");
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
            _menuController.Dispose();
            _remoteTemperatureMonitor?.StopWorker();
            TemperatureUI?.Close();
            _temperatureObservers?.Clear();
            SettingsUI?.Close();
        }
    }

    class AdminRightsRequiredException : Exception
    {
        public AdminRightsRequiredException() : base() { }

        public AdminRightsRequiredException(string message) : base(message) { }
    }
}
