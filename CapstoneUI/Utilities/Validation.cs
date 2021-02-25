using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace CapstoneUI.Utilities
{
    /// <summary>
    /// The main Validation class.
    /// Contains static methods for validating strings.
    /// </summary>
    public static class Validation
    {
        /// <summary>
        /// Checks if a string represents a valid date.
        /// </summary>
        /// <param name="input">String to be checked.</param>
        /// <param name="outDateTime">A DateTime reference.</param>
        /// <returns>True for a match.</returns>
        public static bool IsDate(string input, out DateTime outDateTime)
        {
            return DateTime.TryParse(input, out outDateTime);
        }

        /// <summary>
        /// Checks if a string represents a valid time in 12-hour format.
        /// Valid: 9:30am, 12:15PM
        /// </summary>
        /// <param name="input"></param>
        /// <returns>True for a match.</returns>
        public static bool IsTime(string input)
        {
            return Regex.IsMatch(input, @"^(1[012]|[1-9]):[0-5][0-9](\\s)?(?i)(am|pm)$");
        }

        /// <summary>
        /// Checks if a string represents a valid email address.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>True for a match.</returns>
        public static bool IsEmail(string input)
        {
            return Regex.IsMatch(input, @".{1,}@[^.]{1,}"); // update the regex
        }

        /// <summary>
        /// Checks if a string represents a valid phone number.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>True for a match.</returns>
        public static bool IsPhoneNumber(string input)
        {
            return Regex.IsMatch(input, @"^(\+\d{11})$");
        }

        /// <summary>
        /// Checks if a string is only composed of numbers
        /// </summary>
        /// <param name="input"></param>
        /// <returns>True for a match.</returns>
        public static bool IsDigits(string input)
        {
            return Regex.IsMatch(input, @"^[0-9]\d*$");
        }

        /// <summary>
        /// Checks if a string only has letters.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>True for a match.</returns>
        public static bool IsLetters(string input)
        {
            return Regex.IsMatch(input, "^[a-zA-Z]+$");
        }

        /// <summary>
        /// Checks if a string is composed of alphanumeric characters.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>True for a match.</returns>
        public static bool IsAlphanumeric(string input)
        {
            return Regex.IsMatch(input, @"^[a-zA-Z0-9]+$");
        }

        /// <summary>
        /// Checks if a string represents a positive integer.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="outInt">int reference</param>
        /// <returns>True for a match.</returns>
        public static bool IsPositiveInt(string input, out int outInt)
        {
            bool isInt = int.TryParse(input, out outInt);
            return isInt && outInt > 0;
        }

        /// <summary>
        /// Checks if a string is empty.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>True for empty strings.</returns>
        public static bool IsEmpty(string input)
        {
            return input == "";
        }

        /// <summary>
        /// Checks if a string matches our password requirements.
        /// At least 8 characters long
        /// At least one lowercase letter
        /// At least one uppercase letter
        /// At least one number
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsValidPassword(string input)
        {
            return Regex.IsMatch(input, @"^(?=.{8,32}$)(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9]).*");
        }
    }
}