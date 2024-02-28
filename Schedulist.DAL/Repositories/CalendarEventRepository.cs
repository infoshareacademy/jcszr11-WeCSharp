using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;
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
        public PagedResult<CalendarEvent> GetAll(CalendarEventQuery query) //TODO powinno być CalendarEventDto
        {
            var baseQuery = _db.CalendarEvents
                .Where(r => query.SearchPhrase == null
                || (r.CalendarEventName.ToLower().Contains(query.SearchPhrase.ToLower())
                || r.CalendarEventDescription.ToLower().Contains(query.SearchPhrase.ToLower())));

            var calendarEvents = baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();

            var totalItemsCount = baseQuery.Count();

            //var calendarEventDto = _mapper.Map<List<CalendarEventDto>>(calendarEvents);

            var result = new PagedResult<CalendarEvent>(calendarEvents, totalItemsCount, query.PageSize, query.PageNumber); //TODO powinno być <CalendarEventDto> i calendarEventDto
            return result;
        }
    }
}
