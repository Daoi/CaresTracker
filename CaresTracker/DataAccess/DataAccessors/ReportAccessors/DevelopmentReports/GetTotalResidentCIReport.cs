using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CaresTracker.DataAccess.DataAccessors.ReportAccessors.DevelopmentReports
{
    public class GetTotalResidentCIReport : DataSupport, IData
    {
        public GetTotalResidentCIReport()
        {
            CommandText = "R_GetTotalResidentCIReport";
            CommandType = CommandType.StoredProcedure;
        }

        /// <summary>
        /// Gets the number of residents per Chronic Illness at the housing development
        /// </summary>
        /// <param name="developmentID"></param>
        /// <returns></returns>
        public DataTable ExecuteCommand(int developmentID)
        {
            Parameters = new MySqlParameter[]
            {
                new MySqlParameter("devID", developmentID)
            };

            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }
    }
}