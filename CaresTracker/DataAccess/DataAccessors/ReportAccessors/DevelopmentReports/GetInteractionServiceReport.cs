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