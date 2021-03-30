using CaresTracker.DataModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CaresTracker.DataAccess.DataAccessors.InteractionAccessors
{
    public class SymptomsInsertSQLWriter
    {
        /// <summary>
        /// Write a SQL command for inserting symptoms
        /// </summary>
        /// <param name="symptoms"></param>
        /// <param name="InteractionID"></param>
        /// <returns>SQL command text to insert symptoms</returns>
        public static string WriteSQL(List<Symptom> symptoms, int InteractionID)
        {
            StringBuilder sCommand = new StringBuilder("INSERT INTO InteractionSymptom (InteractionID, SymptomID) VALUES ");
            List<string> rows = new List<string>(); //The rows to insert
            foreach (Symptom s in symptoms)
            {
                rows.Add(string.Format($"('{MySqlHelper.EscapeString(InteractionID.ToString())}'," +
                    $"'{MySqlHelper.EscapeString(s.SymptomID.ToString())}')"));
            }
            sCommand.Append(string.Join(",", rows));
            sCommand.Append(";");
            return sCommand.ToString();
        }
    }
}