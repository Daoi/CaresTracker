using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        private SqlParameter[] Parameters;
        /// <summary>
        /// Create an array to store the parameters in the constructor.
        /// The constructor is called by the CHWWriter class when a new instance of it is created. 
        /// </summary>
        public ResidentParameters()
        {
            Parameters = new SqlParameter[]
            {
                new SqlParameter("@ResidenceID", SqlDbType.Int, 50),
                new SqlParameter("@FirstName", SqlDbType.VarChar, 50),
                new SqlParameter("@LastName", SqlDbType.VarChar, 50),
                new SqlParameter("@DateOfBirth", SqlDbType.DateTime, 50),
                new SqlParameter("@ResidentEmail", SqlDbType.VarChar, 50),
                new SqlParameter("@ResidentPhoneNumber", SqlDbType.VarChar, 50),
                new SqlParameter("@RelationshipToHoH", SqlDbType.VarChar, 50),
                new SqlParameter("@Gender", SqlDbType.VarChar, 50),
                new SqlParameter("@FamilySize", SqlDbType.Int, 50)
            };
        }

        /// <summary>
        /// Takes a list of values and fills the Parameter array with them.
        /// </summary>
        /// <param name="values">A collection of values intended to be used as SQL stored procedure parameters. It's necessary that the values
        /// match the order established in the constructor. I.e. The value for UserName needs to be first in the list. </param>
        /// <returns></returns>
        public SqlParameter[] Fill(List<string> values)
        {
            //Make sure there are a correct number of parameters provided
            if (values.Count != Parameters.Length)
            {
                throw new IndexOutOfRangeException($"Parameter count error. Provided: {values.Count} Allowed: {Parameters.Length}");
            }
            //Add the parameters
            for (int i = 0; i < Parameters.Length; i++)
            {
                Parameters[i].Value = values[i];
            }

            return Parameters;

        }
    }
}