using CapstoneUI.DataAccess.DataAccessors.Examples;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneUI.DataAccess.DataAccessors
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