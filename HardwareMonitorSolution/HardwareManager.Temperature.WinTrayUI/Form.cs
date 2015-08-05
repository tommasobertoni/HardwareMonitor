using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HardwareManager.Temperature.WinTrayUI
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();

            #region Init Thermometer Picture Box
            thermometerPictureBox1.MarginLeft = 20;
            thermometerPictureBox1.MarginRight = 20;
            thermometerPictureBox1.MarginTop = 5;
            thermometerPictureBox1.MarginBottom = 35;
            thermometerPictureBox1.Percentage = 0;
            #endregion

            #region Init Screens

            #endregion
        }
    }
}
