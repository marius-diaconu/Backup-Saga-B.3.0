using System;
using System.Windows.Forms;
using ClassLibrary;

namespace SettingsApp
{
    public partial class ChangeSagaPathForm : Form
    {
        private BackupSettings BS;

        public ChangeSagaPathForm(BackupSettings bs)
        {
            BS = bs;
            InitializeComponent();
        }

        private void ChangeSagaPathForm_Load(object sender, EventArgs e)
        {
            SagaPathTextBox.Text = null;
        }

        private void SagaPathBtn_Click(object sender, EventArgs e)
        {
            SagaBrowserDialog.ShowDialog();
            SagaPathTextBox.Text = SagaBrowserDialog.SelectedPath;
        }

        private void SeveSettingsBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SagaPathTextBox.Text))
            {
                MessageBox.Show(
                    $"Calea de acces catre folderul {App.name.ToUpper()}.{App.ver} nu poate fi goala!", 
                    "Error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
            else
            {
                BS.FromPath = SagaPathTextBox.Text;
                BS.Save();
                Close();
            }
        }
    }
}
