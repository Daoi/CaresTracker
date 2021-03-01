using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CapstoneUI.DataAccess.DataAccessors
{
    /// <summary>
    /// Retrieve a DataTable containing the all Residents fromt the Resident table.
    /// </summary>
    public class GetAllResident : DataSupport, IData
    {
        public GetAllResident()
        {
            CommandText = "GetAllResident";
            CommandType = CommandType.StoredProcedure;
        }

        public DataTable RunCommand()
        {
            ExecuteQuery eq = new ExecuteQuery(); //Create instance of class that handles command obj
            return eq.ExecuteAdapter(this); //Run the command
        }
    }
}