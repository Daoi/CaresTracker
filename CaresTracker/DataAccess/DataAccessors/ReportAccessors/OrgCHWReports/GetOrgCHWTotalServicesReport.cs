using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CaresTracker.DataAccess.DataAccessors.ReportAccessors.OrgCHWReports
{
    public class GetOrgCHWTotalServicesReport : DataSupport, IData
    {
        public GetOrgCHWTotalServicesReport()
        {
            CommandText = "R_GetOrgCHWTotalServicesReport";
            CommandType = CommandType.StoredProcedure;
        }

        /// <summary>
        /// Gets the number of times each service has been requested in the given timeframe
        /// </summary>
        /// <param name="developmentID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataTable ExecuteCommand(int developmentID, string startDate, string endDate)
        {
            Parameters = new MySqlParameter[]
            {
                new MySqlParameter("devID", developmentID),
                new MySqlParameter("startDate", startDate),
                new MySqlParameter("endDate", endDate),
            };

            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }
    }
}