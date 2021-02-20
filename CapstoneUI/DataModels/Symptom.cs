using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneUI.DataModels
{
    public class Symptom
    {
        public int SymptomID { get; set; }
        public bool IsCritical { get; set; }
        public string Name { get; set; }

    }
}