using ClassLibrary;

namespace SettingsApp
{
    partial class MenuForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuForm));
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.LicenseValabilityLabel = new System.Windows.Forms.Label();
            this.FollowMe = new System.Windows.Forms.Label();
            this.MailMe = new System.Windows.Forms.Label();
            this.PhoneNo = new System.Windows.Forms.Label();
            this.CreatedOn = new System.Windows.Forms.Label();
            this.CreatedBy = new System.Windows.Forms.Label();
            this.MenuPanel = new System.Windows.Forms.Panel();
            this.StartManualBackup = new System.Windows.Forms.Button();
            this.ChangeAutoBackupBtn = new System.Windows.Forms.Button();
            this.ChangeKeepRecBtn = new System.Windows.Forms.Button();
            this.ChangeBackupPathBtn = new System.Windows.Forms.Button();
            this.ChangeSagaPathBtn = new System.Windows.Forms.Button();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.LogsAndErrorsPanel = new System.Windows.Forms.Panel();
            this.HelpBtn = new System.Windows.Forms.Button();
            this.ErrorFileBtn = new System.Windows.Forms.Button();
            this.LogFileBtn = new System.Windows.Forms.Button();
            this.HeaderPanel.SuspendLayout();
            this.MenuPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            this.LogsAndErrorsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // HeaderPanel
            // 
            this.HeaderPanel.Controls.Add(this.LicenseValabilityLabel);
            this.HeaderPanel.Controls.Add(this.FollowMe);
            this.HeaderPanel.Controls.Add(this.MailMe);
            this.HeaderPanel.Controls.Add(this.PhoneNo);
            this.HeaderPanel.Controls.Add(this.CreatedOn);
            this.HeaderPanel.Controls.Add(this.CreatedBy);
            this.HeaderPanel.Location = new System.Drawing.Point(205, 0);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.Size = new System.Drawing.Size(470, 160);
            this.HeaderPanel.TabIndex = 3;
            // 
            // LicenseValabilityLabel
            // 
            this.LicenseValabilityLabel.AutoSize = true;
            this.LicenseValabilityLabel.Location = new System.Drawing.Point(5, 135);
            this.LicenseValabilityLabel.Name = "LicenseValabilityLabel";
            this.LicenseValabilityLabel.Size = new System.Drawing.Size(0, 20);
            this.LicenseValabilityLabel.TabIndex = 5;
            // 
            // FollowMe
            // 
            this.FollowMe.AutoSize = true;
            this.FollowMe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FollowMe.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.FollowMe.Location = new System.Drawing.Point(5, 110);
            this.FollowMe.Name = "FollowMe";
            this.FollowMe.Size = new System.Drawing.Size(236, 20);
            this.FollowMe.TabIndex = 4;
            this.FollowMe.Text = "Click to follow us on: Facebook";
            this.FollowMe.Click += new System.EventHandler(this.FollowMe_Click);
            // 
            // MailMe
            // 
            this.MailMe.AutoSize = true;
            this.MailMe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MailMe.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.MailMe.Location = new System.Drawing.Point(5, 85);
            this.MailMe.Name = "MailMe";
            this.MailMe.Size = new System.Drawing.Size(386, 20);
            this.MailMe.TabIndex = 3;
            this.MailMe.Text = "Click to mail me on: marius.diaconu76@gmail.com";
            this.MailMe.Click += new System.EventHandler(this.MailMe_Click);
            // 
            // PhoneNo
            // 
            this.PhoneNo.AutoSize = true;
            this.PhoneNo.Location = new System.Drawing.Point(5, 60);
            this.PhoneNo.Name = "PhoneNo";
            this.PhoneNo.Size = new System.Drawing.Size(201, 20);
            this.PhoneNo.TabIndex = 2;
            this.PhoneNo.Text = "Phone No: +40760545808";
            // 
            // CreatedOn
            // 
            this.CreatedOn.AutoSize = true;
            this.CreatedOn.Location = new System.Drawing.Point(5, 35);
            this.CreatedOn.Name = "CreatedOn";
            this.CreatedOn.Size = new System.Drawing.Size(176, 20);
            this.CreatedOn.TabIndex = 1;
            this.CreatedOn.Text = "Created On: July 2020";
            // 
            // CreatedBy
            // 
            this.CreatedBy.AutoSize = true;
            this.CreatedBy.Location = new System.Drawing.Point(5, 10);
            this.CreatedBy.Name = "CreatedBy";
            this.CreatedBy.Size = new System.Drawing.Size(221, 20);
            this.CreatedBy.TabIndex = 0;
            this.CreatedBy.Text = "Created By: Marius Diaconu";
            // 
            // MenuPanel
            // 
            this.MenuPanel.Controls.Add(this.StartManualBackup);
            this.MenuPanel.Controls.Add(this.ChangeAutoBackupBtn);
            this.MenuPanel.Controls.Add(this.ChangeKeepRecBtn);
            this.MenuPanel.Controls.Add(this.ChangeBackupPathBtn);
            this.MenuPanel.Controls.Add(this.ChangeSagaPathBtn);
            this.MenuPanel.Location = new System.Drawing.Point(205, 160);
            this.MenuPanel.Name = "MenuPanel";
            this.MenuPanel.Size = new System.Drawing.Size(470, 270);
            this.MenuPanel.TabIndex = 4;
            // 
            // StartManualBackup
            // 
            this.StartManualBackup.Location = new System.Drawing.Point(5, 183);
            this.StartManualBackup.Name = "StartManualBackup";
            this.StartManualBackup.Size = new System.Drawing.Size(450, 35);
            this.StartManualBackup.TabIndex = 6;
            this.StartManualBackup.Text = "Backup la Comanda";
            this.StartManualBackup.UseVisualStyleBackColor = true;
            this.StartManualBackup.Click += new System.EventHandler(this.StartManualBackup_Click);
            // 
            // ChangeAutoBackupBtn
            // 
            this.ChangeAutoBackupBtn.Location = new System.Drawing.Point(5, 138);
            this.ChangeAutoBackupBtn.Name = "ChangeAutoBackupBtn";
            this.ChangeAutoBackupBtn.Size = new System.Drawing.Size(450, 35);
            this.ChangeAutoBackupBtn.TabIndex = 5;
            this.ChangeAutoBackupBtn.Text = "Actualizeaza ora/minut pentru backup-ul automat";
            this.ChangeAutoBackupBtn.UseVisualStyleBackColor = true;
            this.ChangeAutoBackupBtn.Click += new System.EventHandler(this.ChangeAutoBackupBtn_Click);
            // 
            // ChangeKeepRecBtn
            // 
            this.ChangeKeepRecBtn.Location = new System.Drawing.Point(5, 93);
            this.ChangeKeepRecBtn.Name = "ChangeKeepRecBtn";
            this.ChangeKeepRecBtn.Size = new System.Drawing.Size(450, 35);
            this.ChangeKeepRecBtn.TabIndex = 4;
            this.ChangeKeepRecBtn.Text = "Actualizeaza numarul de ani pentru pastrarea firmelor sterse";
            this.ChangeKeepRecBtn.UseVisualStyleBackColor = true;
            this.ChangeKeepRecBtn.Click += new System.EventHandler(this.ChangeKeepRecBtn_Click);
            // 
            // ChangeBackupPathBtn
            // 
            this.ChangeBackupPathBtn.Location = new System.Drawing.Point(5, 48);
            this.ChangeBackupPathBtn.Name = "ChangeBackupPathBtn";
            this.ChangeBackupPathBtn.Size = new System.Drawing.Size(450, 35);
            this.ChangeBackupPathBtn.TabIndex = 3;
            this.ChangeBackupPathBtn.Text = $"Actualizeaza locatia de backup pentru: {App.name.ToUpper()}.{App.ver}";
            this.ChangeBackupPathBtn.UseVisualStyleBackColor = true;
            this.ChangeBackupPathBtn.Click += new System.EventHandler(this.ChangeBackupPathBtn_Click);
            // 
            // ChangeSagaPathBtn
            // 
            this.ChangeSagaPathBtn.Location = new System.Drawing.Point(5, 5);
            this.ChangeSagaPathBtn.Name = "ChangeSagaPathBtn";
            this.ChangeSagaPathBtn.Size = new System.Drawing.Size(450, 35);
            this.ChangeSagaPathBtn.TabIndex = 0;
            this.ChangeSagaPathBtn.Text = $"Actualizeaza calea de acces catre {App.name.ToUpper()}.{App.ver}";
            this.ChangeSagaPathBtn.UseVisualStyleBackColor = true;
            this.ChangeSagaPathBtn.Click += new System.EventHandler(this.ChangeSagaPathBtn_Click);
            // 
            // PictureBox
            // 
            this.PictureBox.BackgroundImage = global::SettingsApp.Properties.Resources.yellow_bg;
            this.PictureBox.Location = new System.Drawing.Point(0, 0);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(195, 515);
            this.PictureBox.TabIndex = 0;
            this.PictureBox.TabStop = false;
            // 
            // LogsAndErrorsPanel
            // 
            this.LogsAndErrorsPanel.Controls.Add(this.HelpBtn);
            this.LogsAndErrorsPanel.Controls.Add(this.ErrorFileBtn);
            this.LogsAndErrorsPanel.Controls.Add(this.LogFileBtn);
            this.LogsAndErrorsPanel.Location = new System.Drawing.Point(205, 453);
            this.LogsAndErrorsPanel.Name = "LogsAndErrorsPanel";
            this.LogsAndErrorsPanel.Size = new System.Drawing.Size(465, 45);
            this.LogsAndErrorsPanel.TabIndex = 5;
            // 
            // HelpBtn
            // 
            this.HelpBtn.Location = new System.Drawing.Point(345, 5);
            this.HelpBtn.Name = "HelpBtn";
            this.HelpBtn.Size = new System.Drawing.Size(110, 35);
            this.HelpBtn.TabIndex = 2;
            this.HelpBtn.Text = "Help";
            this.HelpBtn.UseVisualStyleBackColor = true;
            this.HelpBtn.Click += new System.EventHandler(this.HelpBtn_Click);
            // 
            // ErrorFileBtn
            // 
            this.ErrorFileBtn.Location = new System.Drawing.Point(178, 5);
            this.ErrorFileBtn.Name = "ErrorFileBtn";
            this.ErrorFileBtn.Size = new System.Drawing.Size(110, 35);
            this.ErrorFileBtn.TabIndex = 1;
            this.ErrorFileBtn.Text = "Errors";
            this.ErrorFileBtn.UseVisualStyleBackColor = true;
            this.ErrorFileBtn.Click += new System.EventHandler(this.ErrorFileBtn_Click);
            // 
            // LogFileBtn
            // 
            this.LogFileBtn.Location = new System.Drawing.Point(5, 5);
            this.LogFileBtn.Name = "LogFileBtn";
            this.LogFileBtn.Size = new System.Drawing.Size(110, 35);
            this.LogFileBtn.TabIndex = 0;
            this.LogFileBtn.Text = "Logs";
            this.LogFileBtn.UseVisualStyleBackColor = true;
            this.LogFileBtn.Click += new System.EventHandler(this.LogFileBtn_Click);
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 503);
            this.Controls.Add(this.LogsAndErrorsPanel);
            this.Controls.Add(this.MenuPanel);
            this.Controls.Add(this.HeaderPanel);
            this.Controls.Add(this.PictureBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(700, 550);
            this.MinimumSize = new System.Drawing.Size(700, 550);
            this.Name = "MenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = $"{App.name}.{App.ver} Backup Settings Menu - Version: {App.Version}";
            this.Load += new System.EventHandler(this.MenuForm_Load);
            this.HeaderPanel.ResumeLayout(false);
            this.HeaderPanel.PerformLayout();
            this.MenuPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.LogsAndErrorsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBox;
        private System.Windows.Forms.Panel HeaderPanel;
        private System.Windows.Forms.Label FollowMe;
        private System.Windows.Forms.Label MailMe;
        private System.Windows.Forms.Label PhoneNo;
        private System.Windows.Forms.Label CreatedOn;
        private System.Windows.Forms.Label CreatedBy;
        private System.Windows.Forms.Panel MenuPanel;
        private System.Windows.Forms.Button ChangeSagaPathBtn;
        private System.Windows.Forms.Button ChangeBackupPathBtn;
        private System.Windows.Forms.Button ChangeKeepRecBtn;
        private System.Windows.Forms.Button ChangeAutoBackupBtn;
        private System.Windows.Forms.Label LicenseValabilityLabel;
        private System.Windows.Forms.Panel LogsAndErrorsPanel;
        private System.Windows.Forms.Button ErrorFileBtn;
        private System.Windows.Forms.Button LogFileBtn;
        private System.Windows.Forms.Button HelpBtn;
        private System.Windows.Forms.Button StartManualBackup;
    }
}