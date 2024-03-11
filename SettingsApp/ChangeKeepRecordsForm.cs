using System;
using System.Windows.Forms;
using ClassLibrary;

namespace SettingsApp
{
    public partial class ChangeKeepRecordsForm : Form
    {
        private BackupSettings BS;
        public ChangeKeepRecordsForm(BackupSettings bs)
        {
            BS = bs;
            InitializeComponent();
        }

        private void ChangeKeepRecordsForm_Load(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(BS.KeepDC)) KeepDelCompInput.Value = BS.KeepDC;
        }

        private void SaveSettingsBtn_Click(object sender, EventArgs e)
        {
            if ((int)KeepDelCompInput.Value == 0)
            {
                MessageBox.Show(
                    $"Numarul de ani, pentru care programul pastreaza copii de rezerva, " +
                    $"aferente firmelor sterse din {App.name.ToUpper()}.{App.ver}, nu poate fi egal cu zero!", 
                    "Warning", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning
                );
            }
            else
            {
                BS.KeepDC = (int)KeepDelCompInput.Value;
            }

            if (Convert.ToBoolean(BS.KeepRec) && Convert.ToBoolean(BS.KeepSalvBd) && Convert.ToBoolean(BS.KeepDC))
            {
                BS.Save();
            }

            Close();
        }

        private void KeepRecInput_ValueChanged(object sender, EventArgs e)
        {
        }

        private void KeepSalvBdInput_ValueChanged(object sender, EventArgs e)
        {
        }

        private void KeepDelCompInput_ValueChanged(object sender, EventArgs e)
        {
            if (KeepDelCompInput.Value < 1)
            {
                MessageBox.Show(
                    "Aceasta valoare nu poate fi mai mica de 1!", 
                    "Warning", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning
                );
                KeepDelCompInput.Value = 1;
            }
        }
    }
}
