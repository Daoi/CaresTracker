﻿using CapstoneUI.DataModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneUI.DataAccess.DataAccessors.ResidentAccessors
{
    public class UpdateResidentParameters
    {
        private MySqlParameter[] Parameters;


        public UpdateResidentParameters()
        {
            Parameters = new MySqlParameter[]
            {
                new MySqlParameter("@ResidentID", MySqlDbType.Int32),
                new MySqlParameter("@HouseID", MySqlDbType.Int32),
                new MySqlParameter("@ResidentFirstName", MySqlDbType.VarChar, 50),
                new MySqlParameter("@ResidentLastName", MySqlDbType.VarChar, 50),
                new MySqlParameter("@DateOfBirth", MySqlDbType.VarChar, 50),
                new MySqlParameter("@ResidentEmail", MySqlDbType.VarChar, 50),
                new MySqlParameter("@ResidentPhoneNumber", MySqlDbType.VarChar, 50),
                new MySqlParameter("@RelationshipToHoH", MySqlDbType.VarChar, 50),
                new MySqlParameter("@Gender", MySqlDbType.VarChar, 50),
                new MySqlParameter("@Race", MySqlDbType.VarChar, 50),
                new MySqlParameter("@PreferredLanguage", MySqlDbType.VarChar, 50),
                new MySqlParameter("@VaccineInterest", MySqlDbType.Bit),
                new MySqlParameter("@VaccineEligibility", MySqlDbType.Int32),
                new MySqlParameter("@VaccineAppointmentDate", MySqlDbType.VarChar, 50)
            };
        }

        public MySqlParameter[] Fill(Resident resident)
        {
            Parameters[0].Value = resident.ResidentID;
            Parameters[1].Value = resident.Home.HouseID;
            Parameters[2].Value = resident.ResidentFirstName;
            Parameters[3].Value = resident.ResidentLastName;
            Parameters[4].Value = resident.DateOfBirth;
            Parameters[5].Value = resident.ResidentEmail;
            Parameters[6].Value = resident.ResidentPhoneNumber;
            Parameters[7].Value = resident.RelationshipToHoH;
            Parameters[8].Value = resident.Gender;
            Parameters[9].Value = resident.Race;
            Parameters[10].Value = resident.PreferredLanguage;
            Parameters[11].Value = resident.VaccineInterest;
            Parameters[12].Value = resident.VaccineEligibility;
            Parameters[13].Value = resident.VaccineAppointmentDate;
            return Parameters;

        }
    }
}