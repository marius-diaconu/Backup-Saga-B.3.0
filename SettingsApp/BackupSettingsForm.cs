using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;
using ClassLibrary;

namespace SettingsApp
{
    public partial class BackupSettingsForm : Form
    {
        private BackupSettings BS;
        private Tasks TS;

        public BackupSettingsForm(BackupSettings bs, Tasks ts)
        {
            BS = bs;
            TS = ts;
            InitializeComponent();
        }

        private void BackupSettingsForm_Load(object sender, EventArgs e)
        {
            if (BS.Exists())
            {
                if (!string.IsNullOrEmpty(BS.FromPath) && Directory.Exists(BS.FromPath)) SagaPathTextBox.Text = BS.FromPath;
                else SagaPathTextBox.Text = null;

                if (!string.IsNullOrEmpty(BS.ToPath) && Directory.Exists(BS.ToPath)) BackupPathTextBox.Text = BS.ToPath;
                else BackupPathTextBox.Text = null;

                if (Convert.ToBoolean(BS.KeepDC)) KeepDelCompInput.Value = BS.KeepDC;
                if (!string.IsNullOrEmpty(BS.BackupDay))
                {
                    if (BS.BackupDay == "Monday") Monday.Checked = true;
                    if (BS.BackupDay == "Tuesday") Tuesday.Checked = true;
                    if (BS.BackupDay == "Wednesday") Wednesday.Checked = true;
                    if (BS.BackupDay == "Thursday") Thursday.Checked = true;
                    if (BS.BackupDay == "Friday") Friday.Checked = true;
                }

                if (TS.Exists())
                {
                    if (TS.TaskType == Tasks.Type.DAILY.ToString()) DailyTask.Checked = true;
                    if (TS.TaskType == Tasks.Type.WEEKLY.ToString()) WeeklyTask.Checked = true;

                    AutoBackupCheckBox.Checked = Convert.ToBoolean(TS.IsAuto);
                    if (!string.IsNullOrEmpty(TS.TaskTime)) AutoBackupTimePicker.Value = DateTime.Parse(TS.TaskTime);

                    ShutdownCheckBox.Checked = Convert.ToBoolean(TS.IsShutdown);
                }
            }
        }

        private void MailMe_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:marius.diaconu76@gmail.com");
        }

