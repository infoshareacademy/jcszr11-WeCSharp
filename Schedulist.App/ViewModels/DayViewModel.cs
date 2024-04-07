using Microsoft.AspNetCore.Mvc.Rendering;
using Schedulist.DAL.Models;

namespace Schedulist.App.ViewModels
{
    public class DayViewModel(DateOnly date, User user, WorkModeForUser workModeForUser, List<WorkMode> workModes, List<CalendarEvent> calendarEvents, string currentUrl)
    {
        public DateOnly Date { get; set; } = date;
        public User User { get; set; } = user;
        public WorkModeForUser WorkModeForUser { get; set; } = workModeForUser;
        public List<CalendarEvent> CalendarEvents { get; set; } = calendarEvents;
        public List<SelectListItem> GetAllWorkModeNames { get; set; }
        public List<WorkMode> WorkModes { get; set; } = workModes;
        public string CurrentUrl { get; set; } = currentUrl;
    }
}