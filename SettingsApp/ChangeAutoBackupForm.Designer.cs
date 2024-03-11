using ClassLibrary;
namespace SettingsApp
{
    partial class ChangeAutoBackupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeAutoBackupForm));
            this.TaskSettingsPanel = new System.Windows.Forms.Panel();
            this.AutoBackupTimePicker = new System.Windows.Forms.DateTimePicker();
            this.AutoBackupCheckBox = new System.Windows.Forms.CheckBox();
            this.BackupDayPanel = new System.Windows.Forms.Panel();
            this.Friday = new System.Windows.Forms.RadioButton();
            this.Thursday = new System.Windows.Forms.RadioButton();
            this.Wednesday = new System.Windows.Forms.RadioButton();
            this.Tuesday = new System.Windows.Forms.RadioButton();
            this.Monday = new System.Windows.Forms.RadioButton();
            this.BackupDayLabel = new System.Windows.Forms.Label();
            this.SaveSettingsPanel = new System.Windows.Forms.Panel();
            this.SaveSettingsBtn = new System.Windows.Forms.Button();
            this.ShutdownLabel = new System.Windows.Forms.Label();
            this.ShutdownCheckBox = new System.Windows.Forms.CheckBox();
            this.DailyTask = new System.Windows.Forms.RadioButton();
            this.WeeklyTask = new System.Windows.Forms.RadioButton();
            this.TaskSettingsPanel.SuspendLayout();
            this.BackupDayPanel.SuspendLayout();
            this.SaveSettingsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TaskSettingsPanel
            // 
            this.TaskSettingsPanel.Controls.Add(this.WeeklyTask);
            this.TaskSettingsPanel.Controls.Add(this.DailyTask);
            this.TaskSettingsPanel.Controls.Add(this.AutoBackupTimePicker);
            this.TaskSettingsPanel.Controls.Add(this.AutoBackupCheckBox);
            this.TaskSettingsPanel.Location = new System.Drawing.Point(3, 70);
            this.TaskSettingsPanel.Name = "TaskSettingsPanel";
            this.TaskSettingsPanel.Size = new System.Drawing.Size(430, 66);
            this.TaskSettingsPanel.TabIndex = 7;
            // 
            // AutoBackupTimePicker
            // 
            this.AutoBackupTimePicker.CustomFormat = "HH:mm:ss";
            this.AutoBackupTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.AutoBackupTimePicker.Location = new System.Drawing.Point(29, 20);
            this.AutoBackupTimePicker.Name = "AutoBackupTimePicker";
            this.AutoBackupTimePicker.ShowUpDown = true;
            this.AutoBackupTimePicker.Size = new System.Drawing.Size(94, 26);
            this.AutoBackupTimePicker.TabIndex = 2;
            // 
            // AutoBackupCheckBox
            // 
            this.AutoBackupCheckBox.AutoSize = true;
            this.AutoBackupCheckBox.Location = new System.Drawing.Point(11, 25);
            this.AutoBackupCheckBox.Name = "AutoBackupCheckBox";
            this.AutoBackupCheckBox.Size = new System.Drawing.Size(18, 17);
            this.AutoBackupCheckBox.TabIndex = 1;
            this.AutoBackupCheckBox.UseVisualStyleBackColor = true;
            this.AutoBackupCheckBox.CheckedChanged += new System.EventHandler(this.AutoBackupCheckBox_CheckedChanged);
            // 
            // BackupDayPanel
            // 
            this.BackupDayPanel.Controls.Add(this.Friday);
            this.BackupDayPanel.Controls.Add(this.Thursday);
            this.BackupDayPanel.Controls.Add(this.Wednesday);
            this.BackupDayPanel.Controls.Add(this.Tuesday);
            this.BackupDayPanel.Controls.Add(this.Monday);
            this.BackupDayPanel.Controls.Add(this.BackupDayLabel);
            this.BackupDayPanel.Location = new System.Drawing.Point(2, 5);
            this.BackupDayPanel.Name = "BackupDayPanel";
            this.BackupDayPanel.Size = new System.Drawing.Size(526, 62);
            this.BackupDayPanel.TabIndex = 8;
            // 
            // Friday
            // 
            this.Friday.AutoSize = true;
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
            // SaveSettingsPanel
            // 
            this.SaveSettingsPanel.Controls.Add(this.SaveSettingsBtn);
            this.SaveSettingsPanel.Controls.Add(this.ShutdownLabel);
            this.SaveSettingsPanel.Controls.Add(this.ShutdownCheckBox);
            this.SaveSettingsPanel.Location = new System.Drawing.Point(1, 140);
            this.SaveSettingsPanel.Name = "SaveSettingsPanel";
            this.SaveSettingsPanel.Size = new System.Drawing.Size(430, 59);
            this.SaveSettingsPanel.TabIndex = 9;
            // 
            // SaveSettingsBtn
            // 
            this.SaveSettingsBtn.Location = new System.Drawing.Point(232, 17);
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
            this.ShutdownLabel.Location = new System.Drawing.Point(34, 23);
            this.ShutdownLabel.Name = "ShutdownLabel";
            this.ShutdownLabel.Size = new System.Drawing.Size(162, 20);
            this.ShutdownLabel.TabIndex = 4;
            this.ShutdownLabel.Text = "Cu Shutdown la final";
            // 
            // ShutdownCheckBox
            // 
            this.ShutdownCheckBox.AutoSize = true;
            this.ShutdownCheckBox.Location = new System.Drawing.Point(13, 25);
            this.ShutdownCheckBox.Name = "ShutdownCheckBox";
            this.ShutdownCheckBox.Size = new System.Drawing.Size(18, 17);
            this.ShutdownCheckBox.TabIndex = 3;
            this.ShutdownCheckBox.UseVisualStyleBackColor = true;
            // 
            // DailyTask
            // 
            this.DailyTask.AutoSize = true;
            this.DailyTask.Location = new System.Drawing.Point(195, 20);
            this.DailyTask.Name = "DailyTask";
            this.DailyTask.Size = new System.Drawing.Size(69, 24);
            this.DailyTask.TabIndex = 3;
            this.DailyTask.TabStop = true;
            this.DailyTask.Text = "zilnic";
            this.DailyTask.UseVisualStyleBackColor = true;
            // 
            // WeeklyTask
            // 
            this.WeeklyTask.AutoSize = true;
            this.WeeklyTask.Location = new System.Drawing.Point(280, 20);
            this.WeeklyTask.Name = "WeeklyTask";
            this.WeeklyTask.Size = new System.Drawing.Size(116, 24);
            this.WeeklyTask.TabIndex = 4;
            this.WeeklyTask.TabStop = true;
            this.WeeklyTask.Text = "saptamanal";
            this.WeeklyTask.UseVisualStyleBackColor = true;
            // 
            // ChangeAutoBackupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 203);
            this.Controls.Add(this.SaveSettingsPanel);
            this.Controls.Add(this.BackupDayPanel);
            this.Controls.Add(this.TaskSettingsPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(450, 250);
            this.MinimumSize = new System.Drawing.Size(450, 250);
            this.Name = "ChangeAutoBackupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change Auto Backup Form";
            this.Load += new System.EventHandler(this.ChangeAutoBackupForm_Load);
            this.TaskSettingsPanel.ResumeLayout(false);
            this.TaskSettingsPanel.PerformLayout();
            this.BackupDayPanel.ResumeLayout(false);
            this.BackupDayPanel.PerformLayout();
            this.SaveSettingsPanel.ResumeLayout(false);
            this.SaveSettingsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TaskSettingsPanel;
        private System.Windows.Forms.DateTimePicker AutoBackupTimePicker;
        private System.Windows.Forms.CheckBox AutoBackupCheckBox;
        private System.Windows.Forms.Panel BackupDayPanel;
        private System.Windows.Forms.RadioButton Friday;
        private System.Windows.Forms.RadioButton Thursday;
        private System.Windows.Forms.RadioButton Wednesday;
        private System.Windows.Forms.RadioButton Tuesday;
        private System.Windows.Forms.RadioButton Monday;
        private System.Windows.Forms.Label BackupDayLabel;
        private System.Windows.Forms.Panel SaveSettingsPanel;
        private System.Windows.Forms.Button SaveSettingsBtn;
        private System.Windows.Forms.Label ShutdownLabel;
        private System.Windows.Forms.CheckBox ShutdownCheckBox;
        private System.Windows.Forms.RadioButton WeeklyTask;
        private System.Windows.Forms.RadioButton DailyTask;
    }
}