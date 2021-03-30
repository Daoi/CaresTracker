using System;
using System.Collections.Generic;
using System.Data;
using CaresTracker.DataModels;
using System.Text;
using MySql.Data.MySqlClient;

namespace CaresTracker.DataAccess.DataAccessors.InteractionAccessors
{
    public class InteractionWriter : DataSupport, IData
    {
        public Interaction interact;

        public InteractionWriter(Interaction interaction)
        {
            CommandText = "AddInteraction";
            CommandType = CommandType.StoredProcedure;
            Parameters = new InteractionParameters().Fill(interaction);
            interact = interaction;
        }

        public int ExecuteCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();

            try
            {
                object resultObj = eq.ExecuteScalar(this);
                interact.InteractionID = Convert.ToInt32(resultObj); //Get the id of interaction
            }
            catch (Exception e)
            {
                throw e;
            }
            //If interaction insert succeeds 
            try
            {
                if (interact.RequestedServices.Count > 0)
                {
                    string servicesCommand = ServicesInsertSQLWriter.WriteSQL(interact.RequestedServices, interact.InteractionID);
                    new CTextWriter(servicesCommand).ExecuteCommand();
                }
                if (interact.Symptoms.Count > 0)
                {
                    string symptomsCommand = SymptomsInsertSQLWriter.WriteSQL(interact.Symptoms, interact.InteractionID);
                    new CTextWriter(symptomsCommand).ExecuteCommand();
                }
                return 1;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}