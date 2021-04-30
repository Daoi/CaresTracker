using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace CaresTracker.DataAccess.DataAccessors.ResidentAccessors
{
    public class DeleteOldChronicIllnesses : DataSupport, IData
    {
        /// <summary>
        /// Delete the old chronic illnesses before inserting new ones
        /// </summary>
        public DeleteOldChronicIllnesses()
        {
            CommandText = "DeleteOldChronicIllnesses";
            CommandType = CommandType.StoredProcedure;
        }

        public int ExecuteCommand(int residentID)
        {
            Parameters = new MySqlParameter[1] { new MySqlParameter("resID", residentID) };
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteNonQuery(this);
        }
    }
}
