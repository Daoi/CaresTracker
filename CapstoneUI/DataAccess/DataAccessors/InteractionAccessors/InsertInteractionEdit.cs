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

        public int ExecuteCommand(string date, string reason, int id)
        {
            Parameters = new MySqlParameter[3] { new MySqlParameter("InteractionID", id), new MySqlParameter("EditReason", reason), new MySqlParameter("EditDate", date) };
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteNonQuery(this);
        }
    }
}