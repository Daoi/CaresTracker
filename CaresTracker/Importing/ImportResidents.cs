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
        public static (int inserted, List<ImportError> errors) Import(List<ImportFile> residents, int devID, int regionID)
        {
            int successful = 0;
            int currentRow = 1;
            List<ImportError> errors = new List<ImportError>();


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
                res.ResidentPhoneNumber = "000-000-0000"; //No phone number is provided in the PHA spreadsheet
                //House info
                House home = new House();
                home.Address = record.Address;
                home.ZipCode = record.Zipcode;
                home.DevelopmentID = devID;
                home.RegionID = regionID;
                home.UnitNumber = string.IsNullOrWhiteSpace(record.SecondaryAddress) ? "N/A" : record.SecondaryAddress;
                res.Home = home;

                string fullname = $"{res.ResidentFirstName} {res.ResidentLastName}";

                int writeResult = WriteResident(res);
                List<string> missingValues = ValidateValues(res);

                if (missingValues.Count > 0)
                {
                    string s = string.Join("<br />", missingValues);
                    errors.Add(new ImportError(currentRow, $"The following columns contained invalid values: <br /> {s}", fullname));
                }
                else {
                
                    if (writeResult == 1)
                        successful++;
                    else if (writeResult == 0)
                    {
                        errors.Add(new ImportError(currentRow, "Duplicate Resident", fullname));
                    }
                    else if (writeResult == -1)
                    {
                        errors.Add(new ImportError(currentRow, "SQL Error", fullname));
                    }
                }

                currentRow++;
            }

            return (successful, errors);
        }

        private static int WriteResident(Resident newResident)
        {
            try
            {
                ResidentWriter RW = new ResidentWriter(newResident);
                object AddResidentResult = RW.ExecuteCommand();

                if (AddResidentResult == DBNull.Value) //If null Resident is NOT unique
                {
                    return 0; //Uniqueness error
                }

                House residentHouse = newResident.Home;
                // Write new House to the database
                AddHouse AH = new AddHouse(residentHouse);
                object AddHouseResult = AH.ExecuteCommand();

                // Update new Resident's HouseID to match new House
                UpdateHouseID UHI = new UpdateHouseID();
                UHI.ExecuteCommand(Convert.ToInt32(AddResidentResult), Convert.ToInt32(AddHouseResult));

                return 1; //Succuess
            }
            catch (Exception e)
            {
                return -1; //SQL Error?
            }
        }

        private static List<string> ValidateValues(Resident res)
        {
            List<string> invalidColumns = new List<string>();

            if (string.IsNullOrWhiteSpace(res.ResidentFirstName))
            {
                invalidColumns.Add("Resident First Name(FIRST NAME)");
            }

            if (string.IsNullOrWhiteSpace(res.ResidentLastName))
            {
                invalidColumns.Add("Resident Last Name(LAST NAME)");
            }

            if (string.IsNullOrWhiteSpace(res.DateOfBirth))
            {
                invalidColumns.Add("Date of Birth(DATE OF BIRTH)");
            }

            if (string.IsNullOrWhiteSpace(res.Home.Address))
            {
                invalidColumns.Add("Resident Address(CURRENT ADDRESS-1)");
            }

            if (string.IsNullOrWhiteSpace(res.Home.ZipCode))
            {
                invalidColumns.Add("Zipcode(CURRENT ZIP CODE)");
            }

            return invalidColumns;
        }

    }
}