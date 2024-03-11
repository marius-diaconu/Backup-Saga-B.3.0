using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;
using ClassLibrary;

namespace SettingsApp
{
    static class Program
    {
        public static Tasks ts;
        public static BackupSettings bs;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Database.IsOpen)
            {
                Database.CheckIfTablesExists();

                ts = Tasks.Last();
                if (ts.Exists())
                {
                    string path = Assembly.GetEntryAssembly().Location;
                    string oldPath = !string.IsNullOrEmpty(ts.ExePath) ? ts.ExePath : null;
                    ts.ExePath = Path.Combine(Path.GetDirectoryName(path), App.backup_app);
                    ts.Save();

                    if (Convert.ToBoolean(ts.IsAuto) && !string.IsNullOrEmpty(ts.ExePath))
                    {
                        using (TaskService service = new TaskService())
                        {
                            if (!service.RootFolder.AllTasks.Any(t => t.Name == App.TaskName.ToString()))
                            {
                                ts.CreateTask();
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(oldPath) && oldPath != ts.ExePath)
                                {
                                    ts.DeleteTask();
                                    ts.CreateTask();
                                }
                            }
						
                            if (service.RootFolder.AllTasks.Any(t => t.Name == App.old_task))
                            {
                                TaskDefinition td = service.NewTask();
                                service.RootFolder.DeleteTask(App.old_task);
                            }
                        }
                    }
                }

                bs = BackupSettings.Last();

                if (bs.Exists() && ts.Exists())
                {
                    Application.Run(new MenuForm(bs, ts));
                }
                else
                {
                    Application.Run(new BackupSettingsForm(bs, ts));
                }
            }
        }
    }
}
