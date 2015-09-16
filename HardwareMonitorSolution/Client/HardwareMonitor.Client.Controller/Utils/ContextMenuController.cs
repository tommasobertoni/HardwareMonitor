using System;
using static System.Diagnostics.Process;
using System.Reflection;
using System.Windows.Forms;
using static HardwareMonitor.Client.Domain.Utils.LogsManager;
using static HardwareMonitor.Client.Controller.Properties.Resources;
using HardwareMonitor.Client.Domain.Contracts;
using System.Collections.Generic;
using System.IO;
using HardwareMonitor.Client.Settings.Utils;

namespace HardwareMonitor.Client.Controller.Utils
{
    public class ContextMenuController : IDisposable
    {
        public const int NOTIFICATION_TIMEOUT = 10000;
        
        public const string APPLICATION_NAME = "Hardware Monitor Client";

        public const string INFO_ICON_NAME = "Info";
        public const string SETTINGS_ICON_NAME = "Settings";

        public const string LOGS_ICON_NAME = "Logs";
        public const string START_RECORDING_ACTION_ICON_NAME = "Start recording";
        public const string STOP_RECORDING_ACTION_ICON_NAME = "Stop and save";
        public const string VERBOSE_LOGS_ICON_NAME = "Verbose";
        public const string DEBUG_LOGS_ICON_NAME = "Debug";
        public const string WARNING_LOGS_ICON_NAME = "Warning";
        public const string ERROR_LOGS_ICON_NAME = "Error";
        public const string NO_LOGS_ICON_NAME = "no logs found";

        public const string MONITORS_ICON_NAME = "Monitors";

        public const string TEMPERATURE_ICON_NAME = "Temperature";

        public bool IsShowingNotification { get; private set; }

        private NotifyIcon _notifyIcon;
        private ToolStripMenuItem _monitorsItem, _logsItem;
        private ToolStripSeparator _logsToolStripSeparator;
        private ToolStripItem _toggleRecordingAction;
        private ToolStripItem _verboseLogsItem, _debugLogsItem, _warningLogsItem, _errorLogsItem;
        private ToolStripItem _infoItem, _settingsItem;
        private ToolStripItem _temperatureItem;

        private bool _closeContextMenu = true, _closeLogsDropdown = true;

        public delegate void NoParametersEventHandler();
        public event NoParametersEventHandler OnRestartClicked, OnExitClicked;

        public bool IsRecording { get; private set; } = false;
        private HardwareValuesRecorderMenuController _recorder;

        private IController _controller;
        private ClientSettingsHandler _clientSettings = new ClientSettingsHandler();

