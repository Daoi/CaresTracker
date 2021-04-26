using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CaresTracker.DataAccess.DataAccessors.ReportAccessors
{
    public class GetInteractionLanguageReport : DataSupport, IData
    {
        public GetInteractionLanguageReport()
        {
            CommandText = "R_GetInteractionLanguageReport";
            CommandType = CommandType.StoredProcedure;
        }

        /// <summary>
        /// Finds the number of interactions had per resident primary language
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