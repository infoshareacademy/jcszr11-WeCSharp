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
        public ValidationResult CalendarEventOverlappingValidation(CalendarEvent calendarEvent);
        public ValidationResult CalendarEventTimesValidation(CalendarEvent calendarEvent);
    }
}
