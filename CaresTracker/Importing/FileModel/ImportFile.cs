using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ClosedXML.Excel;
using CsvHelper;
using CsvHelper.Configuration.Attributes;

namespace CaresTracker.Importing.FileModel
{
    public class ImportFile
    {
        [Name("Resident Name")]
        public string ResidentName { get; set; }
        [Name("Gender")]
        public string Gender { get; set; }
        [Name("Race")]
        public string Race { get; set; }
        [Name("Ethnicity")]
        public string Ethnicity { get; set; }
        [Name("Age in Yrs")]
        public string Age { get; set; }
        [Name("Unit Address")]
        public string UnitAddress { get; set; }
        [Name("Apt #")]
        public string AptNum { get; set; }
        [Name("Unit")]
        public string Unit { get; set; }
        [Name("City")]
        public string City { get; set; }
        [Name("State")]
        public string State { get; set; }
        [Name("Postal")]
        public string Zipcode { get; set; }
        [Name("Phone")]
        public string Phone { get; set; }
        [Name("Relationship to HOH")]
        public string HoHRelation { get; set; }
        [Name("HOH Name")]
        public string HoH { get; set; }
        [Name("Client ID")]
        public string ClientID { get; set; }
        [Name("Site Name")]
        public string Development { get; set; }


        public static bool GenerateTemplate()
        {
            try
            {
                XLWorkbook workbook = new XLWorkbook();
                using (workbook)
                {
                    var worksheet = workbook.Worksheets.Add("Import Template");
                    worksheet.Cell("A1").Value = "Resident Name";
                    worksheet.Cell("B1").Value = "Gender";
                    worksheet.Cell("C1").Value = "Race";
                    worksheet.Cell("D1").Value = "Ethnicity";
                    worksheet.Cell("E1").Value = "Age in Yrs";
                    worksheet.Cell("F1").Value = "Unit";
                    worksheet.Cell("G1").Value = "Unit Address";
                    worksheet.Cell("H1").Value = "Apt #";
                    worksheet.Cell("I1").Value = "City";
                    worksheet.Cell("J1").Value = "State";
                    worksheet.Cell("K1").Value = "Postal";
                    worksheet.Cell("L1").Value = "Phone";
                    worksheet.Cell("M1").Value = "Relationship to HOH";
                    worksheet.Cell("N1").Value = "HOH Name";
                    worksheet.Cell("O1").Value = "Client ID";
                    worksheet.Cell("P1").Value = "Site Name";

                    HttpResponse httpResponse = HttpContext.Current.Response;
                    httpResponse.Clear();
                    httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    httpResponse.AddHeader("content-disposition", $"attachment;filename=\"ImportTemplate.xlsx\"");

                    // Flush the workbook to the Response.OutputStream
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        workbook.SaveAs(memoryStream);
                        memoryStream.WriteTo(httpResponse.OutputStream);
                        memoryStream.Close();
                    }
                    httpResponse.End();
                    return true;

                }


            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}