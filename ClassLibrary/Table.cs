using Dapper;
using System;

namespace ClassLibrary
{
    class Table
    {
        private static readonly string _table = "sqlite_master";
        public string type { get; set; }
        public string name { get; set; }

        /// <summary>
        /// get database table name
        /// </summary>
        /// <returns>string of database table name</returns>
        public static string GetTable() { return _table; }

        /// <summary>
        /// class static method Exists()
        /// </summary>
        /// <param name="table">string of table name</param>
        /// <returns>boolean true if database has table</returns>
        public static bool Exists(string table)
        {
            var sql = $"SELECT * FROM `{GetTable()}` WHERE type = 'table' AND name = '{table}'";

            try
            {
                var results = Database.Query<Table>(sql);
                if (results.Count > 0)
                {
                    foreach (var result in results)
                    {
                        if (string.Compare(table, result.name) == 0) return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// class public static Execute()
        /// </summary>
        /// <param name="sql">string of query sql</param>
        /// <returns>boolean</returns>
        public static bool Execute(string sql)
        {
            try
            {
                Database.Sqlite.Execute(sql);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// internal class Column
        /// </summary>
        internal class Column
        {
            public string name { get; set; }

            /// <summary>
            /// class static method Exists()
            /// </summary>
            /// <param name="table">string of table name</param>
            /// <param name="column">string of column name</param>
            /// <returns>boolean true if table has column</returns>
            public static bool Exists(string table, string column)
            {
                var sql = $"SELECT * FROM pragma_table_info('{table}') WHERE name = '{column}'";

                try
                {
                    var results = Database.Query<Column>(sql);
                    if (results.Count > 0)
                    {
                        foreach (var result in results)
                        {
                            if (string.Compare(column, result.name) == 0) return true;
                        }
                        return false;
                    }
                    return false;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        } /// end of Column internal class declaration

    } /// end of Table class declaration
}
