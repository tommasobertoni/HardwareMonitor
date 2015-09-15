namespace HardwareMonitor.Client.Settings
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
            this.components = new System.ComponentModel.Container();
            this.cbStartupNotification = new System.Windows.Forms.CheckBox();
            this.cbStartupRun = new System.Windows.Forms.CheckBox();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.labelApplicationTitle = new System.Windows.Forms.Label();
            this.labelPrivilegesTitle = new System.Windows.Forms.Label();
            this.cbAdminRights = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.labelAdminRightsInfo = new System.Windows.Forms.Label();
            this.btnThemeLight = new System.Windows.Forms.Button();
            this.labelThemeTitle = new System.Windows.Forms.Label();
            this.btnThemeDark = new System.Windows.Forms.Button();
            this.cbDeveloperMode = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cbStartupNotification
            // 
            this.cbStartupNotification.AutoSize = true;
            this.cbStartupNotification.Location = new System.Drawing.Point(21, 67);
            this.cbStartupNotification.Name = "cbStartupNotification";
            this.cbStartupNotification.Size = new System.Drawing.Size(256, 21);
            this.cbStartupNotification.TabIndex = 0;
            this.cbStartupNotification.Text = "Show notification at application start";
            this.cbStartupNotification.UseVisualStyleBackColor = true;
            // 
            // cbStartupRun
            // 
            this.cbStartupRun.AutoSize = true;
            this.cbStartupRun.Location = new System.Drawing.Point(21, 40);
            this.cbStartupRun.Name = "cbStartupRun";
            this.cbStartupRun.Size = new System.Drawing.Size(216, 21);
            this.cbStartupRun.TabIndex = 1;
            this.cbStartupRun.Text = "Run the application at startup";
            this.cbStartupRun.UseVisualStyleBackColor = true;
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveSettings.Location = new System.Drawing.Point(114, 369);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(115, 45);
            this.btnSaveSettings.TabIndex = 2;
            this.btnSaveSettings.Text = "Save";
            this.btnSaveSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // labelApplicationTitle
            // 
            this.labelApplicationTitle.AutoSize = true;
            this.labelApplicationTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelApplicationTitle.Location = new System.Drawing.Point(17, 7);
            this.labelApplicationTitle.Name = "labelApplicationTitle";
            this.labelApplicationTitle.Size = new System.Drawing.Size(102, 20);
            this.labelApplicationTitle.TabIndex = 3;
            this.labelApplicationTitle.Text = "Application";
            // 
            // labelPrivilegesTitle
            // 
            this.labelPrivilegesTitle.AutoSize = true;
            this.labelPrivilegesTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPrivilegesTitle.Location = new System.Drawing.Point(17, 105);
            this.labelPrivilegesTitle.Name = "labelPrivilegesTitle";
            this.labelPrivilegesTitle.Size = new System.Drawing.Size(92, 20);
            this.labelPrivilegesTitle.TabIndex = 4;
            this.labelPrivilegesTitle.Text = "Privileges";
            // 
            // cbAdminRights
            // 
            this.cbAdminRights.AutoSize = true;
            this.cbAdminRights.Location = new System.Drawing.Point(21, 138);
            this.cbAdminRights.Name = "cbAdminRights";
            this.cbAdminRights.Size = new System.Drawing.Size(241, 21);
            this.cbAdminRights.TabIndex = 5;
            this.cbAdminRights.Text = "Start application with admin rights";
            this.cbAdminRights.UseVisualStyleBackColor = true;
            this.cbAdminRights.CheckedChanged += new System.EventHandler(this.cbAdminRights_CheckedChanged);
            // 
            // labelAdminRightsInfo
            // 
            this.labelAdminRightsInfo.AutoSize = true;
            this.labelAdminRightsInfo.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAdminRightsInfo.Location = new System.Drawing.Point(27, 317);
            this.labelAdminRightsInfo.Name = "labelAdminRightsInfo";
            this.labelAdminRightsInfo.Size = new System.Drawing.Size(277, 32);
            this.labelAdminRightsInfo.TabIndex = 8;
            this.labelAdminRightsInfo.Text = "* The configuration requires the\r\napplication to start with administrator rights";
            this.labelAdminRightsInfo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.labelAdminRightsInfo.Visible = false;
            // 
            // btnThemeLight
            // 
            this.btnThemeLight.Location = new System.Drawing.Point(21, 235);
            this.btnThemeLight.Name = "btnThemeLight";
            this.btnThemeLight.Size = new System.Drawing.Size(106, 38);
            this.btnThemeLight.TabIndex = 9;
            this.btnThemeLight.Text = "Force Light";
            this.btnThemeLight.UseVisualStyleBackColor = true;
            this.btnThemeLight.Click += new System.EventHandler(this.btnThemeLight_Click);
            // 
            // labelThemeTitle
            // 
            this.labelThemeTitle.AutoSize = true;
            this.labelThemeTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelThemeTitle.Location = new System.Drawing.Point(17, 203);
            this.labelThemeTitle.Name = "labelThemeTitle";
            this.labelThemeTitle.Size = new System.Drawing.Size(65, 20);
            this.labelThemeTitle.TabIndex = 10;
            this.labelThemeTitle.Text = "Theme";
            // 
            // btnThemeDark
            // 
            this.btnThemeDark.BackColor = System.Drawing.SystemColors.ControlText;
            this.btnThemeDark.ForeColor = System.Drawing.SystemColors.Control;
            this.btnThemeDark.Location = new System.Drawing.Point(133, 235);
            this.btnThemeDark.Name = "btnThemeDark";
            this.btnThemeDark.Size = new System.Drawing.Size(106, 38);
            this.btnThemeDark.TabIndex = 11;
            this.btnThemeDark.Text = "Force Dark";
            this.btnThemeDark.UseVisualStyleBackColor = false;
            this.btnThemeDark.Click += new System.EventHandler(this.btnThemeDark_Click);
            // 
            // cbDeveloperMode
            // 
            this.cbDeveloperMode.AutoSize = true;
            this.cbDeveloperMode.Location = new System.Drawing.Point(21, 165);
            this.cbDeveloperMode.Name = "cbDeveloperMode";
            this.cbDeveloperMode.Size = new System.Drawing.Size(134, 21);
            this.cbDeveloperMode.TabIndex = 12;
            this.cbDeveloperMode.Text = "Developer mode";
            this.cbDeveloperMode.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(330, 433);
            this.Controls.Add(this.cbDeveloperMode);
            this.Controls.Add(this.btnThemeDark);
            this.Controls.Add(this.labelThemeTitle);
            this.Controls.Add(this.btnThemeLight);
            this.Controls.Add(this.labelAdminRightsInfo);
            this.Controls.Add(this.cbAdminRights);
            this.Controls.Add(this.labelPrivilegesTitle);
            this.Controls.Add(this.labelApplicationTitle);
            this.Controls.Add(this.btnSaveSettings);
            this.Controls.Add(this.cbStartupRun);
            this.Controls.Add(this.cbStartupNotification);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbStartupNotification;
        private System.Windows.Forms.CheckBox cbStartupRun;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.Label labelApplicationTitle;
        private System.Windows.Forms.Label labelPrivilegesTitle;
        private System.Windows.Forms.CheckBox cbAdminRights;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label labelAdminRightsInfo;
        private System.Windows.Forms.Button btnThemeLight;
        private System.Windows.Forms.Label labelThemeTitle;
        private System.Windows.Forms.Button btnThemeDark;
        private System.Windows.Forms.CheckBox cbDeveloperMode;
    }
}