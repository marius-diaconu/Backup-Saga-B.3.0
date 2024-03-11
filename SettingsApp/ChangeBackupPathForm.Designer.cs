using ClassLibrary;

namespace SettingsApp
{
    partial class ChangeBackupPathForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeBackupPathForm));
            this.BackupPathPanel = new System.Windows.Forms.Panel();
            this.SaveSettingsBtn = new System.Windows.Forms.Button();
            this.BackupPathBtn = new System.Windows.Forms.Button();
            this.BackupPathTextBox = new System.Windows.Forms.TextBox();
            this.BackupPathLabel = new System.Windows.Forms.Label();
            this.BackupBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.BackupPathPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // BackupPathPanel
            // 
            this.BackupPathPanel.Controls.Add(this.SaveSettingsBtn);
            this.BackupPathPanel.Controls.Add(this.BackupPathBtn);
            this.BackupPathPanel.Controls.Add(this.BackupPathTextBox);
            this.BackupPathPanel.Controls.Add(this.BackupPathLabel);
            this.BackupPathPanel.Location = new System.Drawing.Point(5, 5);
            this.BackupPathPanel.Name = "BackupPathPanel";
            this.BackupPathPanel.Size = new System.Drawing.Size(392, 155);
            this.BackupPathPanel.TabIndex = 5;
            // 
            // SaveSettingsBtn
            // 
            this.SaveSettingsBtn.Location = new System.Drawing.Point(75, 100);
            this.SaveSettingsBtn.Name = "SaveSettingsBtn";
            this.SaveSettingsBtn.Size = new System.Drawing.Size(200, 40);
            this.SaveSettingsBtn.TabIndex = 10;
            this.SaveSettingsBtn.Text = "Salveaza Setarile";
            this.SaveSettingsBtn.UseVisualStyleBackColor = true;
            this.SaveSettingsBtn.Click += new System.EventHandler(this.SaveSettingsBtn_Click);
            // 
            // BackupPathBtn
            // 
            this.BackupPathBtn.Location = new System.Drawing.Point(250, 40);
            this.BackupPathBtn.Name = "BackupPathBtn";
            this.BackupPathBtn.Size = new System.Drawing.Size(100, 35);
            this.BackupPathBtn.TabIndex = 9;
            this.BackupPathBtn.Text = "Selecteaza";
            this.BackupPathBtn.UseVisualStyleBackColor = true;
            this.BackupPathBtn.Click += new System.EventHandler(this.BackupPathBtn_Click);
            // 
            // BackupPathTextBox
            // 
            this.BackupPathTextBox.Location = new System.Drawing.Point(25, 45);
            this.BackupPathTextBox.Name = "BackupPathTextBox";
            this.BackupPathTextBox.Size = new System.Drawing.Size(220, 26);
            this.BackupPathTextBox.TabIndex = 8;
            // 
            // BackupPathLabel
            // 
            this.BackupPathLabel.AutoSize = true;
            this.BackupPathLabel.Location = new System.Drawing.Point(25, 10);
            this.BackupPathLabel.Name = "BackupPathLabel";
            this.BackupPathLabel.Size = new System.Drawing.Size(355, 20);
            this.BackupPathLabel.TabIndex = 7;
            this.BackupPathLabel.Text = $"Selectati locatia de backup pentru {App.name.ToUpper()}.{App.ver}";
            // 
            // BackupBrowserDialog
            // 
            this.BackupBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // ChangeBackupPathForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 168);
            this.Controls.Add(this.BackupPathPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(450, 215);
            this.MinimumSize = new System.Drawing.Size(450, 215);
            this.Name = "ChangeBackupPathForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change " + App.name + "." + App.ver + " Backup Path Form";
            this.Load += new System.EventHandler(this.ChangeBackupPathForm_Load);
            this.BackupPathPanel.ResumeLayout(false);
            this.BackupPathPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel BackupPathPanel;
        private System.Windows.Forms.Button BackupPathBtn;
        private System.Windows.Forms.TextBox BackupPathTextBox;
        private System.Windows.Forms.Label BackupPathLabel;
        private System.Windows.Forms.Button SaveSettingsBtn;
        private System.Windows.Forms.FolderBrowserDialog BackupBrowserDialog;
    }
}