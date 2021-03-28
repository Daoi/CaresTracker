using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

namespace CapstoneUI.DataAccess.DataAccessors.DevelopmentAccessors
{
    public class GetDevelopmentsByUserID : DataSupport, IData
    {
        public GetDevelopmentsByUserID()
        {
            CommandText = "GetDevelopmentsByUserID";
            CommandType = CommandType.StoredProcedure;
        }

        /// <summary>
        /// Gets the developments based on the user's organization,
        /// and sorts them by the times selected by this user.
        /// </summary>
        /// <param name="userid">Current user's id.</param>
        /// <returns></returns>
        public DataTable ExecuteCommand(int userid)
        {
            Parameters = new MySqlParameter[1] { new MySqlParameter("userid", userid) };
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }
    }
}