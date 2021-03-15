using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CapstoneUI.DataModels;
using System.Text;
using MySql.Data.MySqlClient;

namespace CapstoneUI.DataAccess.DataAccessors
{
    public class AddEvent : DataSupport, IData
    {
        DataModels.Event theEvent;

        public AddEvent(DataModels.Event newEvent)
        {
            CommandText = "AddEvent";
            CommandType = CommandType.StoredProcedure;
            Parameters = new EventParameters().Fill(newEvent);
            theEvent = newEvent;
        }

        public int ExecuteCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();
            object ret = eq.ExecuteScalar(this);
            theEvent.EventID = int.Parse(ret.ToString());
            AddHosts(theEvent.Hosts);
            return 1;
        }

        private void AddHosts(List<CARESUser> hosts)
        {
            StringBuilder sCommand = new StringBuilder("INSERT INTO EventHost (EventID, HealthWorkerID) VALUES ");
            List<string> rows = new List<string>(); //The rows to insert
            foreach(CARESUser user in hosts)
            {
                rows.Add(string.Format($"('{MySqlHelper.EscapeString(theEvent.EventID.ToString())}'," +
                    $"'{MySqlHelper.EscapeString(user.UserID.ToString())}')"));
            }
            sCommand.Append(string.Join(",", rows));
            sCommand.Append(";");
            try
            {
                new HostWriter(sCommand.ToString()).ExecuteCommand();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}