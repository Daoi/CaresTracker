using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CapstoneUI.DataModels;

namespace CapstoneUI.DataAccess.DataAccessors
{
    public class HostWriter : DataSupport, IData
    {
        public HostWriter (string cText)
        {
            CommandText = cText;
            CommandType = CommandType.Text;
        }

        public int ExecuteCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteNonQuery(this);
        }
    }
}