using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class BackupSettings
    {
        /**
         * ----------------------------------
         * Start declaring class properties
         * ----------------------------------
         */
        private static readonly string _table = "BackupSettings";
        public int ID { get; set; }
        public string FromPath { get; set; }
        public string ToPath { get; set; }
        public string SalvBd { get; set; }
        public string FirmeDBF { get; set; }
        public int KeepRec { get; set; }
        public int KeepSalvBd { get; set; }
        public int KeepDC { get; set; }
        public string BackupDay { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }

        private static readonly string[] TableFields = {
            "FromPath",
            "ToPath",
            "SalvBd",
            "FirmeDBF",
            "KeepRec",
            "KeepSalvBd",
            "KeepDC",
            "BackupDay",
            "CreatedAt",
            "UpdatedAt"
        };

        /**
         * ---------------------
         * Start class methods
         * ---------------------
         */
        public static string GetTable() { return _table; }

        /// <summary>
        /// create database table if not exists
        /// </summary>
        /// <returns>boolean</returns>
        public static bool CreateTable()
        {
            var sql = $"CREATE TABLE IF NOT EXISTS `{GetTable()}` (" +
                "`ID` INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "`FromPath` TEXT NOT NULL, " +
                "`ToPath` TEXT NOT NULL, " +
                "`SalvBd` TEXT NOT NULL, " +
                "`FirmeDBF` TEXT NOT NULL, " +
                "`KeepRec` INTEGER UNSIGNED NOT NULL, " +
                "`KeepSalvBd` INTEGER UNSIGNED NOT NULL DEFAULT 30, " +
                "`KeepDC` INTEGER UNSIGNED NOT NULL DEFAULT 1, " +
                "`BackupDay` TEXT NOT NULL, " +
                "`CreatedAt` TEXT NOT NULL, " +
                "`UpdatedAt` TEXT NULL DEFAULT NULL" +
                ")";

            return Table.Execute(sql);
        }

        /// <summary>
        /// class specific query method
        /// </summary>
        /// <param name="sql">string query sql</param>
        /// <returns>enumerable list of resources</returns>
        public static List<BackupSettings> Query(string sql)
        {
            return Database.Query<BackupSettings>(sql);
        }
        /// <summary>
        /// get first resource from database order by id asc
        /// </summary>
        /// <returns>found resource</returns>
        public static BackupSettings First()
        {
            var sql = $"SELECT * FROM `{GetTable()}` ORDER BY `ID` ASC LIMIT 1";
            return Instantiate(Query(sql));
        }

        /// <summary>
        /// get last resource from database order by id desc
        /// </summary>
        /// <returns>found resource</returns>
        public static BackupSettings Last()
        {
            var sql = $"SELECT * FROM `{GetTable()}` ORDER BY `ID` DESC LIMIT 1";
            return Instantiate(Query(sql));
        }

        /// <summary>
        /// get resource where column is equal with provided value
        /// </summary>
        /// <param name="column">string of column name</param>
        /// <param name="value">string of column value</param>
        /// <returns>found resource</returns>
        public static BackupSettings FindOrFail(string column, string value)
        {
            var sql = $"SELECT * FROM `{GetTable()}` WHERE `{Utility.UcFirst(Utility.Trim(column))}` = " +
                $"'{Utility.Trim(value)}'";
            return Instantiate(Query(sql));
        }

        /// <summary>
        /// get all untrashed resources
        /// </summary>
        /// <returns>enumerable list of resources</returns>
        public static List<BackupSettings> Get()
        {
            var sql = $"SELECT * FROM `{GetTable()}`";
            return Query(sql);
        }

        /// <summary>
        /// count all resources
        /// </summary>
        /// <returns>integer</returns>
        public static int Count()
        {
            var sql = $"SELECT * FROM `{GetTable()}`";
            return Query(sql).Count;
        }

        /// <summary>
        /// check if resource is already persisted or not
        /// </summary>
        /// <returns>boolean</returns>
        public bool Exists()
        {
            return Convert.ToBoolean(ID);
        }

        /// <summary>
        /// create a resource if is not persisted
        /// or update a persisted resource
        /// </summary>
        /// <returns>boolean</returns>
        public bool Save()
        {
            return Convert.ToBoolean(ID) ? Update() : Create();
        }

        /// <summary>
        /// persist a resource to database
        /// </summary>
        /// <returns>boolean</returns>
        private bool Create()
        {
            CreatedAt = Utility.GetTimestamp();
            ID = Database.Execute(Database.InsertBuilder(GetTable(), TableFields), this);
            return Convert.ToBoolean(ID);
        }

        /// <summary>
        /// update persisted resource
        /// </summary>
        /// <returns>boolean</returns>
        private bool Update()
        {
            UpdatedAt = Utility.Trim(Utility.GetTimestamp());
            int affectedRows = Database.Execute(Database.UpdateBuilder(GetTable(), TableFields, "ID"), this);
            return Convert.ToBoolean(affectedRows);
        }

        /// <summary>
        /// delete resource from database
        /// </summary>
        /// <returns>boolean</returns>
        public bool Delete()
        {
            var sql = $"DELETE FROM `{GetTable()}` WHERE `ID` = @ID";
            int affectedRows = Database.Execute(sql, this);
            return Convert.ToBoolean(affectedRows);
        }

        /**
         * ------------------------------
         * Start class utility methods
         * ------------------------------
         */

        /// <summary>
        /// class override ToString method
        /// </summary>
        /// <returns>json object reprezentation</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// instantiate first element of a list
        /// or create a new class instance
        /// </summary>
        /// <returns>class instance</returns>
        private static BackupSettings Instantiate(List<BackupSettings> list)
        {
            if (list.Count > 0 && list.Count == 1)
            {
                return list[0];
            }
            else
            {
                return new BackupSettings();
            }
        }

        /// <summary>
        /// reset table autoincrement value and reorder the table ID's
        /// </summary>
        public static void RefreshTable()
        {
            SqliteSequence sequence = SqliteSequence.First(GetTable());
            if (sequence.Exists())
            {
                if (Table.Execute($"ALTER TABLE `{GetTable()}` RENAME TO `temp_{GetTable()}`"))
                {
                    if (CreateTable())
                    {
                        var sql = $"INSERT INTO `{GetTable()}` (`{string.Join("`,`", TableFields)}`) " +
                            $"SELECT `{string.Join("`,`", TableFields)}` FROM `temp_{GetTable()}`";

                        if (Database.Execute(sql) > 0)
                        {
                            if (Table.Execute($"DROP TABLE `temp_{GetTable()}`"))
                            {
                                sql = $"SELECT * FROM `{GetTable()}` ORDER BY `ID` DESC LIMIT 1";
                                BackupSettings record = Instantiate(Query(sql));
                                if (record.Exists())
                                {
                                    sequence.seq = record.ID;
                                    sequence.Update();
                                    return;
                                }
                                else
                                {
                                    sequence.Destroy();
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            return;
        }
    }
}
