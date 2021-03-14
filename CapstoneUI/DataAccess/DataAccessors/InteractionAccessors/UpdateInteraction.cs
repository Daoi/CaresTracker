using System.Data;
using CapstoneUI.DataModels;


namespace CapstoneUI.DataAccess.DataAccessors.InteractionAccessors
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
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteNonQuery(this);
        }

        private void UpdateInteractionSymptoms()
        {
            new UpdateInteractionSymptoms(interaction.Symptoms, interaction.InteractionID).ExecuteCommand();
        }
    }
}