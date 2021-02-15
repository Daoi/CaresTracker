using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace CapstoneUI.DataAccess.DataAccessors
{
    public class LookUpUser : DataSupport, IData
    {
        /// <summary>
        /// Important Note:
        /// Using this method of accessing the database, it's ideal to have
        /// a class for each stored procedure.
        /// e.g. this class would be the "GetUserCommandClass" (probably with a better name)
        /// This way, whenever theres a problem we know exactly where to look and avoid issues with
        /// multiple commands originating from the same class. If a command requires something special
        /// it can be handled in its own class. e.g. if userinput is formatted differently then its stored in the db
        /// Could handle the formatting here in a method, or maybe ideally that would have its own class too
        /// </summary>

        //A constructor to set some defaults that will be the same everytime we use this class(i.e. the command name and type)
        public LookUpUser()
        {
            //ConnectionString = GetConnectionString("dbConnectionString");
            CommandText = "GetUserExample";
            CommandType = CommandType.StoredProcedure;
        }
        /// <summary>
        /// Create the parameters for the SQL Stored procedure. The procedure has two variables, @UserAge and @TestResult
        /// These variables values are passed in, but can be set in the class or where ever it makes sense to
        /// After that, we create an EQ Object, and then call the appropriate sql method. ExecuteAdapter or ExecuteScalar would
        /// work here, execute scalar is probably better for a situation where you only expect one row/item to be returned but oh well
        /// </summary>
        /// <param name="age"></param>
        /// <param name="testResult"></param>
        /// <returns></returns>
        public DataTable RunCommand(string name)
        {
            Parameters = new SqlParameter[1] { new SqlParameter("UserName", name) }; //Add parameters required for command
            ExecuteQuery eq = new ExecuteQuery(); //Create instance of class that handles command obj
            return eq.ExecuteAdapter(this); //Run the command
        }



    }
}