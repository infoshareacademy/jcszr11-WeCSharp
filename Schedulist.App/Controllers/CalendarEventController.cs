using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Schedulist.App.Models;
using Schedulist.App.Services;
using Schedulist.Business;
using Schedulist.DAL;
using System.Diagnostics;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Schedulist.App.Controllers
{
    public class CalendarEventController : ControlerBase
    {
        private readonly CsvCalendarEventRepository _repository;
        private CalendarEventService _calendarEventService;
        public CalendarEventController(ILogger<CalendarEventController> logger, CsvCalendarEventRepository repository) : base(logger) 
        {
            _repository = repository;

        }
       // public List<CalendarEvent> _calendarEvents = new CsvCalendarEventRepository("..\\Schedulist\\CalendarEvents.csv").GetAllCalendarEvents();
        
       

        // GET: CalendarEventController
        public IActionResult Index()
        {
            var model = _repository.GetAllCalendarEvents();
            //var model = Events;
            return View(model);
         
        }

        // GET: CalendarEventController/Details/5
        public IActionResult Details(int id)
        {
            var calendarEvent = _repository.GetAllCalendarEvents()[id];
            return View(calendarEvent);
        }

        //public IActionResult ChosenDateEventDetails(int id, User user, DateOnly date)
        //{
        //    var calendarEvent = manageCalendarEvent.ShowUserCalendarEvent(user, date)[id];
        //    return View(calendarEvent);
        //}

        //GET: CalendarEventController/Create
        public ActionResult Create()
        {
            Debug.WriteLine($"Creating Calendar Event");
            return View();
        }

        // POST: CalendarEventController/Create
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

                calendarEventService.Create(calendarEvent);
                Debug.WriteLine($"Creating Calendar Event");

                //return RedirectToAction("Day", "Calendar");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                Debug.WriteLine($"Exception occurred");
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
