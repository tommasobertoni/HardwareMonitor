using System;
using System.Reflection;
using System.Windows.Forms;

namespace HardwareMonitor.Client.Controller.Utils
{
    public class ContextMenuController : IDisposable
    {
        public const int NOTIFICATION_TIMEOUT = 10000;

        public const string APPLICATION_NAME = "Hardware Monitor Client";
        public const string MONITORS_ICON_NAME = "Monitors";
        public const string INFO_ICON_NAME = "Info";
        public const string SETTINGS_ICON_NAME = "Settings";
        public const string TEMPERATURE_ICON_NAME = "Temperature";

        public bool IsShowingNotification { get; private set; }

        private NotifyIcon _notifyIcon;
        private ToolStripMenuItem _monitorsItem;
        private ToolStripItem _infoItem;
        private ToolStripItem _settingsItem;
        private ToolStripItem _temperatureItem;

        public delegate void NoParametersEventHandler();
        public event NoParametersEventHandler OnRestartClicked;
        public event NoParametersEventHandler OnExitClicked;

        public ContextMenuController()
        {
            _notifyIcon = new NotifyIcon()
            {
                Text = APPLICATION_NAME,
                Visible = true,
                Icon = UACUtils.IsUserAdministrator ? Properties.Resources.ohmuaclogo : Properties.Resources.ohmlogo
            };

            _notifyIcon.BalloonTipClosed += (s, e) => IsShowingNotification = false;
            _notifyIcon.BalloonTipClicked += (s, e) => IsShowingNotification = false;

            var trayMenuStrip = new ContextMenuStrip();

            _monitorsItem = trayMenuStrip.Items.Add(MONITORS_ICON_NAME) as ToolStripMenuItem;
            _monitorsItem.Name = MONITORS_ICON_NAME;
            _monitorsItem.Available = false;

            _temperatureItem = _monitorsItem.DropDown.Items.Add(TEMPERATURE_ICON_NAME);
            _temperatureItem.Available = false;
            _temperatureItem.VisibleChanged += (s, e) => InvalidateMonitorsIcon();

            _settingsItem = trayMenuStrip.Items.Add(SETTINGS_ICON_NAME);
            _settingsItem.Name = SETTINGS_ICON_NAME;
            _settingsItem.Available = false;

            trayMenuStrip.Items.Add(new ToolStripSeparator());

            _infoItem = trayMenuStrip.Items.Add("About", Properties.Resources.info_icon.ToBitmap(), (s, e) =>
            {
                MessageBox.Show(new Form() { TopMost = true /*pretty bad, I know*/ },
                    "HardwareMonitor windows service and winform developed by Tommaso Bertoni, 2015.\n\nBased on the OpenHardwareMonitorLib project.",
                    $"{APPLICATION_NAME} - {INFO_ICON_NAME}", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });

            trayMenuStrip.Items.Add("Restart", null, (s, e) => OnRestartClicked?.Invoke());

            trayMenuStrip.Items.Add("Exit", null, (s, e) => OnExitClicked?.Invoke());
            
            _notifyIcon.ContextMenuStrip = trayMenuStrip;
            _notifyIcon.ContextMenuStrip.Closed += (s, e) => _infoItem.Available = true;
            _notifyIcon.MouseClick += NotifyIcon_click;
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
            _infoItem.Dispose();
            _settingsItem.Dispose();
            _temperatureItem.Dispose();
            _notifyIcon.Dispose();
        }
    }
}
