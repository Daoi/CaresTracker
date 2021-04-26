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


        public static bool GenerateTemplate()
        {
            try
            {
                XLWorkbook workbook = new XLWorkbook();
                using (workbook)
                {
                    var worksheet = workbook.Worksheets.Add("Import Template");
                    worksheet.Cell("A1").Value = "LAST NAME";
                    worksheet.Cell("B1").Value = "FIRST NAME";
                    worksheet.Cell("C1").Value = "GENDER";
                    worksheet.Cell("D1").Value = "RACE";
                    worksheet.Cell("E1").Value = "ETHNICITY";
                    worksheet.Cell("F1").Value = "DATE OF BIRTH";
                    worksheet.Cell("G1").Value = "AGE";
                    worksheet.Cell("H1").Value = "CURRENT ADDRESS-1";
                    worksheet.Cell("I1").Value = "CURRENT ADDRESS-2";
                    worksheet.Cell("J1").Value = "CURRENT CITY";
                    worksheet.Cell("K1").Value = "CURRENT STATE";
                    worksheet.Cell("L1").Value = "CURRENT ZIP CODE";
                    worksheet.Cell("M1").Value = "PRIMARY E-MAIL";
                    worksheet.Cell("N1").Value = "HEAD OF HOUSEHOLD-STATUS";
                    worksheet.Cell("O1").Value = "HEAD OF HOUSEHOLD";


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