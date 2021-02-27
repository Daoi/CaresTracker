using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneUI.DataModels
{
    public class HousingDevelopment
    {
        public int DevelopmentID { get; set; }
        public string DevelopmentName { get; set; }
        public int NumUnits { get; set; }
        public string SiteType { get; set; }
        public string OfficeAddress { get; set; }
    }
}