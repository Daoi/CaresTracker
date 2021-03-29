using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CapstoneUI.DataAccess.DataAccessors.CARESUserAccessors
{
    public class GetUser: DataSupport, IData
    {
        public GetUser()
        {
            CommandText = "GetUser";
            CommandType = CommandType.StoredProcedure;
        }

        /// <summary>
        /// Searches for a CARESUser by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public DataTable RunCommand(string username)
        {
            Parameters = new MySqlParameter[1] { new MySqlParameter("username", username) };
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }
    }
}