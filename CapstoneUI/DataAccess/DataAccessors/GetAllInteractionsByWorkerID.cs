using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
namespace CapstoneUI.DataAccess.DataAccessors
{
    public class GetAllInteractionsByWorkerID : DataSupport, IData
    {
        public GetAllInteractionsByWorkerID()
        {
            //ConnectionString = GetConnectionString("dbConnectionString");
            CommandText = "GetAllInteractionsByResidentAttributes ";
            CommandType = CommandType.StoredProcedure;
        }
        /// <summary>
        /// Return all of the interactions for a specific worker
        /// </summary>
        /// <param name="workerID">CARESUser ID</param>
        /// <returns></returns>
        public DataTable RunCommand(int workerID)
        {
            Parameters = new MySqlParameter[1] { new MySqlParameter("WorkerID", workerID) };
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }
    }
}