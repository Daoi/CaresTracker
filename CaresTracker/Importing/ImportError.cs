using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaresTracker.Importing
{
    public class ImportError
    {
        public int Row { get; set; }
        public string ErrorText { get; set; }
        public string ResidentName { get; set; }

        public ImportError(int index, string et, string name)
        {
            Row = index;
            ErrorText = et;
            ResidentName = name;
        }

        public override string ToString()
        {
            return $"Row #{Row} with Resident Name: {ResidentName} failed due to error: {ErrorText}";
        }
    }
}