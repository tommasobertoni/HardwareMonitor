using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace HardwareMonitor.Client.Temperature.CustomControls
{
    /// <summary>
    /// Original code from: http://www.codeproject.com/Articles/18526/RadioListBox-A-ListBox-with-Radio-Buttons-Winforms
    /// </summary>
    public class RadioListBox : ListBox
    {
        private StringFormat Align;
        protected bool IsTransparent = false;
        private Brush BackBrush;

        // Allows the BackColor to be transparent
        public override Color BackColor
        {
            get
            {
                if (IsTransparent)
                    return Color.Transparent;
                else
                    return base.BackColor;
            }
            set
            {
                if (value == Color.Transparent)
                {
                    IsTransparent = true;
                    base.BackColor = (this.Parent == null) ? SystemColors.Window : this.Parent.BackColor;
                }
                else
                {
                    IsTransparent = false;
                    base.BackColor = value;
                }

                if (this.BackBrush != null)
                    this.BackBrush.Dispose();
                BackBrush = new SolidBrush(base.BackColor);

                Invalidate();
            }
        }

        // Hides these properties in the designer
        [Browsable(false)]
        public override DrawMode DrawMode
        {
            get
            {
                return base.DrawMode;
            }
            set
            {
                if (value != DrawMode.OwnerDrawFixed)
                    throw new Exception("Invalid value for DrawMode property");
                else
                    base.DrawMode = value;
            }
        }
        [Browsable(false)]
        public override SelectionMode SelectionMode
        {
            get
            {
                return base.SelectionMode;
            }
            set
            {
                if (value != SelectionMode.One)
                    throw new Exception("Invalid value for SelectionMode property");
                else
                    base.SelectionMode = value;
            }
        }

        // Public constructor
        public RadioListBox()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.SelectionMode = SelectionMode.One;
            this.ItemHeight = this.FontHeight;
            
            this.Align = new StringFormat(StringFormat.GenericDefault);
            this.Align.LineAlignment = StringAlignment.Center;
        }

        // Main paiting method
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            int maxItem = this.Items.Count - 1;

            if (e.Index < 0 || e.Index > maxItem)
            {
                // Erase all background if control has no items
                e.Graphics.FillRectangle(BackBrush, this.ClientRectangle);
                return;
            }

            int size = e.Font.Height; // button size depends on font height, not on item height

            // Calculate bounds for background, if last item paint up to bottom of control
            Rectangle backRect = e.Bounds;
            if (e.Index == maxItem)
                backRect.Height = this.ClientRectangle.Top + this.ClientRectangle.Height - e.Bounds.Top;

            // if selected, mark the background differently
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(Brushes.Lime, e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(BackBrush, backRect);
            }

            // Determines text color/brush
            Brush textBrush;
            bool isChecked = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            RadioButtonState state = isChecked ? RadioButtonState.CheckedNormal : RadioButtonState.UncheckedNormal;
            if ((e.State & DrawItemState.Disabled) == DrawItemState.Disabled)
            {
                textBrush = SystemBrushes.GrayText;
                state = isChecked ? RadioButtonState.CheckedDisabled : RadioButtonState.UncheckedDisabled;
            }
            else if ((e.State & DrawItemState.Grayed) == DrawItemState.Grayed)
            {
                textBrush = SystemBrushes.GrayText;
                state = isChecked ? RadioButtonState.CheckedDisabled : RadioButtonState.UncheckedDisabled;
            }
            else
            {
                textBrush = SystemBrushes.FromSystemColor(this.ForeColor);
            }
            
            // Determines bounds for text and radio button
            Size glyphSize = RadioButtonRenderer.GetGlyphSize(e.Graphics, state);
            Point glyphLocation = e.Bounds.Location;
            glyphLocation.Y += (e.Bounds.Height - glyphSize.Height) / 2;
            var rightPadding = 5;
            Rectangle bounds = new Rectangle(e.Bounds.X + glyphSize.Width + rightPadding, e.Bounds.Y, e.Bounds.Width - glyphSize.Width - rightPadding, e.Bounds.Height);

            // Draws the radio button
            RadioButtonRenderer.DrawRadioButton(e.Graphics, glyphLocation, state);
            
            // Draws the text
            if (!string.IsNullOrEmpty(DisplayMember)) // Bound Datatable? Then show the column written in Displaymember
                e.Graphics.DrawString(((System.Data.DataRowView)this.Items[e.Index])[this.DisplayMember].ToString(),
                    e.Font, textBrush, bounds, this.Align);
            else
                e.Graphics.DrawString(this.Items[e.Index].ToString(), e.Font, textBrush, bounds, this.Align);

            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }

        // Prevent background erasing
        protected override void DefWndProc(ref Message m)
        {
            if (m.Msg == 0x0014)  // WM_ERASEBKGND
            {
                m.Result = (IntPtr)1; // avoid default background erasing
                return;
            }

            base.DefWndProc(ref m);
        }

        // Other event handlers
        protected override void OnHandleCreated(EventArgs e)
        {
            if (this.FontHeight > this.ItemHeight)
                this.ItemHeight = this.FontHeight;

            base.OnHandleCreated(e);
        }
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);

            if (this.FontHeight > this.ItemHeight)
                this.ItemHeight = this.FontHeight;
            Update();
        }
        protected override void OnParentChanged(EventArgs e)
        {
            // Force to change backcolor
            this.BackColor = this.BackColor;
        }
        protected override void OnParentBackColorChanged(EventArgs e)
        {
            // Force to change backcolor
            this.BackColor = this.BackColor;
        }
    }
}
