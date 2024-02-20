using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;


namespace Schedulist.DAL.Repositories
{
    public class CalendarEventRepository : BaseRepository, ICalendarEventRepository
    {
        public CalendarEventRepository(SchedulistDbContext db, ILogger<BaseRepository> logger) : base (db, logger)
        {

        }

        public List<CalendarEvent> GetAllCalendarEvents()
        {
            try
            {
                return _db.CalendarEvents.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving Calendar Events from the database.");
                return new List<CalendarEvent>();
            }
        }
        public CalendarEvent GetCalendarEventById(int id)
        {
            try
            {
                return _db.CalendarEvents.FirstOrDefault(c => c.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving Calendar Event from the database.");
                return new CalendarEvent();
            }
        }
        public bool CreateCalendarEvent(CalendarEvent calendarEvent)
        {
            try
            {
                _db.CalendarEvents.Add(calendarEvent);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving Calendar Event to database.");
                return false;
            }
        }
        public bool UpdateCalendarEvent(CalendarEvent calendarEvent)
        {
            try
            {
                
                _db.CalendarEvents.Update(calendarEvent);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating Calendar Event in database.");
                return false;
            }
        }
        public bool DeleteCalendarEvent(CalendarEvent calendarEvent)
        {
            try
            {
                _db.CalendarEvents.Remove(calendarEvent);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting Calendar Event in database.");
                return false;
            }
        }
        public ValidationResult CalendarEventOverlappingValidation(DateOnly calendarEventDate, TimeOnly calendarEventStartTime, TimeOnly calendarEventEndTime, int userId)
        {
            List<CalendarEvent> allCalendarEvents = GetAllCalendarEvents();
            var providedStartTime = allCalendarEvents.FirstOrDefault(c => c.UserId == userId &&
                                    c.CalendarEventDate == calendarEventDate && ((calendarEventStartTime >= c.CalendarEventStartTime && calendarEventStartTime < c.CalendarEventEndTime)
                                    || (c.CalendarEventStartTime > calendarEventStartTime && c.CalendarEventStartTime < calendarEventEndTime)));

            if (providedStartTime != null)
            {
                return new ValidationResult("There is already a Calendar Event with the provided Start time or that takes place at the same time. Please provide different values.");
            }
            return ValidationResult.Success;
        }

        public ValidationResult CalendarEventTimesValidation(TimeOnly calendarEventStartTime, TimeOnly calendarEventEndTime)
        {
            if (calendarEventStartTime >= calendarEventEndTime)
            {
                return new ValidationResult("Start Time cannot be later or the same time as End Time!");
            }
            return ValidationResult.Success;
        }
    }
}
