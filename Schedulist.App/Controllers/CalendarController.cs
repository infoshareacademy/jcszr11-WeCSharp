using Microsoft.AspNetCore.Mvc;
using Schedulist.App.Models;
using System.Diagnostics;

namespace Schedulist.App.Controllers
{
    public class CalendarController : ControlerBase
    {
        private CalendarParams _calendarParams;
        public CalendarController(ILogger<CalendarController> logger) : base(logger) { }

        public IActionResult Index()
        {
            _calendarParams = new CalendarParams();
            logger.LogInformation("Drawing calendar");
            var calendarParams = new CalendarParams();
            return View(_calendarParams);
        }
        public IActionResult PreviousMonth(DateTime date)
        {
            _calendarParams = new CalendarParams(date.AddMonths(-1));
            logger.LogInformation($"Current date to draw: {date}");
            return View("Index", _calendarParams);
        }
        public IActionResult NextMonth(DateTime date)
        {
            _calendarParams = new CalendarParams(date.AddMonths(1));
            logger.LogInformation($"Current date to draw: {date}");
            return View("Index", _calendarParams);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
