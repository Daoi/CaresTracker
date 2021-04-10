using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

namespace CaresTracker.DataAccess.DataAccessors.InteractionAccessors
{
    public class GetInteractionEditHistory : DataSupport, IData
    {
        public GetInteractionEditHistory()
        {
            CommandText = "GetInteractionEditHistory";
            CommandType = CommandType.StoredProcedure;
        }
        /// <summary>
        /// Return the edit history of an interaction
        /// </summary>
        /// <param name="interactionID">ID of the interaction to find edit history of</param>
        /// <returns></returns>
        public DataTable RunCommand(int interactionID)
        {
            Parameters = new MySqlParameter[1] { new MySqlParameter("InteractionID", interactionID) };
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }
    }
}
