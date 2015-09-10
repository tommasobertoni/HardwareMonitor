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
            this.btnSave = new System.Windows.Forms.Button();
            this.soundResourcesRadioListBox = new HardwareMonitor.Client.Temperature.CustomControls.SoundResourcesRadioListBox();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(52, 422);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(249, 48);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Confirm";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // soundResourcesRadioListBox
            // 
            this.soundResourcesRadioListBox.BackColor = System.Drawing.Color.Transparent;
            this.soundResourcesRadioListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.soundResourcesRadioListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.soundResourcesRadioListBox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.soundResourcesRadioListBox.FormattingEnabled = true;
            this.soundResourcesRadioListBox.ItemHeight = 36;
            this.soundResourcesRadioListBox.Location = new System.Drawing.Point(12, 12);
            this.soundResourcesRadioListBox.Name = "soundResourcesRadioListBox";
            this.soundResourcesRadioListBox.Size = new System.Drawing.Size(326, 360);
            this.soundResourcesRadioListBox.TabIndex = 3;
            // 
            // SoundChooserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 487);
            this.Controls.Add(this.soundResourcesRadioListBox);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SoundChooserForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Sound Chooser";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SoundChooserForm_Load_1);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSave;
        private CustomControls.SoundResourcesRadioListBox soundResourcesRadioListBox;
    }
}