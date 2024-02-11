using Microsoft.AspNetCore.Mvc;
using Schedulist.App.Models;
using Schedulist.DAL;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Schedulist.App.Controllers
{
    public class CalendarController : ControlerBase
    {
        int i = 0;
        private User _user;
        private readonly CalendarEvent _newCalendarEvent;
        private MonthViewModel? _monthViewModel;
        private readonly IWorkModeRepository _workModesRepository;
        private readonly ICalendarEventRepository _calendarEventRepository;
        public CalendarController(ILogger<CalendarController> logger, User user, IWorkModeRepository workModesRepository, ICalendarEventRepository calendarEventRepository) : base(logger)
        {
            _user = user;
            _workModesRepository = workModesRepository;
            _calendarEventRepository = calendarEventRepository;
            _newCalendarEvent = new CalendarEvent();
        }

        public IActionResult Index()
        {
            List<CalendarEvent> calendarEvents = _calendarEventRepository.GetAllCalendarEvents();
            //List<CalendarEvent> calendarEventsToDraw = new List<CalendarEvent>();
            foreach (CalendarEvent calendarEvent in calendarEvents)
            {
                if (calendarEvent.UserId == _user.Id && calendarEvent.CalendarEventDate.Month == DateTime.Now.Month) 
                    calendarEvents.Add(calendarEvent);
            }
            _monthViewModel = new MonthViewModel(calendarEvents);
            Debug.WriteLine($"Drawing calendar for: {_monthViewModel.CurrentDate:y}");
            return View(_monthViewModel);
        }

        public IActionResult PreviousMonth(DateTime date)
        {
            List<CalendarEvent> calendarEvents = _calendarEventRepository.GetAllCalendarEvents();
            List<CalendarEvent> calendarEventsToDraw = new List<CalendarEvent>();
            foreach (CalendarEvent calendarEvent in calendarEvents)
            {
                if (calendarEvent.UserId == _user.Id && calendarEvent.CalendarEventDate.Month == date.AddMonths(-1).Month) calendarEventsToDraw.Add(calendarEvent);
            }
            _monthViewModel = new MonthViewModel(date.AddMonths(-1), calendarEventsToDraw);
            Debug.WriteLine($"Drawing calendar for: {_monthViewModel.CurrentDate:y}");
            return View("Index", _monthViewModel);
        }
        public IActionResult NextMonth(DateTime date)
        {
            List<CalendarEvent> calendarEvents = _calendarEventRepository.GetAllCalendarEvents();
            List<CalendarEvent> calendarEventsToDraw = new List<CalendarEvent>();
            foreach (CalendarEvent calendarEvent in calendarEvents)
            {
                if (calendarEvent.UserId == _user.Id && calendarEvent.CalendarEventDate.Month == date.AddMonths(1).Month) calendarEventsToDraw.Add(calendarEvent);
            }
            _monthViewModel = new MonthViewModel(date.AddMonths(1), calendarEventsToDraw);
            Debug.WriteLine($"Drawing calendar for: {_monthViewModel.CurrentDate:y}");
            return View("Index", _monthViewModel);
        }

        public IActionResult Day(DateTime date)
        {
            var successMessage = TempData["Success"] as string;
            TempData["ReturnUrl"] = HttpContext.Request.Path + HttpContext.Request.QueryString;

            DateOnly dateOnly = DateOnly.FromDateTime(date);
            WorkModeForUser workMode = _workModesRepository.GetWorkModeByUserIdAndDateOfWorkMode((int)_user.Id, dateOnly);
            string workModeString;
            if (workMode != null) workModeString = workMode.WorkMode.Name;
            else workModeString = "No work mode";
            List<CalendarEvent> calendarEvents = _calendarEventRepository.GetAllCalendarEvents();
            List<CalendarEvent> calendarEventsToDraw = new List<CalendarEvent>();
            foreach (CalendarEvent calendarEvent in calendarEvents)
            {
                if (calendarEvent.UserId == _user.Id && calendarEvent.CalendarEventDate == dateOnly) calendarEventsToDraw.Add(calendarEvent);
            }
            var dayViewModel = new DayViewModel(dateOnly, _user, workModeString, calendarEventsToDraw);
            Debug.WriteLine($"Drawing calendar day for: {dateOnly}");
            return View(dayViewModel);
        }

        //GET: CalendarController/Create
        public ActionResult Create(int id)
        {
            var calendarEvent = _newCalendarEvent;
            calendarEvent.UserId = id;

            Debug.WriteLine($"Creating Calendar Event started.");
            return View(calendarEvent);
        }

        // POST: CalendarController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CalendarEvent calendarEvent)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(calendarEvent);
                }


                var validationResult = _calendarEventRepository.CalendarEventStartTimeOverlappingValidation(calendarEvent.CalendarEventDate, calendarEvent.CalendarEventStartTime, calendarEvent.CalendarEventEndTime, calendarEvent.UserId);
                if (validationResult != ValidationResult.Success)
                {
                    ModelState.AddModelError(nameof(calendarEvent.CalendarEventStartTime), validationResult.ErrorMessage);
                    return View(calendarEvent);
                }
                _calendarEventRepository.CreateCalendarEvent(calendarEvent);
                Debug.WriteLine($"Created Calendar Event.");
                PopupNotification("Calendar event has been created successfully");
                var returnUrl = TempData["ReturnUrl"] as string;
                return Redirect(returnUrl);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception occurred: {ex.Message}");
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
