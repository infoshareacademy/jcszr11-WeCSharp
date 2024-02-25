using Microsoft.AspNetCore.Mvc;
using Schedulist.App.Models.Enum;
using Schedulist.App.Services.Interfaces;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Schedulist.App.Controllers
{
    public class CalendarEventController : ControllerBase
    {
        private readonly ICalendarEventRepository _calendarEventRepository;
        private readonly ICalendarEventService _calendarEventService;
        public CalendarEventController(ILogger<CalendarEventController> logger, ICalendarEventRepository calendarEventRepository, ICalendarEventService calendarEventService) : base(logger) 
        {
            _calendarEventRepository = calendarEventRepository;
            _calendarEventService = calendarEventService;
        }

        // GET: CalendarEventController
        [HttpGet]
        [ResponseCache(Duration = 30, NoStore = true)]
        public IActionResult Index()
        {
            var calendarEvents = _calendarEventRepository.GetAllCalendarEvents();
            return View(calendarEvents);
        }

        // GET: CalendarEventController/Details/5
        [HttpGet]
        [ResponseCache(Duration = 30, NoStore = true)]
        public IActionResult Details(int id)
        {
            var calendarEvent = _calendarEventRepository.GetAllCalendarEvents()[id];
            return View(calendarEvent);
        }

        //GET: CalendarEventController/Create
        [HttpGet]
        [ResponseCache(Duration = 30, NoStore = true)]
        public ActionResult Create()
        {
            logger.LogInformation($"Creating Calendar Event started.");
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
                var validationResults = _calendarEventService.ValidateCalendarEvent(calendarEvent);
                if (validationResults.Any(x => x != ValidationResult.Success))
                {
                    validationResults.Where(x => x != ValidationResult.Success).ToList().ForEach(x => ModelState.AddModelError(nameof(calendarEvent.CalendarEventEndTime), x.ErrorMessage));
                    return View(calendarEvent);
                }
                if (calendarEvent.Id == 0)
                {
                    _calendarEventRepository.CreateCalendarEvent(calendarEvent);
                    logger.LogInformation("Calendar Event created.");
                    PopupNotification("Calendar event has been created successfully");
                }
                else
                {
                    _calendarEventRepository.UpdateCalendarEvent(calendarEvent);
                    logger.LogInformation($"Modified Calendar Event.");
                    PopupNotification("Calendar event has been updated successfully");
                }
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                PopupNotification("Error occurred while deleting calendar event", notificationType: NotificationType.error);
                logger.LogInformation($"Exception occurred: {ex.Message}");
                return Ok();
            }
        }

        // GET: CalendarEventController/Edit/5
        [HttpGet]
        [ResponseCache(Duration = 30, NoStore = true)]
        public ActionResult Edit(int id)
        {
            var model = _calendarEventRepository.GetCalendarEventById(id);
            logger.LogInformation($"Deleting Calendar Event started.");
            return View("Create", model);
        }

        // POST: CalendarEventController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, CalendarEvent calendarEvent)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return View(id);
        //        }
        //        _calendarEventRepository.UpdateCalendarEvent(calendarEvent);
        //        Debug.WriteLine($"Modified Calendar Event.");
        //        PopupNotification("Calendar event has been updated successfully");
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        // POST: CalendarEventController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                CalendarEvent calendarEventToDelete = _calendarEventRepository.GetCalendarEventById(id);
                _calendarEventRepository.DeleteCalendarEvent(calendarEventToDelete);
                logger.LogInformation($"Deleted Calendar Event.");
                PopupNotification("calendar event has been successfully deleted");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogInformation($"Exception occurred: {ex.Message}");
                PopupNotification("Error occurred while deleting calendar event", notificationType: NotificationType.error);
                return View();
            }
        }
    }
}
