using CaresTracker.DataModels;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Text;

namespace CaresTracker.DataAccess.DataAccessors.EventAccessors
{
    public class AddAttendees
    {
        public static string WriteSQL(DataModels.Event theEvent, List<Resident> attendees)
        {
            StringBuilder sCommand = new StringBuilder("INSERT INTO EventAttendee (EventID, ResidentID) VALUES ");
            List<string> rows = new List<string>(); //The rows to insert
            foreach (Resident res in attendees)
            {
                rows.Add(string.Format($"('{MySqlHelper.EscapeString(theEvent.EventID.ToString())}'," +
                    $"'{MySqlHelper.EscapeString(res.ResidentID.ToString())}')"));
            }
            sCommand.Append(string.Join(",", rows));
            sCommand.Append(";");

            return sCommand.ToString();
        }
    }
}