using HardwareMonitor.Client.Controller.Monitors;
using HardwareMonitor.Client.Domain.Contracts;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using HardwareMonitor.Client.Controller.Utils;
using HardwareMonitor.Client.Controller.Contracts;

namespace HardwareMonitor.Client.Controller
{
    public class HardwareMonitorController : IController
    {
        private const string _APPLICATION_NAME = "Hardware Monitor Client";
        private const int _NOTIFICATION_TIMEOUT = 10000;
        private const string _MONITORS_ICON_NAME = "Monitors";
        private const string _SETTINGS_ICON_NAME = "Settings";

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
            set
            {
                RemoveObserver(_temperatureUI);
                _temperatureUI?.Close();
                _temperatureUI = value;
                GetMonitorsToolStripItemCollection().RemoveByKey(_TEMPERATURE_ICON_NAME);
                
                if (_temperatureUI != null)
                {
                    _temperatureUI.Name = _TEMPERATURE_UI_NAME;

                    _temperatureUI.OnUpdateTimeChanged += (s, e) =>
                    {
                        _remoteTemperatureMonitor.Settings.Update();
                    };

                    _temperatureUI.OnObserversCountChanged += (s, observersCount) =>
                    {
                        _remoteTemperatureMonitor.Settings.ObserversCount = observersCount;
                    };

                    _temperatureUI.OnNotification += (s, message) =>
                    {
                        if (!_isShowingNotification)
                        {
                            _isShowingNotification = true;
                            _notifyIcon.ShowBalloonTip(_NOTIFICATION_TIMEOUT, _APPLICATION_NAME, message, ToolTipIcon.Warning);
                        }
                    };

                    _temperatureUI.OnLog += (s, messsage) => Console.WriteLine(messsage);
                    _temperatureUI.OnRequestUpdate += (s, e) => NotifyTemperature(_temperatureUI);

                    GetMonitorsToolStripItemCollection().Add(_TEMPERATURE_ICON_NAME, _temperatureUI.Icon,
                        (s, e) => _temperatureUI?.Show(true)).Name = _TEMPERATURE_ICON_NAME;

                    AddObserver(_temperatureUI);
                }
            }
        }
        #endregion

        private ToolStripItemCollection GetMonitorsToolStripItemCollection()
        {
            var items = _notifyIcon.ContextMenuStrip.Items;
            var monitorsItem = items[items.IndexOfKey(_MONITORS_ICON_NAME)] as ToolStripMenuItem;
            return monitorsItem.DropDown.Items;
        }

        private ClientSettingsHandler _clientSettings;

        private NotifyIcon _notifyIcon;
        private bool _isShowingNotification;

        public HardwareMonitorController()
        {
            _clientSettings = new ClientSettingsHandler();

            #region Broadcast services
            bool servicesStarted = false;
            if (_clientSettings.StartupBroadcastServices)
            {
                if (BroadcastServices.IsUserAdministrator)
                {
                    servicesStarted = true;
                    servicesStarted = servicesStarted && BroadcastServices.Temperature.Start(this);
                }
                else
                {
                    CloseAll();
                    throw new AdminRightsRequiredException("Administrator rights are required to start the broadcast services");
                }
            }
            #endregion

            Application.ApplicationExit += (s, e) =>
            {
                CloseAll();
            };

            #region Init tray icon
            _notifyIcon = new NotifyIcon()
            {
                Text = _APPLICATION_NAME,
                Visible = true,
                Icon = BroadcastServices.IsUserAdministrator ? Properties.Resources.ohmuaclogo : Properties.Resources.ohmlogo
            };

            _notifyIcon.BalloonTipClosed += (s, e) => _isShowingNotification = false;
            _notifyIcon.BalloonTipClicked += (s, e) => _isShowingNotification = false;

            var trayMenuStrip = new ContextMenuStrip();
            
            trayMenuStrip.Items.Add(_MONITORS_ICON_NAME).Name = _MONITORS_ICON_NAME;
            trayMenuStrip.Items.Add(_SETTINGS_ICON_NAME, Properties.Resources.Settings, (snd, evt) =>
            {
                var settingsForm = new SettingsForm(
                    BroadcastServices.IsUserAdministrator,
                    BroadcastServices.Temperature.IsRunning);

                var settingsOperations = settingsForm as IClientSettingsOperations;
                if (settingsOperations != null)
                {
                    settingsOperations.OnToggleBroadcastServices += (s, startServices) =>
                    {
                        string action;
                        bool result;
                        if (startServices)
                        {
                            action = "started";
                            result = BroadcastServices.Temperature.Start(this);
                        }
                        else
                        {
                            action = "stopped";
                            result = BroadcastServices.Temperature.Stop(this);
                        }

                        string negative = result ? "" : "n't";
                        _notifyIcon.ShowBalloonTip(_NOTIFICATION_TIMEOUT, _APPLICATION_NAME,
                            $"The broadcast services have{negative} been {action} successfully", ToolTipIcon.None);
                    };

                    settingsOperations.OnSavedSettings += (s, e) => _clientSettings?.Update();
                }
                settingsForm.ShowDialog();
            });

            trayMenuStrip.Items.Add(new ToolStripSeparator());

            trayMenuStrip.Items.Add("Restart", null, (s, e) => {

                var requiresAdministratorRights = _clientSettings.StartProgramAsAdmin || _clientSettings.StartupBroadcastServices;
                CloseAll();

                //if the privileges requested are different that the ones acquired
                if (requiresAdministratorRights != BroadcastServices.IsUserAdministrator)
                {
                    ProcessUtils.RerunCurrentProcess(requiresAdministratorRights);
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
                var subSentence = servicesStarted ? " and the services have" : " has";
                _notifyIcon.ShowBalloonTip(_NOTIFICATION_TIMEOUT, _APPLICATION_NAME,
                    $"The program{subSentence} been started successfully", ToolTipIcon.None);
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
            BroadcastServices.Temperature.Stop(this);
            _remoteTemperatureMonitor?.StopWorker();
            _temperatureUI?.Close();
            _temperatureObservers?.Clear();
        }
    }

    class AdminRightsRequiredException : Exception
    {
        public AdminRightsRequiredException() : base() { }

        public AdminRightsRequiredException(string message) : base(message) { }
    }
}
