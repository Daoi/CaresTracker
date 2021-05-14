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
                string[] names = record.ResidentName.Split(' ');
                try
                {
                    res.ResidentFirstName = Utilities.NameCapitalization.FormatName(names[0]);
                    res.ResidentLastName = Utilities.NameCapitalization.FormatName(names[1]);
                }
                catch (Exception e)
                {
                    errors.Add(new ImportError(currentRow, $"The Resident Name column contained invalid values or was blank for this record", ""));
                    currentRow++;
                    continue;

                }

                res.ResidentPhoneNumber = record.Phone.Replace('/', '-').Trim();
                if (!Utilities.Validation.IsPhoneNumber(res.ResidentPhoneNumber).isValid)
                {
                    if (string.IsNullOrWhiteSpace(res.ResidentPhoneNumber))
                        res.ResidentPhoneNumber = "000-000-0000";
                    if (res.ResidentPhoneNumber.Length == 8)
                        res.ResidentPhoneNumber = "000-" + res.ResidentPhoneNumber;
                }

                res.RelationshipToHoH = record.HoHRelation;
                try
                {
                    res.DateOfBirth = DateTime.Today.AddYears(-int.Parse(record.Age)).ToString("yyyy-MM-dd"); 
                }
                catch (Exception e)
                {
                    res.DateOfBirth = DateTime.Today.ToString("yyyy-MM-dd");
                }
                res.Gender = record.Gender;
                res.Race = string.IsNullOrWhiteSpace(record.Race) ? "Unknown" : record.Race;
                //House info
                House home = new House();
                home.Address = record.UnitAddress;
                home.ZipCode = record.Zipcode;
                home.DevelopmentID = devID;
                home.RegionID = regionID;
                home.UnitNumber = string.IsNullOrWhiteSpace(record.AptNum) ? "N/A" : record.AptNum;
                res.Home = home;

                string fullname = $"{res.ResidentFirstName} {res.ResidentLastName}";

                List<string> missingValues = ValidateValues(res);

                if (missingValues.Count > 0)
                {
                    string s = string.Join("<br />", missingValues);
                    errors.Add(new ImportError(currentRow, $"The following columns contained invalid values: <br /> {s}", fullname));
                }
                else
                {
                    int writeResult = WriteResident(res);

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
                //Mark the resident as imported
                new SetImported().ExecuteCommand(Convert.ToInt32(AddResidentResult), true, true);

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
                invalidColumns.Add("Resident First Name(Resident Name)");
            }

            if (string.IsNullOrWhiteSpace(res.ResidentLastName))
            {
                invalidColumns.Add("Resident Last Name(Resident Name)");
            }

            if (string.IsNullOrWhiteSpace(res.DateOfBirth))
            {
                invalidColumns.Add("Age/DoB(Age in Yrs)");
            }

            if (string.IsNullOrWhiteSpace(res.Home.Address))
            {
                invalidColumns.Add("Unit Address(Unit Address)");
            }

            if (string.IsNullOrWhiteSpace(res.Home.ZipCode))
            {
                invalidColumns.Add("Zipcode(Postal)");
            }



            return invalidColumns;
        }

    }
}