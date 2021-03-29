using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace CapstoneUI.DataAccess.DataAccessors.CARESUserAccessors
{
    public class UpdateLastLogin : DataSupport, IData
    {
        public UpdateLastLogin()
        {
            CommandText = "UpdateLastLogin";
            CommandType = CommandType.StoredProcedure;
        }

        public int ExecuteCommand(string username, string loginTime)
        {
            Parameters = new MySqlParameter[] { new MySqlParameter("Username", username), new MySqlParameter("LoginTime", loginTime) };
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteNonQuery(this);
        }
    }
}