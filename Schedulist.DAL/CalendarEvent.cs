using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace Schedulist.DAL
{
    public class CalendarEvent
    {
        //[Index(0)]
        [Name("calendarEventId")]
        public int CalendarEventId { get; set; }
       // [Index(1)]
        [Name("calendarEventName")]
        public string CalendarEventName { get; set; }
       // [Index(2)]
        [Name("calendarEventDescription")]
        public string CalendarEventDescription { get; set; }
        //[Index(3)]
        [Name("calendarEventStartDateTime")]
        public DateTime CalendarEventStartDateTime { get; set; }
        //[Index(4)]
        [Name("calendarEventEndDateTime")]
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
