using System;
using System.Linq;
using System.Net.Mail;

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
    }
}
