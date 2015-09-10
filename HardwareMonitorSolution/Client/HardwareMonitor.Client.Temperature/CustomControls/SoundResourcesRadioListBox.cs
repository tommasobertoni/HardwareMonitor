using HardwareMonitor.Client.Temperature.Utils;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HardwareMonitor.Client.Temperature.CustomControls
{
    public class SoundResourcesRadioListBox : RadioListBox
    {
        private Bitmap PlayButtonImage { get; set; }

        private List<ResourcePlayer> _resourcesList;

        public SoundResourcesRadioListBox() : base()
        {
            IsTransparent = true;
            BorderStyle = BorderStyle.None;
            PlayButtonImage = new Bitmap(Properties.Resources.play_button);
        }

        public void Load(List<ResourcePlayer> resourcesList)
        {
            _resourcesList = resourcesList;
            foreach (var resource in _resourcesList)
            {
                if (!resource.IsLoadCompleted) resource.Load();
                Items.Add(resource);
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (SelectedIndex > -1 && _resourcesList != null && SelectedIndex < _resourcesList.Count)
            {
                var rect = GetItemRectangle(SelectedIndex);
                if (rect.Contains(e.Location) && rect.GetPlayButtonRectangleFromItemRectangle().Contains(e.X, e.Y))
                    _resourcesList[SelectedIndex].Play();
            }

            base.OnMouseClick(e);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);

            //Draw play button
            e.Graphics.DrawImage(PlayButtonImage, e.Bounds.GetPlayButtonRectangleFromItemRectangle());
        }
    }

    static class Utils
    {
        private const float _MARGIN = 6;

        public static RectangleF GetPlayButtonRectangleFromItemRectangle(this Rectangle itemRectangle)
        {
            var w = itemRectangle.Height - _MARGIN;
            var h = itemRectangle.Height - _MARGIN * 2;
            var x = itemRectangle.Width - w - _MARGIN;
            var y = itemRectangle.Location.Y + _MARGIN;

            return new RectangleF(x, y, w, h);
        }
    }
}
