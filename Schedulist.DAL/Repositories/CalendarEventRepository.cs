using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;
using Schedulist.App.Exceptions;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;


namespace Schedulist.DAL.Repositories
{
    public class CalendarEventRepository : BaseRepository, ICalendarEventRepository
    {
        public CalendarEventRepository(SchedulistDbContext db, ILogger<BaseRepository> logger) : base(db, logger)
        {

        }

        public List<CalendarEvent> GetAllCalendarEvents()
        {
            return _db.CalendarEvents.Include(e => e.User).ToList();
        }
        public CalendarEvent GetCalendarEventById(int id)
        {
            var calendarEvent = _db.CalendarEvents.FirstOrDefault(c => c.Id == id);
            if (calendarEvent == null) throw new NotFoundException("Calendar Event not found!");
            return calendarEvent;
        }
        public bool CreateCalendarEvent(CalendarEvent calendarEvent)
        {
            _db.CalendarEvents.Add(calendarEvent);
            _db.SaveChanges();
            return true;
        }
        public void UpdateCalendarEvent(int id, CalendarEvent updatedCalendarEvent)
        {
            var calendarEvent = _db.CalendarEvents.FirstOrDefault(r => r.Id == id);
            if (calendarEvent == null) throw new NotFoundException("Calendar Event not found!");
            calendarEvent.CalendarEventDate = updatedCalendarEvent.CalendarEventDate;
            calendarEvent.CalendarEventDescription = updatedCalendarEvent.CalendarEventDescription;
            calendarEvent.CalendarEventStartTime = updatedCalendarEvent.CalendarEventStartTime;
            calendarEvent.CalendarEventEndTime = updatedCalendarEvent.CalendarEventEndTime;
            calendarEvent.UserId = updatedCalendarEvent.UserId;
            calendarEvent.CalendarEventName = updatedCalendarEvent.CalendarEventName;
            _db.SaveChanges();
        }
        public bool DeleteCalendarEvent(CalendarEvent calendarEvent)
        {
            _db.CalendarEvents.Remove(calendarEvent);
            _db.SaveChanges();
            return true;
        }
        public ValidationResult CalendarEventOverlappingValidation(CalendarEvent calendarEvent)
        {
            List<CalendarEvent> allCalendarEvents = GetAllCalendarEvents();
            var providedStartTime = allCalendarEvents.FirstOrDefault(c => c.UserId == calendarEvent.UserId && c.Id != calendarEvent.Id &&
                                    c.CalendarEventDate == calendarEvent.CalendarEventDate && ((calendarEvent.CalendarEventStartTime >= c.CalendarEventStartTime && calendarEvent.CalendarEventStartTime < c.CalendarEventEndTime)
                                    || (c.CalendarEventStartTime > calendarEvent.CalendarEventStartTime && c.CalendarEventStartTime < calendarEvent.CalendarEventEndTime)));

            if (providedStartTime != null)
            {
                return new ValidationResult("There is already a Calendar Event with the provided Start time or that takes place at the same time. Please provide different values.");
            }
            return ValidationResult.Success;
        }

        public ValidationResult CalendarEventTimesValidation(CalendarEvent calendarEvent)
        {
            if (calendarEvent.CalendarEventStartTime >= calendarEvent.CalendarEventEndTime)
            {
                return new ValidationResult("Start Time cannot be later or the same time as End Time!");
            }
            return ValidationResult.Success;
        }
    }
}
