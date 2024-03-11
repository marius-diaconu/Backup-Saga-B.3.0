using System;
using System.Windows.Forms;
using ClassLibrary;

namespace SettingsApp
{
    public partial class ChangeBackupPathForm : Form
    {
        private BackupSettings BS;
        public ChangeBackupPathForm(BackupSettings bs)
        {
            BS = bs;
            InitializeComponent();
        }

        private void ChangeBackupPathForm_Load(object sender, EventArgs e)
        {
            BackupPathTextBox.Text = null;
        }

        private void BackupPathBtn_Click(object sender, EventArgs e)
        {
            BackupBrowserDialog.ShowDialog();
            BackupPathTextBox.Text = BackupBrowserDialog.SelectedPath;
        }

        private void SaveSettingsBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(BackupPathTextBox.Text))
            {
                MessageBox.Show(
                    $"Trebuie sa selectati locatia de backup pentru {App.name.ToUpper()}.{App.ver}, inainte de a salva setarile!", 
                    "Error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error
                );
            }
            else
            {
                string old_path = BS.ToPath.ToString();
                BS.ToPath = BackupPathTextBox.Text;
                Utility.DisplayNotification(
                    "Va rugam nu inchideti fereastra, asteptati actualizarea bazei de date cu noua locatie. " +
                    "La finalul actualizarii ferestra se va inchide automat. Multumim!", 
                    5000
                );

                Control ctrl = new Control
                {
                    UseWaitCursor = true
                };

                bool checkIfUpdateCompanyRecords = OnPathChange.UpdateCompanyRecords(BS, old_path);

                if (checkIfUpdateCompanyRecords)
                {
                    ctrl.UseWaitCursor = false;
                    BS.Save();
                    Close();
                }
            }
        }
    }
}
