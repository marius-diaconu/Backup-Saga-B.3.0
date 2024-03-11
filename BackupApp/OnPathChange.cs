using System;
using System.Collections.Generic;
using System.IO;
using ClassLibrary;

namespace BackupApp
{
    public static class OnPathChange
    {
        public static bool UpdateCompanyRecords(BackupSettings bs)
        {
            List<Company> companies = Company.Get();

            foreach (var comp in companies)
            {
                if (!Directory.Exists(Path.Combine(bs.ToPath, Path.Combine(App.saves, $"{ comp.CodFirma} - {comp.NumeFirma}"))))
                {
                    Utility.DirExistsCheckOrCreate(Path.Combine(bs.ToPath, Path.Combine(App.saves, $"{ comp.CodFirma} - {comp.NumeFirma}")));
                    Utility.ChangePermissions(Path.Combine(bs.ToPath, Path.Combine(App.saves, $"{ comp.CodFirma} - {comp.NumeFirma}")));
                }

                List<CompanyRecords> records = CompanyRecords.Get(comp.Guid);

                foreach (var record in records)
                {
                    if (!File.Exists(
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
                        File.Copy(
                            record.Filepath, 
                            Path.Combine(
                                bs.ToPath, 
                                Path.Combine(
                                    App.saves, 
                                    Path.Combine($"{comp.CodFirma} - {comp.NumeFirma}", record.Filename)
                                )
                            )
                        );

                        File.Delete(record.Filepath);

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
                        record.Filepath = Path.Combine(
                            bs.ToPath, 
                            Path.Combine(
                                App.saves, 
                                Path.Combine($"{comp.CodFirma} - {comp.NumeFirma}", record.Filename)
                            )
                        );

                        record.Save();
                    }
                }

                if (
                    Directory.Exists(
                        Path.Combine(
                            bs.ToPath, 
                            Path.Combine(App.active, $"{comp.CodFirma} - {comp.NumeFirma}")
                        )
                    )
                )
                {
                    Directory.Delete(
                        Path.Combine(
                            bs.ToPath, 
                            Path.Combine(App.active, $"{comp.CodFirma} - {comp.NumeFirma}")
                        ), 
                        true
                    );
                }
            }
            return true;
        }
    }
}
