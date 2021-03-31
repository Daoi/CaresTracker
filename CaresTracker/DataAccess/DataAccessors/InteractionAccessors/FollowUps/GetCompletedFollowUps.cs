using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace CaresTracker.DataAccess.DataAccessors.InteractionAccessors.FollowUps
{
    /// <summary>
    /// Get the follow ups marked as uncompleted for the current CARESUser
    /// Defaults to getting uncompletetd
    /// </summary>
    public class GetCompletedFollowUps : DataSupport, IData
    {

        public GetCompletedFollowUps()
        {
            //ConnectionString = GetConnectionString("dbConnectionString");
            CommandText = "CompletedFollowUps";
            CommandType = CommandType.StoredProcedure;
        }
        /// <summary>
        /// Retrieve the set of follow ups deteremined by completed
        /// </summary>
        /// <param name="name">Username of the current user</param>
        /// <param name="completed">False = uncompleted true = completed</param>
        /// <returns></returns>
        public DataTable RunCommand(string name)
        {

            Parameters = new MySqlParameter[1] { new MySqlParameter("Username", name) }; 
            ExecuteQuery eq = new ExecuteQuery(); 
            return eq.ExecuteAdapter(this); 
        }
    }
}