using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CaresTracker.DataAccess.DataAccessors.ReportAccessors
{
    public class GetTotalVaccineReport : DataSupport, IData
    {
        public GetTotalVaccineReport()
        {
            CommandText = "R_GetTotalVaccineReport";
            CommandType = CommandType.StoredProcedure;
        }

        /// <summary>
        /// Finds the number of residents from this development in each vaccine status category
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