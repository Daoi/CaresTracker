using System.Data;
using CaresTracker.DataModels;

namespace CaresTracker.DataAccess.DataAccessors.InteractionAccessors
{
    public class UpdateInteraction : DataSupport, IData
    {
        Interaction interaction;
        public UpdateInteraction(Interaction interaction)
        {
            Parameters = new InteractionUpdateParameters().Fill(interaction);
            CommandText = "UpdateInteraction";
            CommandType = CommandType.StoredProcedure;
            this.interaction = interaction;
        }
        /// <summary>
        /// Update an interaction excluding Follow up status
        /// </summary>
        /// <returns></returns>
        public int ExecuteCommand()
        {
            new UpdateInteractionSymptoms(interaction.Symptoms, interaction.InteractionID).ExecuteCommand();

            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteNonQuery(this);
        }
    }
}