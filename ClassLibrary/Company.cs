using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ClassLibrary
{
    public class Company
    {
        /**
         * ----------------------------------
         * Start declaring class properties
         * ----------------------------------
         */
        private static readonly string _table = "Companies";
        public int ID { get; set; }
        public string CodFirma { get; set; }
        public string NumeFirma { get; set; }
        public string CuiFirma { get; set; }
        public string RegComFirma { get; set; }
        public string Guid { get; set; }
        public int IsSalvBd { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string DeletedAt { get; set; }

        private static readonly string[] TableFields = {
            "CodFirma",
            "NumeFirma",
            "CuiFirma",
            "RegComFirma",
            "Guid",
            "IsSalvBd",
            "CreatedAt",
            "UpdatedAt",
            "DeletedAt"
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
                "`CodFirma` TEXT NOT NULL, " +
                "`NumeFirma` TEXT NOT NULL, " +
                "`CuiFirma` TEXT NULL DEFAULT NULL, " +
                "`RegComFirma` TEXT NULL DEFAULT NULL, " +
                "`Guid` TEXT NOT NULL UNIQUE, " +
                "`IsSalvBd` INTEGER UNSIGNED NOT NULL DEFAULT 0, " +
                "`CreatedAt` TEXT NOT NULL, " +
                "`UpdatedAt` TEXT NULL DEFAULT NULL, " +
                "`DeletedAt` TEXT NULL DEFAULT NULL" +
                ")";

            return Table.Execute(sql);
        }

        /// <summary>
        /// class specific query method
        /// </summary>
        /// <param name="sql">string query sql</param>
        /// <returns>enumerable list of resources</returns>
        public static List<Company> Query(string sql)
        {
            return Database.Query<Company>(sql);
        }

        /// <summary>
        /// get first untrashed resource from database
        /// </summary>
        /// <returns>found resource</returns>
        public static Company First()
        {
            var sql = $"SELECT * FROM `{GetTable()}` WHERE `DeletedAt` IS NULL ORDER BY `ID` ASC LIMIT 1";
            return Instantiate(Query(sql));
        }

        /// <summary>
        /// get the last untrashed resource from database
        /// </summary>
        /// <returns>found resource</returns>
        public static Company Last()
        {
            var sql = $"SELECT * FROM `{GetTable()}` WHERE `DeletedAt` IS NULL ORDER BY `ID` DESC LIMIT 1";
            return Instantiate(Query(sql));
        }

        /// <summary>
        /// get first resource where Guid is equal with provided guid
        /// </summary>
        /// <param name="guid">string guid</param>
        /// <returns>found resource</returns>
        public static Company FindByGuid(string guid)
        {
            var sql = $"SELECT * FROM `{GetTable()}` WHERE `Guid` = '{Utility.Trim(guid)}' AND `DeletedAt` IS NULL LIMIT 1";
            return Instantiate(Query(sql));
        }

        /// <summary>
        /// get first resource that matches to the provided cod_firma and cui_firma
        /// </summary>
        /// <param name="column">string column name</param>
        /// <param name="value">string column value</param>
        /// <returns>found resource</returns>
        public static Company Where(string column, string value)
        {
            var sql = $"SELECT * FROM `{GetTable()}` WHERE `{Utility.UcFirst(column)}` = '{Utility.Trim(value)}' " +
                $"AND `DeletedAt` IS NULL LIMIT 1";
            return Instantiate(Query(sql));
        }

        /// <summary>
        /// get first resource that matches to the provided cod_firma and cui_firma
        /// </summary>
        /// <param name="CodFirma">string cod firma</param>
        /// <param name="CuiFirma">string cui firma/param>
        /// <returns>found resource</returns>
        public static Company FindOrFail(string CodFirma, string CuiFirma)
        {
            var sql = $"SELECT * FROM `{GetTable()}` WHERE `CodFirma` = '{Utility.Trim(CodFirma)}' " +
                $"AND `CuiFirma` = '{Utility.Trim(CuiFirma)}' LIMIT 1";
            return Instantiate(Query(sql));
        }

        /// <summary>
        /// get all untrashed resources
        /// </summary>
        /// <returns>enumerable list of resources</returns>
        public static List<Company> Get()
        {
            var sql = $"SELECT * FROM `{GetTable()}` WHERE `DeletedAt` IS NULL";
            return Query(sql);
        }

        /// <summary>
        /// get trashed resource
        /// </summary>
        /// <param name="CuiFirma">string cui firma</param>
        /// <returns>found resource</returns>
        public static Company Trashed(string CuiFirma)
        {
            var sql = $"SELECT* FROM `{GetTable()}` WHERE `CuiFirma` = '{Utility.Trim(CuiFirma)}' " +
                $"AND `DeletedAt` IS NOT NULL LIMIT 1";
            return Instantiate(Query(sql));
        }

        /// <summary>
        /// get trashed resource
        /// </summary>
        /// <param name="column">string column name</param>
        /// <param name="value">string column value</param>
        /// <returns>found resource</returns>
        public static Company WhereTrashed(string column, string value)
        {
            var sql = $"SELECT* FROM `{GetTable()}` WHERE `{Utility.UcFirst(column)}` = '{Utility.Trim(value)}' " +
                $"AND `DeletedAt` IS NOT NULL LIMIT 1";
            return Instantiate(Query(sql));
        }

        /// <summary>
        /// get all trashed resources
        /// </summary>
        /// <returns>enumerable list of resources</returns>
        public static List<Company> GetTrashed()
        {
            var sql = $"SELECT * FROM `{GetTable()}` WHERE `DeletedAt` IS NOT NULL";
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
        /// checks if resource exists or not
        /// </summary>
        /// <returns>boolean</returns>
        public bool Exists()
        {
            return Convert.ToBoolean(ID);
        }

        /// <summary>
        /// create database resord if resource is not persisted
        /// or update a persisted resource
        /// </summary>
        /// <returns>boolean</returns>
        public bool Save()
        {
            return Convert.ToBoolean(ID) ? Update() : Create();
        }

        /// <summary>
        /// create database record for unpersisted resource
        /// </summary>
        /// <returns>boolean</returns>
        private bool Create()
        {
            CreatedAt = Utility.GetTimestamp();
            ID = Database.Execute(Database.InsertBuilder(GetTable(), TableFields), this);
            return Convert.ToBoolean(ID);
        }

        /// <summary>
        /// update database record for persisted resource
        /// </summary>
        /// <returns>boolean</returns>
        private bool Update()
        {
            UpdatedAt = Utility.Trim(Utility.GetTimestamp());
            int affectedRows = Database.Execute(Database.UpdateBuilder(GetTable(), TableFields, "ID"), this);
            return Convert.ToBoolean(affectedRows);
        }

        /// <summary>
        /// trash resource
        /// </summary>
        /// <returns>boolean</returns>
        public bool Delete()
        {
            DeletedAt = Utility.Trim(Utility.GetTimestamp());
            var sql = $"UPDATE `{GetTable()}` SET `DeletedAt` = @DeletedAt WHERE `ID` = @ID";
            int affectedRows = Database.Execute(sql, this);
            return Convert.ToBoolean(affectedRows);
        }

        /// <summary>
        /// restore trashed resource
        /// </summary>
        /// <returns>boolean</returns>
        public bool Restore()
        {
            var sql = $"UPDATE `{GetTable()}` SET `DeletedAt` = NULL WHERE `ID` = @ID";
            int affectedRows = Database.Execute(sql, this);
            return Convert.ToBoolean(affectedRows);
        }

        /// <summary>
        /// delete resource from database
        /// </summary>
        /// <returns>boolean</returns>
        public bool Destroy()
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
        /// instantiate first element from list
        /// or create new class instance
        /// </summary>
        /// <param name="list">enumerable list</param>
        /// <returns>class instance</returns>
        private static Company Instantiate(List<Company> list)
        {
            if (list.Count > 0 && list.Count == 1)
            {
                return list[0];
            }
            else
            {
                return new Company();
            }
        }

        /// <summary>
        /// generates random string
        /// </summary>
        /// <returns>string</returns>
        public static string GenerateGuid(int length, bool lowerCase)
        {
            return Str.Random(GetTable(), length, lowerCase);
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
                                Company record = Instantiate(Query(sql));
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
