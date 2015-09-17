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
using HardwareMonitor.Client.Controller.Utils;
using System.Drawing;
using System.Text;

namespace HardwareMonitor.Client.Controller.Controllers
{
    public class ContextMenuController : IDisposable
    {
        public const int NOTIFICATION_TIMEOUT = 10000;
        
        public const string APPLICATION_NAME = "Hardware Monitor Client";

        public const string INFO_ICON_NAME = "Info",
                            SETTINGS_ICON_NAME = "Settings",
                            LOGS_ICON_NAME = "Logs",
                            VERBOSE_LOGS_ICON_NAME = "Verbose",
                            DEBUG_LOGS_ICON_NAME = "Debug",
                            WARNING_LOGS_ICON_NAME = "Warning",
                            ERROR_LOGS_ICON_NAME = "Error",
                            RECORD_ICON_NAME = "Record",
                            START_RECORDING_ACTION_ICON_NAME = "Start recording",
                            STOP_RECORDING_ACTION_ICON_NAME = "Stop and save",
                            MONITORS_ICON_NAME = "Monitors",
                            TEMPERATURE_ICON_NAME = "Temperature";

        //private ICollection<ToolStripMenuItem> _recordableHardwareMonitorTypesItems = new List<ToolStripMenuItem>();

        public bool IsShowingNotification { get; private set; }

        private NotifyIcon _notifyIcon;
        private ToolStripMenuItem _monitorsItem, _logsItem, _recordsItem;
        private ToolStripItem _toggleRecordingAction, _selectAllAction, _unselectAllAction,
                              _verboseLogsItem, _debugLogsItem, _warningLogsItem, _errorLogsItem,
                              _infoItem, _temperatureItem, _settingsItem;

        private bool _closeContextMenu = true, _closeLogsDropdown = true, _closeRecordsDropdown = true;

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

            #region Records
            _recordsItem = contextMenuStrip.Items.Add(RECORD_ICON_NAME, start_record_icon) as ToolStripMenuItem;
            _recordsItem.DropDown.Closing += RecordsDropDown_Closing;

            var recordable = _recordsItem.DropDown.Items;

            foreach (var hwmType in Enum.GetValues(typeof(HardwareMonitorType)))
            {
                var item = new ToolStripMenuItem()
                {
                    Text = hwmType.ToString(),
                    CheckOnClick = true,
                    Tag = hwmType
                };
                item.MouseDown += CancelRecordsDropdownClosing;
                recordable.Add(item);
            }

            recordable.Add(new ToolStripSeparator());
            _selectAllAction = recordable.Add("Select All", null, (s, e) => ToggleHardwareMonitorTypesSelection(true));
            _selectAllAction.MouseDown += CancelRecordsDropdownClosing;
            _unselectAllAction = recordable.Add("Unselect All", null, (s, e) => ToggleHardwareMonitorTypesSelection(false));
            _unselectAllAction.MouseDown += CancelRecordsDropdownClosing;

