using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CapstoneUI.DataAccess.DataAccessors.EventAccessors
{
    public class GetUpcomingEvents : DataSupport, IData
    {
        public GetUpcomingEvents()
    {
        CommandText = "GetAllEvents";
        CommandType = CommandType.StoredProcedure;
    }

    public DataTable ExecuteCommand()
    {
        ExecuteQuery eq = new ExecuteQuery();
        return eq.ExecuteAdapter(this);
    }
}
}