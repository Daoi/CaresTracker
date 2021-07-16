using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaresTracker.Utilities
{
    public class NameCapitalization
    {
        public static string FormatName(string input)
        {

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new FormatException("Invalid or empty name");
            }

            input = input.ToLower();

            if (input.Length > 1)
                return char.ToUpper(input[0]) + input.Substring(1);

            return input.ToUpper();

        }
    }
}