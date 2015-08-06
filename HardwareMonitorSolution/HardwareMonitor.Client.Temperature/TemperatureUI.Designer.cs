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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemperatureUI));
            this.labelLastMeasuredTemperature = new System.Windows.Forms.Label();
            this.rbNoNotif = new System.Windows.Forms.RadioButton();
            this.rbTrayNotif = new System.Windows.Forms.RadioButton();
            this.rbMessageBoxNotif = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.trackBarObservers = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.trackbarUpdateTime = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBarTemperature = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.labelTemperature = new System.Windows.Forms.Label();
            this.labelUpdateTime = new System.Windows.Forms.Label();
            this.labelObservers = new System.Windows.Forms.Label();
            this.thermometerPictureBox1 = new HardwareMonitor.Client.Temperature.ThermometerPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarObservers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackbarUpdateTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTemperature)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thermometerPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelLastMeasuredTemperature
            // 
            this.labelLastMeasuredTemperature.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLastMeasuredTemperature.Location = new System.Drawing.Point(0, 0);
            this.labelLastMeasuredTemperature.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelLastMeasuredTemperature.Name = "labelLastMeasuredTemperature";
            this.labelLastMeasuredTemperature.Size = new System.Drawing.Size(120, 49);
            this.labelLastMeasuredTemperature.TabIndex = 36;
            this.labelLastMeasuredTemperature.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rbNoNotif
            // 
            this.rbNoNotif.AutoSize = true;
            this.rbNoNotif.Location = new System.Drawing.Point(410, 388);
            this.rbNoNotif.Margin = new System.Windows.Forms.Padding(4);
            this.rbNoNotif.Name = "rbNoNotif";
            this.rbNoNotif.Size = new System.Drawing.Size(61, 21);
            this.rbNoNotif.TabIndex = 35;
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
            this.rbTrayNotif.TabIndex = 34;
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
            this.rbMessageBoxNotif.TabIndex = 33;
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
            this.label5.Size = new System.Drawing.Size(161, 23);
            this.label5.TabIndex = 32;
            this.label5.Text = "Notification Event";
            // 
            // trackBarObservers
            // 
            this.trackBarObservers.BackColor = System.Drawing.SystemColors.Control;
            this.trackBarObservers.LargeChange = 1;
            this.trackBarObservers.Location = new System.Drawing.Point(224, 277);
            this.trackBarObservers.Margin = new System.Windows.Forms.Padding(4);
            this.trackBarObservers.Maximum = 20;
            this.trackBarObservers.Name = "trackBarObservers";
            this.trackBarObservers.Size = new System.Drawing.Size(304, 56);
            this.trackBarObservers.TabIndex = 30;
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
            this.label4.TabIndex = 29;
            this.label4.Text = "Observers";
            // 
            // trackbarUpdateTime
            // 
            this.trackbarUpdateTime.BackColor = System.Drawing.SystemColors.Control;
            this.trackbarUpdateTime.LargeChange = 2;
            this.trackbarUpdateTime.Location = new System.Drawing.Point(224, 172);
            this.trackbarUpdateTime.Margin = new System.Windows.Forms.Padding(4);
            this.trackbarUpdateTime.Maximum = 120;
            this.trackbarUpdateTime.Minimum = 1;
            this.trackbarUpdateTime.Name = "trackbarUpdateTime";
            this.trackbarUpdateTime.Size = new System.Drawing.Size(304, 56);
            this.trackbarUpdateTime.SmallChange = 2;
            this.trackbarUpdateTime.TabIndex = 27;
            this.trackbarUpdateTime.TickFrequency = 10;
            this.trackbarUpdateTime.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackbarUpdateTime.Value = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Marlett", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(152, 131);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 23);
            this.label2.TabIndex = 26;
            this.label2.Text = "Update time";
            // 
            // trackBarTemperature
            // 
            this.trackBarTemperature.BackColor = System.Drawing.SystemColors.Control;
            this.trackBarTemperature.LargeChange = 1;
            this.trackBarTemperature.Location = new System.Drawing.Point(224, 67);
            this.trackBarTemperature.Margin = new System.Windows.Forms.Padding(4);
            this.trackBarTemperature.Maximum = 110;
            this.trackBarTemperature.Minimum = 30;
            this.trackBarTemperature.Name = "trackBarTemperature";
            this.trackBarTemperature.Size = new System.Drawing.Size(304, 56);
            this.trackBarTemperature.TabIndex = 38;
            this.trackBarTemperature.TickFrequency = 5;
            this.trackBarTemperature.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarTemperature.Value = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Marlett", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(152, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 23);
            this.label1.TabIndex = 37;
            this.label1.Text = "Temperature Alert Level";
            // 
            // labelTemperature
            // 
            this.labelTemperature.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTemperature.Location = new System.Drawing.Point(152, 67);
            this.labelTemperature.Name = "labelTemperature";
            this.labelTemperature.Size = new System.Drawing.Size(64, 47);
            this.labelTemperature.TabIndex = 40;
            this.labelTemperature.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTimespan
            // 
            this.labelUpdateTime.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUpdateTime.Location = new System.Drawing.Point(152, 172);
            this.labelUpdateTime.Name = "labelTimespan";
            this.labelUpdateTime.Size = new System.Drawing.Size(64, 47);
            this.labelUpdateTime.TabIndex = 41;
            this.labelUpdateTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelObservers
            // 
            this.labelObservers.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelObservers.Location = new System.Drawing.Point(152, 277);
            this.labelObservers.Name = "labelObservers";
            this.labelObservers.Size = new System.Drawing.Size(64, 47);
            this.labelObservers.TabIndex = 42;
            this.labelObservers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // thermometerPictureBox1
            // 
            this.thermometerPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("thermometerPictureBox1.Image")));
            this.thermometerPictureBox1.Location = new System.Drawing.Point(5, 52);
            this.thermometerPictureBox1.MarginBottom = 0;
            this.thermometerPictureBox1.MarginLeft = 0;
            this.thermometerPictureBox1.MarginRight = 0;
            this.thermometerPictureBox1.MarginTop = 0;
            this.thermometerPictureBox1.Name = "thermometerPictureBox1";
            this.thermometerPictureBox1.Percentage = 100;
            this.thermometerPictureBox1.Size = new System.Drawing.Size(113, 386);
            this.thermometerPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.thermometerPictureBox1.TabIndex = 39;
            this.thermometerPictureBox1.TabStop = false;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 438);
            this.Controls.Add(this.labelObservers);
            this.Controls.Add(this.labelUpdateTime);
            this.Controls.Add(this.labelTemperature);
            this.Controls.Add(this.thermometerPictureBox1);
            this.Controls.Add(this.trackBarTemperature);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelLastMeasuredTemperature);
            this.Controls.Add(this.rbNoNotif);
            this.Controls.Add(this.rbTrayNotif);
            this.Controls.Add(this.rbMessageBoxNotif);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.trackBarObservers);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.trackbarUpdateTime);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form";
            this.Text = "Hardware Monitor - Temperature";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarObservers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackbarUpdateTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTemperature)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thermometerPictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLastMeasuredTemperature;
        private System.Windows.Forms.RadioButton rbNoNotif;
        private System.Windows.Forms.RadioButton rbTrayNotif;
        private System.Windows.Forms.RadioButton rbMessageBoxNotif;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar trackBarObservers;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar trackbarUpdateTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackBarTemperature;
        private System.Windows.Forms.Label label1;
        private ThermometerPictureBox thermometerPictureBox1;
        private System.Windows.Forms.Label labelTemperature;
        private System.Windows.Forms.Label labelUpdateTime;
        private System.Windows.Forms.Label labelObservers;
    }
}

