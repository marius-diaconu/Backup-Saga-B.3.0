using System;
using System.IO;
using System.Windows.Forms;
using ClassLibrary;

namespace SettingsApp
{
    partial class BackupSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackupSettingsForm));
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.FollowMe = new System.Windows.Forms.Label();
            this.MailMe = new System.Windows.Forms.Label();
            this.PhoneNo = new System.Windows.Forms.Label();
            this.CreatedOn = new System.Windows.Forms.Label();
            this.CreatedBy = new System.Windows.Forms.Label();
            this.SagaPathPanel = new System.Windows.Forms.Panel();
            this.SagaPathBtn = new System.Windows.Forms.Button();
            this.SagaPathTextBox = new System.Windows.Forms.TextBox();
            this.SagaPathLabel = new System.Windows.Forms.Label();
            this.BackupPathPanel = new System.Windows.Forms.Panel();
            this.BackupPathBtn = new System.Windows.Forms.Button();
            this.BackupPathTextBox = new System.Windows.Forms.TextBox();
            this.BackupPathLabel = new System.Windows.Forms.Label();
            this.KeepRecPanel = new System.Windows.Forms.Panel();
            this.KeepDelCompInput = new System.Windows.Forms.NumericUpDown();
            this.KeepDelCompLabel = new System.Windows.Forms.Label();
            this.TaskSettingsPanel = new System.Windows.Forms.Panel();
            this.TaskTypeLabel = new System.Windows.Forms.Label();
            this.WeeklyTask = new System.Windows.Forms.RadioButton();
            this.DailyTask = new System.Windows.Forms.RadioButton();
            this.AutoBackupTimePicker = new System.Windows.Forms.DateTimePicker();
            this.AutoBackupCheckBox = new System.Windows.Forms.CheckBox();
            this.AutoBackupLabel = new System.Windows.Forms.Label();
            this.SagaBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.BackupBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.BackupDayPanel = new System.Windows.Forms.Panel();
            this.Friday = new System.Windows.Forms.RadioButton();
            this.Thursday = new System.Windows.Forms.RadioButton();
            this.Wednesday = new System.Windows.Forms.RadioButton();
            this.Tuesday = new System.Windows.Forms.RadioButton();
            this.Monday = new System.Windows.Forms.RadioButton();
            this.BackupDayLabel = new System.Windows.Forms.Label();
            this.SaveSettingPanel = new System.Windows.Forms.Panel();
            this.SaveSettingsBtn = new System.Windows.Forms.Button();
            this.ShutdownLabel = new System.Windows.Forms.Label();
            this.ShutdownCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            this.HeaderPanel.SuspendLayout();
            this.SagaPathPanel.SuspendLayout();
            this.BackupPathPanel.SuspendLayout();
            this.KeepRecPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KeepDelCompInput)).BeginInit();
            this.TaskSettingsPanel.SuspendLayout();
            this.BackupDayPanel.SuspendLayout();
            this.SaveSettingPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // PictureBox
            // 
            this.PictureBox.BackgroundImage = global::SettingsApp.Properties.Resources.yellow_bg;
            this.PictureBox.Location = new System.Drawing.Point(0, 0);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(195, 565);
            this.PictureBox.TabIndex = 0;
            this.PictureBox.TabStop = false;
            // 
            // HeaderPanel
            // 
            this.HeaderPanel.Controls.Add(this.FollowMe);
            this.HeaderPanel.Controls.Add(this.MailMe);
            this.HeaderPanel.Controls.Add(this.PhoneNo);
            this.HeaderPanel.Controls.Add(this.CreatedOn);
            this.HeaderPanel.Controls.Add(this.CreatedBy);
            this.HeaderPanel.Location = new System.Drawing.Point(205, 5);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.Size = new System.Drawing.Size(526, 140);
            this.HeaderPanel.TabIndex = 1;
            // 
            // FollowMe
            // 
            this.FollowMe.AutoSize = true;
            this.FollowMe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FollowMe.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.FollowMe.Location = new System.Drawing.Point(5, 114);
            this.FollowMe.Name = "FollowMe";
            this.FollowMe.Size = new System.Drawing.Size(236, 20);
            this.FollowMe.TabIndex = 4;
            this.FollowMe.Text = "Click to follow us on Facebook";
            this.FollowMe.Click += new System.EventHandler(this.FollowMe_Click);
            // 
            // MailMe
            // 
            this.MailMe.AutoSize = true;
            this.MailMe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MailMe.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.MailMe.Location = new System.Drawing.Point(5, 88);
            this.MailMe.Name = "MailMe";
            this.MailMe.Size = new System.Drawing.Size(386, 20);
            this.MailMe.TabIndex = 3;
            this.MailMe.Text = "Click to mail me on: marius.diaconu76@gmail.com";
            this.MailMe.Click += new System.EventHandler(this.MailMe_Click);
            // 
            // PhoneNo
            // 
            this.PhoneNo.AutoSize = true;
            this.PhoneNo.Location = new System.Drawing.Point(5, 62);
            this.PhoneNo.Name = "PhoneNo";
            this.PhoneNo.Size = new System.Drawing.Size(201, 20);
            this.PhoneNo.TabIndex = 2;
            this.PhoneNo.Text = "Phone No: +40760545808";
            // 
            // CreatedOn
            // 
            this.CreatedOn.AutoSize = true;
            this.CreatedOn.Location = new System.Drawing.Point(5, 36);
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
            // SagaPathPanel
            // 
            this.SagaPathPanel.Controls.Add(this.SagaPathBtn);
            this.SagaPathPanel.Controls.Add(this.SagaPathTextBox);
            this.SagaPathPanel.Controls.Add(this.SagaPathLabel);
            this.SagaPathPanel.Location = new System.Drawing.Point(205, 145);
            this.SagaPathPanel.Name = "SagaPathPanel";
            this.SagaPathPanel.Size = new System.Drawing.Size(526, 80);
            this.SagaPathPanel.TabIndex = 3;
            // 
            // SagaPathBtn
            // 
            this.SagaPathBtn.Location = new System.Drawing.Point(310, 35);
            this.SagaPathBtn.Name = "SagaPathBtn";
            this.SagaPathBtn.Size = new System.Drawing.Size(100, 35);
            this.SagaPathBtn.TabIndex = 2;
            this.SagaPathBtn.Text = "Selecteaza";
            this.SagaPathBtn.UseVisualStyleBackColor = true;
            this.SagaPathBtn.Click += new System.EventHandler(this.SagaPathBtn_Click);
            // 
            // SagaPathTextBox
            // 
            this.SagaPathTextBox.Location = new System.Drawing.Point(5, 40);
            this.SagaPathTextBox.Name = "SagaPathTextBox";
            this.SagaPathTextBox.Size = new System.Drawing.Size(295, 26);
            this.SagaPathTextBox.TabIndex = 1;
            this.SagaPathTextBox.TextChanged += new System.EventHandler(this.SagaPathTextBox_TextChanged);
            // 
            // SagaPathLabel
            // 
            this.SagaPathLabel.AutoSize = true;
            this.SagaPathLabel.Location = new System.Drawing.Point(3, 16);
            this.SagaPathLabel.Name = "SagaPathLabel";
            this.SagaPathLabel.Size = new System.Drawing.Size(228, 20);
            this.SagaPathLabel.TabIndex = 0;
            this.SagaPathLabel.Text = $"Selectati folderul {App.name.ToUpper()}.{App.ver}";
            // 
            // BackupPathPanel
            // 
            this.BackupPathPanel.Controls.Add(this.BackupPathBtn);
            this.BackupPathPanel.Controls.Add(this.BackupPathTextBox);
            this.BackupPathPanel.Controls.Add(this.BackupPathLabel);
            this.BackupPathPanel.Location = new System.Drawing.Point(205, 226);
            this.BackupPathPanel.Name = "BackupPathPanel";
            this.BackupPathPanel.Size = new System.Drawing.Size(526, 80);
            this.BackupPathPanel.TabIndex = 4;
            // 
            // BackupPathBtn
            // 
            this.BackupPathBtn.Location = new System.Drawing.Point(310, 35);
            this.BackupPathBtn.Name = "BackupPathBtn";
            this.BackupPathBtn.Size = new System.Drawing.Size(100, 35);
            this.BackupPathBtn.TabIndex = 9;
            this.BackupPathBtn.Text = "Selecteaza";
            this.BackupPathBtn.UseVisualStyleBackColor = true;
            this.BackupPathBtn.Click += new System.EventHandler(this.BackupPathBtn_Click);
            // 
            // BackupPathTextBox
            // 
            this.BackupPathTextBox.Location = new System.Drawing.Point(5, 40);
            this.BackupPathTextBox.Name = "BackupPathTextBox";
            this.BackupPathTextBox.Size = new System.Drawing.Size(295, 26);
            this.BackupPathTextBox.TabIndex = 8;
            this.BackupPathTextBox.TextChanged += new System.EventHandler(this.BackupPathTextBox_TextChanged);
            // 
            // BackupPathLabel
            // 
            this.BackupPathLabel.AutoSize = true;
            this.BackupPathLabel.Location = new System.Drawing.Point(2, 15);
            this.BackupPathLabel.Name = "BackupPathLabel";
            this.BackupPathLabel.Size = new System.Drawing.Size(355, 20);
            this.BackupPathLabel.TabIndex = 7;
            this.BackupPathLabel.Text = $"Selectati locatia de backup pentru {App.name.ToUpper()}.{App.ver}";
            // 
            // KeepRecPanel
            // 
            this.KeepRecPanel.Controls.Add(this.KeepDelCompInput);
            this.KeepRecPanel.Controls.Add(this.KeepDelCompLabel);
            this.KeepRecPanel.Location = new System.Drawing.Point(205, 307);
            this.KeepRecPanel.Name = "KeepRecPanel";
            this.KeepRecPanel.Size = new System.Drawing.Size(526, 68);
            this.KeepRecPanel.TabIndex = 5;
            // 
            // KeepDelCompInput
            // 
            this.KeepDelCompInput.Location = new System.Drawing.Point(8, 33);
            this.KeepDelCompInput.Name = "KeepDelCompInput";
            this.KeepDelCompInput.Size = new System.Drawing.Size(96, 26);
            this.KeepDelCompInput.TabIndex = 5;
            this.KeepDelCompInput.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.KeepDelCompInput.ValueChanged += new System.EventHandler(this.KeepDelCompInput_ValueChanged);
            // 
            // KeepDelCompLabel
            // 
            this.KeepDelCompLabel.AutoSize = true;
            this.KeepDelCompLabel.Location = new System.Drawing.Point(4, 10);
            this.KeepDelCompLabel.Name = "KeepDelCompLabel";
            this.KeepDelCompLabel.Size = new System.Drawing.Size(485, 20);
            this.KeepDelCompLabel.TabIndex = 4;
            this.KeepDelCompLabel.Text = $"Numarul de ani pentru pastrarea firmelor sterse din {App.name.ToUpper()}.{App.ver}";
            // 
            // TaskSettingsPanel
            // 
            this.TaskSettingsPanel.Controls.Add(this.TaskTypeLabel);
            this.TaskSettingsPanel.Controls.Add(this.WeeklyTask);
            this.TaskSettingsPanel.Controls.Add(this.DailyTask);
            this.TaskSettingsPanel.Controls.Add(this.AutoBackupTimePicker);
            this.TaskSettingsPanel.Controls.Add(this.AutoBackupCheckBox);
            this.TaskSettingsPanel.Controls.Add(this.AutoBackupLabel);
            this.TaskSettingsPanel.Location = new System.Drawing.Point(205, 436);
            this.TaskSettingsPanel.Name = "TaskSettingsPanel";
            this.TaskSettingsPanel.Size = new System.Drawing.Size(526, 65);
            this.TaskSettingsPanel.TabIndex = 6;
            // 
            // TaskTypeLabel
            // 
            this.TaskTypeLabel.AutoSize = true;
            this.TaskTypeLabel.Location = new System.Drawing.Point(216, 8);
            this.TaskTypeLabel.Name = "TaskTypeLabel";
            this.TaskTypeLabel.Size = new System.Drawing.Size(140, 20);
            this.TaskTypeLabel.TabIndex = 5;
            this.TaskTypeLabel.Text = "Tipul backup-ului:";
            // 
            // WeeklyTask
            // 
            this.WeeklyTask.AutoSize = true;
            this.WeeklyTask.Location = new System.Drawing.Point(280, 31);
            this.WeeklyTask.Name = "WeeklyTask";
            this.WeeklyTask.Size = new System.Drawing.Size(116, 24);
            this.WeeklyTask.TabIndex = 4;
            this.WeeklyTask.TabStop = true;
            this.WeeklyTask.Text = "saptamanal";
            this.WeeklyTask.UseVisualStyleBackColor = true;
            // 
            // DailyTask
            // 
            this.DailyTask.AutoSize = true;
            this.DailyTask.Checked = true;
            this.DailyTask.Location = new System.Drawing.Point(193, 32);
            this.DailyTask.Name = "DailyTask";
            this.DailyTask.Size = new System.Drawing.Size(69, 24);
            this.DailyTask.TabIndex = 3;
            this.DailyTask.TabStop = true;
            this.DailyTask.Text = "zilnic";
            this.DailyTask.UseVisualStyleBackColor = true;
            // 
            // AutoBackupTimePicker
            // 
            this.AutoBackupTimePicker.CustomFormat = "HH:mm:ss";
            this.AutoBackupTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.AutoBackupTimePicker.Location = new System.Drawing.Point(34, 30);
            this.AutoBackupTimePicker.Name = "AutoBackupTimePicker";
            this.AutoBackupTimePicker.ShowUpDown = true;
            this.AutoBackupTimePicker.Size = new System.Drawing.Size(80, 26);
            this.AutoBackupTimePicker.TabIndex = 2;
            this.AutoBackupTimePicker.ValueChanged += new System.EventHandler(this.AutoBackupTimePicker_ValueChanged);
            // 
            // AutoBackupCheckBox
            // 
            this.AutoBackupCheckBox.AutoSize = true;
            this.AutoBackupCheckBox.Location = new System.Drawing.Point(10, 35);
            this.AutoBackupCheckBox.Name = "AutoBackupCheckBox";
            this.AutoBackupCheckBox.Size = new System.Drawing.Size(18, 17);
            this.AutoBackupCheckBox.TabIndex = 1;
            this.AutoBackupCheckBox.UseVisualStyleBackColor = true;
            this.AutoBackupCheckBox.CheckedChanged += new System.EventHandler(this.AutoBackupCheckBox_CheckedChanged);
            // 
            // AutoBackupLabel
            // 
            this.AutoBackupLabel.AutoSize = true;
            this.AutoBackupLabel.Location = new System.Drawing.Point(5, 7);
            this.AutoBackupLabel.Name = "AutoBackupLabel";
            this.AutoBackupLabel.Size = new System.Drawing.Size(141, 20);
            this.AutoBackupLabel.TabIndex = 0;
            this.AutoBackupLabel.Text = "Backup Automat?";
            // 
            // SagaBrowserDialog
            // 
            this.SagaBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // BackupBrowserDialog
            // 
            this.BackupBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // BackupDayPanel
            // 
            this.BackupDayPanel.Controls.Add(this.Friday);
            this.BackupDayPanel.Controls.Add(this.Thursday);
            this.BackupDayPanel.Controls.Add(this.Wednesday);
            this.BackupDayPanel.Controls.Add(this.Tuesday);
            this.BackupDayPanel.Controls.Add(this.Monday);
            this.BackupDayPanel.Controls.Add(this.BackupDayLabel);
            this.BackupDayPanel.Location = new System.Drawing.Point(204, 372);
            this.BackupDayPanel.Name = "BackupDayPanel";
            this.BackupDayPanel.Size = new System.Drawing.Size(526, 62);
            this.BackupDayPanel.TabIndex = 7;
            // 
            // Friday
            // 
            this.Friday.AutoSize = true;
            this.Friday.Checked = true;
            this.Friday.Location = new System.Drawing.Point(328, 33);
            this.Friday.Name = "Friday";
            this.Friday.Size = new System.Drawing.Size(73, 24);
            this.Friday.TabIndex = 5;
            this.Friday.TabStop = true;
            this.Friday.Text = "Vineri";
            this.Friday.UseVisualStyleBackColor = true;
            // 
            // Thursday
            // 
            this.Thursday.AutoSize = true;
            this.Thursday.Location = new System.Drawing.Point(263, 33);
            this.Thursday.Name = "Thursday";
            this.Thursday.Size = new System.Drawing.Size(52, 24);
            this.Thursday.TabIndex = 4;
            this.Thursday.TabStop = true;
            this.Thursday.Text = "Joi";
            this.Thursday.UseVisualStyleBackColor = true;
            // 
            // Wednesday
            // 
            this.Wednesday.AutoSize = true;
            this.Wednesday.Location = new System.Drawing.Point(163, 33);
            this.Wednesday.Name = "Wednesday";
            this.Wednesday.Size = new System.Drawing.Size(91, 24);
            this.Wednesday.TabIndex = 3;
            this.Wednesday.TabStop = true;
            this.Wednesday.Text = "Miercuri";
            this.Wednesday.UseVisualStyleBackColor = true;
            // 
            // Tuesday
            // 
            this.Tuesday.AutoSize = true;
            this.Tuesday.Location = new System.Drawing.Point(83, 33);
            this.Tuesday.Name = "Tuesday";
            this.Tuesday.Size = new System.Drawing.Size(68, 24);
            this.Tuesday.TabIndex = 2;
            this.Tuesday.TabStop = true;
            this.Tuesday.Text = "Marti";
            this.Tuesday.UseVisualStyleBackColor = true;
            // 
            // Monday
            // 
            this.Monday.AutoSize = true;
            this.Monday.Location = new System.Drawing.Point(9, 33);
            this.Monday.Name = "Monday";
            this.Monday.Size = new System.Drawing.Size(62, 24);
            this.Monday.TabIndex = 1;
            this.Monday.TabStop = true;
            this.Monday.Text = "Luni";
            this.Monday.UseVisualStyleBackColor = true;
            // 
            // BackupDayLabel
            // 
            this.BackupDayLabel.AutoSize = true;
            this.BackupDayLabel.Location = new System.Drawing.Point(5, 5);
            this.BackupDayLabel.Name = "BackupDayLabel";
            this.BackupDayLabel.Size = new System.Drawing.Size(491, 20);
            this.BackupDayLabel.TabIndex = 0;
            this.BackupDayLabel.Text = $"Alege ziua pentru salvarea datelor tuturor firmelor in {App.name.ToUpper()}.{App.ver}";
            // 
            // SaveSettingPanel
            // 
            this.SaveSettingPanel.Controls.Add(this.SaveSettingsBtn);
            this.SaveSettingPanel.Controls.Add(this.ShutdownLabel);
            this.SaveSettingPanel.Controls.Add(this.ShutdownCheckBox);
            this.SaveSettingPanel.Location = new System.Drawing.Point(205, 499);
            this.SaveSettingPanel.Name = "SaveSettingPanel";
            this.SaveSettingPanel.Size = new System.Drawing.Size(526, 51);
            this.SaveSettingPanel.TabIndex = 8;
            // 
            // SaveSettingsBtn
            // 
            this.SaveSettingsBtn.Location = new System.Drawing.Point(326, 8);
            this.SaveSettingsBtn.Name = "SaveSettingsBtn";
            this.SaveSettingsBtn.Size = new System.Drawing.Size(190, 35);
            this.SaveSettingsBtn.TabIndex = 5;
            this.SaveSettingsBtn.Text = "Salveaza Setarile";
            this.SaveSettingsBtn.UseVisualStyleBackColor = true;
            this.SaveSettingsBtn.Click += new System.EventHandler(this.SaveSettingsBtn_Click);
            // 
            // ShutdownLabel
            // 
            this.ShutdownLabel.AutoSize = true;
            this.ShutdownLabel.Location = new System.Drawing.Point(32, 14);
            this.ShutdownLabel.Name = "ShutdownLabel";
            this.ShutdownLabel.Size = new System.Drawing.Size(171, 20);
            this.ShutdownLabel.TabIndex = 4;
            this.ShutdownLabel.Text = "Cu Shutdown la final?";
            // 
            // ShutdownCheckBox
            // 
            this.ShutdownCheckBox.AutoSize = true;
            this.ShutdownCheckBox.Location = new System.Drawing.Point(9, 15);
            this.ShutdownCheckBox.Name = "ShutdownCheckBox";
            this.ShutdownCheckBox.Size = new System.Drawing.Size(18, 17);
            this.ShutdownCheckBox.TabIndex = 3;
            this.ShutdownCheckBox.UseVisualStyleBackColor = true;
            // 
            // BackupSettingsForm
            // 
            this.ClientSize = new System.Drawing.Size(732, 553);
            this.Controls.Add(this.SaveSettingPanel);
            this.Controls.Add(this.BackupDayPanel);
            this.Controls.Add(this.TaskSettingsPanel);
            this.Controls.Add(this.KeepRecPanel);
            this.Controls.Add(this.BackupPathPanel);
            this.Controls.Add(this.SagaPathPanel);
            this.Controls.Add(this.HeaderPanel);
            this.Controls.Add(this.PictureBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(750, 600);
            this.MinimumSize = new System.Drawing.Size(750, 600);
            this.Name = "BackupSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = $"{App.name}.{App.ver} Backup Settings Form - Version: {App.Version}";
            this.Load += new System.EventHandler(this.BackupSettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.HeaderPanel.ResumeLayout(false);
            this.HeaderPanel.PerformLayout();
            this.SagaPathPanel.ResumeLayout(false);
            this.SagaPathPanel.PerformLayout();
            this.BackupPathPanel.ResumeLayout(false);
            this.BackupPathPanel.PerformLayout();
            this.KeepRecPanel.ResumeLayout(false);
            this.KeepRecPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KeepDelCompInput)).EndInit();
            this.TaskSettingsPanel.ResumeLayout(false);
            this.TaskSettingsPanel.PerformLayout();
            this.BackupDayPanel.ResumeLayout(false);
            this.BackupDayPanel.PerformLayout();
            this.SaveSettingPanel.ResumeLayout(false);
            this.SaveSettingPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox PictureBox;
        private Panel HeaderPanel;
        private Label CreatedBy;
        private Label CreatedOn;
        private Label FollowMe;
        private Label MailMe;
        private Label PhoneNo;
        private Panel SagaPathPanel;
        private Label SagaPathLabel;
        private TextBox SagaPathTextBox;
        private Button SagaPathBtn;
        private Panel BackupPathPanel;
        private Label BackupPathLabel;
        private Button BackupPathBtn;
        private TextBox BackupPathTextBox;
        private Panel KeepRecPanel;
        private NumericUpDown KeepDelCompInput;
        private Label KeepDelCompLabel;
        private Panel TaskSettingsPanel;
        private Label AutoBackupLabel;
        private CheckBox AutoBackupCheckBox;
        private DateTimePicker AutoBackupTimePicker;
        private FolderBrowserDialog SagaBrowserDialog;
        private FolderBrowserDialog BackupBrowserDialog;
        private Panel BackupDayPanel;
        private Label BackupDayLabel;
        private RadioButton Monday;
        private RadioButton Tuesday;
        private RadioButton Friday;
        private RadioButton Thursday;
        private RadioButton Wednesday;
        private Panel SaveSettingPanel;
        private Button SaveSettingsBtn;
        private Label ShutdownLabel;
        private CheckBox ShutdownCheckBox;
        private RadioButton DailyTask;
        private RadioButton WeeklyTask;
        private Label TaskTypeLabel;
    }
}

