using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CaresTracker.DataModels;

namespace CaresTracker.DataAccess.DataAccessors.ResidentAccessors
{
    public class UpdateResidentChronicIllnesses
    {
        string command;
        int resID;
        List<ChronicIllness> illnesses;

        public UpdateResidentChronicIllnesses(List<ChronicIllness> updates, int id)
        {
            //Build Update Command for multiple rows
            resID = id;
            illnesses = updates;
            command = InsertResidentChronicIllnesses.WriteSQL(updates, id);
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
                new DeleteOldChronicIllnesses().ExecuteCommand(resID);

                if (illnesses.Count > 0)
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