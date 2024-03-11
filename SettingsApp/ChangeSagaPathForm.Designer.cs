using ClassLibrary;

namespace SettingsApp
{
    partial class ChangeSagaPathForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeSagaPathForm));
            this.SagaPathPanel = new System.Windows.Forms.Panel();
            this.SeveSettingsBtn = new System.Windows.Forms.Button();
            this.SagaPathBtn = new System.Windows.Forms.Button();
            this.SagaPathTextBox = new System.Windows.Forms.TextBox();
            this.SagaPathLabel = new System.Windows.Forms.Label();
            this.SagaBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SagaPathPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // SagaPathPanel
            // 
            this.SagaPathPanel.Controls.Add(this.SeveSettingsBtn);
            this.SagaPathPanel.Controls.Add(this.SagaPathBtn);
            this.SagaPathPanel.Controls.Add(this.SagaPathTextBox);
            this.SagaPathPanel.Controls.Add(this.SagaPathLabel);
            this.SagaPathPanel.Location = new System.Drawing.Point(5, 5);
            this.SagaPathPanel.Name = "SagaPathPanel";
            this.SagaPathPanel.Size = new System.Drawing.Size(410, 165);
            this.SagaPathPanel.TabIndex = 4;
            // 
            // SeveSettingsBtn
            // 
            this.SeveSettingsBtn.Location = new System.Drawing.Point(100, 105);
            this.SeveSettingsBtn.Name = "SeveSettingsBtn";
            this.SeveSettingsBtn.Size = new System.Drawing.Size(200, 40);
            this.SeveSettingsBtn.TabIndex = 3;
            this.SeveSettingsBtn.Text = "Salveaza Setarile";
            this.SeveSettingsBtn.UseVisualStyleBackColor = true;
            this.SeveSettingsBtn.Click += new System.EventHandler(this.SeveSettingsBtn_Click);
            // 
            // SagaPathBtn
            // 
            this.SagaPathBtn.Location = new System.Drawing.Point(285, 40);
            this.SagaPathBtn.Name = "SagaPathBtn";
            this.SagaPathBtn.Size = new System.Drawing.Size(100, 35);
            this.SagaPathBtn.TabIndex = 2;
            this.SagaPathBtn.Text = "Selecteaza";
            this.SagaPathBtn.UseVisualStyleBackColor = true;
            this.SagaPathBtn.Click += new System.EventHandler(this.SagaPathBtn_Click);
            // 
            // SagaPathTextBox
            // 
            this.SagaPathTextBox.Location = new System.Drawing.Point(25, 45);
            this.SagaPathTextBox.Name = "SagaPathTextBox";
            this.SagaPathTextBox.Size = new System.Drawing.Size(250, 26);
            this.SagaPathTextBox.TabIndex = 1;
            // 
            // SagaPathLabel
            // 
            this.SagaPathLabel.AutoSize = true;
            this.SagaPathLabel.Location = new System.Drawing.Point(70, 10);
            this.SagaPathLabel.Name = "SagaPathLabel";
            this.SagaPathLabel.Size = new System.Drawing.Size(228, 20);
            this.SagaPathLabel.TabIndex = 0;
            this.SagaPathLabel.Text = $"Selectati folderul {App.name.ToUpper()}.{App.ver}";
            // 
            // SagaBrowserDialog
            // 
            this.SagaBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // ChangeSagaPathForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 183);
            this.Controls.Add(this.SagaPathPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(445, 230);
            this.MinimumSize = new System.Drawing.Size(445, 230);
            this.Name = "ChangeSagaPathForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change " + App.name + "." + App.ver + " Path Form";
            this.Load += new System.EventHandler(this.ChangeSagaPathForm_Load);
            this.SagaPathPanel.ResumeLayout(false);
            this.SagaPathPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel SagaPathPanel;
        private System.Windows.Forms.Button SagaPathBtn;
        private System.Windows.Forms.TextBox SagaPathTextBox;
        private System.Windows.Forms.Label SagaPathLabel;
        private System.Windows.Forms.Button SeveSettingsBtn;
        private System.Windows.Forms.FolderBrowserDialog SagaBrowserDialog;
    }
}