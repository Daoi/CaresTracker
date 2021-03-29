using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

namespace CaresTracker.DataAccess.DataAccessors.ServiceAccessors
{
    public class InsertService : DataSupport, IData
    {
        public InsertService(string serviceName)
        {
            CommandText = "InsertService";
            CommandType = CommandType.StoredProcedure;
            Parameters = new MySqlParameter[]
            {
                new MySqlParameter("@ServiceName", serviceName)
            };
        }

        public int ExecuteCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteNonQuery(this);
        }
    }
}
