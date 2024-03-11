using System;
using System.IO;

namespace ClassLibrary
{
    public static class Credentials
    {
        public static void DisplayCredentials(int topLine)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Utility.InsertTopLine(topLine);
            Console.WriteLine("|");
            Console.WriteLine($"|    {App.name}.{App.ver} Backup App created by: Marius Diaconu");
            Console.WriteLine("|    Created on: July 2020");
            Console.WriteLine("|    Phone: +40760545808");
            Console.WriteLine("|    Email: marius.diaconu76@gmail.com");
            Console.WriteLine("|    Follow me on: https://www.facebook.com/backup.saga");
            Console.WriteLine("|");
            Utility.InsertBottomLine(topLine);
            Console.ResetColor();
        }

        public static bool InsertCredentials(string filePath)
        {
            int lines = 75;
            using (StreamWriter file = new StreamWriter(filePath, true))
            {
                file.WriteLine("/*");
                for (int i = 0; i < lines; i++)
                {
                    if (i == 0) file.Write('|');
                    else if (i == (lines - 1)) file.WriteLine("=");
                    else file.Write('=');
                }

                file.WriteLine("|");
                file.WriteLine($"|    {App.name}.{App.ver} Backup App created by: Marius Diaconu");
                file.WriteLine("|    Created on: July 2020");
                file.WriteLine("|    Phone: +40760545808");
                file.WriteLine("|    Email: marius.diaconu76@gmail.com");
                file.WriteLine("|    Follow me on: https://www.facebook.com/backup.saga");
                file.WriteLine("|");

                for (int i = 0; i < lines; i++)
                {
                    if (i == 0) file.Write('|');
                    else if (i == (lines - 1)) file.WriteLine('=');
                    else file.Write('=');
                }

                file.WriteLine("*/");
                file.Close();
                return true;
            }
        }
    }
}
