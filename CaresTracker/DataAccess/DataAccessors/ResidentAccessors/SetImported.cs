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

        public int ExecuteCommand(int residentID, bool imported, bool dob)
        {
            Parameters = new MySqlParameter[3] { new MySqlParameter("ResidentID", residentID), new MySqlParameter("Imported", imported), new MySqlParameter("DobImported", dob) };
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteNonQuery(this);
        }
    }
}