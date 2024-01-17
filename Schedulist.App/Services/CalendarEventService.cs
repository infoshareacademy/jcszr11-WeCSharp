using Schedulist.Business.Actions;
using Schedulist.DAL;

namespace Schedulist.App.Services
{
    public class CalendarEventService
    {
        private readonly CsvCalendarEventRepository _repository;
        public CalendarEvent GetCalendarEventById(int id)
        {
            CsvCalendarEventRepository repository = new CsvCalendarEventRepository("..\\Schedulist\\CalendarEvents.csv");
            var calendarEvents = repository.GetAllCalendarEvents();
            var calendarEventsById = calendarEvents
                .FirstOrDefault(c => c.CalendarEventId == id);
            return calendarEventsById;
        }
        public CalendarEvent Create(CalendarEvent calendarEvent)
        {
            //var calendarEvents = _repository.GetAllCalendarEvents();
            CsvCalendarEventRepository repository = new CsvCalendarEventRepository("..\\Schedulist\\CalendarEvents.csv");
            repository.AddCalendarEvent(calendarEvent);
            return calendarEvent;
        }
        public int Delete(int id)
        {
            CsvCalendarEventRepository repository = new CsvCalendarEventRepository("..\\Schedulist\\CalendarEvents.csv");
            repository.DeleteCalendarEvent(id);
            return id;
        }
        public CalendarEvent Edit(CalendarEvent calendarEvent)
        {
            CsvCalendarEventRepository repository = new CsvCalendarEventRepository("..\\Schedulist\\CalendarEvents.csv");

            var newCalendarEvent = GetCalendarEventById(calendarEvent.CalendarEventId);

            newCalendarEvent.CalendarEventDate = calendarEvent.CalendarEventDate;
            newCalendarEvent.CalendarEventDescription = calendarEvent.CalendarEventDescription;
            newCalendarEvent.CalendarEventName = calendarEvent.CalendarEventName;
            newCalendarEvent.CalendarEventStartTime = calendarEvent.CalendarEventStartTime;
            newCalendarEvent.CalendarEventEndTime = calendarEvent.CalendarEventEndTime;

            repository.ModifyCalendarEvent(newCalendarEvent);
            return calendarEvent;
        }
    }
}
