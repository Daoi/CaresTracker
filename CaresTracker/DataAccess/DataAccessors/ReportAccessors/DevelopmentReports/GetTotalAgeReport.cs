using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CaresTracker.DataAccess.DataAccessors.ReportAccessors
{
    public class GetTotalAgeReport : DataSupport, IData
    {
        public GetTotalAgeReport()
        {
            CommandText = "R_GetTotalAgeReport";
            CommandType = CommandType.StoredProcedure;
        }

        /// <summary>
        /// Finds the number of residents per age at this development
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