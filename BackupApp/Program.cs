using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Win32.TaskScheduler;
using System.Management;
using ClassLibrary;

namespace BackupApp
{
    public static class Program
    {
        private static BackupSettings bs;
        private static Tasks ts;
        private static bool isBackupDone = false;
        private static bool isShutdown = false;
        private static int timer = 10000;
        private static int sec = timer / 1000;

        public static void Main()
        {
            Console.Title = $"{App.name.ToUpper()}.{App.ver} BACKUP APP - Version: {App.Version}";
            Credentials.DisplayCredentials(75);

            if (Database.IsOpen)
            {
                Database.CheckIfTablesExists();

                Utility.CreateLogFile();
                Utility.CreateErrorFile();

                // Task Section
                ts = Tasks.Last();
                if (ts.Exists())
                {
                    isShutdown = Convert.ToBoolean(ts.IsShutdown);
                    if (Convert.ToBoolean(ts.IsAuto))
                    {
                        using (TaskService service = new TaskService())
                        {
                            if (!service.RootFolder.AllTasks.Any(t => t.Name == App.TaskName.ToString()))
                            {
                                ts.ExePath = Assembly.GetEntryAssembly().Location;
                                ts.CreateTask();
                                ts.Save();
                            }
                            else
                            {
                                if (ts.ExePath != Assembly.GetEntryAssembly().Location)
                                {
                                    ts.ExePath = Assembly.GetEntryAssembly().Location;
                                    ts.DeleteTask();
                                    ts.CreateTask();
                                    ts.Save();
                                }
                            }
                            if (service.RootFolder.AllTasks.Any(t => t.Name == App.old_task))
                            {
                                TaskDefinition td = service.NewTask();
                                service.RootFolder.DeleteTask(App.old_task);
                            }
                        }
                    }
                    else
                    {
                        ts.ExePath = Assembly.GetEntryAssembly().Location;
                        ts.Save();
                    }
                }
                else
                {
                    ts.ExePath = Assembly.GetEntryAssembly().Location;
                    ts.Save();
                } // End of Task Section

                // BackupSettings Section
                bs = BackupSettings.Last();

                if (bs.Exists())
                {
                    ResourceFiles rf = ResourceFiles.Last();
                    if (!Directory.Exists(@bs.FromPath))
                    {
                        Utility.DisplayError(
                            $"=> Calea de acces catre directorul {App.name.ToUpper()}.{App.ver} s-a modificat. " +
                            "Va rugam deschideti fereastra de setari si faceti modificarile necesare."
                        );
                    }

                    if (!Directory.Exists(@bs.ToPath))
                    {
                        Utility.DisplayError(
                            $"=> Calea de acces catre directorul de Backup pentru {App.name.ToUpper()}.{App.ver} " +
                            "s-a modificat. Va rugam deschideti fereastra de setari si faceti modificarile necesare."
                        );
                    }

                    if (!Directory.Exists(Path.Combine(bs.FromPath, bs.SalvBd)))
                    {
                        if (rf.Exists())
                        {
                            if (Directory.Exists(Path.Combine(bs.FromPath, rf.SalvBd)))
                            {
                                bs.SalvBd = rf.SalvBd; ;
                                bs.Save();
                            }
                            else
                            {
                                Utility.DisplayError(
                                    $"=> Anumite fisiere si/sau dosare din {App.name.ToUpper()}.{App.ver} " +
                                    "s-au modificat. Va rugam contactati dezvoltatorul."
                                );
                            }
                        }
                    }

                    if (!File.Exists(Path.Combine(bs.FromPath, bs.FirmeDBF)))
                    {
                        if (rf.Exists())
                        {
                            if (File.Exists(Path.Combine(bs.FromPath, rf.FirmeDBF)))
                            {
                                bs.FirmeDBF = rf.FirmeDBF;
                                bs.Save();
                            }
                            else
                            {
                                Utility.DisplayError(
                                    $"=> Anumite fisiere si/sau dosare din {App.name.ToUpper()}.{App.ver} " +
                                    "s-au modificat. Va rugam contactati dezvoltatorul."
                                );
                            }
                        }
                    }

                    if (
                            Directory.Exists(Path.Combine(bs.ToPath, App.active)) && 
                            Directory.Exists(Path.Combine(bs.ToPath, App.salv_bd)) &&
                            !Directory.Exists(Path.Combine(bs.ToPath, App.saves))
                        )
                    {
                        Utility.InitWarning(
                            "Va rugam, nu inchideti aceasta fereastra, baza de date a programului de backup este in " +
                            "proces de actualizare. La finalul executiei, automat, va pornii procesul de backup. Va multumim!"
                        );

                        OnPathChange.UpdateCompanyRecords(bs);

                        Directory.Delete(Path.Combine(bs.ToPath, App.active), true);
                        Directory.Delete(Path.Combine(bs.ToPath, App.salv_bd), true);
                    }
                } // End of BackupSettings Section

                // Backup Section
                if ( 
                        Directory.Exists(@bs.FromPath) && 
                        Directory.Exists(@bs.ToPath) &&
                        Directory.Exists(Path.Combine(bs.FromPath, bs.SalvBd)) && 
                        File.Exists(Path.Combine(bs.FromPath, bs.FirmeDBF)) 
                    )
                {
                    timer = 30000;
                    sec = timer / 1000;

                    Utility.InitWarning(
                        $"=> IN {sec} DE SECUNDE INCEPE PROCESUL DE BACKUP, DACA DORITI INTRERUPEREA ACESTUIA, " +
                        "ESTE SUFICIENT SA INCHIDETI ACEASTA FEREASTRA."
                    );

                    Utility.Timer(timer);
                    timer = 10000;
                    sec = timer / 1000;

                    BackupAndCleaning.SetProperties(bs, ts);

                    BackupAndCleaning.GetCompanies();

                    if (BackupAndCleaning.BackupAll())
                    {
                        BackupAndCleaning.CleanSaga();
                        BackupAndCleaning.CleanSagaBackup();
                    }

                    BackupAndCleaning.RemoveDeletedCompanies();

                    if (Database.IsOpen)
                    {
                        Database.RefreshTables();

                        Utility.InitWarning(
                            "Va rugam asteptati, optimizarea tabele-lor in progres!"
                        );

                        Database.CloseConn();
                    }

                    StoreDatabase();

                    isBackupDone = true;
                }
                else
                {
                    Utility.InitError(
                        $"=> Este necesara setarea cailor de acces catre programul {App.name.ToUpper()}.{App.ver} si catre " +
                        $"directorul de Backup pentru {App.name.ToUpper()}.{App.ver}, inainte de a rula aceasta aplicatie."
                    );
                } // End of Backup Section
            }

            if (isBackupDone)
            {
                if (isShutdown)
                {
                    Utility.InitMessage(
                        $"=> Backup terminat cu succes, in {sec} sec, aceast program si PC-ul se vor autoinchide!"
                    );
                }
                else
                {
                    Utility.InitMessage(
                        $"=> Backup terminat cu succes, in {sec} sec, aceast program se va autoinchide!"
                    );
                }
            }
            else
            {
                if (Database.IsOpen) Database.CloseConn();

                if (isShutdown)
                {
                    Utility.InitMessage(
                        $"=> In {sec} sec, aceast program si PC-ul se vor autoinchide! Niciun Backup realizat!"
                    );
                }
                else
                {
                    Utility.InitMessage(
                        $"=> In {sec} sec, aceast program se va autoinchide! Niciun Backup realizat!"
                    );
                }
            }

            Utility.Timer(timer);

            if (isShutdown)
            {
                Shutdown();
            }

            Environment.Exit(0);
        }

