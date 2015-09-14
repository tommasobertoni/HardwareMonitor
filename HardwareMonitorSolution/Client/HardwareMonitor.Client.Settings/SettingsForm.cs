using System;
using System.Windows.Forms;
using HardwareMonitor.Client.Domain.Contracts;
using HardwareMonitor.Client.Domain.Entities;
using System.Drawing;
using HardwareMonitor.Client.Settings.Utils;

namespace HardwareMonitor.Client.Settings
{
    public partial class SettingsForm : Form, IClientSettingsUI
    {
        private ClientSettingsHandler _settings;

        string IView.Name
        {
            get
            {
                return Text;
            }

            set
            {
                Text = value;
            }
        }

        Image IView.Icon { get; } = Properties.Resources.settings_icon.ToBitmap();

        public event EventHandler OnSavedSettings;
        public event EventHandler<Theme> OnForceTheme;
        public event EventHandler<string> OnNotification;
        public event EventHandler OnViewExit;
        public event EventHandler OnRequestUpdate;

        public SettingsForm()
        {
            InitializeComponent();

            Icon = Properties.Resources.settings_icon;
            _settings = new ClientSettingsHandler();

            cbStartupRun.Checked = _settings.RunAtStartup;
            cbStartupNotification.Checked = _settings.StartupNotification;
            cbAdminRights.Checked = _settings.StartProgramAsAdmin;

            FormClosing += (s, e) =>
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    Hide();
                    e.Cancel = true;
                }
            };

            ApplyTheme();
        }
        
        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            _settings.RunAtStartup = cbStartupRun.Checked;
            _settings.StartupNotification = cbStartupNotification.Checked;
            _settings.StartProgramAsAdmin = cbAdminRights.Checked;
            Hide();
            OnSavedSettings?.Invoke(this, null);
        }

        private void ChangeLabelAdminRightsInfoVisibility()
        {
            labelAdminRightsInfo.Visible = cbAdminRights.Checked;
            if (cbAdminRights.Checked)
            {
                btnSaveSettings.Image = Properties.Resources.uac_image;
            }
            else btnSaveSettings.Image = null;
        }

        void IView.Show(bool resetPosition)
        {
            if (resetPosition) CenterToScreen(); //ensure that the form is in the center of the screen
            Show();
        }

        void IView.ForceTheme(Theme theme)
        {
            if (_settings.Theme != theme)
            {
                _settings.Theme = theme;
                ApplyTheme();
            }
        }

        private void ApplyTheme()
        {
            Color backgroundColor, foregroundColor;
            switch (_settings.Theme)
            {
                case Theme.Dark:
                    backgroundColor = SystemColors.ControlText;
                    foregroundColor = SystemColors.Control;
                    break;

                default:
                    backgroundColor = SystemColors.Control;
                    foregroundColor = SystemColors.ControlText;
                    break;
            }

            BackColor = backgroundColor;
            labelApplicationTitle.ForeColor = foregroundColor;
            cbStartupRun.ForeColor = foregroundColor;
            cbStartupNotification.ForeColor = foregroundColor;
            labelPrivilegesTitle.ForeColor = foregroundColor;
            cbAdminRights.ForeColor = foregroundColor;
            labelThemeTitle.ForeColor = foregroundColor;
            labelAdminRightsInfo.ForeColor = foregroundColor;
            btnSaveSettings.ForeColor = foregroundColor;
            btnSaveSettings.BackColor = backgroundColor;
        }

        void IView.Close()
        {
            Cursor.Current = Cursors.Default;
            Dispose();
            Close();
        }

        private void cbAdminRights_CheckedChanged(object sender, EventArgs e)
        {
            ChangeLabelAdminRightsInfoVisibility();
        }

        private void ForceTheme(Theme theme)
        {
            Cursor.Current = Cursors.WaitCursor;
            OnForceTheme?.Invoke(this, theme);
            Cursor.Current = Cursors.Default;
        }

        private void btnThemeLight_Click(object sender, EventArgs e)
        {
            ForceTheme(Theme.Light);
        }

        private void btnThemeDark_Click(object sender, EventArgs e)
        {
            ForceTheme(Theme.Dark);
        }
    }
}
