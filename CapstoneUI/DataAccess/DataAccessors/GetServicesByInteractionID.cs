using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CapstoneUI.DataAccess
{
    public class GetServicesByInteractionID : DataSupport, IData
    {

        public GetServicesByInteractionID()
        {
            CommandText = "GetServicesByInteractionID";
            CommandType = CommandType.StoredProcedure;
        }
        /// <summary>
        /// Get all services associated with an interaction
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