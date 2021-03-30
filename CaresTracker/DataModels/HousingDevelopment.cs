using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CaresTracker.DataModels
{
    public class HousingDevelopment
    {
        public int DevelopmentID { get; set; }
        public string DevelopmentName { get; set; }
        public int NumUnits { get; set; }
        public string SiteType { get; set; }
        public string OfficeAddress { get; set; }

        public HousingDevelopment()
        {

        }


        public HousingDevelopment(DataRow dataRow)
        {
            DevelopmentID = int.Parse(dataRow["DevelopmentID"].ToString());
            DevelopmentName = dataRow["DevelopmentName"].ToString();
            OfficeAddress = dataRow["OfficeAddress"].ToString();
            SiteType = dataRow["SiteType"].ToString();
            NumUnits = int.Parse(dataRow["NumUnits"].ToString());
        }
    }
}