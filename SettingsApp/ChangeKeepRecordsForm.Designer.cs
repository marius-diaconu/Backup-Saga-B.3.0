using ClassLibrary;

namespace SettingsApp
{
    partial class ChangeKeepRecordsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeKeepRecordsForm));
            this.KeepRecPanel = new System.Windows.Forms.Panel();
            this.SaveSettingsBtn = new System.Windows.Forms.Button();
            this.KeepDelCompInput = new System.Windows.Forms.NumericUpDown();
            this.KeepDelCompLabel = new System.Windows.Forms.Label();
            this.KeepRecPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KeepDelCompInput)).BeginInit();
            this.SuspendLayout();
            // 
            // KeepRecPanel
            // 
            this.KeepRecPanel.Controls.Add(this.SaveSettingsBtn);
            this.KeepRecPanel.Controls.Add(this.KeepDelCompInput);
            this.KeepRecPanel.Controls.Add(this.KeepDelCompLabel);
            this.KeepRecPanel.Location = new System.Drawing.Point(5, 5);
            this.KeepRecPanel.Name = "KeepRecPanel";
            this.KeepRecPanel.Size = new System.Drawing.Size(525, 92);
            this.KeepRecPanel.TabIndex = 6;
            // 
            // SaveSettingsBtn
            // 
            this.SaveSettingsBtn.Location = new System.Drawing.Point(290, 45);
            this.SaveSettingsBtn.Name = "SaveSettingsBtn";
            this.SaveSettingsBtn.Size = new System.Drawing.Size(200, 35);
            this.SaveSettingsBtn.TabIndex = 6;
            this.SaveSettingsBtn.Text = "Salveaza Setarile";
            this.SaveSettingsBtn.UseVisualStyleBackColor = true;
            this.SaveSettingsBtn.Click += new System.EventHandler(this.SaveSettingsBtn_Click);
            // 
            // KeepDelCompInput
            // 
            this.KeepDelCompInput.Location = new System.Drawing.Point(8, 51);
            this.KeepDelCompInput.Name = "KeepDelCompInput";
            this.KeepDelCompInput.Size = new System.Drawing.Size(165, 26);
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
            this.KeepDelCompLabel.Location = new System.Drawing.Point(4, 8);
            this.KeepDelCompLabel.Name = "KeepDelCompLabel";
            this.KeepDelCompLabel.Size = new System.Drawing.Size(525, 20);
            this.KeepDelCompLabel.TabIndex = 4;
            this.KeepDelCompLabel.Text = "Numarul de ani pentru pastrarea backup-ului firmelor sterse din " + App.name.ToUpper() + "." + App.ver;
            // 
            // ChangeKeepRecordsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 103);
            this.Controls.Add(this.KeepRecPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(550, 150);
            this.MinimumSize = new System.Drawing.Size(550, 150);
            this.Name = "ChangeKeepRecordsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = App.name + "." + App.ver + " Backup - Change Keep Records Form";
            this.Load += new System.EventHandler(this.ChangeKeepRecordsForm_Load);
            this.KeepRecPanel.ResumeLayout(false);
            this.KeepRecPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KeepDelCompInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel KeepRecPanel;
        private System.Windows.Forms.NumericUpDown KeepDelCompInput;
        private System.Windows.Forms.Label KeepDelCompLabel;
        private System.Windows.Forms.Button SaveSettingsBtn;
    }
}