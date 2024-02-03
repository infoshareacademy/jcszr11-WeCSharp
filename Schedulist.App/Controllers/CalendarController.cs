using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Schedulist.App.Models;
using Schedulist.App.Services;
using Schedulist.DAL;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Schedulist.App.Controllers
{
    public class CalendarController : ControlerBase
    {
        private User _user;
        private MonthViewModel _mounthViewModel;
        private readonly IWorkModesRepository _workModesRepository;
        public CalendarController(ILogger<CalendarController> logger, User user, IWorkModesRepository workModesRepository) : base(logger)
        {
            _user = user;
            _workModesRepository = workModesRepository;
        }

        public IActionResult Index()
        {
            List<CalendarEvent> allCalendarEvents = new CsvCalendarEventRepository("..\\Schedulist\\CalendarEvents.csv").GetAllCalendarEvents();
            List<CalendarEvent> calendarEventsToDraw = new List<CalendarEvent>();
            foreach (CalendarEvent calendarEvent in allCalendarEvents)
            {
                if (calendarEvent.AssignedToUser == _user.Id && calendarEvent.CalendarEventDate.Month == DateTime.Now.Month) calendarEventsToDraw.Add(calendarEvent);
            }
            _mounthViewModel = new MonthViewModel(calendarEventsToDraw);
            Debug.WriteLine($"Drawing calendar for: {_mounthViewModel.CurrentDate:y}");
            return View(_mounthViewModel);
        }

        public IActionResult PreviousMonth(DateTime date)
        {
            List<CalendarEvent> allCalendarEvents = new CsvCalendarEventRepository("..\\Schedulist\\CalendarEvents.csv").GetAllCalendarEvents();
            List<CalendarEvent> calendarEventsToDraw = new List<CalendarEvent>();
            foreach (CalendarEvent calendarEvent in allCalendarEvents)
            {
                if (calendarEvent.AssignedToUser == _user.Id && calendarEvent.CalendarEventDate.Month == date.AddMonths(-1).Month) calendarEventsToDraw.Add(calendarEvent);
            }
            _mounthViewModel = new MonthViewModel(date.AddMonths(-1), calendarEventsToDraw);
            Debug.WriteLine($"Drawing calendar for: {_mounthViewModel.CurrentDate:y}");
            return View("Index", _mounthViewModel);
        }
        public IActionResult NextMonth(DateTime date)
        {
            List<CalendarEvent> allCalendarEvents = new CsvCalendarEventRepository("..\\Schedulist\\CalendarEvents.csv").GetAllCalendarEvents();
            List<CalendarEvent> calendarEventsToDraw = new List<CalendarEvent>();
            foreach (CalendarEvent calendarEvent in allCalendarEvents)
            {
                if (calendarEvent.AssignedToUser == _user.Id && calendarEvent.CalendarEventDate.Month == date.AddMonths(1).Month) calendarEventsToDraw.Add(calendarEvent);
            }
            _mounthViewModel = new MonthViewModel(date.AddMonths(1), calendarEventsToDraw);
            Debug.WriteLine($"Drawing calendar for: {_mounthViewModel.CurrentDate:y}");
            return View("Index", _mounthViewModel);
        }

        public IActionResult Day(DateTime date)
        {
            var successMessage = TempData["Success"] as string;
            TempData["ReturnUrl"] = HttpContext.Request.Path + HttpContext.Request.QueryString;

            DateOnly dateOnly = DateOnly.FromDateTime(date);
            //CSVWorkModesRepository _csvWorkModesRepository = new("..\\Schedulist\\WorkModes.csv");
            WorkModesToUser workMode = _workModesRepository.GetWorkModeByUserAndDate((int)_user.Id, dateOnly);
            string workModeString;
            if (workMode != null) workModeString = workMode.WorkMode.Name;
            else workModeString = "No work mode";
            List<CalendarEvent> allCalendarEvents = new CsvCalendarEventRepository("..\\Schedulist\\CalendarEvents.csv").GetAllCalendarEvents();
            List<CalendarEvent> calendarEventsToDraw = new List<CalendarEvent>();
            foreach (CalendarEvent calendarEvent in allCalendarEvents)
            {
                if (calendarEvent.AssignedToUser == _user.Id && calendarEvent.CalendarEventDate == dateOnly) calendarEventsToDraw.Add(calendarEvent);
            }
            var vm = new DayViewModel(dateOnly, _user, workModeString, calendarEventsToDraw);
            Debug.WriteLine($"Drawing calendar day for: {dateOnly}");
            return View(vm);
        }

        //GET: CalendarController/Create
        public ActionResult Create(int id)
        {
            var calendarEvent = new CalendarEvent();
            calendarEvent.AssignedToUser = id;

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


                CalendarEventService calendarEventService = new CalendarEventService();
                var validationResult = calendarEventService.CalendarEventStartTimeOverlappingValidation(calendarEvent.CalendarEventDate, calendarEvent.CalendarEventStartTime, calendarEvent.CalendarEventEndTime, calendarEvent.AssignedToUser);
                if (validationResult != ValidationResult.Success)
                {
                    ModelState.AddModelError(nameof(calendarEvent.CalendarEventStartTime), validationResult.ErrorMessage);
                    return View(calendarEvent);
                }
                calendarEventService.Create(calendarEvent);
                Debug.WriteLine($"Created Calendar Event.");
                TempData["Success"] = "Calendar Event has been created successfully";
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
