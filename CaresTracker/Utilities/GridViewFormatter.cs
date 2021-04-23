using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaresTracker.Utilities
{
    public class GridViewFormatter
    {
        public static string FormatDates(object value)
        {
            try
            {
                return DateTime.Parse(value.ToString()).ToString("MM-dd-yyyy");
            }
            catch (Exception e)
            {
                return "Invalid value";
            }
        }

        public static string FormatTimes(object value)
        {
            try
            {
                return DateTime.Parse(value.ToString()).ToString("hh:mm tt");
            }
            catch (Exception e)
            {
                return "Invalid value";
            }
        }

    }
}