        public ContextMenuController(IController controller)
        {
            _controller = controller;
            _notifyIcon = new NotifyIcon()
            {
                Text = APPLICATION_NAME,
                Visible = true,
                Icon = UACUtils.IsUserAdministrator ? ohmuaclogo : ohmlogo
            };

            _notifyIcon.BalloonTipClosed += (s, e) => IsShowingNotification = false;
            _notifyIcon.BalloonTipClicked += (s, e) => IsShowingNotification = false;

            var contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Closing += ContextMenuStrip_Closing;

            #region Monitors
            _monitorsItem = contextMenuStrip.Items.Add(MONITORS_ICON_NAME) as ToolStripMenuItem;
            _monitorsItem.Name = MONITORS_ICON_NAME;
            _monitorsItem.Available = false;

            _temperatureItem = _monitorsItem.DropDown.Items.Add(TEMPERATURE_ICON_NAME);
            _temperatureItem.Available = false;
            _temperatureItem.VisibleChanged += (s, e) => InvalidateMonitorsIcon();
            #endregion

            #region Logs
            _logsItem = contextMenuStrip.Items.Add(LOGS_ICON_NAME, null) as ToolStripMenuItem;
            contextMenuStrip.Opening += InvalidateLogsDropDown;
            _logsItem.MouseHover += InvalidateLogsDropDown;
            _logsItem.MouseDown += (s, e) =>
            {
                if (!_logsItem.AllowDrop)
                    CancelContextMenuStripClosing(s, e);
            };
            _logsItem.DropDown.Closing += LogsDropDown_Closing;

            var logs = _logsItem.DropDown.Items;

            _toggleRecordingAction = logs.Add(START_RECORDING_ACTION_ICON_NAME, start_record_icon, (s, e) =>
            {
                if (IsRecording)
                {
                    _recorder.Save();
                    _recorder.Close();

                    _controller.RemoveObserver(_recorder);
                    _recorder = null;
                    
                    IsRecording = false;
                    _toggleRecordingAction.Text = START_RECORDING_ACTION_ICON_NAME;
                    _toggleRecordingAction.Image = start_record_icon;
                }
                else
                {
                    _recorder = new HardwareValuesRecorderMenuController(this, HardwareMonitorType.Temperature);
                    _controller.AddObserver(_recorder);
                    IsRecording = true;
                    _toggleRecordingAction.Text = STOP_RECORDING_ACTION_ICON_NAME;
                    _toggleRecordingAction.Image = stop_record_icon;
                }
            });
            _toggleRecordingAction.MouseDown += CancelLogsDropdownClosing;

            logs.Add((_logsToolStripSeparator = new ToolStripSeparator()));

            //if dropdown shows log file but that file has actually been deleted, don't close the dropdown if the item gets clicked
            (_verboseLogsItem = logs.Add(VERBOSE_LOGS_ICON_NAME, verbose_logs_icon,
                (s, e) => OpenLogs(LogLevel.Verbose))).MouseDown += CancelLogsDropdownClosing;
            (_debugLogsItem = logs.Add(DEBUG_LOGS_ICON_NAME, debug_logs_icon,
                (s, e) => OpenLogs(LogLevel.Debug))).MouseDown += CancelLogsDropdownClosing;
            (_warningLogsItem = logs.Add(WARNING_LOGS_ICON_NAME, warning_logs_icon,
                (s, e) => OpenLogs(LogLevel.Warning))).MouseDown += CancelLogsDropdownClosing;
            (_errorLogsItem = logs.Add(ERROR_LOGS_ICON_NAME, error_logs_icon,
                (s, e) => OpenLogs(LogLevel.Error))).MouseDown += CancelLogsDropdownClosing;
            #endregion

            _settingsItem = contextMenuStrip.Items.Add(SETTINGS_ICON_NAME);
            _settingsItem.Name = SETTINGS_ICON_NAME;
            _settingsItem.Available = false;
            
            contextMenuStrip.Items.Add(new ToolStripSeparator());

            _infoItem = contextMenuStrip.Items.Add("About", info_icon.ToBitmap(), (s, e) =>
            {
                MessageBox.Show(new Form() { TopMost = true /*pretty bad, I know*/ },
                    "HardwareMonitor windows service and winform developed by Tommaso Bertoni, 2015.\n\nBased on the OpenHardwareMonitorLib project.",
                    $"{APPLICATION_NAME} - {INFO_ICON_NAME}", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });

            contextMenuStrip.Items.Add("Restart", null, (s, e) => OnRestartClicked?.Invoke());

            contextMenuStrip.Items.Add("Exit", null, (s, e) => OnExitClicked?.Invoke());
            
            _notifyIcon.ContextMenuStrip = contextMenuStrip;
            _notifyIcon.ContextMenuStrip.Closed += (s, e) => _infoItem.Available = true;
            _notifyIcon.MouseClick += NotifyIcon_click;
        }

        private void ContextMenuStrip_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (!_closeContextMenu)
            {
                e.Cancel = true;
                _closeContextMenu = true;
            }
        }

        private void CancelContextMenuStripClosing(object sender, MouseEventArgs e) => _closeContextMenu = false;

        private void InvalidateLogsDropDown(object sender, EventArgs e)
        {
            _clientSettings.Update();
            _verboseLogsItem.Available = File.Exists(LogFilesPath(LogLevel.Verbose)[0]);
            _debugLogsItem.Available = _clientSettings.DeveloperMode && File.Exists(LogFilesPath(LogLevel.Debug)[0]);
            _warningLogsItem.Available = File.Exists(LogFilesPath(LogLevel.Warning)[0]);
            _errorLogsItem.Available = File.Exists(LogFilesPath(LogLevel.Error)[0]);
            
            //hide the strip separator if there isn't any log file
            _logsToolStripSeparator.Available = _verboseLogsItem.Available ||
                                                _debugLogsItem.Available   ||
                                                _warningLogsItem.Available ||
                                                _errorLogsItem.Available;
        }
        
        private void LogsDropDown_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (!_closeLogsDropdown)
            {
                e.Cancel = true;
                _closeLogsDropdown = true;
            }
        }

        private void CancelLogsDropdownClosing(object sender, MouseEventArgs e) => _closeLogsDropdown = false;
        
        private void OpenLogs(LogLevel level)
        {
            var path = LogFilesPath(level)[0];
            if (File.Exists(path))
            {
                _closeLogsDropdown = true;
                _notifyIcon.ContextMenuStrip.Close();
                Start(path);
            }
        }

