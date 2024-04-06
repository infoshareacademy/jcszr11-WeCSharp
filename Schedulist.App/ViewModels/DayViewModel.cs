using Microsoft.AspNetCore.Mvc.Rendering;
using Schedulist.DAL.Models;

namespace Schedulist.App.ViewModels
{
    public class DayViewModel
    {
        public DateOnly Date { get; set; }
        public User User { get; set; }
        public WorkModeForUser WorkModeForUser { get; set; }
        public List<CalendarEvent> CalendarEvents { get; set; }
        public List<SelectListItem> GetAllWorkModeNames { get; set; }
        public List<WorkMode> WorkModes { get; set; }
        public string CurrentUrl { get; set; }
        public DayViewModel(DateOnly date, User user, WorkModeForUser workModeForUser, List<WorkMode> workModes, List<CalendarEvent> calendarEvents, string currentUrl)
        {
            Date = date;
            User = user;
            WorkModeForUser = workModeForUser;
            WorkModes = workModes;
            CalendarEvents = calendarEvents;
            CurrentUrl = currentUrl;
        }
    }
}