using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

namespace CaresTracker.DataAccess.DataAccessors.ResidentAccessors
{
    public class InsertRace : DataSupport, IData
    {
        public InsertRace(string race)
        {
            CommandText = "InsertRace";
            CommandType = CommandType.StoredProcedure;
            Parameters = new MySqlParameter[]
            {
                new MySqlParameter("@Race", race)
            };
        }

        public int ExecuteCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteNonQuery(this);
        }
    }
}
