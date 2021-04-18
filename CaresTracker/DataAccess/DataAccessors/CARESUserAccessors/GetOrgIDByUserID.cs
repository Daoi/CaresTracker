using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CaresTracker.DataAccess.DataAccessors.CARESUserAccessors
{
    public class GetOrgIDByUserID : DataSupport, IData
    {
        public GetOrgIDByUserID()
        {
            CommandText = "GetOrganizationByWorkerID";
            CommandType = CommandType.StoredProcedure;
        }

        /// <summary>
        /// Returns the organization id of the provided worker ID
        /// </summary>
        /// <param name="WorkerID"></param>
        /// <returns></returns>
        public int RunCommand(int WorkerID)
        {
            Parameters = new MySqlParameter[1] { new MySqlParameter("WorkerID", WorkerID) };
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this).Rows[0].Field<int>("OrganizationID");
        }
    }
}