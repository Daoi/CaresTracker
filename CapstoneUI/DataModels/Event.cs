using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneUI.DataModels
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
        public string EventTopic { get; set; }
        public string EventAttendanceRange { get; set; }
        public List<CARESUser> Hosts { get; set; }
        public List<Resident> Attendees { get; set; }

        public Event() { }
    }
}