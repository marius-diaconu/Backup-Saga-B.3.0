using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ClassLibrary
{
    public class ResourceFiles
    {
        /**
         * ----------------------------------
         * Start declaring class properties
         * ----------------------------------
         */
        private static readonly string _table = "ResourceFiles";
        public int ID { get; set; }
        public string ResourcePath { get; set; }
        public string LogFileName { get; set; }
        public string ErrorFileName { get; set; }
        public string SalvBd { get; set; }
        public string FirmeDBF { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }

        private static readonly string[] TableFields = {
            "ResourcePath",
            "LogFileName",
            "ErrorFileName",
            "SalvBd",
            "FirmeDBF",
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
                    "`ResourcePath` TEXT NOT NULL, " +
                    "`LogFileName` TEXT NOT NULL, " +
                    "`ErrorFileName` TEXT NOT NULL, " +
                    "`SalvBd` TEXT NOT NULL, " +
                    "`FirmeDBF` TEXT NOT NULL, " +
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
        public static List<ResourceFiles> Query(string sql)
        {
            return Database.Query<ResourceFiles>(sql);
        }

        /// <summary>
        /// get first resource from database order by id asc
        /// </summary>
        /// <returns>found resource</returns>
        public static ResourceFiles First()
        {
            var sql = $"SELECT * FROM `{GetTable()}` ORDER BY `ID` ASC LIMIT 1";
            return Instantiate(Query(sql));
        }

        /// <summary>
        /// get the last resource from database order by id desc
        /// </summary>
        /// <returns>found resource</returns>
        public static ResourceFiles Last()
        {
            var sql = $"SELECT * FROM `{GetTable()}` ORDER BY `ID` DESC LIMIT 1";
            return Instantiate(Query(sql));
        }

        /// <summary>
        /// get resource where column is equal with the provided value
        /// </summary>
        /// <param name="column">string column name</param>
        /// <param name="value">string column value</param>
        /// <returns>found resource</returns>
        public static ResourceFiles FindOrFail(string column, string value)
        {
            var sql = $"SELECT * FROM `{GetTable()}` WHERE `{Utility.UcFirst(Utility.Trim(column))}` = " +
                $"'{Utility.Trim(value)}' LIMIT 1";
            return Instantiate(Query(sql));
        }

        /// <summary>
        /// get all untrashed resources
        /// </summary>
        /// <returns>enumerable list of resources</returns>
        public static List<ResourceFiles> Get()
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
        /// check if resource is persisted or not
        /// </summary>
        /// <returns>boolean</returns>
        public bool Exists()
        {
            return Convert.ToBoolean(ID);
        }

        /// <summary>
        /// create if resource is not persisted
        /// update if resource is already persisted
        /// </summary>
        /// <returns>boolean</returns>
        public bool Save()
        {
            return Convert.ToBoolean(ID) ? Update() : Create();
        }

        /// <summary>
        /// persist to database an unpersisted resounce
        /// </summary>
        /// <returns>boolean</returns>
        private bool Create()
        {
            CreatedAt = Utility.GetTimestamp();
            ID = Database.Execute(Database.InsertBuilder(GetTable(), TableFields), this);
            return Convert.ToBoolean(ID);
        }

        /// <summary>
        /// update resource if is already persisted to database
        /// </summary>
        /// <returns>boolean</returns>
        private bool Update()
        {
            UpdatedAt = Utility.GetTimestamp();
            int affectedRows = Database.Execute(Database.UpdateBuilder(GetTable(), TableFields, "ID"), this);
            return Convert.ToBoolean(affectedRows);
        }

        /// <summary>
        /// delete from database persisted resource
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
        /// or create a new instance
        /// </summary>
        /// <param name="list">enumerable list of resources</param>
        /// <returns>class instance</returns>
        private static ResourceFiles Instantiate(List<ResourceFiles> list)
        {
            if (list.Count > 0 && list.Count == 1)
            {
                return list[0];
            }
            else
            {
                return new ResourceFiles();
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
                                ResourceFiles record = Instantiate(Query(sql));
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
