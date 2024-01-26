using Microsoft.AspNetCore.Mvc;
using Schedulist.App.Models;
using Schedulist.DAL;
using System.Diagnostics;

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
            List<CalendarEvent> allCalendarEvents = new CsvCalendarEventRepository("..\\Schedulist\\CalendarEvents.csv").GetAllCalendarEvents();
            List<CalendarEvent> calendarEventsToDraw = new List<CalendarEvent>();
            foreach (CalendarEvent calendarEvent in allCalendarEvents)
            {
                if (calendarEvent.AssignedToUser == _user.Id && calendarEvent.CalendarEventDate.Month == DateTime.Now.Month) calendarEventsToDraw.Add(calendarEvent);
            }
            _calendarParams = new MonthViewModel(calendarEventsToDraw, _userDict);
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
            _calendarParams = new MonthViewModel(date.AddMonths(-1), calendarEventsToDraw, _userDict);
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
            _calendarParams = new MonthViewModel(date.AddMonths(1), calendarEventsToDraw, _userDict);
            return View("Index", _calendarParams);
        }

        public IActionResult ChangeUser(DateTime date, int userId)
        {
            _user = _users.First(obj => obj.Id == userId);
            List<CalendarEvent> allCalendarEvents = new CsvCalendarEventRepository("..\\Schedulist\\CalendarEvents.csv").GetAllCalendarEvents();
            List<CalendarEvent> calendarEventsToDraw = new List<CalendarEvent>(); foreach (CalendarEvent calendarEvent in allCalendarEvents)
            {
                if (calendarEvent.AssignedToUser == _user.Id && calendarEvent.CalendarEventDate.Month == date.Month) calendarEventsToDraw.Add(calendarEvent);
            }
            _calendarParams = new MonthViewModel(date, calendarEventsToDraw, _userDict);
            return View("Index", _calendarParams);
        }

        public IActionResult Day(DateTime date)
        {
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
    }
}
