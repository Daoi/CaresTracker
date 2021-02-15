﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneUI.DataAccess
{
    public class ExecuteQuery : ExecuteBase
    {
        /// <summary>
        /// Executes sql statement, use for commands that should return a data table(multiple rows).
        /// </summary>
        /// <param name="cmdInfo">The class that implements IData used to construct the SQL command</param>
        /// <returns>DataTable</returns>    
        public DataTable ExecuteAdapter(IData cmdInfo)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(cmdInfo.ConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand(cmdInfo.CommandText, cn))
                {
                    ConfigureCommand(cmd, cmdInfo);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cmdInfo.Count = da.Fill(dt);
                    }
                    cmd.Parameters.Clear();
                }
            }
            return dt;
        }

        /// <summary>
        /// Executes sql statement, use for commands that write to database
        /// </summary>
        /// <param name="cmdInfo">The class that implements IData used to construct the SQL command</param>
        /// <returns>Count of rows affected</returns>
        public int ExecuteNonQuery(IData cmdInfo)
        {
            using (SqlConnection cn = new SqlConnection(cmdInfo.ConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand(cmdInfo.CommandText, cn))
                {
                    ConfigureCommand(cmd, cmdInfo);

                    cmdInfo.Count = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }
            return cmdInfo.Count;
        }
        /// <summary>
        /// Execute a sql statement, use for commands that expect a single sql object returned
        /// </summary>
        /// <param name="cmdInfo">The class that implements IData used to construct the SQL command</param>
        /// <returns>The first column of the first row.</returns>
        public object ExecuteScalar(IData cmdInfo)
        {
            object dataObj = null;
            using (SqlConnection cn = new SqlConnection(cmdInfo.ConnectionString))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand(cmdInfo.CommandText, cn))
                {
                    ConfigureCommand(cmd, cmdInfo);

                    dataObj = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                }
            }
            return dataObj;
        }

    }
    /// <summary>
    /// Contains methods to support the ExecuteQuery class.
    /// </summary>
    public class ExecuteBase
    {
        /// <summary>
        /// Configures SqlCommand objects.
        /// </summary>
        /// <param name="cmd">The SQL Command to be run</param>
        /// <param name="cmdInfo">The class that implements IData used to construct the SQL command</param>
        protected void ConfigureCommand(SqlCommand cmd, IData cmdInfo)
        {
            if (cmdInfo.Timeout.HasValue)
            {
                cmd.CommandTimeout = cmdInfo.Timeout.Value;
            }
            else    // use default of 30 seconds
            {
                cmd.CommandTimeout = 30;
            }
            if (cmdInfo.Parameters != null)
            {
                cmd.Parameters.AddRange(cmdInfo.Parameters);
            }
            cmd.CommandType = cmdInfo.CommandType;
        }
    }
}