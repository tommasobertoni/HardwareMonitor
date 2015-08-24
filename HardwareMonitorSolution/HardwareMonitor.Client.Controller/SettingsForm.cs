using HardwareMonitor.Client.Controller.Contracts;
using HardwareMonitor.Client.Controller.Utils;
using System;
using System.Windows.Forms;
using System.Drawing;

namespace HardwareMonitor.Client.Controller
{
    public partial class SettingsForm : Form, IClientSettingsOperations
    {
        private const string _START_SERVICES_TEXT = "Start broadcast services";
        private const string _STOP_SERVICES_TEXT = "Stop broadcast services";

        private ClientSettingsHandler _settings;
        
        public event EventHandler<bool> OnToggleBroadcastServices;
        public event EventHandler OnSavedSettings;

        private bool _areServicesRunning;

        public SettingsForm(bool canStartServices = false, bool areServicesRunning = false)
        {
            InitializeComponent();
            Icon = Properties.Resources.settings_icon;

            _areServicesRunning = areServicesRunning;
            _settings = new ClientSettingsHandler();

            cbStartupRun.Checked = _settings.RunAtStartup;
            cbStartupNotification.Checked = _settings.StartupNotification;
            cbAdminRights.Checked = _settings.StartProgramAsAdmin;
            cbStartupServices.Checked = _settings.StartupBroadcastServices;
            
            btnStartBroadcastServices.Enabled = canStartServices;
            ServicesRunningLayout();
        }
        
        private void ServicesRunningLayout()
        {
            if (_areServicesRunning)
            {
                btnStartBroadcastServices.Text = _STOP_SERVICES_TEXT;
                btnStartBroadcastServices.BackColor = Color.Orange;
            }
            else
            {
                btnStartBroadcastServices.Text = _START_SERVICES_TEXT;
                btnStartBroadcastServices.UseVisualStyleBackColor = true;
            }
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            _settings.RunAtStartup = cbStartupRun.Checked;
            _settings.StartupNotification = cbStartupNotification.Checked;
            _settings.StartProgramAsAdmin = cbAdminRights.Checked;
            _settings.StartupBroadcastServices = cbStartupServices.Checked;
            OnSavedSettings?.Invoke(this, null);
        }

        private void ChangeLabelAdminRightsInfoVisibility()
        {
            var uacRequired = cbAdminRights.Checked || cbStartupServices.Checked;
            labelAdminRightsInfo.Visible = uacRequired;
            if (uacRequired)
            {
                btnSaveSettings.Image = Properties.Resources.uac_icon;
            }
            else btnSaveSettings.Image = null;
        }

        private void cbAdminRights_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLabelAdminRightsInfoVisibility();
        }

        private void cbStartupServices_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLabelAdminRightsInfoVisibility();
        }

        private void btnToggleBroadcastServices_Click(object sender, EventArgs e)
        {
            _areServicesRunning = !_areServicesRunning;
            ServicesRunningLayout();
            OnToggleBroadcastServices?.Invoke(this, _areServicesRunning);
        }
    }
}
