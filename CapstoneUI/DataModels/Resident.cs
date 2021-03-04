using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CapstoneUI.DataAccess.DataAccessors;

namespace CapstoneUI.DataModels
{
    public class Resident
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string ResidentEmail { get; set; }
        public string ResidentPhoneNumber { get; set; }
        public string RelationshipToHoH { get; set; }
        public string Gender { get; set; } //?
        public string Race { get; set; } //?
        public bool VaccineInterest { get; set; }
        public int VaccinePhaseEligibility { get; set; }
        public List<Vaccination> VaccineInfo { get; set; }
        public HousingDevelopment HousingDevelopment { get; set; }
        public int HouseID { get; set; }
        public House Home { get; set; }

        public Resident() { }

        public Resident(DataRow dataRow)
        {
            FirstName = dataRow["FirstName"].ToString();
            LastName = dataRow["LastName"].ToString();
            DateOfBirth = dataRow["DateOfBirth"].ToString();
            ResidentEmail = dataRow["ResidentEmail"].ToString();
            ResidentPhoneNumber = dataRow["ResidentPhoneNumber"].ToString();
            RelationshipToHoH = dataRow["RelationshipToHOH"].ToString();
            Gender = dataRow["Gender"].ToString();
            Race = dataRow["Race"].ToString();

            GetHouseByID gh = new GetHouseByID();
            Home = new House(gh.RunCommand(int.Parse(dataRow["HouseID"].ToString())).Rows[0]); //Look up House by ID, create house obj, add to resident

            if (Home.DevelopmentID != -1) //-1 = HCV/Non development housing
            {
                GetDevelopmentByID gd = new GetDevelopmentByID();
                HousingDevelopment = new HousingDevelopment(gd.RunCommand(Home.DevelopmentID).Rows[0]);
            }
        }



    }
}