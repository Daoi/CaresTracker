using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CaresTracker.DataAccess.DataAccessors.CARESUserAccessors
{
    /// <summary>
    /// Retrieve a DataTable containing the UserId, Username, First name and last name of all CARESUsers.
    /// </summary>
    public class GetAllCHW : DataSupport, IData
    {
        public GetAllCHW()
        {
            CommandText = "GetAllCHW";
            CommandType = CommandType.StoredProcedure;
        }

        public DataTable RunCommand()
        {
            ExecuteQuery eq = new ExecuteQuery(); //Create instance of class that handles command obj
            return eq.ExecuteAdapter(this); //Run the command
        }
    }
}