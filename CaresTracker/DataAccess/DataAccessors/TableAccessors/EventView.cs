using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CaresTracker.DataAccess.DataAccessors.TableAccessors
{
    public class EventView : DataSupport, IData
    {
        public EventView()
        {
            CommandText = "EventExport";
            CommandType = CommandType.StoredProcedure;
        }

        public DataTable RunCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }
    }
}