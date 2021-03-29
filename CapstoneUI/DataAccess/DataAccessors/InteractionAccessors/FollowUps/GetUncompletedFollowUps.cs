using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

namespace CapstoneUI.DataAccess.DataAccessors.InteractionAccessors.FollowUps
{
    /// <summary>
    /// Get the follow ups marked as uncompleted for the current CARESUser
    /// Defaults to getting uncompletetd
    /// </summary>
    public class GetUncompletedFollowUps : DataSupport, IData
    {

        public GetUncompletedFollowUps()
        {
            //ConnectionString = GetConnectionString("dbConnectionString");
            CommandText = "UncompletedFollowUps";
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