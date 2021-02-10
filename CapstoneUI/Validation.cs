using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace CapstoneUI
{
    public static class Validation
    {
        public static bool IsDate(string input, out DateTime outDateTime)
        {
            return DateTime.TryParse(input, out outDateTime);
        }

        public static bool IsTime(string input)
        {
            string clean = input.ToLower().Trim(new Char[] { 'p', 'a', 'm', ' ' });
            return Regex.IsMatch(clean, @"^(?:\d|[01]\d|2[0-3]):[0-5]\d$");
        }

        // very simple email matcher -- need to confirm by sending an actual email instead
        public static bool IsEmail(string input)
        {
            return Regex.IsMatch(input, @".{1,}@[^.]{1,}");
        }

        public static bool IsPhoneNumber(string input)
        {
            return Regex.IsMatch(input, @"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$") 
                || Regex.IsMatch(input, @"^\d{10}$");
        }

        // returns true is string is only numbers
        public static bool IsDigits(string input)
        {
            return Regex.IsMatch(input, @"^[0-9]\d*$");
        }

        public static bool IsLetters(string input)
        {
            return Regex.IsMatch(input, "^[a-zA-Z]+$");
        }

        public static bool IsAlphanumeric(string input)
        {
            return Regex.IsMatch(input, @"^[a-zA-Z0-9]+$");
        }

        public static bool IsPositiveInt(string input, out int outInt)
        {
            bool isInt = int.TryParse(input, out outInt);
            return isInt && outInt > 0;
        }

        public static bool IsEmpty(string input)
        {
            return input == "";
        }
    }
}