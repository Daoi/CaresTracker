using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CaresTracker.DataAccess.DataAccessors.SymptomAccessors
{
    public class GetSymptomsByInteractionID : DataSupport,  IData
    {

        public GetSymptomsByInteractionID()
        {
            CommandText = "GetSymptomsByInteractionID";
            CommandType = CommandType.StoredProcedure;
        }
        /// <summary>
        /// Get all symptoms associated with an interaction
        /// </summary>
        /// <param name="interactionID">ID(pk) of the interaction</param>
        /// <returns></returns>
        public DataTable RunCommand(int interactionID)
        {
            Parameters = new MySqlParameter[1] { new MySqlParameter("InteractionID", interactionID) };
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }

    }
}