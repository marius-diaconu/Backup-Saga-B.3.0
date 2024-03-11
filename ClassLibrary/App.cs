using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ClassLibrary
{
    public static class App
    {
		public static string backup_app = "BackupApp.exe";

        public static string prog_ver = "v1";

        public static string name = "Saga B";

        public static string ver = "3.0";

        public static string db = $"{RemoveWhitespace(name.ToLower())}_backup_database";

        public static string resource_dir = "C:\\SAGA BACKUPS RESOURCE FILES";

        public static string saga_dir = $"{name.ToUpper()}.{ver} RESOURCE FILES";

        public static string saga_exe = "sb.exe";

        public static string db_dir = "DATABASE";

        public static string logs_dir = "LOGS AND ERRORS";

        public static string log_file = "logs.txt";

        public static string error_file = "errors.txt";

        public static string salv_bd = "salv_bd";

        public static string firmeDbf = "FIRME.DBF";

        public static string parent_dir = "SAGA BACKUPS";

        public static string backup_dir = $"BACKUP {name.ToUpper()}.{ver}";

        public static string active = "active";

        public static string saves = "salvari";

        public static string deleted = "sterse";

        public static string unknown = "necunoscute";

        public static string token_filename = $"{RemoveWhitespace(name.ToLower())}-backup-tokens.json";

        public static string old_task = $"{name}.{ver} Backup Task";

        public static string TaskName = $"{name.Replace(' ', '_')}_Backup_Task";

        public static string TaskAuthor = $"{name.Replace(' ', '_')}_BackupApp";

        public static string TaskDescription = $"{name}.{ver} Backup Daily Task";

        public static string Version = "1.1";

        public static string key = "some-random-string";

        public static string Encrypt(string clearText)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(
                    key, 
                    new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 }
                );

                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Decrypt(string cipherText)
        {
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(
                    key, 
                    new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 }
                );

                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public static string RemoveWhitespace(string str)
        {
            return string.IsNullOrEmpty(str) ? str : new string(str.Where(x => !char.IsWhiteSpace(x)).ToArray());
        }
    }
}
