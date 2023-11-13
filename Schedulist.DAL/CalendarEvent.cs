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
        [Name("calendarEventId")]
        public int CalendarEventId { get; set; }
        [Name("calendarEventName")]
        public string CalendarEventName { get; set; }
        [Name("calendarEventDescription")]
        public string CalendarEventDescription { get; set; }
        [Name("calendarEventDate")]
        public DateOnly CalendarEventDate { get; set; }
        [Name("calendarEventStartTime")]
        public TimeOnly CalendarEventStartTime { get; set; }
        [Name("calendarEventEndTime")]
        public TimeOnly CalendarEventEndTime { get; set; }
        [Name("userID")]
        public int AssignedToUser { get; set; } 

        public CalendarEvent(int calendarEventId, string calendarEventName, string calendarEventDescription, DateOnly calendarEventDate,
            TimeOnly calendarEventStartTime, TimeOnly calendarEventEndTime, int userID)
        {
            CalendarEventId = calendarEventId;
            CalendarEventName = calendarEventName;
            CalendarEventDescription = calendarEventDescription;
            CalendarEventDate = calendarEventDate;
            CalendarEventStartTime = calendarEventStartTime;
            CalendarEventEndTime = calendarEventEndTime;
            AssignedToUser = userID;
        }
    }
}
