using HardwareMonitor.Client.Domain.Contracts;
using HardwareMonitor.Client.Temperature.Utils;
using System;
using System.Windows.Forms;
using HardwareMonitor.Client.Domain.Entities;
using System.Drawing;

namespace HardwareMonitor.Client.Temperature
{
    public partial class SoundChooserForm : Form, IView
    {
        Image IView.Icon { get; } = null;

        public SoundChooserForm()
        {
            InitializeComponent();
            CenterToScreen();
        }

        public event EventHandler<string> OnNotification;
        public event EventHandler<string> OnLog;
        public event EventHandler OnViewExit;
        public event EventHandler OnRequestUpdate;

        private void SoundChooserForm_Load_1(object sender, EventArgs e)
        {
            soundResourcesRadioListBox.Load(SoundResourcesManager.Instance.GetResourcesList());
            if (SoundResourcesManager.Instance.SelectedSound != null)
            {
                var index = SoundResourcesManager.Instance.GetResourcesList().IndexOf(SoundResourcesManager.Instance.SelectedSound);
                if (index >= 0) soundResourcesRadioListBox.SetSelected(index, true);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var resource = soundResourcesRadioListBox.SelectedItem as ResourcePlayer;
            resource.Stop();
            SoundResourcesManager.Instance.SelectedSound = resource;
        }

        public void Show(bool resetPosition = false)
        {
            if (resetPosition) CenterToScreen();
            Show();
        }

        void IView.ForceTheme(Theme theme)
        {
            Color backgroundColor, foregroundColor;
            Brush selectionBrush;

            switch (theme)
            {
                case Theme.Dark:
                    backgroundColor = SystemColors.ControlText;
                    foregroundColor = SystemColors.Control;
                    selectionBrush = Brushes.DarkGreen;
                    break;

                default:
                    backgroundColor = SystemColors.Control;
                    foregroundColor = SystemColors.ControlText;
                    selectionBrush = Brushes.Lime;
                    break;
            }

            BackColor = backgroundColor;
            btnSave.ForeColor = foregroundColor;
            btnSave.BackColor = backgroundColor;
            soundResourcesRadioListBox.BackColor = backgroundColor;
            soundResourcesRadioListBox.ForeColor = foregroundColor;
            soundResourcesRadioListBox.SelectionBrush = selectionBrush;
        }
    }
}
