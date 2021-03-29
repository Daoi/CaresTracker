using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

namespace CapstoneUI.DataAccess.DataAccessors.CARESUserAccessors
{
    public class GetWorkerStats : DataSupport, IData
    {
        public GetWorkerStats()
        {
            CommandText = "GetWorkerStats";
            CommandType = CommandType.StoredProcedure;
        }

        /// <summary>
        /// Calculates weekly and total interactions.
        /// Columns returned: InteractionsThisWeek, TotalInteractions
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
