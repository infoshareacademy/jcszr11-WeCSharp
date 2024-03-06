using Schedulist.App.Controllers;
using Schedulist.App.Services.Interfaces;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Schedulist.App.Services
{
    public class CalendarEventService : ControllerBase, ICalendarEventService
    {
        private readonly ICalendarEventRepository _calendarEventRepository;
        public CalendarEventService(ILogger<CalendarEventService> logger, ICalendarEventRepository calendarEventRepository) : base(logger)
        {
            _calendarEventRepository = calendarEventRepository;
        }
        public List<ValidationResult> ValidateCalendarEvent(CalendarEvent calendarEvent)
        {
            var timeValidationResult = _calendarEventRepository.CalendarEventTimesValidation(calendarEvent);
            var validationResult = _calendarEventRepository.CalendarEventOverlappingValidation(calendarEvent);

            return new List<ValidationResult> {
                timeValidationResult,
                validationResult
            };

        }

        public List<ValidationResult> ValidateCalendarEvent(int id)
        {
            throw new NotImplementedException();
        }
    }
}
