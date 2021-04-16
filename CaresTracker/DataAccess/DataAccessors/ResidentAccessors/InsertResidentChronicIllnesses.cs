using CaresTracker.DataModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CaresTracker.DataAccess.DataAccessors.ResidentAccessors
{
    public class InsertResidentChronicIllnesses
    {
        /// <summary>
        /// Write a SQL command for inserting chronic illnesses for a resident
        /// </summary>
        /// <param name="illnesses"></param>
        /// <param name="residentID"></param>
        /// <returns>SQL command text to insert the chronic illnesses</returns>
        public static string WriteSQL(List<ChronicIllness> illnesses, int residentID)
        {
            StringBuilder sCommand = new StringBuilder("INSERT INTO ResidentChronicIllness (ResidentID, ChronicIllnessID) VALUES ");
            List<string> rows = new List<string>(); //The rows to insert
            foreach (ChronicIllness i in illnesses)
            {
                rows.Add(string.Format($"('{MySqlHelper.EscapeString(residentID.ToString())}'," +
                    $"'{MySqlHelper.EscapeString(i.ChronicIllnessID.ToString())}')"));
            }
            sCommand.Append(string.Join(",", rows));
            sCommand.Append(";");
            return sCommand.ToString();
        }
    }
}