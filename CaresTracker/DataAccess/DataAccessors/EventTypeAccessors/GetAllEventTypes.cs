using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CaresTracker.DataAccess.DataAccessors.EventTypeAccessors
{
    public class GetAllEventTypes : DataSupport, IData
    {
        /// <summary>
        /// Retrieve a DataTable containing all event types.
        /// </summary>
        public GetAllEventTypes()
        {
            CommandText = "GetAllEventTypes";
            CommandType = CommandType.StoredProcedure;
        }

        public DataTable ExecuteCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }
    }
}