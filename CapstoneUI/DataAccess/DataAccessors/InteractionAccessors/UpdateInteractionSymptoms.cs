using CapstoneUI.DataModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CapstoneUI.DataAccess.DataAccessors.InteractionAccessors
{
    public class UpdateInteractionSymptoms
    {

        string command;
        int IntID;
        public UpdateInteractionSymptoms(List<Symptom> updates, int id)
        {
            //Build Update Command for multiple rows
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
                return new CTextWriter(command.ToString()).ExecuteCommand();
            }
            catch (Exception e)
            {
                throw e;
            }


        }


    }
}