            _toggleRecordingAction = recordable.Add(START_RECORDING_ACTION_ICON_NAME, start_record_icon, (s, e) =>
            {
                var recordables = _recordsItem.DropDown.Items;

                if (IsRecording)
                {
                    if (DialogResult.Yes == MessageBox.Show("Do you want to stop recording and save the values in a log file?",
                        APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        _recorder.Save();
                        _recorder.Close(true);

                        _controller.RemoveObserver(_recorder);
                        _recorder = null;

                        //re-enables items check and restores names
                        for (int i = 0; i < recordables.Count - 4 /*excludes separator and functionalities*/; i++)
                        {
                            var menuItem = (recordables[i] as ToolStripMenuItem);
                            menuItem.Text = menuItem.Tag.ToString();
                            menuItem.CheckOnClick = true;
                        }

                        _selectAllAction.Enabled = true;
                        _unselectAllAction.Enabled = true;
                        
                        _toggleRecordingAction.Text = START_RECORDING_ACTION_ICON_NAME;
                        _toggleRecordingAction.Image = start_record_icon;

                        IsRecording = false;
                    }
                }
                else
                {
                    List<HardwareMonitorType> selectedTypes = new List<HardwareMonitorType>();
                    HardwareMonitorType hwmTypesToRecord;
                    
                    for (int i = 0; i < recordables.Count - 4 /*excludes separator and functionalities*/; i++)
                        if ((recordables[i] as ToolStripMenuItem).Checked)
                            selectedTypes.Add((HardwareMonitorType)recordables[i].Tag);

                    if (selectedTypes.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        hwmTypesToRecord = selectedTypes[0];
                        foreach (var type in selectedTypes)
                        {
                            hwmTypesToRecord |= type;
                            sb.AppendLine(type.ToString());
                        }

                        if (DialogResult.Yes == MessageBox.Show($"Start recording the following hardware sensors?\n{sb.ToString()}",
                            APPLICATION_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        {
                            //disables items check
                            for (int i = 0; i < recordables.Count - 4 /*excludes separator and functionalities*/; i++)
                            {
                                var menuItem = (recordables[i] as ToolStripMenuItem);
                                menuItem.Checked = false;
                                menuItem.CheckOnClick = false;
                            }

                            _selectAllAction.Enabled = false;
                            _unselectAllAction.Enabled = false;

                            StartRecording(hwmTypesToRecord);
                        }
                    }
                }
            });
            _toggleRecordingAction.MouseDown += CancelRecordsDropdownClosing;
            #endregion

            #region Logs
            _logsItem = contextMenuStrip.Items.Add(LOGS_ICON_NAME, null) as ToolStripMenuItem;
            contextMenuStrip.Opening += InvalidateLogsDropDown;
            _logsItem.MouseDown += (s, e) =>
            {
                if (!_logsItem.AllowDrop)
                    CancelContextMenuStripClosing(s, e);
            };
            _logsItem.DropDown.Closing += LogsDropDown_Closing;

            var logs = _logsItem.DropDown.Items;

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
            _notifyIcon.ContextMenuStrip.Closed += (s, e) =>
            {
                _infoItem.Available = true;
                if (_settingsItem != null) _settingsItem.Available = true;
            };
            _notifyIcon.MouseClick += NotifyIcon_click;
        }

        private void ToggleHardwareMonitorTypesSelection(bool ch)
        {
            var recordables = _recordsItem.DropDown.Items;
            for (int i = 0; i < recordables.Count - 4 /*excludes separator and functionalities*/; i++)
                (recordables[i] as ToolStripMenuItem).Checked = ch;
        }

        public void StartRecording(HardwareMonitorType hwmTypes)
        {
            _recorder = new HardwareValuesRecorderMenuController(this, hwmTypes);
            _controller.AddObserver(_recorder);
            
            _toggleRecordingAction.Text = STOP_RECORDING_ACTION_ICON_NAME;
            _toggleRecordingAction.Image = stop_record_icon;

            IsRecording = true;
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
        }
        
        private void LogsDropDown_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (!_closeLogsDropdown)
            {
                e.Cancel = true;
                _closeLogsDropdown = true;
            }
        }

        private void RecordsDropDown_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (!_closeRecordsDropdown)
            {
                e.Cancel = true;
                _closeRecordsDropdown = true;
            }
        }

        private void CancelLogsDropdownClosing(object sender, MouseEventArgs e) => _closeLogsDropdown = false;

        private void CancelRecordsDropdownClosing(object sender, MouseEventArgs e) => _closeRecordsDropdown = false;

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
                //visible only on right click
                _infoItem.Available = false;
                if (_settingsItem != null) _settingsItem.Available = false;
                //http://stackoverflow.com/questions/2208690/invoke-notifyicons-context-menu
                MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
                mi.Invoke(_notifyIcon, null);
            }
        }

        public void SetSettingsItem(System.Drawing.Image image, EventHandler onClick) => _settingsItem.Apply(image, onClick);

        public void SetSettingsItemVisible(bool visible) => _settingsItem.Available = visible;

        public void SetTemperatureItem(Image image, EventHandler onClick) => _temperatureItem.Apply(image, onClick);

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

            private string _currentRecordFolderName;

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
                        _cmc._recordsItem.DropDown.Items[index].Text = $"{_TEMPERATURE_TEXT} ({collection.Count})"));
                }
            }

            public void Close(bool openRecordFolder = false)
            {
                _hardwareValuesMap.Clear();
                _hardwareLabelsMap.Clear();
                _cmc._logsItem.DropDown.Items.RemoveByKey(_TEMPERATURE_TEXT);

                if (openRecordFolder && _currentRecordFolderName != null)
                {
                    _cmc._notifyIcon.ContextMenuStrip.Close();
                    Start(_currentRecordFolderName);
                }
            }

            public void Save()
            {
                ICollection<Tuple<float, DateTime>> collection;
                lock (_hardwareValuesMap)
                {
                    if (_hardwareValuesMap.TryGetValue(HardwareMonitorType.Temperature, out collection))
                        HardwareRecordsManager.Save(HardwareMonitorType.Temperature, collection, out _currentRecordFolderName);
                }
            }

            public void StopRecording() => IsRecording = false;
        }
    }

    static class ContextMenuUtils
    {
        public static void Apply(this ToolStripItem item, Image image, EventHandler onClick)
        {
            item.Image = image;
            item.Click += onClick;
        }

        public static int GetDropDownIndex(this ToolStripItem item) => (item.OwnerItem as ToolStripMenuItem).DropDownItems.IndexOf(item);
    }
}
