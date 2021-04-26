using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CaresTracker.DataAccess.DataAccessors.ServiceAccessors
{
    public class GetEnabledServices : DataSupport, IData
    {
        /// <summary>
        /// Retrieve a DataTable containing all enabled services.
        /// </summary>
        public GetEnabledServices()
        {
            CommandText = "GetEnabledServices";
            CommandType = CommandType.StoredProcedure;
        }

        public DataTable ExecuteCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }
    }
}