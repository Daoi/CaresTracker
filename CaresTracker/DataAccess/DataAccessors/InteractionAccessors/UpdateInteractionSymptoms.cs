using CaresTracker.DataModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CaresTracker.DataAccess.DataAccessors.InteractionAccessors
{
    public class UpdateInteractionSymptoms
    {
        string command;
        int IntID;
        List<Symptom> symptoms;
        public UpdateInteractionSymptoms(List<Symptom> updates, int id)
        {
            //Build Update Command for multiple rows
            symptoms = updates;
            IntID = id;
            command = SymptomsInsertSQLWriter.WriteSQL(updates, id);
        }

        /// <summary>
        /// Update symptoms selection for interaction. Deletes old symptoms belonging to ID.
        /// </summary>
        /// <returns></returns>
        public int ExecuteCommand()
        {
            //Run Command
            try
            {
                new DeleteOldInteractionSymptoms().ExecuteCommand(IntID);

                if (symptoms.Count > 0)
                    return new CTextWriter(command.ToString()).ExecuteCommand();
                else
                    return 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
