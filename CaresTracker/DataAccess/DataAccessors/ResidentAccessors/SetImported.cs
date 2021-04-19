using System.Data;
using MySql.Data.MySqlClient;

namespace CaresTracker.DataAccess.DataAccessors.ResidentAccessors
{
    public class SetImported : DataSupport, IData
    {
        public SetImported()
        {
            CommandText = "SetImported";
            CommandType = CommandType.StoredProcedure;
        }

        public int ExecuteCommand(int residentID, bool imported)
        {
            Parameters = new MySqlParameter[2] { new MySqlParameter("ResidentID", residentID), new MySqlParameter("Imported", imported) };
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteNonQuery(this);
        }
    }
}