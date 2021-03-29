using CaresTracker.DataModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CaresTracker.DataAccess.DataAccessors.InteractionAccessors
{
    public class ServicesInsertSQLWriter
    {
        /// <summary>
        /// Insert a list of services
        /// </summary>
        /// <param name="services"></param>
        /// <returns>SQL Command for inserting services</returns>
        public static string WriteSQL(List<Service> services, int InteractionID)
        {

            StringBuilder sCommand = new StringBuilder("INSERT INTO InteractionService (InteractionID, ServiceID, IsCompleted) VALUES ");
            List<string> rows = new List<string>(); //The rows to insert
            foreach (Service s in services)
            {
                rows.Add(string.Format($"('{MySqlHelper.EscapeString(InteractionID.ToString())}'," +
                    $"'{MySqlHelper.EscapeString(s.ServiceID.ToString())}'," +
                    $"'{0}')"));
            }
            sCommand.Append(string.Join(",", rows));
            sCommand.Append(";");

            return sCommand.ToString();
        }
    }
}