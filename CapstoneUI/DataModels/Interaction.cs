using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneUI.DataModels
{
    public class Interaction
    {
        public int InteractionID { get; set; }
        public int ResidentID { get; set; }
        public int HealthWorkerID { get; set; }
        public string DateOfContact { get; set; }
        public string MethodOfContact { get; set; }
        public string LocationOfContact { get; set; }
        public string COVIDTestLocation { get; set; }
        public string COVIDTestResult { get; set; }
        public string ActionPlan { get; set; }
        public List<Symptom> Symptoms { get; set; }
        public List<Service> RequestedServices { get; set; }
        public List<Service> OfferedServices { get; set; }
    }
}