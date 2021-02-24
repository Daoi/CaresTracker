using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneUI.DataModels
{
    public class House
    {
        public int HouseID { get; set; }
        public int RegionID { get; set; }
        public string Address { get; set; }
        public string HouseType { get; set; }
        public int DevelopmentID { get; set; }
        public string ZipCode { get; set; }
    }
}