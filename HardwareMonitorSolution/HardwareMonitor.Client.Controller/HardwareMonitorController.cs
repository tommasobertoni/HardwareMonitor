using HardwareMonitor.Client.Controller.Monitors;
using HardwareMonitor.Client.Domain.Contracts;
using HardwareMonitor.Client.Domain.Entities;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using HardwareMonitor.Client.Controller.Utils;
using System.IO.Pipes;
using HardwareMonitor.Client.Controller.Contracts;
using System.IO;
using HardwareMonitor.Client.TemperatureWCF;
using System.ServiceModel;

namespace HardwareMonitor.Client.Controller
{
    public class HardwareMonitorController : IController
    {
        private const string _APPLICATION_NAME = "HardwareMonitor";
        private const int _NOTIFICATION_TIMEOUT = 10000;
        private const string _MONITORS_ICON_NAME = "Monitors";
        private const string _SETTINGS_ICON_NAME = "Settings";

        private BinaryWriter _binWriter;
        private PipeStream _launcherPipe;
        public PipeStream LauncherPipe {
            get
            {
                return _launcherPipe;
            }

            set
            {
                _binWriter?.Dispose();
                _launcherPipe = value;
                if (_launcherPipe != null) _binWriter = new BinaryWriter(_launcherPipe);
            }
        }

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

                    _temperatureUI.OnObserversCountChanged += (s, e) =>
                    {
                        if (e.Value != null && e.Save && _remoteTemperatureMonitor != null)
                            _remoteTemperatureMonitor.Settings.ObserversCount = (int)e.Value;
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

        private NotifyIcon _notifyIcon;
        private bool _isShowingNotification;

        private ClientSettingsHandler _clientSettings;

        public HardwareMonitorController()
        {
            Application.ApplicationExit += (s, e) =>
            {
                _temperatureUI?.Close();
                _remoteTemperatureMonitor?.StopWorker();
                _notifyIcon?.Dispose();
            };

            _clientSettings = new ClientSettingsHandler();

            #region Init tray icon
            _notifyIcon = new NotifyIcon()
            {
                Text = _APPLICATION_NAME,
                Visible = true,
                Icon = Properties.Resources.ohmlogo
            };

            _notifyIcon.BalloonTipClosed += (s, e) => _isShowingNotification = false;
            _notifyIcon.BalloonTipClicked += (s, e) => _isShowingNotification = false;

            var trayMenuStrip = new ContextMenuStrip();

            trayMenuStrip.Items.Add(_MONITORS_ICON_NAME).Name = _MONITORS_ICON_NAME;
            trayMenuStrip.Items.Add(_SETTINGS_ICON_NAME, Properties.Resources.Settings, (snd, evt) =>
            {
                var settingsForm = new SettingsForm(
                    BroadcastServices.IsUserAdministrator(),
                    BroadcastServices.Temperature.IsRunning);

                var settingsOperations = settingsForm as IClientSettingsOperations;
                if (settingsOperations != null)
                {
                    settingsOperations.OnAdminStartEnabled += (s, e) => SendToLauncher(ControllerCommand.UPDATE);
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
                }
                settingsForm.ShowDialog();
            });
            trayMenuStrip.Items.Add(new ToolStripSeparator());
            trayMenuStrip.Items.Add("Restart", null, SendRestartSignal);
            trayMenuStrip.Items.Add("Exit", null, (s, e) => Application.Exit());

            _notifyIcon.ContextMenuStrip = trayMenuStrip;
            #endregion

            #region Init Remote Temperature Monitor
            _remoteTemperatureMonitor = new RemoteTemperatureMonitor();
            _remoteTemperatureMonitor.OnEventTriggered += () =>
            {
                NotifyTemperature();
            };
            #endregion

            #region Broadcast services
            bool servicesStarted = false;
            if (BroadcastServices.IsUserAdministrator() && _clientSettings.StartupBroadcastServices)
            {
                servicesStarted = true;
                servicesStarted = servicesStarted && BroadcastServices.Temperature.Start(this);
            }
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

        private void SendRestartSignal(object o, EventArgs e)
        {
            if (SendToLauncher(ControllerCommand.RESTART))
                Application.Exit();
        }

        private bool SendToLauncher(ControllerCommand command)
        {
            if (_launcherPipe == null || _binWriter == null) return false;

            _binWriter.Write((int)command);
            _binWriter.Flush();
            _launcherPipe.WaitForPipeDrain();
            return true;
        }
    }
}
