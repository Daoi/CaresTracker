using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneUI.Utilities
{
    public class TextModeDateFormatter
    {
        /// <summary>
        /// Formats a mm/dd/yyyy format string to 
        /// yyyy-MM-dd format for use in TextMode=Date controls
        /// </summary>
        /// <param name="date">a string representing a valid date(mm/dd/yyyy from db)</param>
        /// <returns>a string in the yyyy-MM-dd format</returns>
        public static string Format(string date)
        {
            return DateTime.Parse(date).ToString("yyyy-MM-dd");
        }



    }
}