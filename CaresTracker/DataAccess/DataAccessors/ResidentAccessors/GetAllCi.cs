using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CaresTracker.DataAccess.DataAccessors.ResidentAccessors
{
    public class GetAllCi : DataSupport, IData
    {
        public GetAllCi()
        {
            CommandText = "GetAllCi";
            CommandType = CommandType.StoredProcedure;
        }

        public DataTable RunCommand()
        {
            ExecuteQuery eq = new ExecuteQuery(); 
            return eq.ExecuteAdapter(this);
        }
    }
}