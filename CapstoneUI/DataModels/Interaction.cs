﻿using CapstoneUI.DataAccess;
using CapstoneUI.DataAccess.DataAccessors;
using System;
using System.Collections.Generic;
using System.Data;
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
        public string SymptomStartDate { get; set; }
        public List<Symptom> Symptoms { get; set; }
        public List<Service> RequestedServices { get; set; }
        public List<Service> CompletedServices { get; set; }

        public Interaction() { }

        public Interaction(DataRow dr)
        {
            InteractionID = int.Parse(dr["InteractionID"].ToString());
            ResidentID = int.Parse(dr["ResidentID"].ToString());
            HealthWorkerID = int.Parse(dr["HealthWorkerID"].ToString());
            DateOfContact = dr["DateOfContact"].ToString();
            MethodOfContact = dr["MethodOfContact"].ToString();
            LocationOfContact = dr["LocationOfContact"].ToString();
            COVIDTestLocation = dr["COVIDTestLocation"].ToString();
            COVIDTestResult = dr["COVIDTestResult"].ToString();
            ActionPlan = dr["ActionPlan"].ToString();
            SymptomStartDate = dr["SymptomStartDate"].ToString();
            try
            {
                DateTime.Parse(SymptomStartDate);
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Invalid date format for interaction ID: {InteractionID}");
                SymptomStartDate = DateTime.Today.ToString();
            }
            //Create Symptom list from DataTable of Symptoms associated with interaction ID
            Symptoms = Symptom.CreateSymptomList(new GetSymptomsByInteractionID().RunCommand(InteractionID));

            var services = Service.CreateServiceLists(new GetServicesByInteractionID().RunCommand(InteractionID));
            RequestedServices = services.requested;
            CompletedServices = services.completed;
        }

    }
}