namespace TemperatureAlertSystem.TrayUI
{
    partial class UIForm
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
            /*if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);*/
            //Visible = false;
            Hide();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            this.labelLastMeasuredTemperatureDesc = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBarTemperature = new System.Windows.Forms.TrackBar();
            this.labelTemperature = new System.Windows.Forms.Label();
            this.labelTimeout = new System.Windows.Forms.Label();
            this.trackBarTimeout = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.rbMessageBoxNotif = new System.Windows.Forms.RadioButton();
            this.rbTrayNotif = new System.Windows.Forms.RadioButton();
            this.rbNoNotif = new System.Windows.Forms.RadioButton();
            this.labelLastMeasuredTemperature = new System.Windows.Forms.Label();
            this.thermometerPictureBox = new TemperatureAlertSystem.TrayUI.ThermometerPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTemperature)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thermometerPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // labelLastMeasuredTemperatureDesc
            // 
            this.labelLastMeasuredTemperatureDesc.Font = new System.Drawing.Font("Marlett", 8.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLastMeasuredTemperatureDesc.Location = new System.Drawing.Point(110, 0);
            this.labelLastMeasuredTemperatureDesc.Name = "labelLastMeasuredTemperatureDesc";
            this.labelLastMeasuredTemperatureDesc.Size = new System.Drawing.Size(290, 40);
            this.labelLastMeasuredTemperatureDesc.TabIndex = 1;
            this.labelLastMeasuredTemperatureDesc.Text = "Last measured temperature,   seconds ago";
            this.labelLastMeasuredTemperatureDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Marlett", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(110, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Temperature Alert Level";
            // 
            // trackBarTemperature
            // 
            this.trackBarTemperature.BackColor = System.Drawing.SystemColors.Control;
            this.trackBarTemperature.Location = new System.Drawing.Point(164, 95);
            this.trackBarTemperature.Maximum = 110;
            this.trackBarTemperature.Minimum = 40;
            this.trackBarTemperature.Name = "trackBarTemperature";
            this.trackBarTemperature.Size = new System.Drawing.Size(228, 45);
            this.trackBarTemperature.TabIndex = 3;
            this.trackBarTemperature.TickFrequency = 5;
            this.trackBarTemperature.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarTemperature.Value = 40;
            // 
            // labelTemperature
            // 
            this.labelTemperature.AutoSize = true;
            this.labelTemperature.Font = new System.Drawing.Font("Marlett", 8.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTemperature.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelTemperature.Location = new System.Drawing.Point(114, 109);
            this.labelTemperature.Name = "labelTemperature";
            this.labelTemperature.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelTemperature.Size = new System.Drawing.Size(0, 15);
            this.labelTemperature.TabIndex = 4;
            // 
            // labelTimeout
            // 
            this.labelTimeout.AutoSize = true;
            this.labelTimeout.Font = new System.Drawing.Font("Marlett", 8.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTimeout.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelTimeout.Location = new System.Drawing.Point(114, 215);
            this.labelTimeout.Name = "labelTimeout";
            this.labelTimeout.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelTimeout.Size = new System.Drawing.Size(0, 15);
            this.labelTimeout.TabIndex = 7;
            // 
            // trackBarTimeout
            // 
            this.trackBarTimeout.BackColor = System.Drawing.SystemColors.Control;
            this.trackBarTimeout.Location = new System.Drawing.Point(164, 201);
            this.trackBarTimeout.Maximum = 120;
            this.trackBarTimeout.Minimum = 10;
            this.trackBarTimeout.Name = "trackBarTimeout";
            this.trackBarTimeout.Size = new System.Drawing.Size(228, 45);
            this.trackBarTimeout.TabIndex = 6;
            this.trackBarTimeout.TickFrequency = 10;
            this.trackBarTimeout.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarTimeout.Value = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Marlett", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(110, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 18);
            this.label4.TabIndex = 5;
            this.label4.Text = "Timeout";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Marlett", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(110, 268);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 18);
            this.label5.TabIndex = 8;
            this.label5.Text = "Notification Event";
            // 
            // rbMessageBoxNotif
            // 
            this.rbMessageBoxNotif.AutoSize = true;
            this.rbMessageBoxNotif.Location = new System.Drawing.Point(113, 305);
            this.rbMessageBoxNotif.Name = "rbMessageBoxNotif";
            this.rbMessageBoxNotif.Size = new System.Drawing.Size(87, 17);
            this.rbMessageBoxNotif.TabIndex = 9;
            this.rbMessageBoxNotif.TabStop = true;
            this.rbMessageBoxNotif.Text = "message box";
            this.rbMessageBoxNotif.UseVisualStyleBackColor = true;
            // 
            // rbTrayNotif
            // 
            this.rbTrayNotif.AutoSize = true;
            this.rbTrayNotif.Location = new System.Drawing.Point(205, 305);
            this.rbTrayNotif.Name = "rbTrayNotif";
            this.rbTrayNotif.Size = new System.Drawing.Size(96, 17);
            this.rbTrayNotif.TabIndex = 10;
            this.rbTrayNotif.TabStop = true;
            this.rbTrayNotif.Text = "tray notification";
            this.rbTrayNotif.UseVisualStyleBackColor = true;
            // 
            // rbNoNotif
            // 
            this.rbNoNotif.AutoSize = true;
            this.rbNoNotif.Location = new System.Drawing.Point(307, 305);
            this.rbNoNotif.Name = "rbNoNotif";
            this.rbNoNotif.Size = new System.Drawing.Size(49, 17);
            this.rbNoNotif.TabIndex = 11;
            this.rbNoNotif.TabStop = true;
            this.rbNoNotif.Text = "none";
            this.rbNoNotif.UseVisualStyleBackColor = true;
            // 
            // labelLastMeasuredTemperature
            // 
            this.labelLastMeasuredTemperature.Font = new System.Drawing.Font("Marlett", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLastMeasuredTemperature.Location = new System.Drawing.Point(0, 0);
            this.labelLastMeasuredTemperature.Name = "labelLastMeasuredTemperature";
            this.labelLastMeasuredTemperature.Size = new System.Drawing.Size(100, 40);
            this.labelLastMeasuredTemperature.TabIndex = 12;
            this.labelLastMeasuredTemperature.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // thermometerPictureBox
            // 
            this.thermometerPictureBox.BackColor = System.Drawing.SystemColors.Control;
            this.thermometerPictureBox.Image = global::TemperatureAlertSystem.TrayUI.Properties.Resources.Thermometer;
            this.thermometerPictureBox.Location = new System.Drawing.Point(0, 40);
            this.thermometerPictureBox.MarginBottom = 60;
            this.thermometerPictureBox.MarginLeft = 31;
            this.thermometerPictureBox.MarginRight = 31;
            this.thermometerPictureBox.MarginTop = 20;
            this.thermometerPictureBox.Name = "thermometerPictureBox";
            this.thermometerPictureBox.Percentage = 100;
            this.thermometerPictureBox.Size = new System.Drawing.Size(100, 300);
            this.thermometerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.thermometerPictureBox.TabIndex = 13;
            this.thermometerPictureBox.TabStop = false;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 341);
            this.Controls.Add(this.thermometerPictureBox);
            this.Controls.Add(this.labelLastMeasuredTemperature);
            this.Controls.Add(this.rbNoNotif);
            this.Controls.Add(this.rbTrayNotif);
            this.Controls.Add(this.rbMessageBoxNotif);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelTimeout);
            this.Controls.Add(this.trackBarTimeout);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelTemperature);
            this.Controls.Add(this.trackBarTemperature);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelLastMeasuredTemperatureDesc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UIForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Temperature Alert System - Settings";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTemperature)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thermometerPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLastMeasuredTemperatureDesc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackBarTemperature;
        private System.Windows.Forms.Label labelTemperature;
        private System.Windows.Forms.Label labelTimeout;
        private System.Windows.Forms.TrackBar trackBarTimeout;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbMessageBoxNotif;
        private System.Windows.Forms.RadioButton rbTrayNotif;
        private System.Windows.Forms.RadioButton rbNoNotif;
        private System.Windows.Forms.Label labelLastMeasuredTemperature;
        private ThermometerPictureBox thermometerPictureBox;

    }
}