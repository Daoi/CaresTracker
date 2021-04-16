using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CaresTracker.DataModels
{
    public class ChronicIllness
    {
        public int ChronicIllnessID { get; set; }
        public string ChronicIllnessName { get; set; }

        public ChronicIllness() { }

        public ChronicIllness(int id)
        {
            ChronicIllnessID = id;
        }

        public ChronicIllness(int id, string name)
        {
            ChronicIllnessID = id;
            ChronicIllnessName = name;
        }

        public ChronicIllness(DataRow dr)
        {
            ChronicIllnessID = int.Parse(dr["ChronicIllnessID"].ToString());
            ChronicIllnessName = dr["ChronicIllnessName"].ToString();
        }

        /// <summary>
        /// Create a list of ChronicIllness objects from a ChronicIllness DataTable
        /// </summary>
        /// <param name="dt">Data table queried from the ChronicIllness table</param>
        /// <returns></returns>
        public static List<ChronicIllness> CreateChronicIllnessList(DataTable dt)
        {
            return dt.Rows.OfType<DataRow>()
                .Select(dr => new ChronicIllness(dr))
                .ToList();
        }
    }
}