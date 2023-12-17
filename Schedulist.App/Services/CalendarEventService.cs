using Schedulist.DAL;

namespace Schedulist.App.Services
{
    public class CalendarEventService
    {
        private readonly CsvCalendarEventRepository repository;
        public List<CalendarEvent> ShowUserCalendarEvent(User user, DateOnly specifiedDate)
        {
            var calendarEvents = repository.GetAllCalendarEvents();
            var userCalendarEvents = calendarEvents
                .Where(c => c.AssignedToUser == user.Id && c.CalendarEventDate == specifiedDate)
                .OrderBy(c => c.CalendarEventStartTime)
                .ToList();
            return userCalendarEvents;
        }
    }
}
