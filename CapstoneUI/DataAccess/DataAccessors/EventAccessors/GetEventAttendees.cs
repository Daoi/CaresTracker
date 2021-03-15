using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace CapstoneUI.DataAccess.DataAccessors.EventAccessors
{
    public class GetEventAttendees : DataSupport, IData
    {
        public GetEventAttendees()
        {
            CommandText = "GetEventAttendees";
            CommandType = CommandType.StoredProcedure;
        }

        /// <summary>
        /// Returns all the residents tied to an event
        /// </summary>
        /// <param name="eventid"></param>
        /// <returns></returns>
        public DataTable ExecuteCommand(int eventid)
        {
            Parameters = new MySqlParameter[1] { new MySqlParameter("eventid", eventid) };
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }
    }
}