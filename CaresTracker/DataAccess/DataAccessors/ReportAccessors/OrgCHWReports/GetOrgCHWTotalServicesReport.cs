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
        /// Gets the total number of times each service has been requested for the given Org/CHW
        /// </summary>
        /// <param name="domainID"></param>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public DataTable ExecuteCommand(int domainID, char reportType)
        {
            Parameters = new MySqlParameter[]
            {
                new MySqlParameter("domainID", domainID),
                new MySqlParameter("reportType", reportType),
            };

            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }
    }
}