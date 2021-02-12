using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CapstoneUI.DataAccess.DataAccessors.Examples
{
    public class CHWParameters
    {
        private SqlParameter[] Parameters;

        public CHWParameters()
        {
            Parameters = new SqlParameter[]
            {
                new SqlParameter("@theUserName", SqlDbType.VarChar, 50),
                new SqlParameter("@thePassword", SqlDbType.VarChar, 50),
                new SqlParameter("@theFirstName", SqlDbType.VarChar, 50),
                new SqlParameter("@theLastName", SqlDbType.VarChar, 50),
                new SqlParameter("@theUserEmail", SqlDbType.VarChar, 50),
                new SqlParameter("@theUserPhoneNumber", SqlDbType.VarChar, 50)

            };
        }

        public SqlParameter[] Fill(List<string> values)
        {
            if(values.Count > Parameters.Length)
            {
                throw new IndexOutOfRangeException($"More parameters than allowed. Provided: {values.Count} Allowed: {Parameters.Length}");
          
            }

            for(int i = 0; i < Parameters.Length; i++)
            {
                Parameters[i].Value = values[i];
            }

            return Parameters;

        }
    }
}