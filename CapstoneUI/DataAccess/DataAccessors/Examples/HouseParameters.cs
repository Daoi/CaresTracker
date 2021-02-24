using CapstoneUI.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CapstoneUI.DataAccess.DataAccessors.Examples
{
    /// <summary>
    /// Class to handle creating/filling the parameters for the CHW creation SQL Command
    /// Seperating them out makes it easy to change them if the DB is changed/helps with organization
    /// </summary>

    public class HouseParameters
    {
        private SqlParameter[] Parameters;
        /// <summary>
        /// Create an array to store the parameters in the constructor.
        /// The constructor is called by the CHWWriter class when a new instance of it is created. 
        /// </summary>
        public HouseParameters()
        {
            Parameters = new SqlParameter[]
            {
                new SqlParameter("@RegionID", SqlDbType.Int),
                new SqlParameter("@Address", SqlDbType.VarChar, 50),
                new SqlParameter("@NumOccupants", SqlDbType.Int),
                new SqlParameter("@HouseType", SqlDbType.VarChar, 50),
                new SqlParameter("@DevelopmentID", SqlDbType.Int),
                new SqlParameter("@ZipCode", SqlDbType.VarChar, 50),
            };
        }

        /// <summary>
        /// Takes a list of values and fills the Parameter array with them.
        /// </summary>
        /// <param name="values">A collection of values intended to be used as SQL stored procedure parameters. It's necessary that the values
        /// match the order established in the constructor. I.e. The value for UserName needs to be first in the list. </param>
        /// <returns></returns>
        public SqlParameter[] Fill(House house)
        {
            // Add the parameters
            // Done manually for now, couldn't figure out how to pass in house object and iterate through the object to fill in parameters
            Parameters[0].Value = house.RegionID;
            Parameters[1].Value = house.Address;
            Parameters[2].Value = house.NumOccupants;
            Parameters[3].Value = house.HouseType;
            Parameters[4].Value = house.DevelopmentID;
            Parameters[5].Value = house.ZipCode;

            // If IDs are 0, null is entered into the DB
            if (house.RegionID == 0)
            {
                Parameters[0].Value = null;
            }
            if (house.DevelopmentID == 0)
            {
                Parameters[4].Value = null;
            }



            return Parameters;

        }
    }
}