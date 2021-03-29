using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

namespace CapstoneUI.DataAccess.DataAccessors.HouseAccessors
{
    public class GetHouse : DataSupport, IData
    {

        public GetHouse()
        {
            //ConnectionString = GetConnectionString("dbConnectionString");
            CommandText = "GetHouse";
            CommandType = CommandType.StoredProcedure;
        }

        public DataTable RunCommand(string address)
        {
            Parameters = new MySqlParameter[1] { new MySqlParameter("Address", address) }; //Add parameters required for command
            ExecuteQuery eq = new ExecuteQuery(); //Create instance of class that handles command obj
            return eq.ExecuteAdapter(this); //Run the command
        }



    }
}