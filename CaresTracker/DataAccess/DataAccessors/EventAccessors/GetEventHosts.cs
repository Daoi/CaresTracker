using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace CaresTracker.DataAccess.DataAccessors.EventAccessors
{
    public class GetEventHosts : DataSupport, IData
    {
        public GetEventHosts()
        {
            CommandText = "GetEventHosts";
            CommandType = CommandType.StoredProcedure;
        }

        /// <summary>
        /// Returns all CARESUsers tied to an event
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