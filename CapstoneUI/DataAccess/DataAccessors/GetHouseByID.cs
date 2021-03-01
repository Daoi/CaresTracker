using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

namespace CapstoneUI.DataAccess.DataAccessors
{
    public class GetHouseByID : DataSupport, IData
    {

        public GetHouseByID()
        {
            CommandText = "GetHouseByID";
            CommandType = CommandType.StoredProcedure;
        }
        /// <summary>
        /// Get the house with the provided ID
        /// </summary>
        /// <param name="houseID">ID(pk) of the house</param>
        /// <returns></returns>
        public DataTable RunCommand(int houseID)
        {
            Parameters = new MySqlParameter[1] { new MySqlParameter("HouseID", houseID) }; 
            ExecuteQuery eq = new ExecuteQuery(); 
            return eq.ExecuteAdapter(this); 
        }



    }
}