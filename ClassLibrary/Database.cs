using System;
using System.IO;
using System.Collections.Generic;
using System.Data.SQLite;
using Dapper;

namespace ClassLibrary
{
    public static class Database
    {
        /**
         * ----------------------------------
         * Start declaring class properties
         * ----------------------------------
         */
        private static bool _is_open;
        private static string _database_name;
        private static string _database_path;
        private static SQLiteConnection _sqlite;

        /// <summary>
        /// class static constructor Database
        /// </summary>
        static Database()
        {
            string db_path = Path.Combine(App.resource_dir, Path.Combine(App.saga_dir, App.db_dir));
            DatabaseName = $"{App.db}.db";
            DatabasePath = Path.Combine(db_path, DatabaseName);

            Utility.DirExistsCheckOrCreate(@db_path);
            Utility.ChangePermissions(@db_path);


            if (CheckIfDatabaseExists())
            {
                Sqlite = new SQLiteConnection("Data Source=" + DatabasePath);

                if (OpenConn())
                {
                    IsOpen = true;
                }
            }
        }

        /// <summary>
        /// class public static property sqlite
        /// </summary>
        public static SQLiteConnection Sqlite
        {
            get { return _sqlite; }
            set { _sqlite = value; }
        }

        /// <summary>
        /// class public static property IsOpen
        /// </summary>
        public static bool IsOpen
        {
            get { return _is_open; }
            set { _is_open = value; }
        }

        /// <summary>
        /// class public static property DatabaseName
        /// </summary>
        public static string DatabaseName
        {
            get { return _database_name; }
            set { _database_name = value; }
        }

        /// <summary>
        /// class public static property DatabasePath
        /// </summary>
        public static string DatabasePath
        {
            get { return _database_path; }
            set { _database_path = value; }
        }

