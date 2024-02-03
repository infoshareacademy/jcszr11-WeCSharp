using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace Schedulist.DAL.Models
{
    public class CalendarEvent : IValidatableObject
    {
        [Name("calendarEventId")]
        [Display(Name = "Calendar Event ID")]
        public int CalendarEventId { get; set; }
        [Name("calendarEventName")]
        [Display(Name = "Calendar Event Name")]
        [Required]
        public string CalendarEventName { get; set; }
        [Name("calendarEventDescription")]
        [Display(Name = "Description")]
        [Required]
        public string CalendarEventDescription { get; set; }
        [Name("calendarEventDate")]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Required]
        public DateOnly CalendarEventDate { get; set; }
        [Name("calendarEventStartTime")]
        [Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        [Required]
        public TimeOnly CalendarEventStartTime { get; set; }
        [Name("calendarEventEndTime")]
        [Display(Name = "End Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        [Required]
        public TimeOnly CalendarEventEndTime { get; set; }
        [Name("userID")]
        [Display(Name = "Assigned to user with ID")]
        public int AssignedToUser { get; set; }
        public User User { get; set; }

        public CalendarEvent()
        {

        }
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CalendarEventEndTime <= CalendarEventStartTime)
            {
                yield return new ValidationResult("End Time cannot be earlier or the same time as Start Time of Calendar Event!", new[] { "CalendarEventEndTime" });
            }

        }
    }
}
