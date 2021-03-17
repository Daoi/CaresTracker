using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CapstoneUI.DataModels;
using MySql.Data.MySqlClient;

namespace CapstoneUI.DataAccess.DataAccessors
{
    /// <summary>
    /// This class updates the HouseID for Residents
    /// ResidentID and HouseID must be passed in
    /// </summary>
    /// 
    public class UpdateHouseID : DataSupport, IData
    {

        public UpdateHouseID()
        {
            CommandText = "UpdateHouseID";
            CommandType = CommandType.StoredProcedure;
        }
        /// <param name="ResidentID">ID of the resident.</param> 
        /// <param name="HouseID">ID of house to link to resident.</param>
        public int ExecuteCommand(int ResidentID, int HouseID)
        {
            Parameters = new MySqlParameter[] { new MySqlParameter("ResidentID", ResidentID), new MySqlParameter("HouseID", HouseID) };
            ExecuteQuery eq = new ExecuteQuery();

            return eq.ExecuteNonQuery(this);
        }
    }
}