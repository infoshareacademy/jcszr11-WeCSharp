using Microsoft.AspNetCore.Mvc;
using Schedulist.App.Models;
using System.Diagnostics;

namespace Schedulist.App.Controllers
{
    public class HomeController : ControlerBase
    {
        private CalendarParams _calendarParams;
        public HomeController(ILogger<HomeController> logger) : base(logger) { }

        [HttpGet]
        public IActionResult Index()
        {
            _calendarParams = new CalendarParams();
            logger.LogInformation("Drawing calendar");
            var calendarParams = new CalendarParams();
            return View(_calendarParams);
        }
        [HttpPost]
        public IActionResult NextMonth(int id)
        {
            _calendarParams.CurrentDate.AddMonths(1);
            logger.LogInformation($"Current date: {_calendarParams.CurrentDate}");
            return View();
        }
        //[HttpPost]
        //public IActionResult Index(int monthsToAdd)
        //{
        //    _calendarParams.CurrentDate.AddMonths(monthsToAdd);
        //    logger.LogInformation($"Current date: {_calendarParams.CurrentDate}");
        //    return View(_calendarParams);
        //}

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
