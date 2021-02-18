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

        public List<string> ExecuteCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();
            DataTable dataTable = eq.ExecuteAdapter(this);
            List<string> list = new List<string>();

            foreach (DataRow row in dataTable.Rows)
            {
                string name = row.Field<string>("DevelopmentName");
                list.Add(name);
            }
            return list;
        }
    }
}