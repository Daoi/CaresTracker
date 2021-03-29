using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CaresTracker.DataModels;
using MySql.Data.MySqlClient;

namespace CaresTracker.DataAccess.DataAccessors.InteractionAccessors
{
    public class DeleteOldInteractionSymptoms : DataSupport, IData
    {
        /// <summary>
        /// Delete the old symptoms before inserting new symptoms for interaction update
        /// </summary>
        public DeleteOldInteractionSymptoms()
        {
            CommandText = "DeleteOldInteractionSymptoms";
            CommandType = CommandType.StoredProcedure;
        }

        public int ExecuteCommand(int id)
        {
            Parameters = new MySqlParameter[1] { new MySqlParameter("InteractionID", id) };
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteNonQuery(this);
        }
    }
}