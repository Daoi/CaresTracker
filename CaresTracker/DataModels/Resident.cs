﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CaresTracker.DataAccess.DataAccessors;
using CaresTracker.DataAccess.DataAccessors.HouseAccessors;

namespace CaresTracker.DataModels
{
    public class Resident
    {
        public int ResidentID { get; set; }
        public string ResidentFirstName { get; set; }
        public string ResidentLastName { get; set; }
        public string DateOfBirth { get; set; }
        public string ResidentEmail { get; set; }
        public string ResidentPhoneNumber { get; set; }
        public string RelationshipToHoH { get; set; }
        public string Gender { get; set; } //?
        public string Race { get; set; } //?
        public string PreferredLanguage { get; set; }
        public bool? VaccineInterest { get; set; }
        public int? VaccineEligibility { get; set; }
        public string VaccineAppointmentDate { get; set; } 
        public HousingDevelopment HousingDevelopment { get; set; }
        public int HouseID { get; set; }
        public House Home { get; set; }
        public string FullName { get { return $"{ResidentFirstName} {ResidentLastName}"; } }

        public Resident() { }

        public Resident(DataRow dataRow)
        {
            ResidentID = int.Parse(dataRow["ResidentID"].ToString());
            ResidentFirstName = dataRow["ResidentFirstName"].ToString();
            ResidentLastName = dataRow["ResidentLastName"].ToString();
            DateOfBirth = dataRow["DateOfBirth"].ToString();
            ResidentEmail = dataRow["ResidentEmail"].ToString();
            ResidentPhoneNumber = dataRow["ResidentPhoneNumber"].ToString();
            RelationshipToHoH = dataRow["RelationshipToHOH"].ToString();
            Gender = dataRow["Gender"].ToString();
            Race = dataRow["Race"].ToString();
            PreferredLanguage = dataRow["PreferredLanguage"].ToString();

            GetHouseByID gh = new GetHouseByID();
            Home = new House(gh.RunCommand(int.Parse(dataRow["HouseID"].ToString())).Rows[0]); //Look up House by ID, create house obj, add to resident

            if (Home.DevelopmentID != -1) //-1 = HCV/Non development housing
            {
                GetDevelopmentByID gd = new GetDevelopmentByID();
                HousingDevelopment = new HousingDevelopment(gd.RunCommand(Home.DevelopmentID).Rows[0]);
            }

            VaccineInterest = dataRow["VaccineInterest"] != DBNull.Value ? (bool?)dataRow["VaccineInterest"] : null;
            VaccineEligibility = dataRow["VaccineEligibility"] != DBNull.Value ? (int?)dataRow["VaccineEligibility"] : null;
            VaccineAppointmentDate = dataRow["VaccineAppointmentDate"].ToString();
        }

        /// <summary>
        /// Create a list of Resident objects from a Resident DataTable
        /// </summary>
        /// <param name="dataTable">Contains Resident table data</param>
        /// <returns></returns>
        public static List<Resident> CreateEventAttendeeList(DataTable dataTable)
        {
            return dataTable.Rows.OfType<DataRow>()
                .Select(dr => new Resident(dr))
                .ToList();
        }
    }
}