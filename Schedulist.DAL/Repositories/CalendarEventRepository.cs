using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories.Interfaces;


namespace Schedulist.DAL.Repositories
{
    public class CalendarEventRepository : BaseRepository, ICalendarEventRepository
    {
        public CalendarEventRepository(DBContact db, ILogger<BaseRepository> logger) : base (db, logger)
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
                return new CalendarEvent;
            }
        }
        public bool SaveCalendarEvent(CalendarEvent calendarEvent)
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
    }
}
