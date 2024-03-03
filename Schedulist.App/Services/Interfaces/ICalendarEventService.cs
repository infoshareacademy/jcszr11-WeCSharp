using Schedulist.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Schedulist.App.Services.Interfaces
{
    public interface ICalendarEventService
    {
        public List<ValidationResult> ValidateCalendarEvent(CalendarEvent calendarEvent);
    }
}
