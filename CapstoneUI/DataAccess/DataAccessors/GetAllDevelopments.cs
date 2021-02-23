using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CapstoneUI.DataAccess.DataAccessors
{
    public class GetAllDevelopments : DataSupport, IData
    {
        public GetAllDevelopments()
        {
            CommandText = "GetAllDevelopments";
            CommandType = CommandType.StoredProcedure;
        }

        public DataTable ExecuteCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }
    }
}