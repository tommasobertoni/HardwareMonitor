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
            this.components = new System.ComponentModel.Container();
            this.cbStartupNotification = new System.Windows.Forms.CheckBox();
            this.cbStartupRun = new System.Windows.Forms.CheckBox();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbAdminRights = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.labelAdminRightsInfo = new System.Windows.Forms.Label();
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
            this.btnSaveSettings.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSaveSettings.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveSettings.Location = new System.Drawing.Point(107, 251);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(115, 45);
            this.btnSaveSettings.TabIndex = 2;
            this.btnSaveSettings.Text = "Save";
            this.btnSaveSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Application";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Privileges";
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
            this.labelAdminRightsInfo.Location = new System.Drawing.Point(18, 198);
            this.labelAdminRightsInfo.Name = "labelAdminRightsInfo";
            this.labelAdminRightsInfo.Size = new System.Drawing.Size(277, 32);
            this.labelAdminRightsInfo.TabIndex = 8;
            this.labelAdminRightsInfo.Text = "* The configuration requires the\r\napplication to start with administrator rights";
            this.labelAdminRightsInfo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.labelAdminRightsInfo.Visible = false;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 308);
            this.Controls.Add(this.labelAdminRightsInfo);
            this.Controls.Add(this.cbAdminRights);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSaveSettings);
            this.Controls.Add(this.cbStartupRun);
            this.Controls.Add(this.cbStartupNotification);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbAdminRights;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label labelAdminRightsInfo;
    }
}