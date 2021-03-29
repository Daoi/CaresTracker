using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using MySql.Data.MySqlClient;

namespace CapstoneUI.DataAccess.DataAccessors.GenericAccessors
{
    public class UpdateRecordIsEnabled
    {
        string command;
        /// <summary>
        /// Initializes the command text to be executed.
        /// </summary>
        /// <param name="tableName">The name of the table to be updated</param>
        /// <param name="idColName">The name of the primary key column</param>
        /// <param name="isEnabledColName">The name of the IsEnabled column</param>
        /// <param name="pairs">The ID - IsEnabled value pairs as a list of tuples</param>
        public UpdateRecordIsEnabled(string table, string idColName, string isEnabledColName, List<(int id, bool isEnabled)> pairs)
        {
            // build update command
            StringBuilder sCommand = new StringBuilder($"UPDATE {table} SET {isEnabledColName} = CASE ");

            // append cases
            List<string> cases = new List<string>();
            foreach ((int id, bool isEnabled) pair in pairs)
            {
                cases.Add(string.Format($"WHEN {idColName} = {MySqlHelper.EscapeString(pair.id.ToString())} " +
                    $"THEN {MySqlHelper.EscapeString(pair.isEnabled.ToString())}"));
            }
            sCommand.Append(string.Join(Environment.NewLine, cases));
            sCommand.Append(Environment.NewLine + "END;");
            command = sCommand.ToString();
        }

        /// <summary>
        /// Update the IsEnabled field for each ID
        /// </summary>
        /// <returns>Number of rows affected by the update</returns>
        public int ExecuteCommand()
        {
            try
            {
                return new CTextWriter(command.ToString()).ExecuteCommand();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}