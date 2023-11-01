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
        //[Index(1)]
        [Name("calendarEventName")]
        public string CalendarEventName { get; set; }
        //[Index(2)]
        [Name("calendarEventDescription")]
        public string CalendarEventDescription { get; set; }
        //[Index(3)]
        [Name("calendarEventDate")]
        public DateOnly CalendarEventDate { get; set; }
        //[Index(4)]
        [Name("calendarEventStartTime")]
        public TimeOnly CalendarEventStartTime { get; set; }
        //[Index(5)]
        [Name("calendarEventEndTime")]
        public TimeOnly CalendarEventEndTime { get; set; }

        public CalendarEvent(int calendarEventId, string calendarEventName, string calendarEventDescription, DateOnly calendarEventDate,
            TimeOnly calendarEventStartTime, TimeOnly calendarEventEndTime)

        {
            CalendarEventId = calendarEventId;
            CalendarEventName = calendarEventName;
            CalendarEventDescription = calendarEventDescription;
            CalendarEventDate = calendarEventDate;
            CalendarEventStartTime = calendarEventStartTime;
            CalendarEventEndTime = calendarEventEndTime;
        }

    }
}
