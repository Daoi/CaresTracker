using CapstoneUI.DataModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CapstoneUI.DataAccess.DataAccessors.Examples
{
    /// <summary>
    /// Class to handle creating/filling the parameters for the CHW creation SQL Command
    /// Seperating them out makes it easy to change them if the DB is changed/helps with organization
    /// </summary>

    public class ResidentParameters
    {
        private MySqlParameter[] Parameters;
        /// <summary>
        /// Create an array to store the parameters in the constructor.
        /// The constructor is called by the CHWWriter class when a new instance of it is created. 
        /// </summary>
        public ResidentParameters()
        {
            Parameters = new MySqlParameter[]
            {
                new MySqlParameter("@FirstName", MySqlDbType.VarChar, 50),
                new MySqlParameter("@LastName", MySqlDbType.VarChar, 50),
                new MySqlParameter("@DateOfBirth", MySqlDbType.VarChar, 50),
                new MySqlParameter("@ResidentEmail", MySqlDbType.VarChar, 50),
                new MySqlParameter("@ResidentPhoneNumber", MySqlDbType.VarChar, 50),
                new MySqlParameter("@RelationshipToHoH", MySqlDbType.VarChar, 50),
                new MySqlParameter("@Gender", MySqlDbType.VarChar, 50),
                new MySqlParameter("@Race", MySqlDbType.VarChar, 50),
                new MySqlParameter("@PreferredLanguage", MySqlDbType.VarChar, 50)
            };
        }

        /// <summary>
        /// Takes a list of values and fills the Parameter array with them.
        /// </summary>
        /// <param name="values">A collection of values intended to be used as SQL stored procedure parameters. It's necessary that the values
        /// match the order established in the constructor. I.e. The value for UserName needs to be first in the list. </param>
        /// <returns></returns>
        public MySqlParameter[] Fill(Resident resident)
        {
            // Add the parameters
            // Done manually for now, couldn't figure out how to pass in house object and iterate through the object to fill in parameters
            Parameters[0].Value = resident.ResidentFirstName;
            Parameters[1].Value = resident.ResidentLastName;
            Parameters[2].Value = resident.DateOfBirth;
            Parameters[3].Value = resident.ResidentEmail;
            Parameters[4].Value = resident.ResidentPhoneNumber;
            Parameters[5].Value = resident.RelationshipToHoH;
            Parameters[6].Value = resident.Gender;
            Parameters[7].Value = resident.Race;
            Parameters[8].Value = resident.PreferredLanguage;
            return Parameters;

        }

    }
}