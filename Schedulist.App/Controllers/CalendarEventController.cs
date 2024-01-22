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
        public CalendarEventService _calendarEventService;
        public CalendarEventController(ILogger<CalendarEventController> logger, CsvCalendarEventRepository repository) : base(logger) 
        {
            _repository = repository;
        }

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

        //GET: CalendarEventController/Create
        public ActionResult Create()
        {
            Debug.WriteLine($"Creating Calendar Event started.");
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
                Debug.WriteLine($"Created Calendar Event.");
                TempData["Success"] = "Calendar Event has been created successfully";
                //return RedirectToAction("Day", "Calendar");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception occurred: {ex.Message}");
                return View();
            }
        }

        // GET: CalendarEventController/Edit/5
        public ActionResult Edit(int id)
        {
            CalendarEventService calendarEventService = new CalendarEventService();
            var model = calendarEventService.GetCalendarEventById(id);
            Debug.WriteLine($"Deleting Calendar Event started.");
            return View(model);
        }

        // POST: CalendarEventController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CalendarEvent calendarEvent)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(id);
                }
                CalendarEventService calendarEventService = new CalendarEventService();
                calendarEventService.Edit(calendarEvent);
                Debug.WriteLine($"Modified Calendar Event.");
                TempData["Modified"] = "Calendar Event has been modified";
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
            CalendarEventService calendarEventService = new CalendarEventService();
            var model = calendarEventService.GetCalendarEventById(id);
            Debug.WriteLine($"Deleting Calendar Event started.");
            return View(model);
        }

        // POST: CalendarEventController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(id);
                }
                CalendarEventService calendarEventService = new CalendarEventService();
                calendarEventService.Delete(id);
                Debug.WriteLine($"Deleted Calendar Event.");
                TempData["Deleted"] = "Calendar Event has been deleted";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception occurred: {ex.Message}");
                return View();
            }
        }
    }
}
