using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class SqliteSequence
    {
        /**
         * ----------------------------------
         * Start declaring class properties
         * ----------------------------------
         */
        private static readonly string _table = "sqlite_sequence";
        public string name { get; set; }
        public long seq { get; set; }

        /**
         * ---------------------
         * Start class methods
         * ---------------------
         */
        public static string GetTable() { return _table; }

        /**
         * class exists method
         * no params required
         * checks if resource exists
         * return boolean
         */
        public bool Exists()
        {
            return Convert.ToBoolean(seq);
        } /// end of Exists method

        /**
         * class first method
         * require string param order, default ascending
         * retrun first or last persisted resource 
         */
        public static SqliteSequence First(string name)
        {
            var sql = $"SELECT * FROM `{GetTable()}` WHERE `name` = '{Utility.Trim(name)}' LIMIT 1";
            return Instantiate(Database.Query<SqliteSequence>(sql));
        } /// end of first method

        /**
         * class private update method
         * no params required
         * return true if resource successfully updated 
         */
        public bool Update()
        {
            var sql = $"UPDATE `{GetTable()}` SET `seq` = @seq WHERE `name` = @name";
            int affectedRows = Database.Execute(sql, this);
            return Convert.ToBoolean(affectedRows);
        } /// end of update method

        /**
         * class destroy method
         * no params required
         * destroy resource
         * return boolean
         */
        public bool Destroy()
        {
            var sql = $"DELETE FROM `{GetTable()}` WHERE `name` = @name";
            int affectedRows = Database.Execute(sql, this);
            return Convert.ToBoolean(affectedRows);
        } /// end of destroy method

        /**
         * ------------------------------
         * Start class utility methods
         * ------------------------------
         */

        /**
         * class override ToString method
         */
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        } /// end of ToString method

        /**
         * class private static method instantiate()
         * required param List<class> list
         * return list[0] if (list.Count > 0) or new class instance
         */
        private static SqliteSequence Instantiate(List<SqliteSequence> list)
        {
            if (list.Count > 0 && list.Count == 1)
            {
                return list[0];
            }
            else
            {
                return new SqliteSequence();
            }
        } /// end of instantiate method

    } /// end of SqliteSequence class
}
