using Microsoft.AspNetCore.Mvc;
using Schedulist.App.Models;
using Schedulist.App.Services;
using Schedulist.DAL;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Schedulist.App.Controllers
{
    public class CalendarController : ControlerBase
    {
        private User _user;
        private MonthViewModel _calendarParams;
        public CalendarController(ILogger<CalendarController> logger, User user) : base(logger)
        {
            _user = user;
        }

        public IActionResult Index()
        {
            List<CalendarEvent> allCalendarEvents = new CsvCalendarEventRepository("..\\Schedulist\\CalendarEvents.csv").GetAllCalendarEvents();
            List<CalendarEvent> calendarEventsToDraw = new List<CalendarEvent>();
            foreach (CalendarEvent calendarEvent in allCalendarEvents)
            {
                if (calendarEvent.AssignedToUser == _user.Id && calendarEvent.CalendarEventDate.Month == DateTime.Now.Month) calendarEventsToDraw.Add(calendarEvent);
            }
            _calendarParams = new MonthViewModel(calendarEventsToDraw);
            Debug.WriteLine($"Drawing calendar for: {_calendarParams.CurrentDate:y}");
            return View(_calendarParams);
        }

        public IActionResult PreviousMonth(DateTime date)
        {
            List<CalendarEvent> allCalendarEvents = new CsvCalendarEventRepository("..\\Schedulist\\CalendarEvents.csv").GetAllCalendarEvents();
            List<CalendarEvent> calendarEventsToDraw = new List<CalendarEvent>();
            foreach (CalendarEvent calendarEvent in allCalendarEvents)
            {
                if (calendarEvent.AssignedToUser == _user.Id && calendarEvent.CalendarEventDate.Month == date.AddMonths(-1).Month) calendarEventsToDraw.Add(calendarEvent);
            }
            _calendarParams = new MonthViewModel(date.AddMonths(-1), calendarEventsToDraw);
            Debug.WriteLine($"Drawing calendar for: {_calendarParams.CurrentDate:y}");
            return View("Index", _calendarParams);
        }
        public IActionResult NextMonth(DateTime date)
        {
            List<CalendarEvent> allCalendarEvents = new CsvCalendarEventRepository("..\\Schedulist\\CalendarEvents.csv").GetAllCalendarEvents();
            List<CalendarEvent> calendarEventsToDraw = new List<CalendarEvent>();
            foreach (CalendarEvent calendarEvent in allCalendarEvents)
            {
                if (calendarEvent.AssignedToUser == _user.Id && calendarEvent.CalendarEventDate.Month == date.AddMonths(1).Month) calendarEventsToDraw.Add(calendarEvent);
            }
            _calendarParams = new MonthViewModel(date.AddMonths(1), calendarEventsToDraw);
            Debug.WriteLine($"Drawing calendar for: {_calendarParams.CurrentDate:y}");
            return View("Index", _calendarParams);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Day(DateTime date)
        {
            var successMessage = TempData["Success"] as string;
            TempData["ReturnUrl"] = HttpContext.Request.Path + HttpContext.Request.QueryString;

            DateOnly dateOnly = DateOnly.FromDateTime(date);
            
            CSVWorkModesRepository _csvWorkModesRepository = new("..\\Schedulist\\WorkModes.csv");
            WorkModesToUser workMode = _csvWorkModesRepository.GetWorkModeByUserAndDate(_user.Id, dateOnly);
            string workModeString;
            if (workMode != null) workModeString = workMode.WorkModeName;
            else workModeString = "No work mode";
            List<CalendarEvent> allCalendarEvents = new CsvCalendarEventRepository("..\\Schedulist\\CalendarEvents.csv").GetAllCalendarEvents();
            List<CalendarEvent> calendarEventsToDraw = new List<CalendarEvent>();
            foreach (CalendarEvent calendarEvent in allCalendarEvents)
            {
                if (calendarEvent.AssignedToUser == _user.Id && calendarEvent.CalendarEventDate == dateOnly) calendarEventsToDraw.Add(calendarEvent);
            }
            var vm = new DayViewModel(dateOnly, _user, workModeString, calendarEventsToDraw);
            
            Debug.WriteLine($"Drawing calendar day for: {dateOnly}");
            TempData["SelectedDate"] = date;
            return View(vm);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        //GET: CalendarController/Create
        public IActionResult Create()
        {
            Debug.WriteLine($"Creating Calendar Event started.");
            return View();
        }

        // POST: CalendarController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CalendarEvent calendarEvent, DateTime date)
        {
            //DateOnly chosenDate = new DateOnly(2024, 1, 30); 
            DateTime selectedDate = (DateTime)TempData["SelectedDate"];
            DateOnly dateOnly = DateOnly.FromDateTime(selectedDate);
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(calendarEvent);
                }
                //DateOnly parsedChosenDate = DateOnly.FromDateTime(DateTime.Parse(chosenDate));
                CalendarEventService calendarEventService = new CalendarEventService();
                calendarEvent.AssignedToUser = (int)_user.Id;
                //calendarEvent.CalendarEventDate = parsedChosenDate;
                calendarEvent.CalendarEventDate = dateOnly;
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
    }
}
