using CaresTracker.DataAccess.DataAccessors.CARESUserAccessors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaresTracker.DataAccess.DataAccessors.CARESUserAccessors
{
    public class CHWWriter : DataSupport, IData
    {

        public CHWWriter(List<string> values)
        {
            CommandText = "AddCHW";
            CommandType = CommandType.StoredProcedure;
            Parameters = new CHWParameters().Fill(values);
        }

        public int ExecuteCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteNonQuery(this);
        }

    }
}