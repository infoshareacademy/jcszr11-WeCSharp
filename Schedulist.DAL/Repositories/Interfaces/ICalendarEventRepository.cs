using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedulist.DAL.Models;

namespace Schedulist.DAL.Repositories.Interfaces
{
    public interface ICalendarEventRepository
    {
        public List<CalendarEvent> GetAllCalendarEvents();
        public CalendarEvent GetCalendarEventById(int id);
        public bool CreateCalendarEvent(CalendarEvent calendarEvent);
        public bool UpdateCalendarEvent(CalendarEvent calendarEvent);
        public bool DeleteCalendarEvent(CalendarEvent calendarEvent);
        public ValidationResult CalendarEventOverlappingValidation(DateOnly calendarEventDate, TimeOnly calendarEventStartTime, TimeOnly calendarEventEndTime, int userId, int calendarEventId);
        public ValidationResult CalendarEventTimesValidation(TimeOnly calendarEventStartTime, TimeOnly calendarEventEndTime);
    }
}
