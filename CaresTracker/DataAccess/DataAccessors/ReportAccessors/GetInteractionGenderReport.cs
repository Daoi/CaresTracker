using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

namespace CaresTracker.DataAccess.DataAccessors.ReportAccessors
{
    public class GetInteractionGenderReport : DataSupport, IData
    {
        public GetInteractionGenderReport()
        {
            CommandText = "R_GetInteractionGenderReport";
            CommandType = CommandType.StoredProcedure;
        }

        /// <summary>
        /// Finds the number of interactions had per resident gender
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