using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.DAL
{
    public class CalendarEvent
    {
        public int CalendarEventId { get; set; }
        public string CalendarEventName { get; set; }
        public string CalendarEventDescription { get; set; }
        public DateTime CalendarEventStartDateTime { get; set; }
        public DateTime CalendarEventEndDateTime { get; set; }
        public CalendarEvent(int calendarEventId,  string calendarEventName, string calendarEventDescription, DateTime calendarEventStartDateTime, DateTime calendarEventEndDateTime)
        {
            CalendarEventId = calendarEventId;
            CalendarEventName = calendarEventName;
            CalendarEventDescription = calendarEventDescription;
            CalendarEventStartDateTime = calendarEventStartDateTime;
            CalendarEventEndDateTime = calendarEventEndDateTime;
        }

    }
}
