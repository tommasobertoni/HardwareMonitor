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
            this.rbMessageNotif = new System.Windows.Forms.RadioButton();
            this.rbMessageAndSoundNotif = new System.Windows.Forms.RadioButton();
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
            this.btnChangeSound = new System.Windows.Forms.Button();
            this.labelSoundName = new System.Windows.Forms.Label();
            this.thermometerPictureBox = new HardwareMonitor.Client.Temperature.CustomControls.ThermometerPictureBox();
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
            this.labelAvgCPUsTemperature.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAvgCPUsTemperature.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.labelAvgCPUsTemperature.Location = new System.Drawing.Point(25, 10);
            this.labelAvgCPUsTemperature.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAvgCPUsTemperature.Name = "labelAvgCPUsTemperature";
            this.labelAvgCPUsTemperature.Size = new System.Drawing.Size(120, 50);
            this.labelAvgCPUsTemperature.TabIndex = 9;
            this.labelAvgCPUsTemperature.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rbNoNotif
            // 
            this.rbNoNotif.AutoSize = true;
            this.rbNoNotif.Location = new System.Drawing.Point(433, 378);
            this.rbNoNotif.Margin = new System.Windows.Forms.Padding(4);
            this.rbNoNotif.Name = "rbNoNotif";
            this.rbNoNotif.Size = new System.Drawing.Size(61, 21);
            this.rbNoNotif.TabIndex = 10;
            this.rbNoNotif.TabStop = true;
            this.rbNoNotif.Text = "none";
            this.rbNoNotif.UseVisualStyleBackColor = true;
            // 
            // rbMessageNotif
            // 
            this.rbMessageNotif.AutoSize = true;
            this.rbMessageNotif.Location = new System.Drawing.Point(339, 378);
            this.rbMessageNotif.Margin = new System.Windows.Forms.Padding(4);
            this.rbMessageNotif.Name = "rbMessageNotif";
            this.rbMessageNotif.Size = new System.Drawing.Size(86, 21);
            this.rbMessageNotif.TabIndex = 11;
            this.rbMessageNotif.TabStop = true;
            this.rbMessageNotif.Text = "message";
            this.rbMessageNotif.UseVisualStyleBackColor = true;
            // 
            // rbMessageAndSoundNotif
            // 
            this.rbMessageAndSoundNotif.AutoSize = true;
            this.rbMessageAndSoundNotif.Location = new System.Drawing.Point(175, 378);
            this.rbMessageAndSoundNotif.Margin = new System.Windows.Forms.Padding(4);
            this.rbMessageAndSoundNotif.Name = "rbMessageAndSoundNotif";
            this.rbMessageAndSoundNotif.Size = new System.Drawing.Size(157, 21);
            this.rbMessageAndSoundNotif.TabIndex = 12;
            this.rbMessageAndSoundNotif.TabStop = true;
            this.rbMessageAndSoundNotif.Text = "sound and message";
            this.rbMessageAndSoundNotif.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Marlett", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(168, 340);
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
            this.trackBarObservers.Location = new System.Drawing.Point(260, 270);
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
            this.label4.Location = new System.Drawing.Point(168, 237);
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
            this.trackbarUpdateTime.Location = new System.Drawing.Point(260, 165);
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
            this.label2.Location = new System.Drawing.Point(168, 132);
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
            this.trackBarTemperatureAlertLevel.Location = new System.Drawing.Point(260, 60);
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
            this.label1.Location = new System.Drawing.Point(168, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 23);
            this.label1.TabIndex = 8;
            this.label1.Text = "Temperature Alert Level";
            // 
            // labelTemperature
            // 
            this.labelTemperature.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTemperature.Location = new System.Drawing.Point(204, 60);
            this.labelTemperature.Name = "labelTemperature";
            this.labelTemperature.Size = new System.Drawing.Size(55, 56);
            this.labelTemperature.TabIndex = 5;
            this.labelTemperature.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelUpdateTime
            // 
            this.labelUpdateTime.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUpdateTime.Location = new System.Drawing.Point(204, 165);
            this.labelUpdateTime.Name = "labelUpdateTime";
            this.labelUpdateTime.Size = new System.Drawing.Size(55, 47);
            this.labelUpdateTime.TabIndex = 4;
            this.labelUpdateTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelObservers
            // 
            this.labelObservers.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelObservers.Location = new System.Drawing.Point(204, 270);
            this.labelObservers.Name = "labelObservers";
            this.labelObservers.Size = new System.Drawing.Size(55, 47);
            this.labelObservers.TabIndex = 3;
            this.labelObservers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nupTemperatureAlertLevel
            // 
            this.nupTemperatureAlertLevel.Location = new System.Drawing.Point(174, 73);
            this.nupTemperatureAlertLevel.Name = "nupTemperatureAlertLevel";
            this.nupTemperatureAlertLevel.Size = new System.Drawing.Size(23, 22);
            this.nupTemperatureAlertLevel.TabIndex = 2;
            // 
            // nupUpdateTime
            // 
            this.nupUpdateTime.Location = new System.Drawing.Point(174, 178);
            this.nupUpdateTime.Name = "nupUpdateTime";
            this.nupUpdateTime.Size = new System.Drawing.Size(23, 22);
            this.nupUpdateTime.TabIndex = 1;
            // 
            // nupObservers
            // 
            this.nupObservers.Location = new System.Drawing.Point(174, 283);
            this.nupObservers.Name = "nupObservers";
            this.nupObservers.Size = new System.Drawing.Size(23, 22);
            this.nupObservers.TabIndex = 0;
            // 
            // btnChangeSound
            // 
            this.btnChangeSound.Location = new System.Drawing.Point(172, 408);
            this.btnChangeSound.Margin = new System.Windows.Forms.Padding(0);
            this.btnChangeSound.Name = "btnChangeSound";
            this.btnChangeSound.Size = new System.Drawing.Size(120, 36);
            this.btnChangeSound.TabIndex = 20;
            this.btnChangeSound.Text = "Change Sound";
            this.btnChangeSound.UseVisualStyleBackColor = true;
            this.btnChangeSound.Click += new System.EventHandler(this.btnChangeSound_Click);
            // 
            // labelSoundName
            // 
            this.labelSoundName.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSoundName.Location = new System.Drawing.Point(309, 408);
            this.labelSoundName.Name = "labelSoundName";
            this.labelSoundName.Size = new System.Drawing.Size(251, 36);
            this.labelSoundName.TabIndex = 19;
            this.labelSoundName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // thermometerPictureBox
            // 
            this.thermometerPictureBox.Image = global::HardwareMonitor.Client.Temperature.Properties.Resources.Thermometer;
            this.thermometerPictureBox.Location = new System.Drawing.Point(25, 65);
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
            this.ClientSize = new System.Drawing.Size(582, 473);
            this.Controls.Add(this.labelSoundName);
            this.Controls.Add(this.btnChangeSound);
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
            this.Controls.Add(this.rbMessageNotif);
            this.Controls.Add(this.rbMessageAndSoundNotif);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.trackBarObservers);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.trackbarUpdateTime);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TemperatureUI";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ShowIcon = false;
            this.Text = " ";
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
        private System.Windows.Forms.RadioButton rbMessageNotif;
        private System.Windows.Forms.RadioButton rbMessageAndSoundNotif;
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
        private CustomControls.ThermometerPictureBox thermometerPictureBox;
        private System.Windows.Forms.Button btnChangeSound;
        private System.Windows.Forms.Label labelSoundName;
    }
}

