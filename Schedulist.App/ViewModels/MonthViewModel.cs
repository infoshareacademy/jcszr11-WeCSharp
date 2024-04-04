using CsvHelper.Configuration.Attributes;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Schedulist.App.ViewModels
{
    public class MonthViewModel
    {
        public DateTime CurrentDate { get; set; }
        public DateTime FirstDayOfTheMonth { get; private set; }
        public DateTime LastDayOfTheMonth { get; private set; }
        public int DaysToDraw { get; private set; }
        public DateTime StartDate { get; private set; }
        public List<CalendarEvent> CalendarEvents { get; set; }
        public string NewCalendarEventName { get; set; }
        [Name("CalendarEventDescription")]
        [Display(Name = "Description")]
        [Required]
        public string NewCalendarEventDescription { get; set; }
        [Name("CalendarEventStartTime")]
        [Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        [Required]
        public TimeOnly NewCalendarEventStartTime { get; set; }
        [Name("CalendarEventEndTime")]
        [Display(Name = "End Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        [Required]
        public TimeOnly NewCalendarEventEndTime { get; set; }

        public Dictionary<string, string> UserDict { get; set; }

        public string UserToEdit;

        public string UserNameDisplay;

        public IWorkModeRepository WorkMode;

        public List<WorkModeForUser> WorkModesToDraw;
        public MonthViewModel(List<CalendarEvent> calendarEvents, Dictionary<string, string> userDict, string userToEdit, string userNameDisplay, IWorkModeRepository workMode, List<WorkModeForUser> workModesToDraw)
        {
            CurrentDate = DateTime.Now;
            FirstDayOfTheMonth = new DateTime(CurrentDate.Year, CurrentDate.Month, 1);
            LastDayOfTheMonth = FirstDayOfTheMonth.AddMonths(1).AddDays(-1);
            DaysToDraw = 42;
            if (LastDayOfTheMonth.DayOfWeek == DayOfWeek.Sunday) DaysToDraw -= 7;
            StartDate = FirstDayOfTheMonth.AddDays(-(int)FirstDayOfTheMonth.DayOfWeek + 1);
            CalendarEvents = calendarEvents;
            UserDict = userDict;
            UserToEdit = userToEdit;
            UserNameDisplay = userNameDisplay;
            WorkMode = workMode;
            WorkModesToDraw = workModesToDraw;
            
    }
        public MonthViewModel(DateTime date, List<CalendarEvent> calendarEvents, Dictionary<string, string> userDict, string userToEdit, IWorkModeRepository workMode, List<WorkModeForUser> workModesToDraw, string userNameDisplay)
        {
            CurrentDate = date;
            FirstDayOfTheMonth = new DateTime(CurrentDate.Year, CurrentDate.Month, 1);
            LastDayOfTheMonth = FirstDayOfTheMonth.AddMonths(1).AddDays(-1);
            if (LastDayOfTheMonth.DayOfWeek == DayOfWeek.Sunday || FirstDayOfTheMonth.DayOfWeek == DayOfWeek.Monday) DaysToDraw -= 7;
            if (FirstDayOfTheMonth.DayOfWeek != DayOfWeek.Sunday)
                StartDate = FirstDayOfTheMonth.AddDays(-(int)FirstDayOfTheMonth.DayOfWeek + 1);
            else StartDate = FirstDayOfTheMonth.AddDays(-(int)FirstDayOfTheMonth.DayOfWeek - 6);
            CalendarEvents = calendarEvents;
            UserDict = userDict;
            UserToEdit = userToEdit;           
            WorkMode = workMode;
            WorkModesToDraw = workModesToDraw;
            UserNameDisplay = userNameDisplay;
        }
    }
}
