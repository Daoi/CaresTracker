using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CapstoneUI.DataModels;

namespace CapstoneUI.DataAccess.DataAccessors
{
    public class DeleteLastHouse : DataSupport, IData
    {
        public DeleteLastHouse()
        {
            CommandText = "DeleteLastHouse";
            CommandType = CommandType.StoredProcedure;
        }

        public int ExecuteCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteNonQuery(this);
        }
    }
}
