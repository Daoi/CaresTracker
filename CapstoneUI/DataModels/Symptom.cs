using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CapstoneUI.DataModels
{
    public class Symptom
    {
        public int SymptomID { get; set; }
        public bool IsCritical { get; set; }
        public string SymptomName { get; set; }

        public Symptom(){}

        public Symptom(DataRow dr)
        {
            SymptomID = int.Parse(dr["SymptomID"].ToString());
            IsCritical = (bool)dr["IsCritical"];
            SymptomName = dr["SymptomName"].ToString();
        }

        /// <summary>
        /// Create a list of Symptom objects from a Symptom DataTable
        /// </summary>
        /// <param name="dt">Data table queried from the SymptomTable</param>
        /// <returns></returns>
        public static List<Symptom> CreateSymptomList(DataTable dt)
        {
            return dt.Rows.OfType<DataRow>()
                .Select(dr => new Symptom(dr))
                .ToList();

        }

        public Symptom(string name, int id)
        {
            SymptomName = name;
            SymptomID = id;
        }


    }
}