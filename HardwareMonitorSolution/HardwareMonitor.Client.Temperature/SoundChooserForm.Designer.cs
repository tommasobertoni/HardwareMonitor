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
            this.soundResourcesRadioListBox1 = new HardwareMonitor.Client.Temperature.CustomControls.SoundResourcesRadioListBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(52, 422);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(249, 48);
            this.button1.TabIndex = 2;
            this.button1.Text = "Confirm";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // soundResourcesRadioListBox1
            // 
            this.soundResourcesRadioListBox1.BackColor = System.Drawing.Color.Transparent;
            this.soundResourcesRadioListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.soundResourcesRadioListBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.soundResourcesRadioListBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.soundResourcesRadioListBox1.FormattingEnabled = true;
            this.soundResourcesRadioListBox1.ItemHeight = 36;
            this.soundResourcesRadioListBox1.Location = new System.Drawing.Point(12, 12);
            this.soundResourcesRadioListBox1.Name = "soundResourcesRadioListBox1";
            this.soundResourcesRadioListBox1.Size = new System.Drawing.Size(326, 360);
            this.soundResourcesRadioListBox1.TabIndex = 3;
            // 
            // SoundChooserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 487);
            this.Controls.Add(this.soundResourcesRadioListBox1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SoundChooserForm";
            this.ShowIcon = false;
            this.Text = "Sound Chooser";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SoundChooserForm_Load_1);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private CustomControls.SoundResourcesRadioListBox soundResourcesRadioListBox1;
    }
}