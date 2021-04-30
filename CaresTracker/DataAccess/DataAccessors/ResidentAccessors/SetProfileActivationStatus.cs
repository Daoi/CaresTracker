using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CaresTracker.DataAccess.DataAccessors.ResidentAccessors
{
    public class SetProfileActivationStatus : DataSupport, IData
    {
        public SetProfileActivationStatus()
        {
            CommandText = "SetProfileActivationStatus";
            CommandType = CommandType.StoredProcedure;
        }

        public int ExecuteCommand(int resID, bool status)
        {
            Parameters = new MySqlParameter[2] { new MySqlParameter("resID", resID), new MySqlParameter("newStatus", status) };
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteNonQuery(this);
        }
    }
}
