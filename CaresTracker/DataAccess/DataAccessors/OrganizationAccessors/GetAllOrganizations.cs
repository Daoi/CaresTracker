using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CaresTracker.DataAccess.DataAccessors.OrganizationAccessors
{
    public class GetAllOrganizations : DataSupport, IData
    {
        public GetAllOrganizations()
        {
            CommandText = "GetAllOrganizations";
            CommandType = CommandType.StoredProcedure;
        }

        public DataTable ExecuteCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }
    }
}