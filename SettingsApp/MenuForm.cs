using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using ClassLibrary;

namespace SettingsApp
{
    public partial class MenuForm : Form
    {
        private BackupSettings BS;
        private Tasks TS;
        private ResourceFiles RF;
        public MenuForm(BackupSettings bs, Tasks ts)
        {
            BS = bs;
            TS = ts;

            ResourceFiles rf = ResourceFiles.Last();
            RF = rf;

            InitializeComponent();
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {

        }

        private void MailMe_Click(object sender, EventArgs e)
        {
            Process.Start("mailto:marius.diaconu76@gmail.com");
        }

        private void FollowMe_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.facebook.com/backup.saga");
        }

        private void ChangeSagaPathBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(BS.FromPath) && Directory.Exists(BS.FromPath))
            {
                MessageBox.Show(
                    $"Aceasta functionalitate va disponibila doar in situatia in care ati mutat Programul " +
                    $"{App.name.ToUpper()}.{App.ver} dintr-o locatia in alta. Daca ati copiat Programul " +
                    $"{App.name.ToUpper()}.{App.ver} dintr-o locatia in alta, fie stergeti vechiul program, fie il redenumiti!",
                    "Warning", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning
                );
            }
            else
            {
                var ChangeSagaPathForm = new ChangeSagaPathForm(BS);
                ChangeSagaPathForm.Show();
            }
        }

        private void ChangeBackupPathBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(BS.ToPath) && Directory.Exists(BS.ToPath))
            {
                MessageBox.Show(
                    "Aceasta functionalitate este disponibila doar in situatia in care ati mutat dosarul de backup la o noua locatie. " +
                    "Daca ati copiat dosarul dintr-o locatie in alta, fie stergeti vechiul dosar, fie il redenumiti.",
                    "Warning", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning
                );
            }
            else
            {
                var ChangeBackupPathForm = new ChangeBackupPathForm(BS);
                ChangeBackupPathForm.Show();
            }
        }

        private void ChangeKeepRecBtn_Click(object sender, EventArgs e)
        {
            var ChangeKeepRecordsForm = new ChangeKeepRecordsForm(BS);
            ChangeKeepRecordsForm.Show();
        }

        private void ChangeAutoBackupBtn_Click(object sender, EventArgs e)
        {
            var ChangeAutoBackupForm = new ChangeAutoBackupForm(TS, BS);
            ChangeAutoBackupForm.Show();
        }

        private void LogFileBtn_Click(object sender, EventArgs e)
        {
            Process.Start(Path.Combine(RF.ResourcePath, RF.LogFileName));
        }

        private void ErrorFileBtn_Click(object sender, EventArgs e)
        {
            Process.Start(Path.Combine(RF.ResourcePath, RF.ErrorFileName));
        }

        private void HelpBtn_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.youtube.com/playlist?list=PLn9QQut7Ug16cc3Gg_X0_odbkPHH2iUsw");
        }

        private void StartManualBackup_Click(object sender, EventArgs e)
        {
            string path = Assembly.GetEntryAssembly().Location;
            Process.Start(Path.Combine(Path.GetDirectoryName(path), App.backup_app));
        }
    }
}
