using System;
using System.Linq;
using System.Text;

namespace ClassLibrary
{
    public class Str
    {
        private static readonly Random _random = new Random();
        public string Guid { get; set; }

        /// <summary>
        /// generates random string
        /// </summary>
        /// <param name="table">string table name</param>
        /// <param name="length">integer</param>
        /// <param name="lowerCase">boolean</param>
        /// <returns>unique random string</returns>
        public static string Random(string table, int length, bool lowerCase)
        {
            return CheckIfExists(table, BuildRandomString(length, lowerCase), length, lowerCase);
        }

        /// <summary>
        /// class private static method CheckIfExists()
        /// </summary>
        /// <param name="table">string table name</param>
        /// <param name="guid">string guid</param>
        /// <param name="length">integer guid length</param>
        /// <param name="lowerCase"></param>
        /// <returns>unique guid</returns>
        private static string CheckIfExists(string table, string guid, int length, bool lowerCase)
        {
            var sql = $"SELECT `Guid` FROM `{table}` WHERE `Guid` = '{Utility.Trim(guid)}'";
            var results = Database.Query<Str>(sql);

            if (results.Count > 0)
            {
                foreach (var result in results)
                {
                    if (string.Compare(guid, result.Guid, false) == 0)
                    {
                        return Random(table, length, lowerCase);
                    }
                }
                return guid;
            }
            return guid;
        }

        /// <summary>
        /// random string constructor
        /// </summary>
        /// <param name="size"></param>
        /// <param name="lower_case"></param>
        /// <returns>random string</returns>
        private static string BuildRandomString(int size, bool lower_case = false)
        {
            var builder = new StringBuilder(size);
            char offset = lower_case ? 'a' : 'A';
            const int letters_offset = 26;
            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + letters_offset);
                builder.Append(@char);
            }
            return lower_case ? builder.ToString().ToLower() : builder.ToString();
        }

    } /// end of Str class declaration
}
