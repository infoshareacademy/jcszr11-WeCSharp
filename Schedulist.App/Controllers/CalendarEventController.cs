using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Schedulist.App.Models.Enum;
using Schedulist.DAL;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories;
using Schedulist.DAL.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing.Text;

namespace Schedulist.App.Controllers
{
    public class CalendarEventController : ControllerBase
    {
        private readonly ICalendarEventRepository _calendarEventRepository;
        private readonly IUserRepository _userRepository;
        public CalendarEventController(ILogger<CalendarEventController> logger, ICalendarEventRepository calendarEventRepository, IUserRepository userRepository) : base(logger) 
        {
            _calendarEventRepository = calendarEventRepository;
            _userRepository = userRepository;
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
            SetupUserList();
            //ViewBag.Users = usersListItems;

            Debug.WriteLine($"Creating Calendar Event started.");
            return View();
        }

        private void SetupUserList()
        {
            var users = _userRepository.GetAllUsers();
            var usersListItems = users.Select(user => new SelectListItem { Text = $"{user.Name} {user.Surname}", Value = user.Id.ToString() });
            ViewBag.Users = new SelectList(usersListItems, "Value", "Text");
        }

        // POST: CalendarEventController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CalendarEvent calendarEvent)
        {
            try
            {

                //if (!ModelState.IsValid)
                //{
                //    return View(calendarEvent);
                //}
                SetupUserList();
                if (calendarEvent.Id == 0)
                {
                    var timeValidationResult = _calendarEventRepository.CalendarEventTimesValidation(calendarEvent.CalendarEventStartTime, calendarEvent.CalendarEventEndTime);
                    var validationResult = _calendarEventRepository.CalendarEventOverlappingValidation(calendarEvent.CalendarEventDate, calendarEvent.CalendarEventStartTime, calendarEvent.CalendarEventEndTime, calendarEvent.UserId, calendarEvent.Id);
                    if (timeValidationResult != ValidationResult.Success)
                    {
                        ModelState.AddModelError(nameof(calendarEvent.CalendarEventEndTime), timeValidationResult.ErrorMessage);
                        return View(calendarEvent);
                    }
                    if (validationResult != ValidationResult.Success)
                    {
                        ModelState.AddModelError(nameof(calendarEvent.CalendarEventStartTime), validationResult.ErrorMessage);
                        return View(calendarEvent);
                    }
                    _calendarEventRepository.CreateCalendarEvent(calendarEvent);
                    Debug.WriteLine($"Created Calendar Event.");
                    PopupNotification("Calendar event has been created successfully");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var timeValidationResult = _calendarEventRepository.CalendarEventTimesValidation(calendarEvent.CalendarEventStartTime, calendarEvent.CalendarEventEndTime);
                    var validationResult = _calendarEventRepository.CalendarEventOverlappingValidation(calendarEvent.CalendarEventDate, calendarEvent.CalendarEventStartTime, calendarEvent.CalendarEventEndTime, calendarEvent.UserId, calendarEvent.Id);
                    if (timeValidationResult != ValidationResult.Success)
                    {
                        ModelState.AddModelError(nameof(calendarEvent.CalendarEventEndTime), timeValidationResult.ErrorMessage);
                        return View(calendarEvent);
                    }
                    if (validationResult != ValidationResult.Success)
                    {
                        ModelState.AddModelError(nameof(calendarEvent.CalendarEventStartTime), validationResult.ErrorMessage);
                        return View(calendarEvent);
                    }
                    _calendarEventRepository.UpdateCalendarEvent(calendarEvent);
                    Debug.WriteLine($"Modified Calendar Event.");
                    PopupNotification("Calendar event has been updated successfully");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                PopupNotification("Error occurred while deleting calendar event", notificationType: NotificationType.error);
                Debug.WriteLine($"Exception occurred: {ex.Message}");
                return Ok();
            }
        }

        // GET: CalendarEventController/Edit/5
        [HttpGet]
        [ResponseCache(Duration = 30, NoStore = true)]
        public ActionResult Edit(int id)
        {
            var model = _calendarEventRepository.GetCalendarEventById(id);
            Debug.WriteLine($"Deleting Calendar Event started.");
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
                Debug.WriteLine($"Deleted Calendar Event.");
                PopupNotification("calendar event has been successfully deleted");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception occurred: {ex.Message}");
                PopupNotification("Error occurred while deleting calendar event", notificationType: NotificationType.error);
                return View();
            }
        }

        //public ActionResult AssignUser()
        //{
        //    using (var db = new SchedulistDbContext())
        //    {
        //        var users = db.Users.ToList();
        //        ViewBag.Users = new SelectList(users, "Id", "Name");
        //    }
        //        return View(); 
        //}
    }
}
