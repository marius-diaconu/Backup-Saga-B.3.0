using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ClassLibrary;

namespace SettingsApp
{
    public static class OnPathChange
    {
        public static bool MoveBackup(BackupSettings bs, string ToPath)
        {
            if (Convert.ToBoolean(bs.ID) && !string.IsNullOrEmpty(ToPath))
            {
                if (Convert.ToBoolean(string.Compare(bs.ToPath.ToString(), ToPath)))
                {
                    try
                    {
                        if (Directory.Exists(Path.Combine(bs.ToPath, App.saves)))
                        {
                            Directory.Delete(Path.Combine(bs.ToPath, App.saves));
                        }

                        Directory.Move(Path.Combine(ToPath, App.saves), Path.Combine(bs.ToPath, App.saves));
                        return true;
                    }
                    catch (Exception e)
                    {
                        //throw e;
                        MessageBox.Show(string.Format(e.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool UpdateCompanyRecords(BackupSettings bs, string ToPath)
        {
            if (Convert.ToBoolean(string.Compare(bs.ToPath.ToString(), ToPath)))
            {
                List<Company> companies = Company.Get();

                foreach (var comp in companies)
                {
                    List<CompanyRecords> records = CompanyRecords.Get(comp.Guid);

                    foreach (var record in records)
                    {
                        if (
                                File.Exists(
                                    Path.Combine(
                                        bs.ToPath, 
                                        Path.Combine(
                                            App.saves, 
                                            Path.Combine($"{comp.CodFirma} - {comp.NumeFirma}", record.Filename)
                                        )
                                    )
                                )
                            )
                        {
                            record.Filepath = Path.Combine(
                                bs.ToPath, 
                                Path.Combine(
                                    App.saves, 
                                    Path.Combine($"{comp.CodFirma} - {comp.NumeFirma}", record.Filename)
                                )
                            );
                            record.Save();
                        }
                        else
                        {
                            record.Destroy();
                        }
                    }
                }
            }
            return true;
        }
    }
}
