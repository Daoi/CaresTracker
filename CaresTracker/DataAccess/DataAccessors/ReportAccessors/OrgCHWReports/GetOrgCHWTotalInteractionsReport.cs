using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CaresTracker.DataAccess.DataAccessors.ReportAccessors.OrgCHWReports
{
    public class GetOrgCHWTotalInteractionsReport : DataSupport, IData
    {
        public GetOrgCHWTotalInteractionsReport()
        {
            CommandText = "R_GetOrgCHWTotalInteractionsReport";
            CommandType = CommandType.StoredProcedure;
        }

        /// <summary>
        /// Finds the number of total interactions for the given Org/CHW
        /// </summary>
        /// <param name="domainID"></param>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public int ExecuteCommand(int domainID, char reportType)
        {
            Parameters = new MySqlParameter[]
            {
                new MySqlParameter("domainID", domainID),
                new MySqlParameter("reportType", reportType),
            };
            ExecuteQuery eq = new ExecuteQuery();
            return int.Parse(eq.ExecuteScalar(this).ToString());
        }
    }
}