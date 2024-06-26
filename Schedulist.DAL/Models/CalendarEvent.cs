﻿using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration.Attributes;


namespace Schedulist.DAL.Models
{
    public class CalendarEvent 
    {
        [Key]
        [Name("CalendarEventId")]
        [Display(Name = "Calendar Event ID")]
        public int Id { get; set; }
        [Name("calendarEventName")]
        [Display(Name = "Calendar Event Name")]
        [Required]
        public string CalendarEventName { get; set; }
        [Name("CalendarEventDescription")]
        [Display(Name = "Description")]
        [Required]
        public string CalendarEventDescription { get; set; }
        [Name("CalendarEventDate")]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Required]
        public DateOnly CalendarEventDate { get; set; }
        [Name("CalendarEventStartTime")]
        [Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        [Required]
        public TimeOnly CalendarEventStartTime { get; set; }
        [Name("CalendarEventEndTime")]
        [Display(Name = "End Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        [Required]
        public TimeOnly CalendarEventEndTime { get; set; }

        //EntityFramework Configuration Section
        [Name("UserId")]
        [Display(Name = "Assigned to user")]
        public string UserId { get; set; }
        public User User { get; set; }
        //EntityFramework Configuration Section

        public CalendarEvent()
        {

        }
        public CalendarEvent(int calendarEventId, string calendarEventName, string calendarEventDescription, DateOnly calendarEventDate,
            TimeOnly calendarEventStartTime, TimeOnly calendarEventEndTime, string userId)
        {
            Id = calendarEventId;
            CalendarEventName = calendarEventName;
            CalendarEventDescription = calendarEventDescription;
            CalendarEventDate = calendarEventDate;
            CalendarEventStartTime = calendarEventStartTime;
            CalendarEventEndTime = calendarEventEndTime;
            UserId = userId;
        }

    }
}
