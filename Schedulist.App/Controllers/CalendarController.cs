using Microsoft.AspNetCore.Mvc;
using Schedulist.App.Models;
using Schedulist.DAL;
using System.Diagnostics;

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
            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
