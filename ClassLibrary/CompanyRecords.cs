using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ClassLibrary
{
    public class CompanyRecords
    {
        private static readonly string _table = "CompanyRecords";
        public int ID { get; set; }
        public string CompanyGuid { get; set; }
        public string Filename { get; set; }
        public string Filepath { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string DeletedAt { get; set; }

        private static readonly string[] TableFields = {
            "CompanyGuid",
            "Filename",
            "Filepath",
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
                "`CompanyGuid` TEXT NOT NULL, " +
                "`Filename` TEXT NOT NULL, " +
                "`Filepath` TEXT NULL DEFAULT NULL, " +
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
        public static List<CompanyRecords> Query(string sql)
        {
            return Database.Query<CompanyRecords>(sql);
        }

        /// <summary>
        /// get first untrashed resource from database
        /// </summary>
        /// <returns>found resource</returns>
        public static CompanyRecords First()
        {
            var sql = $"SELECT * FROM `{GetTable()}` WHERE `DeletedAt` IS NULL ORDER BY `ID` ASC LIMIT 1";
            return Instantiate(Query(sql));
        }

        /// <summary>
        /// get the last resource from database
        /// </summary>
        /// <returns>found resource</returns>
        public static CompanyRecords Last()
        {
            var sql = $"SELECT * FROM `{GetTable()}` WHERE `DeletedAt` IS NULL ORDER BY `ID` DESC LIMIT 1";
            return Instantiate(Query(sql));
        }

        /// <summary>
        /// get first untrashed resource where company guid is equal with provided guid
        /// </summary>
        /// <param name="guid">string company guid</param>
        /// <returns>found resource</returns>
        public static CompanyRecords FindByGuid(string guid)
        {
            var sql = $"SELECT * FROM `{GetTable()}` WHERE `CompanyGuid` = '{Utility.Trim(guid)}' AND `DeletedAt` IS NULL LIMIT 1";
            return Instantiate(Query(sql));
        }

        /// <summary>
        /// get first untrashed resource from database where column contains provided value
        /// </summary>
        /// <param name="column">string column name</param>
        /// <param name="value">string column value</param>
        /// <returns>found resource</returns>
        public static CompanyRecords FindOrFail(string column, string value)
        {
            var sql = $"SELECT * FROM `{GetTable()}` WHERE `{Utility.UcFirst(Utility.Trim(column))}` = " +
                $"'{Utility.Trim(value)}' AND `DeletedAt` IS NULL";
            return Instantiate(Query(sql));
        }

        /// <summary>
        /// get first untrashed resource from database
        /// </summary>
        /// <param name="comp_guid">string company guid</param>
        /// <param name="date">string date</param>
        /// <returns>found resource</returns>
        public static CompanyRecords FindByDate(string comp_guid, string date)
        {
            var sql = $"SELECT * FROM `{GetTable()}` WHERE `CompanyGuid` = '{comp_guid}' " +
                $"AND DATE(`CreatedAt`) = '{date}' LIMIT 1";
            return Instantiate(Query(sql));
        }

        /// <summary>
        /// get all untrashed resources where CompanyGuid is equal with provided CompanyGuid
        /// </summary>
        /// <param name="company_guid">string company guid</param>
        /// <returns>enumerable list of resources</returns>
        public static List<CompanyRecords> Get(string company_guid)
        {
            var sql = $"SELECT * FROM `{GetTable()}` WHERE `CompanyGuid` = '{Utility.Trim(company_guid)}' " +
                $"AND `DeletedAt` IS NULL";
            return Query(sql);
        }

        /// <summary>
        /// get all trashed resources where CompanyGuid is equal with provided company_guid
        /// </summary>
        /// <param name="company_guid">string company guid</param>
        /// <returns>enumerable list of resources</returns>
        public static List<CompanyRecords> GetTrashed(string company_guid)
        {
            var sql = $"SELECT * FROM `{GetTable()}` WHERE `CompanyGuid` = '{Utility.Trim(company_guid)}' " +
                $"AND `DeletedAt` IS NOT NULL";
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
        /// checks if resource exists
        /// </summary>
        /// <returns>boolean</returns>
        public bool Exists()
        {
            return Convert.ToBoolean(ID);
        }

        /// <summary>
        /// create database resord if resource is not persisted
        /// or update database record for persisted resource
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
        /// updates persisted resource
        /// </summary>
        /// <returns>boolean</returns>
        private bool Update()
        {
            UpdatedAt = Utility.GetTimestamp();
            int affectedRows = Database.Execute(Database.UpdateBuilder(GetTable(), TableFields, "ID"), this);
            return Convert.ToBoolean(affectedRows);
        }

        /// <summary>
        /// trash resource
        /// </summary>
        /// <returns>boolean</returns>
        public bool Delete()
        {
            DeletedAt = Utility.GetTimestamp();
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
        /// delete resorce from database
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
        private static CompanyRecords Instantiate(List<CompanyRecords> list)
        {
            if (list.Count > 0 && list.Count == 1)
            {
                return list[0];
            }
            else
            {
                return new CompanyRecords();
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
                                CompanyRecords record = Instantiate(Query(sql));
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
