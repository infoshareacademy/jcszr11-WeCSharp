using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.DAL.Models
{
    public class ManageCalendarEventDto
    {
        [Display(Name = "Calendar Event ID")]
        public int Id { get; set; }
        [Display(Name = "Calendar Event Name")]
        [Required]
        public string CalendarEventName { get; set; }
        [Display(Name = "Description")]
        [Required]
        public string CalendarEventDescription { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Required]
        public DateOnly CalendarEventDate { get; set; }
        [Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        [Required]
        public TimeOnly CalendarEventStartTime { get; set; }
        [Display(Name = "End Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        [Required]
        public TimeOnly CalendarEventEndTime { get; set; }

        [Display(Name = "Assigned to user with ID")]
        public User User { get; set; }
    }
}
