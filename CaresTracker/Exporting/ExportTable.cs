using CaresTracker.DataAccess.DataAccessors;
using CaresTracker.DataAccess.DataAccessors.TableAccessors;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace CaresTracker.Exporting
{
    public class ExportTable
    {
       
        public static bool Export(string table)
        {
            DataTable dt;

            try
            {
                if (table.Equals("InteractionF"))
                {
                    dt = new InteractionView().RunCommand();
                }
                else if (table.Equals("ResidentF"))
                {
                    dt = new ResidentView().RunCommand();
                }
                else if (table.Equals("EventF"))
                {
                    dt = new EventView().RunCommand();
                }
                else
                {
                    dt = new CTextReader($"SELECT * From {table}").ExecuteCommand();
                }

                XLWorkbook wb = new XLWorkbook();

                wb.Worksheets.Add(dt, table);

                HttpResponse httpResponse = HttpContext.Current.Response;
                httpResponse.Clear();
                httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                httpResponse.AddHeader("content-disposition", $"attachment;filename=\"{table}.xlsx\"");

                // Flush the workbook to the Response.OutputStream
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    wb.SaveAs(memoryStream);
                    memoryStream.WriteTo(httpResponse.OutputStream);
                    memoryStream.Close();
                }
                httpResponse.End();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }


    }
}