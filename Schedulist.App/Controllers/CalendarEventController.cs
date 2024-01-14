using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Schedulist.Business;
using Schedulist.DAL;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Schedulist.App.Controllers
{
    public class CalendarEventController : ControlerBase
    {
        public CalendarEventController(ILogger<CalendarEventController> logger, CsvCalendarEventRepository repository) : base(logger) 
        {
            this.repository = repository;

        }
       // public List<CalendarEvent> _calendarEvents = new CsvCalendarEventRepository("..\\Schedulist\\CalendarEvents.csv").GetAllCalendarEvents();
        private readonly CsvCalendarEventRepository repository;
       private ManageCalendarEvent manageCalendarEvent;

        // GET: CalendarEventController
        public IActionResult Index()
        {
            var model = repository.GetAllCalendarEvents();
            //var model = Events;
            return View(model);
         
        }

        // GET: CalendarEventController/Details/5
        public IActionResult Details(int id)
        {
            var calendarEvent = repository.GetAllCalendarEvents()[id];
            return View(calendarEvent);
        }

        public IActionResult ChosenDateEventDetails(int id, User user, DateOnly date)
        {
            var calendarEvent = manageCalendarEvent.ShowUserCalendarEvent(user, date)[id];
            return View(calendarEvent);
        }

        // GET: CalendarEventController/Create
        public ActionResult Create()
        {
            var calendarEvent = manageCalendarEvent.CreateCalendarEvent();
            return View(calendarEvent);
        }

        // POST: CalendarEventController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CalendarEventController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CalendarEventController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CalendarEventController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CalendarEventController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
