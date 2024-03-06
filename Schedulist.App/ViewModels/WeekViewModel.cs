using Microsoft.AspNetCore.Mvc.Rendering;
using Schedulist.DAL.Models;

namespace Schedulist.App.ViewModels
{
    public class WeekViewModel
    {
        public DateOnly StartOfWeek { get; set; }
        public DateOnly EndOfWeek { get; set; }
        public User User { get; set; }
        public string WorkMode { get; set; }
        public List<CalendarEvent> CalendarEvents { get; set; }
        public List<SelectListItem> GetAllWorkModeNames { get; set; }
        public WeekViewModel(DateOnly date, User user, string workMode, List<CalendarEvent> calendarEvents)
        {
            DateOnly startOfWeek = date;
            while (true)
            {
                if (startOfWeek.DayOfWeek == DayOfWeek.Monday)
                {
                    StartOfWeek = startOfWeek;
                    break;
                }
                else
                {
                    startOfWeek = startOfWeek.AddDays(-1);
                }
            }
            DateOnly endOfWeek = date;
            while (true)
            {
                if (endOfWeek.DayOfWeek == DayOfWeek.Sunday)
                {
                    EndOfWeek = endOfWeek;
                    break;
                }
                else
                {
                    endOfWeek = endOfWeek.AddDays(1);
                }
            }
            User = user;
            WorkMode = workMode;
            CalendarEvents = calendarEvents;
        }
    }
}