using CaresTracker.DataModels;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaresTracker.DataAccess.DataAccessors.EventAccessors
{
    public class UpdateEvent : DataSupport, IData
    {
        public UpdateEvent(DataModels.Event e)
        {
            CommandText = "UpdateEvent";
            CommandType = CommandType.StoredProcedure;
            Parameters = new UpdateEventParameters().Fill(e);
        }

        public int ExecuteCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteNonQuery(this);
        }
    }
}