        /// <summary>
        /// class static method OpenConn()
        /// </summary>
        /// <returns>boolean true if connection is successfully opened</returns>
        public static bool OpenConn()
        {
            try
            {
                Sqlite.Open();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// class static method CloseConn()
        /// </summary>
        /// <returns>boolean true if connection successfully closed</returns>
        public static bool CloseConn()
        {
            try
            {
                Sqlite.Close();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// class static method InsertBuilder
        /// </summary>
        /// <param name="table">string table name</param>
        /// <param name="fields">string array of table fields</param>
        /// <returns>string of query sql</returns>
        public static string InsertBuilder(string table, string[] fields)
        {
            return $"INSERT INTO `{table}` (`{string.Join("`, `", fields)}`) VALUES (@{string.Join(", @", fields)})";
        }

        /// <summary>
        /// class static method UpdateBuilder
        /// </summary>
        /// <param name="table">string of table name</param>
        /// <param name="fields">string array of table fields</param>
        /// <param name="column">string of column name for WHERE clause</param>
        /// <returns>string of query sql</returns>
        public static string UpdateBuilder(string table, string[] fields, string column)
        {
            string stringBuilder = null;

            for (int i = 0; i < fields.Length; i++)
            {
                if (i == (fields.Length - 1)) stringBuilder += $"`{fields[i]}` = @{fields[i]}";
                else stringBuilder += $"`{fields[i]}` = @{fields[i]}, ";
            }

            return $"UPDATE `{table}` SET {stringBuilder} WHERE `{column.ToUpper()}` = @{column.ToUpper()}";
        }

        /// <summary>
        /// class static method Query()
        /// </summary>
        /// <param name="sql">string of query sql</param>
        /// <returns>list of template objects</returns>
        public static List<T> Query<T>(string sql)
        {
            try
            {
                return Sqlite.Query<T>(sql).AsList();
            }
            catch (Exception e)
            {
                Utility.DisplayError(string.Format(e.Message));
                throw e;
            }
        }

        /// <summary>
        /// class static method Query()
        /// </summary>
        /// <param name="sql">string of query sql</param>
        /// <param name="obj">bindable object</param>
        /// <returns>list of template objects</returns>
        public static List<T> Query<T>(string sql, object obj)
        {
            try
            {
                return Sqlite.Query<T>(sql, obj).AsList();
            }
            catch (Exception e)
            {
                Utility.DisplayError(string.Format(e.Message));
                throw e;
            }
        }

        /// <summary>
        /// class static method Execute()
        /// </summary>
        /// <param name="sql">string of query sql</param>
        /// <returns>the number of affected rows</returns>
        public static int Execute(string sql)
        {
            try
            {
                int affectedRows = Sqlite.Execute(sql);
                return (affectedRows > 0) ? affectedRows : 0;
            }
            catch (Exception e)
            {
                Utility.DisplayError(string.Format(e.Message));
                throw e;
            }
        }

        /// <summary>
        /// class static method Execute()
        /// </summary>
        /// <param name="sql">string of query sql</param>
        /// <param name="obj">bindable object</param>
        /// <returns>integer number of affected rows</returns>
        public static int Execute(string sql, object obj)
        {
            try
            {
                int affectedRows = Sqlite.Execute(sql, obj);
                return (affectedRows > 0) ? affectedRows : 0;
            }
            catch (Exception e)
            {
                Utility.DisplayError(string.Format(e.Message));
                throw e;
            }
        }

        public static bool CheckIfDatabaseExists()
        {
            if (!File.Exists(DatabasePath))
            {
                SQLiteConnection.CreateFile(DatabasePath);
            }
            return true;
        }

        /// <summary>
        /// create database tables if don't exists
        /// </summary>
        public static void CheckIfTablesExists()
        {
            if (!Table.Exists(ResourceFiles.GetTable()))
            {
                _ = ResourceFiles.CreateTable();
                ResourceFiles rf = ResourceFiles.First();
                if (!rf.Exists())
                {
                    Utility.DirExistsCheckOrCreate(Path.Combine(App.resource_dir, Path.Combine(App.saga_dir, App.logs_dir)));
                    Utility.ChangePermissions(Path.Combine(App.resource_dir, Path.Combine(App.saga_dir, App.logs_dir)));
                    rf.ResourcePath = Path.Combine(App.resource_dir, Path.Combine(App.saga_dir, App.logs_dir));
                    rf.LogFileName = App.log_file;
                    rf.ErrorFileName = App.error_file;
                    rf.SalvBd = App.salv_bd;
                    rf.FirmeDBF = App.firmeDbf;
                    rf.Save();
                    Utility.CreateLogFile();
                    Utility.CreateErrorFile();
                }
                else
                {
                    Utility.DirExistsCheckOrCreate(@rf.ResourcePath);
                    Utility.ChangePermissions(@rf.ResourcePath);
                    Utility.CreateLogFile();
                    Utility.CreateErrorFile();
                }
            }

            if (!Table.Exists(BackupSettings.GetTable()))
            {
                _ = BackupSettings.CreateTable();
            }

            if (!Table.Exists(Tasks.GetTable()))
            {
                _ = Tasks.CreateTable();
            }

            if (!Table.Exists(Company.GetTable()))
            {
                _ = Company.CreateTable();
            }

            if (!Table.Exists(CompanyRecords.GetTable()))
            {
                _ = CompanyRecords.CreateTable();
            }
            return;
        }

        public static void RefreshTables()
        {
            if (Table.Exists(ResourceFiles.GetTable()) && ResourceFiles.Count() > 1)
            {
                ResourceFiles.RefreshTable();
            }

            if (Table.Exists(BackupSettings.GetTable()) && BackupSettings.Count() > 1)
            {
                BackupSettings.RefreshTable();
            }

            if (Table.Exists(Tasks.GetTable()) && Tasks.Count() > 1)
            {
                Tasks.RefreshTable();
            }

            if (Table.Exists(Company.GetTable()) && Company.Count() > 1)
            {
                Company.RefreshTable();
            }

            if (Table.Exists(CompanyRecords.GetTable()) && CompanyRecords.Count() > 1)
            {
                CompanyRecords.RefreshTable();
            }
            return;
        }
    }
}
