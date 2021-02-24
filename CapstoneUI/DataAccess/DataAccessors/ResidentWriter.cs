using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CapstoneUI.DataAccess.DataAccessors
{
    public class ResidentWriter : DataSupport, IData
    {

        public ResidentWriter(List<string> values)
        {
            CommandText = "AddResident";
            CommandType = CommandType.StoredProcedure;
        }

        public int ExecuteCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteNonQuery(this);
        }

    }
}