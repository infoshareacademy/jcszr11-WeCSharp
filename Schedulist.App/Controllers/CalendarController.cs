using Microsoft.AspNetCore.Mvc;
using Schedulist.App.Models;
using Schedulist.DAL;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace Schedulist.App.Controllers
{
    public class CalendarController : ControlerBase
    {
        private User _user;
        private CalendarParams _calendarParams;
        public CalendarController(ILogger<CalendarController> logger, User user) : base(logger)
        {
            _user = user;
        }

        public IActionResult Index()
        {
            _calendarParams = new CalendarParams();
            Debug.WriteLine($"Drawing calendar for: {_calendarParams.CurrentDate:y}");
            return View(_calendarParams);
        }

        public IActionResult PreviousMonth(DateTime date)
        {
            _calendarParams = new CalendarParams(date.AddMonths(-1));
            Debug.WriteLine($"Drawing calendar for: {_calendarParams.CurrentDate:y}");
            return View("Index", _calendarParams);
        }
        public IActionResult NextMonth(DateTime date)
        {
            _calendarParams = new CalendarParams(date.AddMonths(1));
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
            List<CalendarEvent> calendarEvents = new CsvCalendarEventRepository("..\\Schedulist\\CalendarEvents.csv").GetAllCalendarEvents();
            var vm = new DayViewModel(dateOnly, _user, workModeString, calendarEvents);
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
