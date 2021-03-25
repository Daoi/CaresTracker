using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

namespace CapstoneUI.DataAccess.DataAccessors
{
    public class GetDevelopmentByID : DataSupport, IData
    {

        public GetDevelopmentByID()
        {
            CommandText = "GetDevelopmentByID";
            CommandType = CommandType.StoredProcedure;
        }
        /// <summary>
        /// Get the house with the provided ID
        /// </summary>
        /// <param name="developmentID">ID(pk) of the development</param>
        /// <returns></returns>
        public DataTable RunCommand(int developmentID)
        {
            Parameters = new MySqlParameter[1] { new MySqlParameter("DevelopmentID", developmentID) };
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }



    }
}