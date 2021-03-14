using System;
using System.Collections.Generic;
using System.Data;
using CapstoneUI.DataModels;
using System.Text;
using MySql.Data.MySqlClient;

namespace CapstoneUI.DataAccess.DataAccessors.InteractionAccessors
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

            string servicesCommand = ServicesInsertSQLWriter.WriteSQL(interact.RequestedServices, interact.InteractionID);
            string symptomsCommand = SymptomsInsertSQLWriter.WriteSQL(interact.Symptoms, interact.InteractionID);

            try
            {
                new CTextWriter(servicesCommand).ExecuteCommand();
                new CTextWriter(symptomsCommand.ToString()).ExecuteCommand();
                return 1;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}