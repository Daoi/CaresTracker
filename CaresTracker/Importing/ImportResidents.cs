using CaresTracker.DataAccess.DataAccessors.HouseAccessors;
using CaresTracker.DataAccess.DataAccessors.ResidentAccessors;
using CaresTracker.DataModels;
using CaresTracker.Importing.FileModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaresTracker.Importing
{
    public class ImportResidents
    {
        public static (int inserted, List<int> errors) Import(List<ImportFile> residents, int devID, int regionID)
        {
            int successful = 0;
            int row = 1;
            List<int> rowsWithErrors = new List<int>();

            foreach (ImportFile record in residents)
            {
                //Resident Info
                Resident res = new Resident();
                res.ResidentFirstName = record.ResidentFirstName;
                res.ResidentLastName = record.ResidentLastName;
                res.ResidentEmail = record.Email;
                res.RelationshipToHoH = record.HoHRelation;
                try
                {
                    res.DateOfBirth = DateTime.Parse(record.DoB).ToString("yyyy-MM-dd");
                }
                catch (Exception e)
                {
                    res.DateOfBirth = record.DoB;
                }
                res.Gender = record.Gender;
                res.Race = record.Race;
                res.ResidentPhoneNumber = "000-000-0000"; //No address is provided in the PHA spreadsheet
                //House info
                House home = new House();
                home.Address = record.Address;
                home.ZipCode = record.Zipcode;
                home.DevelopmentID = devID;
                home.RegionID = regionID;
                home.UnitNumber = record.SecondaryAddress;
                res.Home = home;
                if (WriteResident(res))
                    successful++;
                else
                    rowsWithErrors.Add(row);
                row++;
            }

            return (successful, rowsWithErrors);
        }

        private static bool WriteResident(Resident newResident)
        {
            try
            {
                ResidentWriter RW = new ResidentWriter(newResident);
                object AddResidentResult = RW.ExecuteCommand();

                if (AddResidentResult == null) //If null Resident is NOT unique
                {
                    return false;
                }

                House residentHouse = newResident.Home;
                // Write new House to the database
                AddHouse AH = new AddHouse(residentHouse);
                object AddHouseResult = AH.ExecuteCommand();

                // Update new Resident's HouseID to match new House
                UpdateHouseID UHI = new UpdateHouseID();
                UHI.ExecuteCommand(Convert.ToInt32(AddResidentResult), Convert.ToInt32(AddHouseResult));

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}