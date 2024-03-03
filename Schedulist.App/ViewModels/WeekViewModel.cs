using Microsoft.AspNetCore.Mvc.Rendering;
using Schedulist.DAL.Models;

namespace Schedulist.App.ViewModels
{
    public class WeekViewModel
    {
        public DateOnly Date { get; set; }
        public User User { get; set; }
        public string WorkMode { get; set; }
        public List<CalendarEvent> CalendarEvents { get; set; }
        public List<SelectListItem> GetAllWorkModeNames { get; set; }
        public WeekViewModel(DateOnly date, User user, string workMode, List<CalendarEvent> calendarEvents)
        {
            Date = date;
            User = user;
            WorkMode = workMode;
            CalendarEvents = calendarEvents;
        }
    }
}