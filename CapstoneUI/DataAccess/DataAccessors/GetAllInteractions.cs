using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CapstoneUI.DataAccess.DataAccessors
{
    public class GetAllInteractions : DataSupport, IData
    {
        /// <summary>
        /// Retrieve a DataTable containing all interactions.
        /// </summary>
        public GetAllInteractions()
        {
            CommandText = "GetAllInteractions";
            CommandType = CommandType.StoredProcedure;
        }

        public DataTable ExecuteCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }
    }
}