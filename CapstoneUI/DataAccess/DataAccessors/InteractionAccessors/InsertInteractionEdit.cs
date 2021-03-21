using System.Data;
using MySql.Data.MySqlClient;


namespace CapstoneUI.DataAccess.DataAccessors.InteractionAccessors
{
    public class InsertInteractionEdit : DataSupport, IData
    {

        public InsertInteractionEdit()
        {
            CommandText = "InsertInteractionEdit";
            CommandType = CommandType.StoredProcedure;
        }

        public int ExecuteCommand(string date, string reason, int intID, int workerID)
        {
            Parameters = new MySqlParameter[4] {
                new MySqlParameter("InteractionID", intID),
                new MySqlParameter("EditReason", reason),
                new MySqlParameter("EditDate", date),
                new MySqlParameter("WorkerID", workerID)
            };
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteNonQuery(this);
        }
    }
}