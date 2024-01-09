using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace Schedulist.DAL
{
    public class CalendarEvent
    {
        [Name("calendarEventId")]
        [Display(Name = "Calendar Event ID")]
        public int CalendarEventId { get; set; }
        [Name("calendarEventName")]
        [Display(Name = "Calendar Event Name")]
        public string CalendarEventName { get; set; }
        [Name("calendarEventDescription")]
        [Display(Name = "Description")]
        public string CalendarEventDescription { get; set; }
        [Name("calendarEventDate")]
        [Display(Name = "Date")]
        public DateOnly CalendarEventDate { get; set; }
        [Name("calendarEventStartTime")]
        [Display(Name = "Start Time")]
        public TimeOnly CalendarEventStartTime { get; set; }
        [Name("calendarEventEndTime")]
        [Display(Name = "End Time")]
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
