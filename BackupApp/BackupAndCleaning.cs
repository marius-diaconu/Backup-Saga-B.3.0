using System;
using System.Linq;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using ClassLibrary;

namespace BackupApp
{
    public static class BackupAndCleaning
    {
        public static string FilePath { get; set; }
        public static string FileName { get; set; }

        private static BackupSettings bs;
        private static Tasks ts;

        private static Regex regex;

        public static void SetProperties(BackupSettings backupSettings, Tasks task)
        {
            regex = new Regex(@"^[a-zA-Z0-9\.\-\& ]+$");
            bs = backupSettings;
            ts = task;
            FilePath = bs.FromPath;
            FileName = bs.FirmeDBF;
        }

        public static void GetCompanies()
        {
            Utility.DisplayMessage("=> Popularea tabelului Companies in progres, va rugam asteptati!");

            var dbfResults = FirmeDbf.Get(FilePath, FileName);

            dbfResults = dbfResults.Where(elem => elem.CuiFirma != null).ToList();

            foreach (var result in dbfResults)
            {
                Company db_company = Company.FindOrFail(result.CodFirma, result.CuiFirma);

                if (db_company.Exists())
                {
                    string compName = db_company.NumeFirma.ToString();

                    string dbfName = regex.IsMatch(Utility.Trim(result.NumeFirma)) ? 
                        Utility.Trim(result.NumeFirma.ToString()) : Utility.Trim(result.CuiFirma.ToString());

                    if (Convert.ToBoolean(string.Compare(compName, dbfName)))
                    {
                        _ = UpdateCompany(result, db_company);

                        Utility.DisplayMessage(
                            $"=> Actualizez firma: {dbfName}, schimb numele din: {compName} in: {dbfName}"
                        );
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    Company deleted_company = Company.Trashed(result.CuiFirma);

                    if (deleted_company.Exists())
                    {
                        string comp_name = deleted_company.NumeFirma.ToString();

                        string dbf_name = regex.IsMatch(result.NumeFirma) ? 
                            Utility.Trim(result.NumeFirma.ToString()) : Utility.Trim(result.CuiFirma.ToString());

                        if ( Convert.ToBoolean(string.Compare(deleted_company.CodFirma.ToString(), Utility.Trim(result.CodFirma.ToString()))) || 
                            Convert.ToBoolean(string.Compare(comp_name, dbf_name)) )
                        {
                            if (RestoreDeletedCompany(deleted_company))
                            {
                                deleted_company.Restore();
                            }

                            _ = UpdateCompany(result, deleted_company);
                        }
                        else
                        {
                            if (RestoreDeletedCompany(deleted_company))
                            {
                                deleted_company.Restore();
                            }
                        }

                        Utility.DisplayMessage($"=> Restaurez backup-ul firmei: {deleted_company.NumeFirma}");
                    }
                    else
                    {
                        db_company.CodFirma = Utility.Trim(result.CodFirma);
                        db_company.NumeFirma = regex.IsMatch(Utility.Trim(result.NumeFirma)) ? 
                            Utility.Trim(result.NumeFirma) : Utility.Trim(result.CuiFirma);

                        db_company.CuiFirma = Utility.Trim(result.CuiFirma);
                        db_company.RegComFirma = Utility.Trim(result.RegComFirma);
                        db_company.Guid = Company.GenerateGuid(16, true);
                        db_company.CreatedAt = Utility.GetTimestamp();
                        db_company.Save();

                        Utility.DisplayMessage(
                            $"=> Inregistrez firma: {(regex.IsMatch(Utility.Trim(result.NumeFirma)) ? Utility.Trim(result.NumeFirma) : Utility.Trim(result.CuiFirma))}"
                        );
                    }
                }
            }

            if (dbfResults.Count > 0) Utility.DisplayMessage("=> Popularea tabelului Companies s-a incheiat cu succes!");

            List<Company> toDelete = new List<Company>();
            List<Company> companyResults = Company.Get();

            if ((dbfResults.Count > 0 || dbfResults.Count == 0) && companyResults.Count > 0)
            {
                toDelete = companyResults.Where(
                    comp => !dbfResults.Any(
                        dbf => dbf.CodFirma == comp.CodFirma && dbf.CuiFirma == comp.CuiFirma
                    )
                ).ToList();
            }

            if (toDelete.Count > 0)
            {
                foreach (var comp in toDelete)
                {
                    if (MoveDeletedCompany(comp))
                    {
                        comp.Delete();
                    }

                    Utility.DisplayMessage(
                        $"=> Mut firma: {comp.NumeFirma} din dosarul: '{ App.saves}' in dosarul: '{App.deleted}', " +
                        $"deoarece aceasta firma nu mai exista in {App.name.ToUpper()}.{App.ver}"
                    );
                }
            }

            return;
        } // end of GetCompanies method

        public static bool BackupAll()
        {
            List<Company> results = Company.Get();

            if (results.Count > 0)
            {
                foreach(Company result in results)
                {
                    BackupCompany(result);
                }
                return true;
            }

            return false;
        } // end of BackupAll method

        private static bool BackupCompany(Company company)
        {
            CompanyRecords comp_rec;
            var innerDir = Path.Combine(bs.ToPath, App.saves);
            var compDir = Path.Combine(innerDir, $"{company.CodFirma} - {company.NumeFirma}");
            var sagaSalvBd = Path.Combine(bs.FromPath, Path.Combine(bs.SalvBd, company.CodFirma));

            try
            {
                _ = Utility.DirExistsCheckOrCreate(innerDir);
                _ = Utility.ChangePermissions(innerDir);

                _ = Utility.DirExistsCheckOrCreate(compDir);
                _ = Utility.ChangePermissions(compDir);

                _ = Utility.DirExistsCheckOrCreate(sagaSalvBd);
                _ = Utility.ChangePermissions(sagaSalvBd);

                FileInfo companySalvBdFile = GetCompanySalvBdLastBackup(company);

                if (companySalvBdFile == null)
                {
                    ZipFile.CreateFromDirectory(
                        Path.Combine(bs.FromPath, company.CodFirma),
                        Path.Combine(sagaSalvBd, Utility.GetDate() + ".zip")
                    );

                    ZipFile.CreateFromDirectory(
                        Path.Combine(bs.FromPath, company.CodFirma),
                        Path.Combine(compDir, Utility.GetDate() + ".zip")
                    );

                    companySalvBdFile = new FileInfo(Path.Combine(compDir, $"{Utility.GetDate()}.zip"));

                    comp_rec = new CompanyRecords
                    {
                        CompanyGuid = company.Guid,
                        Filepath = Path.Combine(compDir, Utility.GetDate() + ".zip"),
                        Filename = Utility.GetDate() + ".zip",
                        CreatedAt = Utility.GetTimestamp()
                    };
                    comp_rec.Save();

                    Utility.DisplayMessage($"=> Arhivez fisierele firmei: {company.NumeFirma} si le salvez in: {compDir}");

                    return true;
                }

                if (ts.TaskType == Tasks.Type.DAILY.ToString())
                {
                    if (DateTime.Now.DayOfWeek.ToString() == bs.BackupDay ||
                        DateTime.Now.Subtract(companySalvBdFile.CreationTime).Days >= 7)
                    {
                        if (File.Exists(Path.Combine(sagaSalvBd, Utility.GetDate() + ".zip")))
                        {
                            File.Delete(Path.Combine(sagaSalvBd, Utility.GetDate() + ".zip"));

                            ZipFile.CreateFromDirectory(
                                Path.Combine(bs.FromPath, company.CodFirma),
                                Path.Combine(sagaSalvBd, Utility.GetDate() + ".zip")
                            );
                        }
                        else
                        {
                            ZipFile.CreateFromDirectory(
                                Path.Combine(bs.FromPath, company.CodFirma),
                                Path.Combine(sagaSalvBd, Utility.GetDate() + ".zip")
                            );
                        }

                        Utility.DisplayMessage(
                            $"=> Arhivez fisierele firmei: {company.NumeFirma} si le salvez in: {sagaSalvBd}"
                        );
                    }

                    if (File.Exists(Path.Combine(compDir, Utility.GetDate() + ".zip")))
                    {
                        File.Delete(Path.Combine(compDir, Utility.GetDate() + ".zip"));

                        comp_rec = CompanyRecords.FindByDate(company.Guid, DateTime.Now.ToString("yyyy-MM-dd"));
                        if (comp_rec.Exists())
                        {
                            comp_rec.CreatedAt = Utility.GetTimestamp();
                            comp_rec.Save();
                        }
                        else
                        {
                            comp_rec.CompanyGuid = company.Guid;
                            comp_rec.Filepath = Path.Combine(compDir, Utility.GetDate() + ".zip");
                            comp_rec.Filename = Utility.GetDate() + ".zip";
                            comp_rec.CreatedAt = Utility.GetTimestamp();
                            comp_rec.Save();
                        }
                    }
                    else
                    {
                        comp_rec = new CompanyRecords
                        {
                            CompanyGuid = company.Guid,
                            Filepath = Path.Combine(compDir, Utility.GetDate() + ".zip"),
                            Filename = Utility.GetDate() + ".zip",
                            CreatedAt = Utility.GetTimestamp()
                        };
                        comp_rec.Save();
                    }

                    ZipFile.CreateFromDirectory(
                        Path.Combine(bs.FromPath, company.CodFirma),
                        Path.Combine(compDir, Utility.GetDate() + ".zip")
                    );

                    Utility.DisplayMessage($"=> Arhivez fisierele firmei: {company.NumeFirma} si le salvez in: {compDir}");
                }

                if (ts.TaskType == Tasks.Type.WEEKLY.ToString())
                {
                    if (DateTime.Now.DayOfWeek.ToString() == bs.BackupDay ||
                        DateTime.Now.Subtract(companySalvBdFile.CreationTime).Days >= 7)
                    {

                        if (File.Exists(Path.Combine(sagaSalvBd, Utility.GetDate() + ".zip")))
                        {
                            File.Delete(Path.Combine(sagaSalvBd, Utility.GetDate() + ".zip"));

                            ZipFile.CreateFromDirectory(
                                Path.Combine(bs.FromPath, company.CodFirma), 
                                Path.Combine(sagaSalvBd, Utility.GetDate() + ".zip")
                            );
                        }
                        else
                        {
                            ZipFile.CreateFromDirectory(
                                Path.Combine(bs.FromPath, company.CodFirma),
                                Path.Combine(sagaSalvBd, Utility.GetDate() + ".zip")
                            );
                        }

                        Utility.DisplayMessage(
                            $"=> Arhivez fisierele firmei: {company.NumeFirma} si le salvez in: {sagaSalvBd}"
                        );

                        if (File.Exists(Path.Combine(compDir, Utility.GetDate() + ".zip")))
                        {
                            File.Delete(Path.Combine(compDir, Utility.GetDate() + ".zip"));

                            comp_rec = CompanyRecords.FindByDate(company.Guid, DateTime.Now.ToString("yyyy-MM-dd"));
                            if (comp_rec.Exists())
                            {
                                comp_rec.CreatedAt = Utility.GetTimestamp();
                                comp_rec.Save();
                            }
                            else
                            {
                                comp_rec.CompanyGuid = company.Guid;
                                comp_rec.Filepath = Path.Combine(compDir, Utility.GetDate() + ".zip");
                                comp_rec.Filename = Utility.GetDate() + ".zip";
                                comp_rec.CreatedAt = Utility.GetTimestamp();
                                comp_rec.Save();
                            }
                        }
                        else
                        {
                            comp_rec = new CompanyRecords
                            {
                                CompanyGuid = company.Guid,
                                Filepath = Path.Combine(compDir, Utility.GetDate() + ".zip"),
                                Filename = Utility.GetDate() + ".zip",
                                CreatedAt = Utility.GetTimestamp()
                            };
                            comp_rec.Save();
                        }

                        ZipFile.CreateFromDirectory(
                            Path.Combine(bs.FromPath, company.CodFirma),
                            Path.Combine(compDir, Utility.GetDate() + ".zip")
                        );

                        Utility.DisplayMessage($"=> Arhivez fisierele firmei: {company.NumeFirma} si le salvez in: {compDir}");
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                //throw e;
                Utility.DisplayError(string.Format(e.Message));
                return false;
            }
        }

        private static bool UpdateCompany(FirmeDbf result, Company company)
        {
            string dbfName = regex.IsMatch(Utility.Trim(result.NumeFirma)) ? 
                Utility.Trim(result.NumeFirma.ToString()) : Utility.Trim(result.CuiFirma.ToString());

            string compRegCom = !string.IsNullOrEmpty(company.RegComFirma) ? company.RegComFirma.ToString() : null;

            string dbfRegCom = !string.IsNullOrEmpty(Utility.Trim(result.RegComFirma)) ? Utility.Trim(result.RegComFirma.ToString()) : null;

            string oldCompDir = Path.Combine(bs.ToPath, Path.Combine(App.saves, $"{company.CodFirma} - {company.NumeFirma}"));

            string newCompDir = Path.Combine(bs.ToPath, Path.Combine(App.saves, $"{Utility.Trim(result.CodFirma)} - {dbfName}"));

            company.CodFirma = Convert.ToBoolean(string.Compare(company.CodFirma.ToString(), result.CodFirma.ToString())) ?
                Utility.Trim(result.CodFirma) : company.CodFirma;

            company.NumeFirma = dbfName;

            company.RegComFirma = Convert.ToBoolean(string.Compare(compRegCom, dbfRegCom)) ? Utility.Trim(result.RegComFirma) : company.RegComFirma;

            try
            {
                _  = company.Save();

                Directory.Delete(@newCompDir, true);

                Directory.Move(@oldCompDir, @newCompDir);

                _ = OnCompanyRename.UpdateCompanyRecords(company, newCompDir);

                Directory.Delete(@oldCompDir, true);

                return true;
            }
            catch (Exception e)
            {
                //throw e;
                Utility.DisplayError(string.Format(e.Message));
                return false;
            }
        } // end of UpdateCompany method

        private static bool MoveDeletedCompany(Company comp)
        {
            List<CompanyRecords> records = CompanyRecords.Get(comp.Guid);

            var compDir = Path.Combine(bs.ToPath, Path.Combine(App.saves, $"{comp.CodFirma} - {comp.NumeFirma}"));
            var deletedDir = Path.Combine(bs.ToPath, Path.Combine(App.deleted, $"{comp.CodFirma} - {comp.NumeFirma}"));

            _ = Utility.DirExistsCheckOrCreate(Path.Combine(bs.ToPath, App.deleted));
            _ = Utility.ChangePermissions(Path.Combine(bs.ToPath, App.deleted));

            try
            {
                _ = Utility.DirExistsCheckOrCreate(deletedDir);
                _ = Utility.ChangePermissions(deletedDir);

                if (records.Count > 0)
                {
                    foreach (CompanyRecords record in records)
                    {
                        if (File.Exists(@record.Filepath) && File.Exists(Path.Combine(deletedDir, record.Filename)))
                        {
                            File.Delete(Path.Combine(deletedDir, record.Filename));
                            File.Move(@record.Filepath, Path.Combine(deletedDir, record.Filename));
                            record.Filepath = Path.Combine(deletedDir, record.Filename);
                            _ = record.Save();
                            _ = record.Delete();
                        }
                        else
                        {
                            if (File.Exists(@record.Filepath))
                            {
                                File.Move(@record.Filepath, Path.Combine(deletedDir, record.Filename));
                                record.Filepath = Path.Combine(deletedDir, record.Filename);
                                _ = record.Save();
                                _ = record.Delete();
                            }
                            else
                            {
                                _ = record.Destroy();
                            }
                        }
                    }

                    Utility.DisplayMessage($"=> Firma: {comp.NumeFirma} a fost stearsa si este mutata in: {deletedDir}");
                }

                Directory.Delete(@compDir, true);

                return true;
            }
            catch (Exception e)
            {
                //throw e;
                Utility.DisplayError(string.Format(e.Message));
                return false;
            }
        } // end of MoveDeletedCompany method

        private static bool RestoreDeletedCompany(Company comp)
        {
            List<CompanyRecords> records = CompanyRecords.GetTrashed(comp.Guid);

            var deletedDir = Path.Combine(bs.ToPath, Path.Combine(App.deleted, $"{comp.CodFirma} - {comp.NumeFirma}"));
            var compDir = Path.Combine(bs.ToPath, Path.Combine(App.saves, $"{comp.CodFirma} - {comp.NumeFirma}"));

            _ = Utility.DirExistsCheckOrCreate(Path.Combine(bs.ToPath, App.saves));
            _ = Utility.ChangePermissions(Path.Combine(bs.ToPath, App.saves));

            try
            {
                _ = Utility.DirExistsCheckOrCreate(compDir);
                _ = Utility.ChangePermissions(compDir);

                if (records.Count > 0)
                {
                    foreach (CompanyRecords record in records)
                    {
                        if (File.Exists(@record.Filepath) && File.Exists(Path.Combine(compDir, record.Filename)))
                        {
                            File.Delete(Path.Combine(compDir, record.Filename));
                        }
                        else
                        {
                            if (File.Exists(@record.Filepath))
                            {
                                File.Move(@record.Filepath, Path.Combine(compDir, record.Filename));
                                record.Filepath = Path.Combine(compDir, record.Filename);
                                record.Save();
                                record.Restore();
                            }
                            else
                            {
                                record.Destroy();
                            }
                        }
                    }

                    Utility.DisplayMessage(
                        $"=> Firma: {comp.NumeFirma} a fost reinregistrata in {App.name.ToUpper()}.{App.ver}, " +
                        $"drept urmare backup-ul acesteia este restaurat la: {compDir}"
                    );
                }

                Directory.Delete(@deletedDir, true);

                return true;

            }
            catch (Exception e)
            {
                //throw e;
                Utility.DisplayError(string.Format(e.Message));
                return false;
            }
        } // end of RestoreDeletedCompany method

        public static void CleanSaga()
        {
            Regex regex = new Regex(@"^(_\d{4})$");
            string[] saga = Directory.GetDirectories(@bs.FromPath).ToArray();

            if (saga.Length > 0)
            {
                foreach (string dir in saga)
                {
                    var dirName = new DirectoryInfo(@dir).Name;
                    if (regex.IsMatch(dirName))
                    {
                        try
                        {
                            if (Directory.Exists(@dir))
                            {
                                Directory.Delete(@dir, true);

                                Utility.DisplayMessage(
                                    $"=> Sterg dosarul: {dirName} din {App.name.ToUpper()}.{App.ver}, deroarece " +
                                    "firma aferenta acestui dosar nu mai exista."
                                );
                            }
                        }
                        catch (Exception e)
                        {
                            //throw e;
                            Utility.DisplayError(string.Format(e.Message));
                        }
                    }
                }

                string[] salv_bd = Directory.GetDirectories(Path.Combine(bs.FromPath, bs.SalvBd)).ToArray();

                foreach (string dir in salv_bd)
                {
                    var dirName = new DirectoryInfo(@dir).Name;

                    Company deleted_company = Company.WhereTrashed("CodFirma", dirName);

                    Company company = Company.Where("CodFirma", dirName);

                    if (deleted_company.Exists())
                    {
                        try
                        {
                            if (!Directory.Exists(Path.Combine(bs.FromPath, dirName)))
                            {
                                if (Directory.Exists(@dir))
                                {
                                    Directory.Delete(@dir, true);

                                    Utility.DisplayMessage(
                                        $"=> Sterg din dosarul {App.name.ToUpper()}.{App.ver}, {bs.SalvBd}, " +
                                        $"dosarul: {dirName} deoarece firma aferenta acestui dosar nu mai exista"
                                    );
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            //throw e;
                            Utility.DisplayError(string.Format(e.Message));
                        }
                    }
                    else if (!company.Exists())
                    {
                        try
                        {
                            var unknown = Path.Combine(bs.ToPath, Path.Combine(App.deleted, App.unknown));

                            _ = Utility.DirExistsCheckOrCreate(@unknown);
                            _ = Utility.ChangePermissions(@unknown);

                            string[] filesArr = Directory.GetFiles(@dir).OrderByDescending(file => new FileInfo(file).CreationTime).ToArray();

                            for (int i = 0; i < filesArr.Length; i++)
                            {
                                if (i >= bs.KeepSalvBd)
                                {
                                    if (File.Exists(@filesArr[i])) File.Delete(@filesArr[i]);
                                }
                            }

                            if (!Directory.Exists(Path.Combine(bs.FromPath, dirName)))
                            {
                                if (File.Exists(Path.Combine(unknown, dirName + ".zip")))
                                {
                                    File.Delete(Path.Combine(unknown, dirName + ".zip"));
                                }

                                ZipFile.CreateFromDirectory(@dir, Path.Combine(unknown, $"{dirName}.zip"));

                                Utility.DisplayMessage(
                                    $"=> Arhivez dosarul : {dirName} in: {unknown}, deoarece aceasta firma nu mai exista"
                                );

                                if (Directory.Exists(@dir))
                                {
                                    Directory.Delete(@dir, true);

                                    Utility.DisplayMessage(
                                        $"=> Sterg din dosarul {App.name.ToUpper()}.{App.ver}, {bs.SalvBd}, " +
                                        $"dosarul: {dirName} deoarece firma aferenta acestui dosar nu mai exista"
                                    );
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            //throw e;
                            Utility.DisplayError(string.Format(e.Message));
                        }
                    }
                }
                return;
            }
            return;
        } // end of CleanSaga method

        public static void CleanSagaBackup()
        {
            List<Company> companiesArray = Company.Get();

            if (companiesArray.Count > 0)
            {
                foreach (Company comp in companiesArray)
                {
                    string sql = $"SELECT * FROM `{CompanyRecords.GetTable()}` WHERE `CompanyGuid` = '{comp.Guid}' " +
                        $"AND `DeletedAt` IS NULL ORDER BY `CreatedAt` DESC";
                    List<CompanyRecords> results = CompanyRecords.Query(sql);

                    if (bs.KeepRec > 0 && results.Count > bs.KeepRec)
                    {
                        for (int i = 0; i < results.Count; i++)
                        {
                            if (i >= bs.KeepRec)
                            {
                                try
                                {
                                    if (File.Exists(@results[i].Filepath)) File.Delete(@results[i].Filepath);
                                    results[i].Destroy();

                                    Utility.DisplayMessage(
                                        $"=> Sterg fisierul: {results[i].Filename}, din backup-ul firmei: {comp.NumeFirma}. " +
                                        $"Curatare backup in progres"
                                    );
                                }
                                catch (Exception e)
                                {
                                    //throw e;
                                    Utility.DisplayError(string.Format(e.Message));
                                }
                            }
                        }
                    }

                    string[] backups = Directory.GetFiles(Path.Combine(bs.ToPath, Path.Combine(App.saves, $"{comp.CodFirma} - {comp.NumeFirma}")));

                    if (backups.Length > 0)
                    {
                        foreach (string file in backups)
                        {
                            try
                            {
                                CompanyRecords record = CompanyRecords.FindOrFail("Filepath", file);
                                if (!record.Exists())
                                {
                                    File.Delete(@file);

                                    Utility.DisplayMessage(
                                        $"=> Sterg fisierul: {file}, din backup-ul " +
                                        $"firmei: {comp.NumeFirma}. Curatare backup in progres"
                                    );
                                }
                            }
                            catch (Exception e)
                            {
                                //throw e;
                                Utility.DisplayError(string.Format(e.Message));
                            }
                        }
                    }
                }

                foreach (Company comp in companiesArray)
                {
                    FileInfo[] sagaSalvBdFiles = GetCompanySalvBdFiles(comp);

                    if (sagaSalvBdFiles.Length > bs.KeepSalvBd)
                    {
                        for (int i = 0; i < sagaSalvBdFiles.Length; i++)
                        {
                            if (i >= bs.KeepSalvBd)
                            {
                                try
                                {
                                    if (File.Exists(sagaSalvBdFiles[i].FullName)) File.Delete(sagaSalvBdFiles[i].FullName);

                                    Utility.DisplayMessage(
                                        $"=> Sterg fisierul: {sagaSalvBdFiles[i].Name}, " +
                                        $"din backup-ul {bs.SalvBd} al firmei: {comp.NumeFirma}. " +
                                        $"Curatare backup in progres"
                                    );
                                }
                                catch (Exception e)
                                {
                                    //throw e;
                                    Utility.DisplayError(String.Format(e.Message));
                                }
                            }
                        }
                    }
                }
                return;
            }
            return;
        } // end of CleanSagaBackup method

        public static bool RemoveDeletedCompanies()
        {
            List<Company> results = Company.GetTrashed();

            if (results.Count > 0)
            {
                DateTime now = DateTime.Now;

                foreach (Company comp in results)
                {
                    DateTime deleted_at = DateTime.Parse(comp.DeletedAt);
                    if (now.Subtract(deleted_at).Days > (365 * bs.KeepDC))
                    {
                        if (Directory.Exists(
                                Path.Combine(bs.ToPath, Path.Combine(App.deleted, $"{comp.CodFirma} - {comp.NumeFirma}"))
                            ))
                        {
                            Directory.Delete(
                                Path.Combine(bs.ToPath, Path.Combine(App.deleted, $"{comp.CodFirma} - {comp.NumeFirma}")),
                                true
                            );
                        }

                        comp.Delete();
                    }
                }
                return true;
            }
            return false;
        } // end of RemoveDeletedCompanies method

        private static FileInfo[] GetCompanySalvBdFiles(Company company)
        {
            DirectoryInfo companySalvBdDir = new DirectoryInfo(Path.Combine(bs.FromPath, Path.Combine(App.salv_bd, company.CodFirma)));
            FileInfo[] sagaSalvBdFiles = companySalvBdDir.GetFiles().OrderBy(file => file.CreationTime).ToArray();
            if (sagaSalvBdFiles.Length > 0)
            {
                Array.Reverse(sagaSalvBdFiles);
                return sagaSalvBdFiles;
            }

            return new FileInfo[0];
        }

        private static FileInfo GetCompanySalvBdLastBackup(Company company)
        {
            DirectoryInfo companySalvBdDir = new DirectoryInfo(Path.Combine(bs.FromPath, Path.Combine(App.salv_bd, company.CodFirma)));
            FileInfo[] sagaSalvBdFiles = companySalvBdDir.GetFiles().OrderBy(file => file.CreationTime).ToArray();
            if (sagaSalvBdFiles.Length > 0)
            {
                Array.Reverse(sagaSalvBdFiles);
                return sagaSalvBdFiles[0];
            }

            return null;
        }
    }
}
