using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CaresTracker.DataAccess.DataAccessors.ChronicIllnessAccessors
{
    public class GetChronicIllnessesByResidentID : DataSupport, IData
    {

        public GetChronicIllnessesByResidentID()
        {
            CommandText = "GetChronicIllnessesByResidentID";
            CommandType = CommandType.StoredProcedure;
        }
        /// <summary>
        /// Get all chronic illnesses associated with a resident
        /// </summary>
        /// <param name="interactionID">ID(pk) of the interaction</param>
        /// <returns></returns>
        public DataTable ExecuteCommand(int residentID)
        {
            Parameters = new MySqlParameter[1] { new MySqlParameter("resID", residentID) };
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }
    }
}