using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CapstoneUI.DataAccess.DataAccessors.RegionAccessors
{
    /// <summary>
    /// Update accessor for editing the organizations assigned
    /// to regions in the Region table.
    /// </summary>
    public class UpdateRegionAssignments
    {
        string command;
        /// <summary>
        /// Initializes the command text to be executed.
        /// </summary>
        /// <param name="pairs">The RegionID - OrganizationID pairs as a list of tuples</param>
        public UpdateRegionAssignments(List<(int regionID, int? orgID)> pairs)
        {
            // build update command
            StringBuilder sCommand = new StringBuilder($"UPDATE Region SET OrganizationID = CASE ");

            // append cases
            List<string> cases = new List<string>();
            foreach ((int regionID, int? orgID) pair in pairs)
            {
                string strOrgID = pair.orgID == null ? "NULL": pair.orgID.ToString();
                cases.Add(string.Format($"WHEN RegionID = {MySqlHelper.EscapeString(pair.regionID.ToString())} " +
                    $"THEN {MySqlHelper.EscapeString(strOrgID)}"));
            }
            sCommand.Append(string.Join(Environment.NewLine, cases));
            sCommand.Append(Environment.NewLine + "END;");
            command = sCommand.ToString();
        }

        /// <summary>
        /// Update Region-Organization assignments
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
