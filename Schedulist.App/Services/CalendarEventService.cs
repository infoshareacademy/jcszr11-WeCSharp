using Schedulist.App.Controllers;
using Schedulist.App.Services.Interfaces;
using Schedulist.App.ViewModels;
using Schedulist.DAL;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Schedulist.App.Services
{
    public class CalendarEventService : ControllerBase, ICalendarEventService
    {
        private readonly ICalendarEventRepository _calendarEventRepository;
        private readonly SchedulistDbContext _db;
        public CalendarEventService(ILogger<CalendarEventService> logger, ICalendarEventRepository calendarEventRepository, SchedulistDbContext db) : base(logger)
        {
            _calendarEventRepository = calendarEventRepository;
            _db = db;
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
        public CalendarEventViewModel GetAll(CalendarEventQuery query) //TODO powinno być CalendarEventDto
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

            var viewModel = new CalendarEventViewModel
            {
                CalendarEvents = calendarEvents,
                TotalItems = totalItemsCount,
                PageSize = query.PageSize,
                PageNumber = query.PageNumber,
            };

            //var calendarEventDto = _mapper.Map<List<CalendarEventDto>>(calendarEvents);

            /*var result = new PagedResult<CalendarEvent>(calendarEvents, totalItemsCount, query.PageSize, query.PageNumber);*/ //TODO powinno być <CalendarEventDto> i calendarEventDto
            return viewModel;
        }
    }

    
}
