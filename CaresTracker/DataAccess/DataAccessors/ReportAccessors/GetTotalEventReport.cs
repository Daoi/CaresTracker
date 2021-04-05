using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CaresTracker.DataAccess.DataAccessors.ReportAccessors
{
    public class GetTotalEventReport : DataSupport, IData
    {
        public GetTotalEventReport()
        {
            CommandText = "R_GetTotalEventReport";
            CommandType = CommandType.StoredProcedure;
        }

        /// <summary>
        /// Finds the number of residents from this development that attended events
        /// </summary>
        /// <param name="developmentID"></param>
        /// <returns></returns>
        public int ExecuteCommand(int developmentID)
        {
            Parameters = new MySqlParameter[] { new MySqlParameter("devID", developmentID) };
            ExecuteQuery eq = new ExecuteQuery();
            return int.Parse(eq.ExecuteScalar(this).ToString());
        }
    }
}