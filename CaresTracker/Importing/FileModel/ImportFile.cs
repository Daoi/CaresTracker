using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CsvHelper;
using CsvHelper.Configuration.Attributes;

namespace CaresTracker.Importing.FileModel
{
    public class ImportFile
    {
        [Name("FIRST NAME")]
        public string ResidentFirstName { get; set; }
        [Name("LAST NAME")]
        public string ResidentLastName { get; set; }
        [Name("GENDER")]
        public string Gender { get; set; }
        [Name("RACE")]
        public string Race { get; set; }
        [Name("ETHNICITY")]
        public string Ethnicity { get; set; }
        [Name("DATE OF BIRTH")]
        public string DoB { get; set; }
        [Name("AGE")]
        public string Age { get; set; }
        [Name("CURRENT ADDRESS-1")]
        public string Address { get; set; }
        [Name("CURRENT ADDRESS-2")]
        public string SecondaryAddress { get; set; }
        [Name("CURRENT CITY")]
        public string City { get; set; }
         [Name("CURRENT STATE")]
        public string State { get; set; }
        [Name("CURRENT ZIP CODE")]
        public string Zipcode { get; set; }
        [Name("PRIMARY E-MAIL")]
        public string Email { get; set; }
        [Name("HEAD OF HOUSEHOLD-STATUS")] 
        public string HoHRelation { get; set; }
        [Name("HEAD OF HOUSEHOLD")]
        public string HoH { get; set; }


    }
}