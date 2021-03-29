using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

namespace CapstoneUI.DataAccess.DataAccessors.InteractionAccessors
{
    public class GetAllInteractionsByResidentAttributes : DataSupport, IData
    {
        public GetAllInteractionsByResidentAttributes()
        {
            //ConnectionString = GetConnectionString("dbConnectionString");
            CommandText = "GetAllInteractionsByResidentAttributes ";
            CommandType = CommandType.StoredProcedure;
        }
        /// <summary>
        /// Get a resident's interaction forms using their First Name, Last Name, and Date of Birth.
        /// </summary>
        /// <param name="fName"></param>
        /// <param name="lName">Last name</param>
        /// <param name="date">mm/dd/yyyy</param>
        /// <returns></returns>
        public DataTable RunCommand(string fName, string lName, string date)
        {
            Parameters = new MySqlParameter[3] { new MySqlParameter("FirstName", fName), new MySqlParameter("LastName", lName), new MySqlParameter("DoB", date) };
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this); 
        }
    }
}