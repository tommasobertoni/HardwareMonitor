namespace HardwareMonitor.Client.Controller
{
    partial class SettingsForm
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
            this.cbStartupNotification = new System.Windows.Forms.CheckBox();
            this.cbStartupRun = new System.Windows.Forms.CheckBox();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbStartupNotification
            // 
            this.cbStartupNotification.AutoSize = true;
            this.cbStartupNotification.Location = new System.Drawing.Point(13, 40);
            this.cbStartupNotification.Name = "cbStartupNotification";
            this.cbStartupNotification.Size = new System.Drawing.Size(256, 21);
            this.cbStartupNotification.TabIndex = 0;
            this.cbStartupNotification.Text = "Show notification at application start";
            this.cbStartupNotification.UseVisualStyleBackColor = true;
            // 
            // cbStartupRun
            // 
            this.cbStartupRun.AutoSize = true;
            this.cbStartupRun.Location = new System.Drawing.Point(13, 13);
            this.cbStartupRun.Name = "cbStartupRun";
            this.cbStartupRun.Size = new System.Drawing.Size(272, 21);
            this.cbStartupRun.TabIndex = 1;
            this.cbStartupRun.Text = "Run the application at windows startup";
            this.cbStartupRun.UseVisualStyleBackColor = true;
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSaveSettings.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveSettings.Location = new System.Drawing.Point(96, 141);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(115, 35);
            this.btnSaveSettings.TabIndex = 2;
            this.btnSaveSettings.Text = "Save";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 188);
            this.Controls.Add(this.btnSaveSettings);
            this.Controls.Add(this.cbStartupRun);
            this.Controls.Add(this.cbStartupNotification);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbStartupNotification;
        private System.Windows.Forms.CheckBox cbStartupRun;
        private System.Windows.Forms.Button btnSaveSettings;
    }
}