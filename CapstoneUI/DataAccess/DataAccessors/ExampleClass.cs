using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace CapstoneUI.DataAccess.DataAccessors
{
    public class ExampleClass : DataSupport, IData
    {
        public ExampleClass()
        {
            ConnectionString = GetConnectionString("dbConnectionString1");
            CommandText = "GetUserExample";
            CommandType = CommandType.StoredProcedure;
        }

        public DataTable LookUpUser(int age, bool testResult)
        {
            Parameters = new SqlParameter[2] { new SqlParameter("UserAge", age), new SqlParameter("TestResult", testResult) };
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }

    }
}