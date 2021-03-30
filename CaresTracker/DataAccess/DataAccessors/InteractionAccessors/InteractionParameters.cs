using CaresTracker.DataModels;
using MySql.Data.MySqlClient;



namespace CaresTracker.DataAccess.DataAccessors.InteractionAccessors
{
    public class InteractionParameters
    {
        private MySqlParameter[] Parameters;

        public InteractionParameters()
        {
            Parameters = new MySqlParameter[]
            {
                new MySqlParameter("@ResidentID", MySqlDbType.Int64, 50),
                new MySqlParameter("@HealthWorkerID", MySqlDbType.Int64, 50),
                new MySqlParameter("@DateOfContact", MySqlDbType.VarChar, 50),
                new MySqlParameter("@MethodOfContact", MySqlDbType.VarChar, 50),
                new MySqlParameter("@LocationOfContact", MySqlDbType.VarChar, 50),
                new MySqlParameter("@COVIDTestLocation", MySqlDbType.VarChar, 50),
                new MySqlParameter("@COVIDTestResult", MySqlDbType.VarChar, 50),
                new MySqlParameter("@ActionPlan", MySqlDbType.VarChar, 500),
                new MySqlParameter("@SymptomStartDate", MySqlDbType.VarChar, 50),
                new MySqlParameter("RequiresFollowUp", MySqlDbType.Bit, 1),
                new MySqlParameter("FollowUpCompleted", MySqlDbType.VarChar, 50),
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
            Parameters[0].Value = interaction.ResidentID;
            Parameters[1].Value = interaction.HealthWorkerID;
            Parameters[2].Value = interaction.DateOfContact;
            Parameters[3].Value = interaction.MethodOfContact;
            Parameters[4].Value = interaction.LocationOfContact;
            Parameters[5].Value = interaction.COVIDTestLocation;
            Parameters[6].Value = interaction.COVIDTestResult;
            Parameters[7].Value = interaction.ActionPlan;
            Parameters[8].Value = interaction.SymptomStartDate;
            Parameters[9].Value = interaction.RequiresFollowUp;
            Parameters[10].Value = interaction.FollowUpCompleted;
            return Parameters;

        }
    }
}