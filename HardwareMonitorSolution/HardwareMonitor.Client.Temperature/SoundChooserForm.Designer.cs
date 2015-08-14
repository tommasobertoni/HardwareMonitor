namespace HardwareMonitor.Client.Temperature
{
    partial class SoundChooserForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.radioListBox = new HardwareMonitor.Client.Temperature.CustomControls.RadioListBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(52, 427);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(249, 48);
            this.button1.TabIndex = 2;
            this.button1.Text = "Confirm";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // radioListBox
            // 
            this.radioListBox.BackColor = System.Drawing.Color.Transparent;
            this.radioListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.radioListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.radioListBox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioListBox.FormattingEnabled = true;
            this.radioListBox.HorizontalScrollbar = true;
            this.radioListBox.ItemHeight = 36;
            this.radioListBox.Location = new System.Drawing.Point(12, 12);
            this.radioListBox.Name = "radioListBox";
            this.radioListBox.Size = new System.Drawing.Size(326, 396);
            this.radioListBox.TabIndex = 1;
            // 
            // SoundChooserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 494);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.radioListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SoundChooserForm";
            this.Text = "SoundChooserForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SoundChooserForm_Load_1);
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControls.RadioListBox radioListBox;
        private System.Windows.Forms.Button button1;
    }
}