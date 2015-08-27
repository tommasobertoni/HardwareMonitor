using HardwareMonitor.Client.Controller.Utils;
using System;
using System.Windows.Forms;
using HardwareMonitor.Client.Domain.Contracts;

namespace HardwareMonitor.Client.Controller
{
    public partial class SettingsForm : Form, IClientSettingsOperations
    {
        private const string _START_SERVICES_TEXT = "Start broadcast services";
        private const string _STOP_SERVICES_TEXT = "Stop broadcast services";

        private ClientSettingsHandler _settings;
        
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
        }
        
        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            _settings.RunAtStartup = cbStartupRun.Checked;
            _settings.StartupNotification = cbStartupNotification.Checked;
            _settings.StartProgramAsAdmin = cbAdminRights.Checked;
            OnSavedSettings?.Invoke(this, null);
        }

        private void ChangeLabelAdminRightsInfoVisibility()
        {
            labelAdminRightsInfo.Visible = cbAdminRights.Checked;
            if (cbAdminRights.Checked)
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
    }
}
