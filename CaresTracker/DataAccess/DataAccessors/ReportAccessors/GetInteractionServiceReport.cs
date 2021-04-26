using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

namespace CaresTracker.DataAccess.DataAccessors.ReportAccessors
{
    public class GetInteractionServiceReport : DataSupport, IData
    {
        public GetInteractionServiceReport()
        {
            CommandText = "R_GetInteractionServiceReport";
            CommandType = CommandType.StoredProcedure;
        }

        /// <summary>
        /// Gets the number of times each service has been requested in the given timeframe
        /// </summary>
        /// <param name="domainID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public DataTable ExecuteCommand(int domainID, string startDate, string endDate, char reportType)
        {
            Parameters = new MySqlParameter[]
            {
                new MySqlParameter("domainID", domainID),
                new MySqlParameter("startDate", startDate),
                new MySqlParameter("endDate", endDate),
                new MySqlParameter("reportType", reportType),
            };

            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }
    }
}