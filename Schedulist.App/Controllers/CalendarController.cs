using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Schedulist.App.Models;
using Schedulist.App.Services;
using Schedulist.DAL;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Schedulist.App.Controllers
{
    public class CalendarController : ControlerBase
    {
        private User _user;
        private List<User> _users;
        private MonthViewModel _calendarParams;
        private Dictionary<string, int> _userDict = new Dictionary<string, int>();
        public CalendarController(ILogger<CalendarController> logger, User user, List<User> users) : base(logger)
        {
            _user = user;
             _users = users;
            foreach (User userToAdd in _users) {
                _userDict.Add($"{userToAdd.Name} {userToAdd.Surname}", userToAdd.Id ?? default(int));
            }
        }

        public IActionResult Index()
        {
            int userToChange = _user.Id ?? default(int);
            List<CalendarEvent> allCalendarEvents = new CsvCalendarEventRepository("..\\Schedulist\\CalendarEvents.csv").GetAllCalendarEvents();
            List<CalendarEvent> calendarEventsToDraw = new List<CalendarEvent>();
            foreach (CalendarEvent calendarEvent in allCalendarEvents)
            {
                if (calendarEvent.AssignedToUser == _user.Id && calendarEvent.CalendarEventDate.Month == DateTime.Now.Month) calendarEventsToDraw.Add(calendarEvent);
            }
            _calendarParams = new MonthViewModel(calendarEventsToDraw, _userDict, userToChange);
            return View(_calendarParams);
        }

        public IActionResult PreviousMonth(DateTime date, int userToEdit)
        {
            _user = _users.First(obj => obj.Id == userToEdit);
            List<CalendarEvent> allCalendarEvents = new CsvCalendarEventRepository("..\\Schedulist\\CalendarEvents.csv").GetAllCalendarEvents();
            List<CalendarEvent> calendarEventsToDraw = new List<CalendarEvent>();
            foreach (CalendarEvent calendarEvent in allCalendarEvents)
            {
                if (calendarEvent.AssignedToUser == _user.Id && calendarEvent.CalendarEventDate.Month == date.AddMonths(-1).Month) calendarEventsToDraw.Add(calendarEvent);
            }
            _calendarParams = new MonthViewModel(date.AddMonths(-1), calendarEventsToDraw, _userDict, userToEdit);
            return View("Index", _calendarParams);
        }
        public IActionResult NextMonth(DateTime date, int userToEdit)
        {
            _user = _users.First(obj => obj.Id == userToEdit);
            List<CalendarEvent> allCalendarEvents = new CsvCalendarEventRepository("..\\Schedulist\\CalendarEvents.csv").GetAllCalendarEvents();
            List<CalendarEvent> calendarEventsToDraw = new List<CalendarEvent>();
            foreach (CalendarEvent calendarEvent in allCalendarEvents)
            {
                if (calendarEvent.AssignedToUser == _user.Id && calendarEvent.CalendarEventDate.Month == date.AddMonths(1).Month) calendarEventsToDraw.Add(calendarEvent);
            }
            _calendarParams = new MonthViewModel(date.AddMonths(1), calendarEventsToDraw, _userDict, userToEdit);
            return View("Index", _calendarParams);
        }

        public IActionResult ChangeUser(DateTime date, int userToEdit)
        {
            _user = _users.First(obj => obj.Id == userToEdit);
            List<CalendarEvent> allCalendarEvents = new CsvCalendarEventRepository("..\\Schedulist\\CalendarEvents.csv").GetAllCalendarEvents();
            List<CalendarEvent> calendarEventsToDraw = new List<CalendarEvent>(); foreach (CalendarEvent calendarEvent in allCalendarEvents)
            {
                if (calendarEvent.AssignedToUser == _user.Id && calendarEvent.CalendarEventDate.Month == date.Month) calendarEventsToDraw.Add(calendarEvent);
            }
            _calendarParams = new MonthViewModel(date, calendarEventsToDraw, _userDict, userToEdit);
            return View("Index", _calendarParams);
        }

        public IActionResult Day(DateTime date, int userToEdit)
        {
            if (userToEdit != 0)
            {
                _user = _users.First(obj => obj.Id == userToEdit);
            }

            TempData["ReturnUrl"] = HttpContext.Request.Path + HttpContext.Request.QueryString;
            TempData["SelectedDate"] = date;
            TempData["DayDate"] = date;
            TempData["UserId"] = userToEdit;
            TempData["UserDetails"] = $"{_user.Name} {_user.Surname}";
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
            var viewModel = new DayViewModel(dateOnly, _user, workModeString, calendarEventsToDraw);
            return View(viewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //GET: CalendarController/Create
        public ActionResult Create()
        {
            Debug.WriteLine($"Creating Calendar Event started.");
            return View();
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
                DateTime selectedDate = (DateTime)TempData.Peek("SelectedDate");
                DateOnly parsedChosenDate = DateOnly.FromDateTime(selectedDate);

                CalendarEventService calendarEventService = new CalendarEventService();
                calendarEvent.AssignedToUser = (int)TempData.Peek("UserId");
                calendarEvent.CalendarEventDate = parsedChosenDate;
                var validationResult = calendarEventService.CalendarEventTimesOverlappingValidation(calendarEvent.CalendarEventDate, calendarEvent.CalendarEventStartTime, calendarEvent.CalendarEventEndTime, calendarEvent.AssignedToUser);
                if (validationResult != ValidationResult.Success)
                {
                    ModelState.AddModelError(nameof(calendarEvent.CalendarEventStartTime), validationResult.ErrorMessage);
                    return View(calendarEvent);
                }
                calendarEventService.Create(calendarEvent);
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
    }
}
