using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace CaresTracker.DataAccess.DataAccessors.ResidentAccessors
{
    public class UpdateResidentVaccine : DataSupport, IData
    {
        public UpdateResidentVaccine()
        {
            CommandText = "UpdateResidentVaccine";
            CommandType = CommandType.StoredProcedure;
        }

        public int ExecuteCommand(int id, bool interest, int phase, string date)
        {
            Parameters = new MySqlParameter[] {
                new MySqlParameter("ResidentID", id),
                new MySqlParameter("VaccineInterest", interest),
                new MySqlParameter("VaccineEligibility", phase),
                new MySqlParameter("VaccineAppointmentDate", date) };

            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteNonQuery(this);
        }
    }
}