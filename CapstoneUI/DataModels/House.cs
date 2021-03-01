using System;
using System.Collections.Generic;
using System.Data;
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
        const int HCV = -1;
        public House()
        {

        }

        public House(DataRow dataRow)
        {
            HouseID = int.Parse(dataRow["HouseID"].ToString());
            RegionID = int.Parse(dataRow["RegionID"].ToString());
            Address = dataRow["Address"].ToString();
            HouseType = dataRow["HouseType"].ToString();
            ZipCode = dataRow["ZipCode"].ToString();

            if (dataRow["DevelopmentID"] == null || dataRow["DevelopmentID"] == DBNull.Value) //Not part of a development
            {
                DevelopmentID = HCV;
            }
            else
            {
                DevelopmentID = int.Parse(dataRow["DevelopmentID"].ToString());
            }
        }
    }
}