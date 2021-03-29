using System;
using System.Collections.Generic;
using System.Data;
using CaresTracker.DataModels;
using System.Text;
using MySql.Data.MySqlClient;

namespace CaresTracker.DataAccess.DataAccessors.InteractionAccessors
{
    public class UpdateInteractionServices
    {
        string command;
        int IntID;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updates">The services</param>
        /// <param name="id">Id of the inteaction</param>
        /// <param name="completion">0 for incomplete 1 for completed</param>
        public UpdateInteractionServices(List<Service> updates, int id, int completion)
        {
            //Build Update Command for multiple rows
            IntID = id;
            StringBuilder sCommand = new StringBuilder($"UPDATE InteractionService SET IsCompleted = '{completion}' WHERE InteractionID = '{MySqlHelper.EscapeString(id.ToString())}' AND (");
            List<string> rows = new List<string>(); //The rows to insert
            foreach (Service s in updates)
            {
                rows.Add(string.Format($"ServiceID = '{MySqlHelper.EscapeString(s.ServiceID.ToString())}'"));
            }
            sCommand.Append(string.Join(" OR ", rows));
            sCommand.Append(");");
            command = sCommand.ToString();
        }
        /// <summary>
        /// Update services that were requested to services that are completed
        /// </summary>
        /// <param name="updates">List of services that are now completed and need to be updated</param>
        /// <param name="id">The interaction ID</param>
        /// <returns></returns>
        public int ExecuteCommand()
        {
            //Run Command
            try
            {
                return new CTextWriter(command.ToString()).ExecuteCommand();
                       
            }
            catch (Exception e)
            {
                throw e;
            }


        }
    }
}