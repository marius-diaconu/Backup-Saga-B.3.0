using Microsoft.Win32.TaskScheduler;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class Tasks
    {
        /**
         * ----------------------------------
         * Start declaring class properties
         * ----------------------------------
         */
        public enum Type { DAILY, WEEKLY };
        private static readonly string _table = "Tasks";
        public int ID { get; set; }
        public string TaskName { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string TaskType { get; set; }
        public string TaskWeekDay { get; set; }
        public string TaskTime { get; set; }
        public int IsAuto { get; set; }
        public int IsShutdown { get; set; }
        public string ExePath { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }

        private static readonly string[] TableFields = {
            "TaskName",
            "Author",
            "Description",
            "TaskType",
            "TaskWeekDay",
            "TaskTime",
            "IsAuto",
            "IsShutdown",
            "ExePath",
            "CreatedAt",
            "UpdatedAt"
        };

        /**
         *------------------------------------------
         *  Start of create windows tasks methods
         *------------------------------------------
         */

        /// <summary>
        /// create a task
        /// </summary>
        /// <retrurns>boolean</retrurns>
        public bool CreateTask()
        {
            using (TaskService ts = new TaskService())
            {
                TaskDefinition td = ts.NewTask();
                string TaskName = App.TaskName.ToString();
                td.RegistrationInfo.Author = App.TaskAuthor.ToString(); ;
                td.RegistrationInfo.Description = App.TaskDescription.ToString();
                td.Settings.Priority = System.Diagnostics.ProcessPriorityClass.Normal;
                td.Settings.StartWhenAvailable = false;
                td.Settings.Enabled = true;

                if (this.TaskType == Type.DAILY.ToString())
                {
                    td.Triggers.Add(
                    new DailyTrigger
                    {
                        StartBoundary = DateTime.Parse(this.TaskTime.ToString()),
                        DaysInterval = 1
                    });
                }

                if (this.TaskType == Type.WEEKLY.ToString())
                {
                    td.Triggers.Add(
                    new WeeklyTrigger
                    {
                        StartBoundary = DateTime.Parse(this.TaskTime),
                        DaysOfWeek = this.GetDayOfTheWeek(),
                        WeeksInterval = 1
                    });
                }

                td.Actions.Add(new ExecAction(this.ExePath, null, null));
                ts.RootFolder.RegisterTaskDefinition(@TaskName, td);
                return true;
            }
        }

        /// <summary>
        /// checks if task already exists
        /// </summary>
        /// <returns>boolean</returns>
        public bool TaskExists()
        {
            using (TaskService ts = new TaskService())
            {
                Task task = ts.GetTask(@App.TaskName);
                if (task != null)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// deletes task
        /// </summary>
        /// <returns>boolean</returns>
        public bool DeleteTask()
        {
            using (TaskService ts = new TaskService())
            {
                Task task = ts.GetTask(@App.TaskName);
                if (task != null)
                {
                    ts.RootFolder.DeleteTask(@App.TaskName);
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// get day of the week from TaskWeekDay
        /// </summary>
        /// <returns>DayOfTheWeek day</returns>
        public DaysOfTheWeek GetDayOfTheWeek()
        {
            if (DayOfWeek.Sunday.ToString() == TaskWeekDay) return DaysOfTheWeek.Sunday;
            if (DayOfWeek.Monday.ToString() == TaskWeekDay) return DaysOfTheWeek.Monday;
            if (DayOfWeek.Tuesday.ToString() == TaskWeekDay) return DaysOfTheWeek.Tuesday;
            if (DayOfWeek.Wednesday.ToString() == TaskWeekDay) return DaysOfTheWeek.Wednesday;
            if (DayOfWeek.Thursday.ToString() == TaskWeekDay) return DaysOfTheWeek.Thursday;
            if (DayOfWeek.Friday.ToString() == TaskWeekDay) return DaysOfTheWeek.Friday;
            if (DayOfWeek.Saturday.ToString() == TaskWeekDay) return DaysOfTheWeek.Saturday;

            return DaysOfTheWeek.Friday;
        }

        /**
        *---------------------------
        *  Start of class methods
        *---------------------------
        */
        public static string GetTable() { return _table; }

        /// <summary>
        /// create database table if not exists
        /// </summary>
        /// <returns></returns>
        public static bool CreateTable()
        {
            var sql = $"CREATE TABLE IF NOT EXISTS `{GetTable()}` (" +
                    "`ID` INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "`TaskName` TEXT NOT NULL, " +
                    "`Author` TEXT NOT NULL, " +
                    "`Description` TEXT NOT NULL, " +
                    "`TaskType` TEXT NULL DEFAULT NULL, " +
                    "`TaskWeekDay` TEXT NULL DEFAULT NULL, " +
                    "`TaskTime` TEXT NULL DEFAULT NULL, " +
                    "`IsAuto` INTEGER UNSIGNED NOT NULL DEFAULT 0, " +
                    "`IsShutdown` INTEGER UNSIGNED NULL DEFAULT 0, " +
                    "`ExePath` TEXT NOT NULL, " +
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
        public static List<Tasks> Query(string sql)
        {
            return Database.Query<Tasks>(sql);
        }

        /// <summary>
        /// get first resource from database order by id asc
        /// </summary>
        /// <returns>found resource</returns>
        public static Tasks First()
        {
            var sql = $"SELECT * FROM `{GetTable()}` ORDER BY `ID` ASC LIMIT 1";
            return Instantiate(Query(sql));
        }

        /// <summary>
        /// get last resource from database order by id desc
        /// </summary>
        /// <returns>found resource</returns>
        public static Tasks Last()
        {
            var sql = $"SELECT * FROM `{GetTable()}` ORDER BY `ID` DESC LIMIT 1";
            return Instantiate(Query(sql));
        }

        /// <summary>
        /// get resource where column is equal with provided value
        /// </summary>
        /// <param name="column">string column name</param>
        /// <param name="value">string column value</param>
        /// <returns>found resource</returns>
        public static Tasks FindOrFail(string column, string value)
        {
            var sql = $"SELECT * FROM `{GetTable()}` WHERE `{Utility.UcFirst(Utility.Trim(column))}` = " +
                $"'{Utility.Trim(value)}' AND `DeletedAt` IS NULL";
            return Instantiate(Query(sql));
        }

        /// <summary>
        /// get all untrashed resources
        /// </summary>
        /// <returns>enumerable list of resources</returns>
        public static List<Tasks> Get()
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
        /// checks if resource is persisted or not
        /// </summary>
        /// <returns>boolean</returns>
        public bool Exists()
        {
            return Convert.ToBoolean(ID);
        }

        /// <summary>
        /// create database record from unpersisted resource
        /// or update persisted resource
        /// </summary>
        /// <returns>boolean</returns>
        public bool Save()
        {
            return Convert.ToBoolean(ID) ? Update() : Create();
        }

        /// <summary>
        /// create database record
        /// </summary>
        /// <returns>boolean</returns>
        private bool Create()
        {
            TaskName = App.TaskName;
            Author = App.TaskAuthor;
            Description = App.TaskDescription;
            CreatedAt = Utility.GetTimestamp();
            ID = Database.Execute(Database.InsertBuilder(GetTable(), TableFields), this);
            return Convert.ToBoolean(ID);
        }

        /// <summary>
        /// update database record
        /// </summary>
        /// <returns>boolean</returns>
        private bool Update()
        {
            UpdatedAt = Utility.GetTimestamp();
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
        /// instantiate first element of a resources list
        /// or create a new class instance
        /// </summary>
        /// <param name="list">enumerable list of resources</param>
        /// <returns>class instance</returns>
        private static Tasks Instantiate(List<Tasks> list)
        {
            if (list.Count > 0 && list.Count == 1)
            {
                return list[0];
            }
            else
            {
                return new Tasks();
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
                                Tasks record = Instantiate(Query(sql));
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
