using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CapstoneUI.DataModels;
using CapstoneUI.DataAccess.DataAccessors.Examples;

namespace CapstoneUI.DataAccess.DataAccessors
{
    public class ResidentWriter : DataSupport, IData
    {

        public ResidentWriter(Resident resident)
        {
            CommandText = "AddResident";
            CommandType = CommandType.StoredProcedure;
            Parameters = new ResidentParameters().Fill(resident);
        }

        public int ExecuteCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteNonQuery(this);
        }

    }
}