using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using DbfDataReader;

namespace ClassLibrary
{
    public class FirmeDbf
    {
        private static readonly int timer = 10000;
        private static readonly int sec = timer / 1000;
        public string CodFirma { get; set; }
        public string NumeFirma { get; set; }
        public string CuiFirma { get; set; }
        public string RegComFirma { get; set; }

        private static Regex regex;

        public FirmeDbf() {}

        public FirmeDbf(string cod_firma, string nume_firma, string cui_firma, string reg_com)
        {
            CodFirma = Utility.Trim(regex.Replace(cod_firma, ""));
            NumeFirma = Utility.Trim(regex.Replace(nume_firma, ""));
            CuiFirma = Utility.Trim(regex.Replace(cui_firma, ""));
            RegComFirma = Utility.Trim(reg_com);
        }

        public static List<FirmeDbf> Get(string filePath, string fileName)
        {
            regex = new Regex(@"[\""\,\<\>\?\/\:\;\'\[\]\{\}\!\@\#\$\%\^\*\(\)\+\=\|\\]");
            List<FirmeDbf> dbf = new List<FirmeDbf>();

            var options = new DbfDataReaderOptions
            {
                SkipDeletedRecords = true
            };

            var dbfPath = Path.Combine(filePath, fileName);

            try
            {
                using (var dbfDataReader = new DbfDataReader.DbfDataReader(dbfPath, options))
                {
                    while (dbfDataReader.Read())
                    {
                        var COD = dbfDataReader.GetString(0);
                        var NUME = dbfDataReader.GetString(1);
                        var CUI = dbfDataReader.GetString(2);
                        var RegCom = dbfDataReader.GetString(3);

                        FirmeDbf obj = new FirmeDbf(COD, NUME, CUI, RegCom);
                        dbf.Add(obj);
                    }
                }
                return dbf;
            }
            catch (Exception e)
            {
                Utility.DisplayError(
                    $"{App.name.ToUpper()}.{App.ver} ESTE DESCHIS! VA RUGAM INCHIDETI TOATE INSTANTELE DE " +
                    $"{App.name.ToUpper()}.{App.ver} INAINTE DE-A RULA ACEASTA APLICATIE!"
                );

                Utility.InitMessage(
                    $"=> In {sec} sec, aceast program se va autoinchide! Niciun Backup realizat!"
                );

                Utility.Timer(timer);
                Environment.Exit(0);
                throw e;
            }
        } // end of Get method
    }
}