        public static void Shutdown()
        {
            ManagementClass mcWin32 = new ManagementClass("Win32_OperatingSystem");
            mcWin32.Get();

            // You can't shutdown without security privileges
            mcWin32.Scope.Options.EnablePrivileges = true;
            ManagementBaseObject mboShutdownParams = mcWin32.GetMethodParameters("Win32Shutdown");

            // Flag 1 means we want to shut down the system. Use "2" to reboot.
            mboShutdownParams["Flags"] = "1";
            mboShutdownParams["Reserved"] = "0";

            foreach (ManagementObject manObj in mcWin32.GetInstances())
            {
                manObj.InvokeMethod("Win32Shutdown", mboShutdownParams, null);
            }
        }

        public static void StoreDatabase()
        {
            string db_resource_path = Path.Combine(
                App.resource_dir, 
                Path.Combine(
                    App.saga_dir, 
                    Path.Combine(App.db_dir, $"{App.db}.db")
                )
            );

            string store_db_dir = Path.Combine(bs.ToPath, App.db_dir);
            string store_db_path = Path.Combine(
                bs.ToPath, 
                Path.Combine(App.db_dir, $"{App.db}.db")
            );

            if (File.Exists(@store_db_path))
            {
                File.Delete(@store_db_path);
                File.Copy(@db_resource_path, @store_db_path);
            }
            else
            {
                Utility.DirExistsCheckOrCreate(store_db_dir);
                Utility.ChangePermissions(store_db_dir);
                File.Copy(@db_resource_path, @store_db_path);
            }

            return;
        }
    }
}
