using Schedulist.DAL.Models;

namespace Schedulist.App.Models
{
    public class MonthViewModel
    {
        public DateTime CurrentDate { get; set; }
        public DateTime FirstDayOfTheMonth { get; private set; }
        public DateTime LastDayOfTheMonth { get; private set; }
        public int DaysToDraw { get; private set; }
        public DateTime StartDate { get; private set; }
        public List<CalendarEvent> CalendarEvents { get; set; }
        public MonthViewModel(List<CalendarEvent> calendarEvents)
        {
            CurrentDate = DateTime.Now;
            FirstDayOfTheMonth = new DateTime(CurrentDate.Year, CurrentDate.Month, 1);
            LastDayOfTheMonth = FirstDayOfTheMonth.AddMonths(1).AddDays(-1);
            DaysToDraw = 42;
            if (LastDayOfTheMonth.DayOfWeek == DayOfWeek.Sunday) DaysToDraw -= 7;
            StartDate = FirstDayOfTheMonth.AddDays(-(int)FirstDayOfTheMonth.DayOfWeek + 1);
            CalendarEvents = calendarEvents;

        }
        public MonthViewModel(DateTime date, List<CalendarEvent> calendarEvents)
        {
            CurrentDate = date;
            FirstDayOfTheMonth = new DateTime(CurrentDate.Year, CurrentDate.Month, 1);
            LastDayOfTheMonth = FirstDayOfTheMonth.AddMonths(1).AddDays(-1);
            if (LastDayOfTheMonth.DayOfWeek == DayOfWeek.Sunday || FirstDayOfTheMonth.DayOfWeek == DayOfWeek.Monday) DaysToDraw -= 7;
            if (FirstDayOfTheMonth.DayOfWeek != DayOfWeek.Sunday)
                StartDate = FirstDayOfTheMonth.AddDays(-(int)FirstDayOfTheMonth.DayOfWeek + 1);
            else StartDate = FirstDayOfTheMonth.AddDays(-(int)FirstDayOfTheMonth.DayOfWeek - 6);
            CalendarEvents = calendarEvents;
        }
    }
}
