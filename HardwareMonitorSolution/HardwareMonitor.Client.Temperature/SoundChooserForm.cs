using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HardwareMonitor.Client.Temperature
{
    public partial class SoundChooserForm : Form
    {
        public SoundChooserForm()
        {
            InitializeComponent();
            CenterToParent();
        }

        private void SoundChooserForm_Load_1(object sender, EventArgs e)
        {
            string elements = "";

            System.Resources.ResourceSet resourceSet = Properties.Resources.ResourceManager.GetResourceSet(
                System.Globalization.CultureInfo.CurrentUICulture, true, true);
            foreach (System.Collections.DictionaryEntry entry in resourceSet)
            {
                if (entry.Value is System.IO.Stream)
                    elements += $"{entry.Key},";
            }

            elements = elements.Substring(0, elements.Length - 1);
            radioListBox.Items.AddRange(elements.Split(new char[] { ',' }));
            radioListBox.SetSelected(0, true);
        }
    }
}
