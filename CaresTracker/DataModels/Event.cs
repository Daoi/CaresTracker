using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using CaresTracker.DataAccess.DataAccessors.EventAccessors;

namespace CaresTracker.DataModels
{
    public class Event
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public string EventType { get; set; }
        public string EventLocation { get; set; }
        public string EventDate { get; set; }
        public string EventStartTime { get; set; }
        public string EventEndTime { get; set; }
        public int MainHostID { get; set; }
        public List<CARESUser> Hosts { get; set; }
        public List<Resident> Attendees { get; set; }

        public Event()
        {

        }

        public Event(DataRow dataRow)
        {
            EventID = int.Parse(dataRow["EventID"].ToString());
            EventName = dataRow["EventName"].ToString();
            EventDescription = dataRow["EventDescription"].ToString();
            EventType = dataRow["EventType"].ToString();
            EventLocation = dataRow["EventLocation"].ToString();
            EventDate = dataRow["EventDate"].ToString();
            EventStartTime = dataRow["EventStartTime"].ToString();
            EventEndTime = dataRow["EventEndTime"].ToString();
            MainHostID = int.Parse(dataRow["MainHostID"].ToString());

            // fill lists
            Hosts = CARESUser.CreateEventHostList(new GetEventHosts().ExecuteCommand(EventID));
            Attendees = Resident.CreateEventAttendeeList(new GetEventAttendees().ExecuteCommand(EventID));
        }
    }
}
