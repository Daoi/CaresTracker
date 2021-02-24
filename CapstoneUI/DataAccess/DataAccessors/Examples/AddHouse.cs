using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CapstoneUI.DataModels;

namespace CapstoneUI.DataAccess.DataAccessors.Examples
{
    public class AddHouse : DataSupport, IData
    {
        public AddHouse(House house)
        {
            CommandText = "AddHouse";
            CommandType = CommandType.StoredProcedure;
            Parameters = new HouseParameters().Fill(house);
        }

        public int ExecuteCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteNonQuery(this);
        }
    }
}