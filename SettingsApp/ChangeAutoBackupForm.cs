using System;
using System.Linq;
using Microsoft.Win32.TaskScheduler;
using System.Windows.Forms;
using ClassLibrary;

namespace SettingsApp
{
    public partial class ChangeAutoBackupForm : Form
    {
        private Tasks TS;
        private BackupSettings BS;
        public ChangeAutoBackupForm(Tasks ts, BackupSettings bs)
        {
            TS = ts;
            BS = bs;
            InitializeComponent();
        }

        private void ChangeAutoBackupForm_Load(object sender, EventArgs e)
        {
            if (BS.BackupDay == "Monday") Monday.Checked = true;
            if (BS.BackupDay == "Tuesday") Tuesday.Checked = true;
            if (BS.BackupDay == "Wednesday") Wednesday.Checked = true;
            if (BS.BackupDay == "Thursday") Thursday.Checked = true;
            if (BS.BackupDay == "Friday") Friday.Checked = true;

            if (TS.TaskType == Tasks.Type.DAILY.ToString()) DailyTask.Checked = true;
            if (TS.TaskType == Tasks.Type.WEEKLY.ToString()) WeeklyTask.Checked = true;

            AutoBackupCheckBox.Checked = Convert.ToBoolean(TS.IsAuto);
            if (Convert.ToBoolean(TS.IsAuto)) AutoBackupTimePicker.Value = DateTime.Parse(TS.TaskTime);
            ShutdownCheckBox.Checked = Convert.ToBoolean(TS.IsShutdown);
        }

        private void AutoBackupCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AutoBackupCheckBox.Checked)
            {
                if (!string.IsNullOrEmpty(TS.TaskTime)) AutoBackupTimePicker.Value = DateTime.Parse(TS.TaskTime);
            }
        }

        private void SaveSettingsBtn_Click(object sender, EventArgs e)
        {
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
                    $"Trebuie sa alegeti o zi din saptamana pentru salvarea tuturor " +
                    $"datelor firmelor in {App.name.ToUpper()}.{App.ver}",
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
                    BS.KeepRec = 6;
                }

                if (WeeklyTask.Checked)
                {
                    TS.TaskType = Tasks.Type.WEEKLY.ToString();
                    BS.KeepRec = 0;
                }
            }
            else
            {
                MessageBox.Show(
                    "Trebuie sa alegeti una dintre cele doua optiuni pentru " +
                    "Tipul backup-ului: zilnic sau saptamanal!",
                    "Warning", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning
                );
                return;
            }

            TS.TaskWeekDay = BS.BackupDay;
            TS.IsAuto = AutoBackupCheckBox.Checked ? 1 : 0;

            if (!Convert.ToBoolean(TS.IsAuto))
            {
                using (TaskService service = new TaskService())
                {
                    if (service.RootFolder.AllTasks.Any(t => t.Name == App.TaskName.ToString()))
                    {
                        TS.DeleteTask();
                    }
                    if (service.RootFolder.AllTasks.Any(t => t.Name == App.old_task))
                    {
                        TaskDefinition td = service.NewTask();
                        service.RootFolder.DeleteTask(App.old_task);
                    }
                }
            }

            DateTime time = DateTime.Parse(AutoBackupTimePicker.Value.ToString());
            TS.TaskTime = time.ToString("HH:mm:ss");

            if (Convert.ToBoolean(TS.IsAuto) && !string.IsNullOrEmpty(TS.ExePath))
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

            TS.IsShutdown = ShutdownCheckBox.Checked ? 1 : 0;
            if (!string.IsNullOrEmpty(BS.BackupDay)) BS.Save();
            TS.Save();
            Close();
        }
    }
}
