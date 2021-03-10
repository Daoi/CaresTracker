using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using CapstoneUI.DataModels;

namespace CapstoneUI.DataAccess.DataAccessors
{
    public class GetResidentByAttributes : DataSupport, IData
    {
        public GetResidentByAttributes()
        {
            CommandText = "GetResidentByAttributes";
            CommandType = CommandType.StoredProcedure;
        }
        public DataTable RunCommand(Resident resident)
        {
            Parameters = new MySqlParameter[4] { new MySqlParameter("FirstName", resident.ResidentFirstName), new MySqlParameter("LastName", resident.ResidentLastName), new MySqlParameter("DOB", resident.DateOfBirth), new MySqlParameter("Address", resident.Home.Address) };
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }
    }
}