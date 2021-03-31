using System.Data;
using MySql.Data.MySqlClient;

namespace CaresTracker.DataAccess
{
    public interface IData
    {
        /// <summary>
        /// System.Data.SqlClient ConnectionString
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// System.Data.SqlClient Command Text: Name of Stored Proc or in-line sql
        /// </summary>
        string CommandText { get; set; }

        /// <summary>
        /// Array of System.Data.SqlClient SqlParameters
        /// </summary>
        MySqlParameter[] Parameters { get; set; }
        /// <summary>
        /// System.Data CommandType
        /// </summary>
        CommandType CommandType { get; set; }

        /// <summary>
        /// Set to the number of rows affected by ExecuteNonQuery and ExecuteAdapter
        /// </summary>
        int Count { get; set; }

        /// <summary>
        /// Sets the command timeout in seconds. If not set will be read 
        /// from  AppSettings value for key="CommandTimeoutSeconds"
        /// </summary>
        int? Timeout { get; set; }
    }
}