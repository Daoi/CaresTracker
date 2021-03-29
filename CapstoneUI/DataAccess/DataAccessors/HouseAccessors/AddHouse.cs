using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CaresTracker.DataModels;

namespace CaresTracker.DataAccess.DataAccessors.HouseAccessors
{
    public class AddHouse : DataSupport, IData
    {
        public AddHouse(House house)
        {
            CommandText = "AddHouse";
            CommandType = CommandType.StoredProcedure;
            Parameters = new HouseParameters().Fill(house);
        }

        public object ExecuteCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteScalar(this);
        }
    }
}