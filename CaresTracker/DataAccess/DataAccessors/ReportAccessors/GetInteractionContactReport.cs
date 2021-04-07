using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CaresTracker.DataAccess.DataAccessors.ReportAccessors
{
    public class GetInteractionContactReport : DataSupport, IData
    {
        public GetInteractionContactReport()
        {
            CommandText = "R_GetInteractionContactReport";
            CommandType = CommandType.StoredProcedure;
        }

        /// <summary>
        /// Finds the number of interactions had per contact method
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