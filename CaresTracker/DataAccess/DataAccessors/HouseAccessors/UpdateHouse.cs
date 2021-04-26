using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CaresTracker.DataModels;

namespace CaresTracker.DataAccess.DataAccessors.HouseAccessors
{
    public class UpdateHouse : DataSupport, IData
    {

        public UpdateHouse(House house)
        {
            CommandText = "UpdateHouse";
            CommandType = CommandType.StoredProcedure;
            Parameters = new HouseParameters().Fill(house).ToList().Concat(
                new List<MySqlParameter>()
                {
                    new MySqlParameter("HouseID", house.HouseID)
                }
            ).ToArray(); 
        }

        public int ExecuteCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteNonQuery(this);
        }
    }
}