        private void NotifyIcon_click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _infoItem.Available = false; //visible only on right click
                //http://stackoverflow.com/questions/2208690/invoke-notifyicons-context-menu
                MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
                mi.Invoke(_notifyIcon, null);
            }
        }

        public void SetSettingsItem(System.Drawing.Image image, EventHandler onClick)
        {
            _settingsItem.Image = image;
            _settingsItem.Click += onClick;
        }

        public void SetSettingsItemVisible(bool visible) => _settingsItem.Available = visible;

        public void SetTemperatureItem(System.Drawing.Image image, EventHandler onClick)
        {
            _temperatureItem.Image = image;
            _temperatureItem.Click += onClick;
        }

        public void SetTemperatureItemVisible(bool visible) => _temperatureItem.Available = visible;

        public bool ShowNotification(string message, ToolTipIcon icon = ToolTipIcon.None, bool forceShow = false)
        {
            if (IsShowingNotification && !forceShow) return false;
            
            IsShowingNotification = true;
            _notifyIcon.ShowBalloonTip(NOTIFICATION_TIMEOUT, APPLICATION_NAME, message, icon);
            return true;
        }

        private void InvalidateMonitorsIcon()
        {
            var monitors = _monitorsItem.DropDown.Items;
            lock (monitors)
            {
                bool show = false;
                for (int i = 0; i < monitors.Count && !show; i++)
                    if (monitors[i].Available)
                        show = true;

                _monitorsItem.Available = show; //show monitors dropdown if there is at least one monitor icon
            }
        }

        public void Dispose()
        {
            _recorder?.Save();
            _recorder?.Close();
            _infoItem.Dispose();
            _settingsItem.Dispose();
            _temperatureItem.Dispose();
            _notifyIcon.Dispose();
        }

        private class HardwareValuesRecorderMenuController : ITemperatureObserver
        {
            private const string _TEMPERATURE_TEXT = "Temperature";

            public bool IsRecording { get; private set; } = true;

            private Dictionary<HardwareMonitorType, ICollection<Tuple<float, DateTime>>> _hardwareValuesMap;
            private Dictionary<HardwareMonitorType, ToolStripLabel> _hardwareLabelsMap;

            ContextMenuController _cmc;

            public HardwareValuesRecorderMenuController(ContextMenuController cmc, HardwareMonitorType hwmTypes)
            {
                _cmc = cmc;
                _hardwareValuesMap = new Dictionary<HardwareMonitorType, ICollection<Tuple<float, DateTime>>>();
                _hardwareLabelsMap = new Dictionary<HardwareMonitorType, ToolStripLabel>();
                
                if (hwmTypes.HasFlag(HardwareMonitorType.Temperature))
                {
                    var label = new ToolStripLabel(_TEMPERATURE_TEXT);
                    label.Name = _TEMPERATURE_TEXT;
                    _hardwareValuesMap.Add(HardwareMonitorType.Temperature, new LinkedList<Tuple<float, DateTime>>());
                    _hardwareLabelsMap.Add(HardwareMonitorType.Temperature, label);
                    _cmc._logsItem.DropDown.Items.Insert(0, label);
                }
            }

            void ITemperatureObserver.OnAvgCPUsTemperatureChanged(float temperature)
            {
                if (!IsRecording) return;

                ICollection<Tuple<float, DateTime>> collection;
                if (_hardwareValuesMap.TryGetValue(HardwareMonitorType.Temperature, out collection))
                {
                    collection.Add(new Tuple<float, DateTime>(temperature, DateTime.Now));

                    var index = _hardwareLabelsMap[HardwareMonitorType.Temperature].GetDropDownIndex();
                    //thread-safe set text
                    _cmc._notifyIcon.ContextMenuStrip.Invoke(new Action(() =>
                        _cmc._logsItem.DropDown.Items[index].Text = $"{_TEMPERATURE_TEXT} ({collection.Count})"));
                }
            }

            public void Close()
            {
                _hardwareValuesMap.Clear();
                _hardwareLabelsMap.Clear();
                _cmc._logsItem.DropDown.Items.RemoveByKey(_TEMPERATURE_TEXT);
            }

            public void Save()
            {
                string currentFolderName = null;
                ICollection<Tuple<float, DateTime>> collection;
                lock (_hardwareValuesMap)
                {
                    if (_hardwareValuesMap.TryGetValue(HardwareMonitorType.Temperature, out collection))
                        HardwareRecordsManager.Save(HardwareMonitorType.Temperature, collection, out currentFolderName);
                }

                if (currentFolderName != null)
                {
                    _cmc._notifyIcon.ContextMenuStrip.Close();
                    Start(currentFolderName);
                }
            }

            public void StopRecording() => IsRecording = false;
        }
    }

    static class ContextMenuUtils
    {
        public static int GetDropDownIndex(this ToolStripItem item) => (item.OwnerItem as ToolStripMenuItem).DropDownItems.IndexOf(item);
    }
}
