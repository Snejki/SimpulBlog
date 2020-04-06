using System;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace SimpulBlog.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsOnlyLettersOrWhiteSpaces(this string text)
        {
            return text.All(x => char.IsLetter(x) || char.IsWhiteSpace(x));
        }

        public static bool IsOnlyLetters(this string text)
        {
            return text.All(x => char.IsLetter(x));
        }

        public static bool IsValiDEmailAddress(this string email)
        {
            try
            {
                new MailAddress(email);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static bool IsTag(this string text)
        {
            return text.First() == '#' && text.Skip(1).All(x => char.IsLetter(x) || char.IsLower(x) || x == '_');
        }

        public static string GenerateSlug(this string phrase)
        {
            string str = phrase.RemoveAccent().ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        private static string RemoveAccent(this string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
    }
}
