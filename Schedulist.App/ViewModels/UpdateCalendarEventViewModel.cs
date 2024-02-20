using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Schedulist.App.ViewModels
{
    public class UpdateCalendarEventViewModel
    {
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
    }
}
