using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CaresTracker.DataModels;
using CaresTracker.DataAccess.DataAccessors.CARESUserAccessors;

namespace CaresTracker.DataAccess.DataAccessors.ResidentAccessors
{
    public class ResidentWriter : DataSupport, IData
    {

        public ResidentWriter(Resident resident)
        {
            CommandText = "AddResident";
            CommandType = CommandType.StoredProcedure;
            Parameters = new ResidentParameters().Fill(resident);
        }
        /// <summary>
        /// Insert new resident into DB and return their ID
        /// </summary>
        /// <returns>Returns an Object representing the INT id or NULL if resident already exists</returns>
        public object ExecuteCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteScalar(this);
        }

    }
}