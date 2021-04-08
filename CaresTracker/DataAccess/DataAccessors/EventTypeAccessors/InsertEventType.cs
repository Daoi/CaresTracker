using System.Data;
using MySql.Data.MySqlClient;

namespace CaresTracker.DataAccess.DataAccessors.EventTypeAccessors
{
    public class InsertEventType : DataSupport, IData
    {
        public InsertEventType(string eventTypeName)
        {
            CommandText = "InsertEventType";
            CommandType = CommandType.StoredProcedure;
            Parameters = new MySqlParameter[]
            {
                new MySqlParameter("@EventTypeName", eventTypeName)
            };
        }

        public int ExecuteCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteNonQuery(this);
        }

    }
}