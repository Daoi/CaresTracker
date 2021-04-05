using CaresTracker.DataAccess.DataAccessors.CARESUserAccessors;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaresTracker.DataAccess.DataAccessors
{
    public class AddSQLUser : DataSupport, IData
    {


        public static void ExecuteCommand(string username, string password)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"CREATE USER '{username}'@'%' IDENTIFIED BY '{password}';");
            sb.Append($"GRANT SELECT, INSERT, UPDATE, EXECUTE ON carestracker_db.* TO {username}@'%';");

            new CTextWriter(sb.ToString()).ExecuteCommand();

        }

    }
}