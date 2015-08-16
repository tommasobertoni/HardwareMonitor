using HardwareMonitor.Client.Controller.Utils;
using System;
using System.Windows.Forms;

namespace HardwareMonitor.Client.Controller
{
    public partial class SettingsForm : Form
    {
        private ClientSettingsHandler _settings;

        public SettingsForm()
        {
            InitializeComponent();
            _settings = new ClientSettingsHandler();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            cbStartupRun.Checked = _settings.RunAtStartup;
            cbStartupNotification.Checked = _settings.StartupNotification;
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            _settings.RunAtStartup = cbStartupRun.Checked;
            _settings.StartupNotification = cbStartupNotification.Checked;
        }
    }
}
