using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace CapstoneUI.DataAccess.DataAccessors
{
    public class UpdateUserStatus : DataSupport, IData
    {
        public UpdateUserStatus()
        {
            CommandText = "UpdateUserStatus";
            CommandType = CommandType.StoredProcedure;
        }

        /// <summary>
        /// Toggles user account activation status (UserStatus).
        /// </summary>
        /// <param name="username"></param>
        /// <param name="newStatus">Must be either: "Active" or "Inactive"</param>
        /// <returns></returns>
        public int RunCommand(string username, string newStatus)
        {
            if (username == null)
            {
                throw new ArgumentException("username must not be null");
            }

            // enforce inputs to keep DB data consistent
            if (newStatus != "Active" && newStatus != "Inactive")
            {
                throw new ArgumentException("newStatus must be either: \"Active\" or \"Inactive\"");
            }

            Parameters = new MySqlParameter[2] 
            {
                new MySqlParameter("username", username),
                new MySqlParameter("newStatus", newStatus)
            };

            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteNonQuery(this);
        }
    }
}