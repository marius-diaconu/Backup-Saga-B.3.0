using System;
using System.IO;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Text.RegularExpressions;

namespace ClassLibrary
{
    public static class Utility
    {
        public static string GetTimestamp()
        {
            DateTime date = DateTime.Now;
            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string GetDate()
        {
            DateTime date = DateTime.Now;
            return date.ToString("dd-MM-yyyy");
        }

        public static bool Timer(int time)
        {
            Thread.Sleep(time);
            return true;
        }

        public static void InsertTopLine(int lines)
        {
            Console.WriteLine("/*");
            for (int i = 0; i < lines; i++)
            {
                if (i == 0) Console.Write('|');
                else if (i == (lines - 1)) Console.WriteLine("=");
                else Console.Write('=');
            }
        }

        public static void InsertBottomLine(int lines)
        {
            for (int i = 0; i < lines; i++)
            {
                if (i == 0) Console.Write('|');
                else if (i == (lines - 1)) Console.WriteLine('=');
                else Console.Write('=');
            }

            Console.WriteLine("*/");
        }

        public static bool ChangePermissions(string path)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path) 
                {
                    Attributes = FileAttributes.Normal
                };
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool DirExistsCheckOrCreate(string path)
        {
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                    return true;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return true;
        }

        public static string Trim(string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                str = str.TrimStart(' ');
                str = str.TrimEnd(' ');
                return str.ToString();
            }
            return null;
        }

        public static void InitWarning(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();

            msg = " AVERTISMENT: " + msg;
            int width = Console.WindowWidth;
            string pattern = @"(?<line>.{1," + width + @"})(?<!\s)(\s+|$)|(?<line>.+?)(\s+|$)";
            var lines = Regex.Matches(msg, pattern).Cast<Match>().Select(m => m.Groups["line"].Value);

            foreach (var line in lines)
            {
                Console.WriteLine("{0, 10}", line);
            }

            Console.ResetColor();
        }

        public static void InitMessage(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();

            msg = " Mesaj: " + msg;
            int width = Console.WindowWidth;
            string pattern = @"(?<line>.{1," + width + @"})(?<!\s)(\s+|$)|(?<line>.+?)(\s+|$)";
            var lines = Regex.Matches(msg, pattern).Cast<Match>().Select(m => m.Groups["line"].Value);

            foreach (var line in lines)
            {
                Console.WriteLine("{0, 10}", line);
            }

            Console.ResetColor();
        }

        public static void DisplayMessage(string msg)
        {
            ResourceFiles rf = ResourceFiles.First();
            string file = Path.Combine(rf.ResourcePath, rf.LogFileName);

            if (!File.Exists(@file)) File.Create(@file);

            using (StreamWriter logFile = new StreamWriter(file, true))
            {
                logFile.WriteLine(msg + ". Timestamp: " + Utility.GetTimestamp());
                logFile.Close();
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();

            msg = " Mesaj: " + msg;
            int width = Console.WindowWidth;
            string pattern = @"(?<line>.{1," + width + @"})(?<!\s)(\s+|$)|(?<line>.+?)(\s+|$)";
            var lines = Regex.Matches(msg, pattern).Cast<Match>().Select(m => m.Groups["line"].Value);

            foreach (var line in lines)
            {
                Console.WriteLine("{0, 10}", line);
            }

            Console.ResetColor();
        }

        public static void InitError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();

            error = " Eroare: " + error;
            int width = Console.WindowWidth;
            string pattern = @"(?<line>.{1," + width + @"})(?<!\s)(\s+|$)|(?<line>.+?)(\s+|$)";
            var lines = Regex.Matches(error, pattern).Cast<Match>().Select(m => m.Groups["line"].Value);

            foreach (var line in lines)
            {
                Console.WriteLine("{0, 10}", line);
            }

            Console.ResetColor();
        }

        public static void DisplayError(string error)
        {
            ResourceFiles rf = ResourceFiles.First();
            string file = Path.Combine(rf.ResourcePath, rf.ErrorFileName);

            if (!File.Exists(@file)) File.Create(@file);

            using (StreamWriter errFile = new StreamWriter(file, true))
            {
                errFile.WriteLine(error + ", error date: " + Utility.GetTimestamp());
                errFile.Close();
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();

            error = " Eroare: " + error;
            int width = Console.WindowWidth;
            string pattern = @"(?<line>.{1," + width + @"})(?<!\s)(\s+|$)|(?<line>.+?)(\s+|$)";
            var lines = Regex.Matches(error, pattern).Cast<Match>().Select(m => m.Groups["line"].Value);

            foreach (var line in lines)
            {
                Console.WriteLine("{0, 10}", line);
            }

            Console.ResetColor();
        }

        public static bool CheckIfLogFileExists()
        {
            ResourceFiles rf = ResourceFiles.First();
            if (rf.Exists())
            {
                string file = Path.Combine(rf.ResourcePath, rf.LogFileName);
                if (File.Exists(@file)) return true;
            }
            return false;
        }

        public static bool CheckIfErrorFileExists()
        {
            ResourceFiles rf = ResourceFiles.First();
            if (rf.Exists())
            {
                string file = Path.Combine(rf.ResourcePath, rf.ErrorFileName);
                if (File.Exists(@file)) return true;
            }
            return false;
        }

        public static bool CreateLogFile()
        {
            ResourceFiles rf = ResourceFiles.Last();
            string file = Path.Combine(rf.ResourcePath, rf.LogFileName);

            if (!File.Exists(@file))
            {
                FileStream fs = File.Create(@file);
                fs.Close();
                Credentials.InsertCredentials(file);
                return true;
            }
            else
            {
                File.WriteAllText(@file, string.Empty);
                Credentials.InsertCredentials(file);
                return true;
            }
        }

        public static bool CreateErrorFile()
        {
            ResourceFiles rf = ResourceFiles.Last();
            string file = Path.Combine(rf.ResourcePath, rf.ErrorFileName);

            if (!File.Exists(@file))
            {
                FileStream fs = File.Create(@file);
                fs.Close();
                Credentials.InsertCredentials(file);
                return true;
            }
            else
            {
                File.WriteAllText(@file, string.Empty);
                Credentials.InsertCredentials(file);
                return true;
            }
        }

        public static void DisplayNotification(string msg, int ms)
        {
            var notification = new NotifyIcon()
            {
                Visible = true,
                Icon = SystemIcons.Information,
                BalloonTipText = msg,
            };

            notification.ShowBalloonTip(ms);

            notification.Dispose();
        }

        /// <summary>
        /// capitalize first character of a string
        /// </summary>
        /// <param name="input">string input</param>
        /// <returns>string</returns>
        public static string UcFirst(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            return $"{input[0].ToString().ToUpper()}{input.Substring(1)}";
        }
    }
}
