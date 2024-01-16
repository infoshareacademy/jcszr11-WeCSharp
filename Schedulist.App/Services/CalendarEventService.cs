using Schedulist.Business.Actions;
using Schedulist.DAL;

namespace Schedulist.App.Services
{
    public class CalendarEventService
    {
        private readonly CsvCalendarEventRepository _repository;
        public List<CalendarEvent> ShowUserCalendarEvent(User user, DateOnly specifiedDate)
        {
            var calendarEvents = _repository.GetAllCalendarEvents();
            var userCalendarEvents = calendarEvents
                .Where(c => c.AssignedToUser == user.Id && c.CalendarEventDate == specifiedDate)
                .OrderBy(c => c.CalendarEventStartTime)
                .ToList();
            return userCalendarEvents;
        }

        public CalendarEvent Create(CalendarEvent calendarEvent)
        {
            //var calendarEvents = _repository.GetAllCalendarEvents();
            _repository.AddCalendarEvent(calendarEvent);
            return calendarEvent;
        }
    }
}
