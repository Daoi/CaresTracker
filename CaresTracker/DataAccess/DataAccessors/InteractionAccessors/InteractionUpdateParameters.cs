using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CaresTracker.DataModels;
using System.Text;
using MySql.Data.MySqlClient;

namespace CaresTracker.DataAccess.DataAccessors.InteractionAccessors
{
    public class InteractionUpdateParameters
    {
        private MySqlParameter[] Parameters;

        public InteractionUpdateParameters()
        {
            Parameters = new MySqlParameter[]
            {
                new MySqlParameter("@InteractionID", MySqlDbType.Int64),
                new MySqlParameter("@ResidentID", MySqlDbType.Int64),
                new MySqlParameter("@DateOfContact", MySqlDbType.VarChar, 50),
                new MySqlParameter("@MethodOfContact", MySqlDbType.VarChar, 50),
                new MySqlParameter("@LocationOfContact", MySqlDbType.VarChar, 50),
                new MySqlParameter("@COVIDTestLocation", MySqlDbType.VarChar, 50),
                new MySqlParameter("@COVIDTestResult", MySqlDbType.VarChar, 50),
                new MySqlParameter("@ActionPlan", MySqlDbType.VarChar, 500),
                new MySqlParameter("@SymptomStartDate", MySqlDbType.VarChar, 50)
            };
        }

        /// <summary>
        /// Create SQL command parameters from interaction object
        /// </summary>
        /// <param name="interaction"></param>
        /// <returns></returns>
        public MySqlParameter[] Fill(Interaction interaction)
        {
            // Add the parameters
            // Done manually for now, couldn't figure out how to pass in house object and iterate through the object to fill in parameters
            Parameters[0].Value = interaction.InteractionID;
            Parameters[1].Value = interaction.ResidentID;
            Parameters[2].Value = interaction.DateOfContact;
            Parameters[3].Value = interaction.MethodOfContact;
            Parameters[4].Value = interaction.LocationOfContact;
            Parameters[5].Value = interaction.COVIDTestLocation;
            Parameters[6].Value = interaction.COVIDTestResult;
            Parameters[7].Value = interaction.ActionPlan;
            Parameters[8].Value = interaction.SymptomStartDate;
            return Parameters;

        }
    }
}