using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

namespace CapstoneUI.DataAccess.DataAccessors
{
    public class GetCHWByID : DataSupport, IData
    {
        public GetCHWByID()
        {
            CommandText = "GetCHWByID";
            CommandType = CommandType.StoredProcedure;
        }
        /// <summary>
        /// Get the user with the provided ID
        /// </summary>
        /// <param name="userid">ID(pk) of the user</param>
        /// <returns></returns>
        public DataTable RunCommand(int userid)
        {
            Parameters = new MySqlParameter[1] { new MySqlParameter("UserID", userid) };
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }
    }
}