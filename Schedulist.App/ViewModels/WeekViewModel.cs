using Microsoft.AspNetCore.Mvc.Rendering;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories.Interfaces;

namespace Schedulist.App.ViewModels
{
    public class WeekViewModel
    {
        public DateOnly StartOfWeek { get; set; }
        public DateOnly EndOfWeek { get; set; }
        public User User { get; set; }
        public List<WorkModeForUser> WorkModesForUser { get; set; }
        public List<CalendarEvent> CalendarEvents { get; set; }
        public List<SelectListItem> GetAllWorkModeNames { get; set; }
        public List<WorkMode> WorkModes { get; set; }
        public WeekViewModel(DateOnly date, User user, List<WorkModeForUser> workModesForUser, List<WorkMode> workModes, List<CalendarEvent> calendarEvents)
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
            WorkModesForUser = workModesForUser;
            WorkModes = workModes;
            CalendarEvents = calendarEvents;
        }
    }
}