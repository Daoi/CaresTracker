using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CaresTracker.DataAccess.DataAccessors.TableAccessors
{
    public class ResidentView : DataSupport, IData
    {
        public ResidentView()
        {
            CommandText = "ResidentExport";
            CommandType = CommandType.StoredProcedure;
        }

        public DataTable RunCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }
    }
}