using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Configuration;
using CaresTracker.DataModels;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace CaresTracker.DataAccess
{
    /// <summary>
    /// Provides default implementation of IData so you only have to change what is needed for your use.
    /// </summary>
    public class DataSupport
    {
        private string defaultConnection = "dbConnectionString";

        public DataSupport()
        {
            CARESUser user;

            if (HttpContext.Current.Session["User"] != null)
            {
                user = HttpContext.Current.Session["User"] as CARESUser;

                if (user.UserType.Equals("T"))
                    ConnectionString = ConfigurationManager.ConnectionStrings[defaultConnection].ConnectionString;
                else
                {

                    try
                    {
                        ConnectionString = ConfigurationManager.ConnectionStrings[user.Username].ConnectionString;
                    }
                    catch
                    {
                        ConnectionString = ConfigurationManager.ConnectionStrings[defaultConnection].ConnectionString;
                    }
                }
            }
            else
            {
                ConnectionString = ConfigurationManager.ConnectionStrings[defaultConnection].ConnectionString;
            }
        }

        /// <summary>
        /// Returns the connection string from the config with the specified name.
        /// </summary>
        /// <param name="connectionName">Name of connection string in config file, e.g. dbConnectionString</param>
        /// <returns></returns>
        protected string GetConnectionString(string connectionName)
        {

            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        /// <summary>
        /// An array of SqlParameters. Leave null if no parameters.
        /// </summary>
        public MySqlParameter[] Parameters { get; set; }

        /// <summary>
        /// ConnectionString. Set using GetConnectionString().
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// SqlCommand CommandText property
        /// </summary>
        public string CommandText { get; set; }

        /// <summary>
        /// SqlCommand CommandType. Should always be set.
        /// </summary>
        public CommandType CommandType { get; set; }

        /// <summary>
        /// The number of rows affected when calling ExecuteNonQuery or ExecuteAdapter
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// The number in seconds to give the command before timing out.
        /// </summary>
        public int? Timeout { get; set; }
    }
}