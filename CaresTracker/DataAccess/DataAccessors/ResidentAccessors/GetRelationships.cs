using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

namespace CaresTracker.DataAccess.DataAccessors.ResidentAccessors
{
    public class GetRelationships : DataSupport, IData
    {

        public GetRelationships()
        {
            CommandText = "GetAllRelationships";
            CommandType = CommandType.StoredProcedure;
        }
        /// <summary>
        /// Get the resident with the provided ID
        /// </summary>
        /// <param name="residentID">ID(pk) of the resident</param>
        /// <returns></returns>
        public DataTable RunCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }
    }
}