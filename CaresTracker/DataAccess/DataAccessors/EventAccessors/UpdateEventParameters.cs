using CaresTracker.DataModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaresTracker.DataAccess.DataAccessors.EventAccessors
{
    public class UpdateEventParameters
    {
        private MySqlParameter[] Parameters;


        public UpdateEventParameters()
        {
            Parameters = new MySqlParameter[]
            {
                
                new MySqlParameter("@EventName", MySqlDbType.VarChar, 50),
                new MySqlParameter("@EventDescription", MySqlDbType.VarChar, 50),
                new MySqlParameter("@EventTypeID", MySqlDbType.Int32),
                new MySqlParameter("@EventLocation", MySqlDbType.VarChar, 50),
                new MySqlParameter("@EventDate", MySqlDbType.VarChar, 50),
                new MySqlParameter("@EventStartTime", MySqlDbType.VarChar, 50),
                new MySqlParameter("@EventEndTime", MySqlDbType.VarChar, 50),
                new MySqlParameter("@MainHostID", MySqlDbType.Int32),
                new MySqlParameter("@EventID", MySqlDbType.Int32),
            };
        }

        /// <summary>
        /// Takes a Event object and fills parameters with object attributes
        /// </summary>
        /// <param name="newEvent">A Event object intended to be used as SQL stored procedure parameters.</param>
        /// <returns></returns>
        public MySqlParameter[] Fill(DataModels.Event newEvent)
        {
            Parameters[0].Value = newEvent.EventName;
            Parameters[1].Value = newEvent.EventDescription;
            Parameters[2].Value = newEvent.EventTypeID;
            Parameters[3].Value = newEvent.EventLocation;
            Parameters[4].Value = newEvent.EventDate;
            Parameters[5].Value = newEvent.EventStartTime;
            Parameters[6].Value = newEvent.EventEndTime;
            Parameters[7].Value = newEvent.MainHostID;
            Parameters[8].Value = newEvent.EventID;
            return Parameters;
        }
    }
}