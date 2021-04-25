using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CaresTracker.DataAccess.DataAccessors.ServiceAccessors
{
    public class GetAllServices : DataSupport, IData
    {
        /// <summary>
        /// Retrieve a DataTable containing all services.
        /// </summary>
        public GetAllServices()
        {
            CommandText = "GetAllServices";
            CommandType = CommandType.StoredProcedure;
        }

        public DataTable ExecuteCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }
    }
}