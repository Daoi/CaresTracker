using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

namespace CapstoneUI.DataAccess.DataAccessors
{
    public class GetResidentByID : DataSupport, IData
    {
        public GetResidentByID()
        {
            CommandText = "GetResidentByID";
            CommandType = CommandType.StoredProcedure;
        }
        /// <summary>
        /// Get the resident with the provided ID
        /// </summary>
        /// <param name="residentid">ID(pk) of the resident</param>
        /// <returns></returns>
        public DataTable RunCommand(int residentid)
        {
            Parameters = new MySqlParameter[1] { new MySqlParameter("ResidentID", residentid) };
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }
    }
}