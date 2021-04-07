using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CaresTracker.DataAccess.DataAccessors.ReportAccessors
{
    public class GetTotalLanguageReport : DataSupport, IData
    {
        public GetTotalLanguageReport()
        {
            CommandText = "R_GetTotalLanguageReport";
            CommandType = CommandType.StoredProcedure;
        }

        /// <summary>
        /// Gets the number of residents per primary language at this development
        /// </summary>
        /// <param name="developmentID"></param>
        /// <returns></returns>
        public DataTable ExecuteCommand(int developmentID)
        {
            Parameters = new MySqlParameter[] { new MySqlParameter("devID", developmentID) };
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }
    }
}