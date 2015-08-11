using System.Drawing;
using System.Windows.Forms;

namespace HardwareMonitor.Client.Temperature
{
    public class ThermometerPictureBox : PictureBox
    {
        private Pen _pen;

        public int MarginLeft { get; set; }

        public int MarginTop { get; set; }

        public int MarginRight { get; set; }

        public int MarginBottom { get; set; }

        private int _percentage = 100;

        public int Value
        {
            get
            {
                return _percentage;
            }

            set
            {
                _percentage = value;
                if (_percentage > 100)
                    _percentage = 100;
                else if (_percentage < 0)
                    _percentage = 0;

                Invalidate();
            }
        }

        public ThermometerPictureBox() : base()
        {
            _pen = new Pen(Color.Red);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            int rectWidth = Width - MarginLeft - MarginRight;
            if (rectWidth < 0) rectWidth = 0;

            int availiableHeight = Height - MarginTop - MarginBottom;

            int rectHeight = availiableHeight * Value / 100;
            if (rectHeight < 0) rectHeight = 0;

            int verticalGap = availiableHeight - rectHeight;

            pe.Graphics.FillRectangle(_pen.Brush, MarginLeft, MarginTop + verticalGap, rectWidth, rectHeight);

            base.OnPaint(pe);
        }
    }
}
