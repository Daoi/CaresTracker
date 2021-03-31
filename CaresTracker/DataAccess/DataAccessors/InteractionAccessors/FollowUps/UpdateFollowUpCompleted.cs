using MySql.Data.MySqlClient;
using System.Data;


namespace CaresTracker.DataAccess.DataAccessors.InteractionAccessors.FollowUps
{
    public class UpdateFollowUpCompleted : DataSupport, IData
    {
        public UpdateFollowUpCompleted()
        {
            CommandText = "UpdateFollowUpCompleted";
            CommandType = CommandType.StoredProcedure;
        }
        /// <summary>
        /// Mark an interaction's follow up status as complete
        /// </summary>
        /// <param name="date">Date the interaction was completed</param>
        /// <param name="id">Id of the interaction</param>
        /// <returns></returns>
        public int ExecuteCommand(string date, int id)
        {
            Parameters = new MySqlParameter[] { new MySqlParameter("CompletetionDate", date), new MySqlParameter("InteractionID", id) };
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteNonQuery(this);
        }
    }
}