using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CaresTracker.DataAccess.DataAccessors.ResidentAccessors
{
    public class GetResidentsByUserID : DataSupport, IData
    {
        public GetResidentsByUserID()
        {
            CommandText = "GetResidentsByUserID";
            CommandType = CommandType.StoredProcedure;
        }

        /// <summary>
        /// Returns a list of residents filtered by the access level/organization of the indicated user
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public DataTable ExecuteCommand(int userid)
        {
            Parameters = new MySqlParameter[1] { new MySqlParameter("userid", userid) };
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }
    }
}
