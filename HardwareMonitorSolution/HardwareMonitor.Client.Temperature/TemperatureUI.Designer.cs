namespace HardwareMonitor.Client.Temperature
{
    partial class TemperatureUI
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
            this.labelAvgCPUsTemperature = new System.Windows.Forms.Label();
            this.rbNoNotif = new System.Windows.Forms.RadioButton();
            this.rbTrayNotif = new System.Windows.Forms.RadioButton();
            this.rbMessageBoxNotif = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.trackBarObservers = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.trackbarUpdateTime = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBarTemperatureAlertLevel = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.labelTemperature = new System.Windows.Forms.Label();
            this.labelUpdateTime = new System.Windows.Forms.Label();
            this.labelObservers = new System.Windows.Forms.Label();
            this.nupTemperatureAlertLevel = new System.Windows.Forms.NumericUpDown();
            this.nupUpdateTime = new System.Windows.Forms.NumericUpDown();
            this.nupObservers = new System.Windows.Forms.NumericUpDown();
            this.thermometerPictureBox = new HardwareMonitor.Client.Temperature.ThermometerPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarObservers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackbarUpdateTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTemperatureAlertLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupTemperatureAlertLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupUpdateTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupObservers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thermometerPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // labelAvgCPUsTemperature
            // 
            this.labelAvgCPUsTemperature.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAvgCPUsTemperature.Location = new System.Drawing.Point(0, 0);
            this.labelAvgCPUsTemperature.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAvgCPUsTemperature.Name = "labelAvgCPUsTemperature";
            this.labelAvgCPUsTemperature.Padding = new System.Windows.Forms.Padding(5, 10, 0, 10);
            this.labelAvgCPUsTemperature.Size = new System.Drawing.Size(120, 50);
            this.labelAvgCPUsTemperature.TabIndex = 9;
            this.labelAvgCPUsTemperature.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rbNoNotif
            // 
            this.rbNoNotif.AutoSize = true;
            this.rbNoNotif.Location = new System.Drawing.Point(410, 388);
            this.rbNoNotif.Margin = new System.Windows.Forms.Padding(4);
            this.rbNoNotif.Name = "rbNoNotif";
            this.rbNoNotif.Size = new System.Drawing.Size(61, 21);
            this.rbNoNotif.TabIndex = 10;
            this.rbNoNotif.TabStop = true;
            this.rbNoNotif.Text = "none";
            this.rbNoNotif.UseVisualStyleBackColor = true;
            // 
            // rbTrayNotif
            // 
            this.rbTrayNotif.AutoSize = true;
            this.rbTrayNotif.Location = new System.Drawing.Point(274, 388);
            this.rbTrayNotif.Margin = new System.Windows.Forms.Padding(4);
            this.rbTrayNotif.Name = "rbTrayNotif";
            this.rbTrayNotif.Size = new System.Drawing.Size(125, 21);
            this.rbTrayNotif.TabIndex = 11;
            this.rbTrayNotif.TabStop = true;
            this.rbTrayNotif.Text = "tray notification";
            this.rbTrayNotif.UseVisualStyleBackColor = true;
            // 
            // rbMessageBoxNotif
            // 
            this.rbMessageBoxNotif.AutoSize = true;
            this.rbMessageBoxNotif.Location = new System.Drawing.Point(152, 388);
            this.rbMessageBoxNotif.Margin = new System.Windows.Forms.Padding(4);
            this.rbMessageBoxNotif.Name = "rbMessageBoxNotif";
            this.rbMessageBoxNotif.Size = new System.Drawing.Size(112, 21);
            this.rbMessageBoxNotif.TabIndex = 12;
            this.rbMessageBoxNotif.TabStop = true;
            this.rbMessageBoxNotif.Text = "message box";
            this.rbMessageBoxNotif.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Marlett", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(148, 343);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(178, 23);
            this.label5.TabIndex = 13;
            this.label5.Text = "Notification Method";
            // 
            // trackBarObservers
            // 
            this.trackBarObservers.BackColor = System.Drawing.SystemColors.Control;
            this.trackBarObservers.LargeChange = 1;
            this.trackBarObservers.Location = new System.Drawing.Point(240, 277);
            this.trackBarObservers.Margin = new System.Windows.Forms.Padding(4);
            this.trackBarObservers.Name = "trackBarObservers";
            this.trackBarObservers.Size = new System.Drawing.Size(300, 56);
            this.trackBarObservers.TabIndex = 14;
            this.trackBarObservers.TickFrequency = 10;
            this.trackBarObservers.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Marlett", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(152, 236);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 23);
            this.label4.TabIndex = 15;
            this.label4.Text = "Observers";
            // 
            // trackbarUpdateTime
            // 
            this.trackbarUpdateTime.BackColor = System.Drawing.SystemColors.Control;
            this.trackbarUpdateTime.LargeChange = 1;
            this.trackbarUpdateTime.Location = new System.Drawing.Point(240, 172);
            this.trackbarUpdateTime.Margin = new System.Windows.Forms.Padding(4);
            this.trackbarUpdateTime.Name = "trackbarUpdateTime";
            this.trackbarUpdateTime.Size = new System.Drawing.Size(300, 56);
            this.trackbarUpdateTime.TabIndex = 16;
            this.trackbarUpdateTime.TickFrequency = 10;
            this.trackbarUpdateTime.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Marlett", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(152, 131);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 23);
            this.label2.TabIndex = 17;
            this.label2.Text = "Update time";
            // 
            // trackBarTemperatureAlertLevel
            // 
            this.trackBarTemperatureAlertLevel.BackColor = System.Drawing.SystemColors.Control;
            this.trackBarTemperatureAlertLevel.LargeChange = 1;
            this.trackBarTemperatureAlertLevel.Location = new System.Drawing.Point(240, 67);
            this.trackBarTemperatureAlertLevel.Margin = new System.Windows.Forms.Padding(4);
            this.trackBarTemperatureAlertLevel.Name = "trackBarTemperatureAlertLevel";
            this.trackBarTemperatureAlertLevel.Size = new System.Drawing.Size(300, 56);
            this.trackBarTemperatureAlertLevel.TabIndex = 7;
            this.trackBarTemperatureAlertLevel.TickFrequency = 5;
            this.trackBarTemperatureAlertLevel.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Marlett", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(152, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 23);
            this.label1.TabIndex = 8;
            this.label1.Text = "Temperature Alert Level";
            // 
            // labelTemperature
            // 
            this.labelTemperature.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTemperature.Location = new System.Drawing.Point(180, 67);
            this.labelTemperature.Name = "labelTemperature";
            this.labelTemperature.Size = new System.Drawing.Size(55, 56);
            this.labelTemperature.TabIndex = 5;
            this.labelTemperature.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelUpdateTime
            // 
            this.labelUpdateTime.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUpdateTime.Location = new System.Drawing.Point(180, 172);
            this.labelUpdateTime.Name = "labelUpdateTime";
            this.labelUpdateTime.Size = new System.Drawing.Size(55, 47);
            this.labelUpdateTime.TabIndex = 4;
            this.labelUpdateTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelObservers
            // 
            this.labelObservers.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelObservers.Location = new System.Drawing.Point(180, 277);
            this.labelObservers.Name = "labelObservers";
            this.labelObservers.Size = new System.Drawing.Size(55, 47);
            this.labelObservers.TabIndex = 3;
            this.labelObservers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nupTemperatureAlertLevel
            // 
            this.nupTemperatureAlertLevel.Location = new System.Drawing.Point(150, 80);
            this.nupTemperatureAlertLevel.Name = "nupTemperatureAlertLevel";
            this.nupTemperatureAlertLevel.Size = new System.Drawing.Size(23, 22);
            this.nupTemperatureAlertLevel.TabIndex = 2;
            // 
            // nupUpdateTime
            // 
            this.nupUpdateTime.Location = new System.Drawing.Point(150, 185);
            this.nupUpdateTime.Name = "nupUpdateTime";
            this.nupUpdateTime.Size = new System.Drawing.Size(23, 22);
            this.nupUpdateTime.TabIndex = 1;
            // 
            // nupObservers
            // 
            this.nupObservers.Location = new System.Drawing.Point(150, 290);
            this.nupObservers.Name = "nupObservers";
            this.nupObservers.Size = new System.Drawing.Size(23, 22);
            this.nupObservers.TabIndex = 0;
            // 
            // thermometerPictureBox
            // 
            this.thermometerPictureBox.Image = global::HardwareMonitor.Client.Temperature.Properties.Resources.Thermometer;
            this.thermometerPictureBox.Location = new System.Drawing.Point(5, 55);
            this.thermometerPictureBox.MarginBottom = 0;
            this.thermometerPictureBox.MarginLeft = 0;
            this.thermometerPictureBox.MarginRight = 0;
            this.thermometerPictureBox.MarginTop = 0;
            this.thermometerPictureBox.Name = "thermometerPictureBox";
            this.thermometerPictureBox.Size = new System.Drawing.Size(120, 386);
            this.thermometerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.thermometerPictureBox.TabIndex = 6;
            this.thermometerPictureBox.TabStop = false;
            this.thermometerPictureBox.Value = 100;
            // 
            // TemperatureUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 438);
            this.Controls.Add(this.nupObservers);
            this.Controls.Add(this.nupUpdateTime);
            this.Controls.Add(this.nupTemperatureAlertLevel);
            this.Controls.Add(this.labelObservers);
            this.Controls.Add(this.labelUpdateTime);
            this.Controls.Add(this.labelTemperature);
            this.Controls.Add(this.thermometerPictureBox);
            this.Controls.Add(this.trackBarTemperatureAlertLevel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelAvgCPUsTemperature);
            this.Controls.Add(this.rbNoNotif);
            this.Controls.Add(this.rbTrayNotif);
            this.Controls.Add(this.rbMessageBoxNotif);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.trackBarObservers);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.trackbarUpdateTime);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TemperatureUI";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.trackBarObservers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackbarUpdateTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTemperatureAlertLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupTemperatureAlertLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupUpdateTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupObservers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thermometerPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAvgCPUsTemperature;
        private System.Windows.Forms.RadioButton rbNoNotif;
        private System.Windows.Forms.RadioButton rbTrayNotif;
        private System.Windows.Forms.RadioButton rbMessageBoxNotif;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar trackBarObservers;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar trackbarUpdateTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackBarTemperatureAlertLevel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelTemperature;
        private System.Windows.Forms.Label labelUpdateTime;
        private System.Windows.Forms.Label labelObservers;
        private System.Windows.Forms.NumericUpDown nupTemperatureAlertLevel;
        private System.Windows.Forms.NumericUpDown nupUpdateTime;
        private System.Windows.Forms.NumericUpDown nupObservers;
        private ThermometerPictureBox thermometerPictureBox;
    }
}