        private void FollowMe_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/backup.saga");
        }

        private void SagaPathTextBox_TextChanged(object sender, EventArgs e)
        {
            ResourceFiles rf = ResourceFiles.Last();
            string saga_path = SagaPathTextBox.Text.ToString();

            if (Directory.Exists(Path.Combine(saga_path, App.salv_bd))) BS.SalvBd = App.salv_bd;
            else
            {
                if (Directory.Exists(Path.Combine(saga_path, rf.SalvBd))) BS.SalvBd = rf.SalvBd;
            }

            if (!Directory.Exists(Path.Combine(saga_path, App.salv_bd)) && !Directory.Exists(Path.Combine(saga_path, rf.SalvBd)))
            {
                Utility.DirExistsCheckOrCreate(Path.Combine(saga_path, rf.SalvBd));
                BS.SalvBd = rf.SalvBd;
            }

            if (File.Exists(Path.Combine(saga_path, App.firmeDbf))) BS.FirmeDBF = App.firmeDbf;
            else
            {
                if (File.Exists(Path.Combine(saga_path, rf.FirmeDBF))) BS.FirmeDBF = rf.FirmeDBF;
            }
        }

        private void SagaPathBtn_Click(object sender, EventArgs e)
        {
            SagaBrowserDialog.ShowDialog();
            string saga_path = SagaBrowserDialog.SelectedPath.ToString();
            if (!string.IsNullOrEmpty(saga_path) && File.Exists(Path.Combine(saga_path, App.saga_exe)))
            {
                SagaPathTextBox.Text = SagaBrowserDialog.SelectedPath;
            }
            else
            {
                MessageBox.Show(
                    $"Acesta aplicatie apartine programului de contabilitate {App.name.ToUpper()}.{App.ver}", 
                    "Error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error
                );
            }
        }

        private void BackupPathTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void BackupPathBtn_Click(object sender, EventArgs e)
        {
            BackupBrowserDialog.ShowDialog();
            Utility.DirExistsCheckOrCreate(
                Path.Combine(
                    BackupBrowserDialog.SelectedPath, 
                    Path.Combine(App.parent_dir, App.backup_dir)
                )
            );

            Utility.ChangePermissions(
                Path.Combine(
                    BackupBrowserDialog.SelectedPath, 
                    Path.Combine(App.parent_dir, App.backup_dir)
                )
            );

            BackupPathTextBox.Text = Path.Combine(
                BackupBrowserDialog.SelectedPath, 
                Path.Combine(App.parent_dir, App.backup_dir)
            );
        }

        private void KeepDelCompInput_ValueChanged(object sender, EventArgs e)
        {
            if (KeepDelCompInput.Value < 1)
            {
                KeepDelCompInput.Value = 1;

                MessageBox.Show(
                    "Aceasta valoare nu poate fi mai mica de 1!", 
                    "Warning", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning
                );
            }
        }

        private void AutoBackupCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void AutoBackupTimePicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void SaveSettingsBtn_Click(object sender, EventArgs e)
        {
            BS.KeepSalvBd = 30;
            if (string.IsNullOrEmpty(SagaPathTextBox.Text))
            {
                MessageBox.Show(
                    $"Trebuie sa selectati calea de acces catre dosarl {App.name.ToUpper()}.{App.ver}, inainte de a salva setarile!", 
                    "Error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error
                );
                return;
            }
            else
            {
                BS.FromPath = SagaPathTextBox.Text;
            }

            if (string.IsNullOrEmpty(BackupPathTextBox.Text))
            {
                MessageBox.Show(
                    $"Trebuie sa selectati locatia de backup pentru {App.name.ToUpper()}.{App.ver}, inainte de a salva setarile!", 
                    "Error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error
                );
                return;
            }
            else
            {
                BS.ToPath = BackupPathTextBox.Text;
            }

            if ((int)KeepDelCompInput.Value == 0)
            {
                MessageBox.Show(
                    "Numarul de ani, pentru care programul pastreaza copii de rezerva, " +
                    $"pentru firmele sterse din {App.name.ToUpper()}.{App.ver}, nu poate fi egal cu zero!", 
                    "Warning", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning
                );
                return;
            }
            else
            {
                BS.KeepDC = (int)KeepDelCompInput.Value;
            }

            if (Monday.Checked || Tuesday.Checked || Wednesday.Checked || Thursday.Checked || Friday.Checked)
            {
                if (Monday.Checked) BS.BackupDay = "Monday";
                if (Tuesday.Checked) BS.BackupDay = "Tuesday";
                if (Wednesday.Checked) BS.BackupDay = "Wednesday";
                if (Thursday.Checked) BS.BackupDay = "Thursday";
                if (Friday.Checked) BS.BackupDay = "Friday";
            }
            else
            {
                MessageBox.Show(
                    $"Trebuie sa alegeti o zi din saptamana pentru salvarea tuturor datelor firmelor in {App.name.ToUpper()}.{App.ver}",
                    "Warning", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (DailyTask.Checked || WeeklyTask.Checked)
            {
                if (DailyTask.Checked)
                {
                    TS.TaskType = Tasks.Type.DAILY.ToString();
                    BS.KeepRec = 7;
                }

                if (WeeklyTask.Checked)
                {
                    TS.TaskType = Tasks.Type.WEEKLY.ToString();
                    BS.KeepRec = 4;
                }
            }
            else
            {
                MessageBox.Show(
                    "Trebuie sa alegeti una dintre cele doua optiuni pentru Tipul backup-ului: zilnic sau saptamanal!",
                    "Warning", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning
                );
                return;
            }

            TS.TaskWeekDay = BS.BackupDay;
            TS.IsAuto = AutoBackupCheckBox.Checked ? 1 : 0;
            DateTime time = DateTime.Parse(AutoBackupTimePicker.Value.ToString());
            TS.TaskTime = AutoBackupCheckBox.Checked ? time.ToString("HH:mm:ss") : null;
            TS.IsShutdown = ShutdownCheckBox.Checked ? 1 : 0;
            string path = Assembly.GetEntryAssembly().Location;
            TS.ExePath = Path.Combine(Path.GetDirectoryName(path), App.backup_app);
            
            if (Convert.ToBoolean(TS.IsAuto))
            {
                using (TaskService service = new TaskService())
                {
                    if (!service.RootFolder.AllTasks.Any(t => t.Name == App.TaskName.ToString()))
                    {
                        TS.CreateTask();
                    }
                    else
                    {
                        TS.DeleteTask();
                        TS.CreateTask();
                    }
                    if (service.RootFolder.AllTasks.Any(t => t.Name == App.old_task))
                    {
                        TaskDefinition td = service.NewTask();
                        service.RootFolder.DeleteTask(App.old_task);
                    }
                }
            }

            if (!string.IsNullOrEmpty(BS.FromPath) && !string.IsNullOrEmpty(BS.SalvBd) && !string.IsNullOrEmpty(BS.FirmeDBF) &&
                !string.IsNullOrEmpty(BS.ToPath) && Convert.ToBoolean(BS.KeepDC) && !string.IsNullOrEmpty(BS.BackupDay))
            {
                BS.Save();
                TS.Save();
            }

            Close();
        } // end of SaveSettingsBtn_Click method
    } // end of BackupSettingsForm class
}
