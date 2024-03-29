﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ClassLibrary;

namespace BackupApp
{
    public static class OnCompanyRename
    {
        public static bool UpdateCompanyRecords(Company company, string companyPath)
        {
            try
            {
                string sql = $"SELECT * FROM `{CompanyRecords.GetTable()}` WHERE `CompanyGuid` = '{company.Guid}'";
                List<CompanyRecords> records = CompanyRecords.Query(sql);

                foreach (CompanyRecords record in records)
                {
                    if (File.Exists(Path.Combine(companyPath, record.Filename)))
                    {
                        record.Filepath = Path.Combine(companyPath, record.Filename);
                        record.Save();

                        Utility.DisplayMessage(
                            $"=> Actualizez inregistrarea: {Path.Combine(companyPath, record.Filename)}" +
                            $" in baza de date, pentru firma: {company.NumeFirma}"
                        );
                    }
                    else
                    {
                        record.Delete();

                        Utility.DisplayMessage(
                            $"=> Sterg inregistrarea: {Path.Combine(companyPath, record.Filename)}" +
                            $" din baza de date, pentru firma: {company.NumeFirma}"
                        );
                    }
                }

                return true;
            }
            catch (Exception e)
            {

                Utility.DisplayError(string.Format(e.Message));
                return false;
            }
        }
    }
}
