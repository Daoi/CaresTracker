using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CapstoneUI.DataAccess.DataAccessors.CARESUserAccessors
{
    public class GetWorkersByUserID : DataSupport, IData
    {
        public GetWorkersByUserID()
        {
            CommandText = "GetWorkersByUserID";
            CommandType = CommandType.StoredProcedure;
        }

        /// <summary>
        /// Returns a list of CHWs filtered by the access level of the indicated user
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public DataTable RunCommand(int userid)
        {
            Parameters = new MySqlParameter[1] { new MySqlParameter("userid", userid) };
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }
    }
}