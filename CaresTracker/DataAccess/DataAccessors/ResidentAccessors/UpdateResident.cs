using CaresTracker.DataModels;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaresTracker.DataAccess.DataAccessors.ResidentAccessors
{
    public class UpdateResident : DataSupport, IData
    {
        public UpdateResident(Resident res)
        {
            CommandText = "UpdateResident";
            CommandType = CommandType.StoredProcedure;
            Parameters = new UpdateResidentParameters().Fill(res);
        }

        public int ExecuteCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteNonQuery(this);
        }
    }
}