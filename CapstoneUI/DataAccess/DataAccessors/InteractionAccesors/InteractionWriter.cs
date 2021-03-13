using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CapstoneUI.DataModels;
using System.Text;
using MySql.Data.MySqlClient;
using CapstoneUI.DataAccess.DataAccessors.InteractionAccesors;

namespace CapstoneUI.DataAccess.DataAccessors.InteractionAccessors
{
    public class InteractionWriter : DataSupport, IData
    {
        public Interaction intact;

        public InteractionWriter(Interaction interaction)
        {
            CommandText = "AddInteraction";
            CommandType = CommandType.StoredProcedure;
            Parameters = new InteractionParameters().Fill(interaction);
            intact = interaction;
        }

        public int ExecuteCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();

            try
            {
               object resultObj = eq.ExecuteScalar(this);
                intact.InteractionID = Convert.ToInt32(resultObj); //Get the id of interaction
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Interaction {intact} could not be added succesfully: {e}");
            }

            WriteServices(intact.RequestedServices);
            WriteSymptoms(intact.Symptoms);
            return 1;
        }

        private void WriteServices(List<Service> services)
        {

            StringBuilder sCommand = new StringBuilder("INSERT INTO InteractionService (InteractionID, ServiceID, IsCompleted) VALUES ");
            List<string> rows = new List<string>(); //The rows to insert
            foreach (Service s in services)
            {
                rows.Add(string.Format($"('{MySqlHelper.EscapeString(intact.InteractionID.ToString())}'," +
                    $"'{MySqlHelper.EscapeString(s.ServiceID.ToString())}'," +
                    $"'{0}')"));
            }
            sCommand.Append(string.Join(",", rows));
            sCommand.Append(";");

            try
            {
                new InteractionServiceWriter(sCommand.ToString()).ExecuteCommand();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        private void WriteSymptoms(List<Symptom> symptoms)
        {
            StringBuilder sCommand = new StringBuilder("INSERT INTO InteractionSymptom (InteractionID, SymptomID) VALUES ");
            List<string> rows = new List<string>(); //The rows to insert
            foreach (Symptom s in symptoms)
            {   
                rows.Add(string.Format($"('{MySqlHelper.EscapeString(intact.InteractionID.ToString())}'," +
                    $"'{MySqlHelper.EscapeString(s.SymptomID.ToString())}')"));
            }
            sCommand.Append(string.Join(",", rows));
            sCommand.Append(";");
            try
            {
                new InteractionServiceWriter(sCommand.ToString()).ExecuteCommand();
            }
            catch (Exception e)
            {
                throw e;
            }

        }



    }
}