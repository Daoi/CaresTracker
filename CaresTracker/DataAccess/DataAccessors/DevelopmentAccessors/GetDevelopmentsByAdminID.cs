using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

namespace CaresTracker.DataAccess.DataAccessors.DevelopmentAccessors
{
    public class GetDevelopmentsByAdminID : DataSupport, IData
    {
        public GetDevelopmentsByAdminID()
        {
            CommandText = "GetDevelopmentsByAdminID";
            CommandType = CommandType.StoredProcedure;
        }

        /// <summary>
        /// Gets the developments based on the admin's type/organization
        /// </summary>
        /// <param name="adminid">Current admin's id.</param>
        /// <returns></returns>
        public DataTable ExecuteCommand(int adminid)
        {
            Parameters = new MySqlParameter[1] { new MySqlParameter("adminid", adminid) };
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }
    }
}