﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly IUserRepository _userRepository;
        private readonly ICalendarEventService _calendarEventService;
        public CalendarEventController(ILogger<CalendarEventController> logger, ICalendarEventRepository calendarEventRepository, ICalendarEventService calendarEventService, IUserRepository userRepository) : base(logger) 
        {
            _calendarEventRepository = calendarEventRepository;
            _userRepository = userRepository;
            _calendarEventService = calendarEventService;
        }



        // GET: CalendarEventController
        [HttpGet]
        [ResponseCache(Duration = 30, NoStore = true)]
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            var userManager = HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
            var user = userManager.GetUserAsync(HttpContext.User).Result;

            if (User.IsInRole("Admin"))
            {
                var calendarEvents = _calendarEventRepository.GetAllCalendarEvents();
                return View(calendarEvents);
            }
            else
            {
                var calendarEvents = _calendarEventRepository.GetAllCalendarEvents().Where(c => c.UserId == user.Id);
                return View(calendarEvents);
            }

            
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
            logger.LogInformation($"Creating Calendar Event started.");
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
                SetupUserList();
                var validationResults = _calendarEventService.ValidateCalendarEvent(calendarEvent);
                if (validationResults.Any(x => x != ValidationResult.Success))
                {
                    validationResults.Where(x => x != ValidationResult.Success).ToList().ForEach(x => ModelState.AddModelError(nameof(calendarEvent.CalendarEventEndTime), x.ErrorMessage));
                    return View(calendarEvent);
                }
                    _calendarEventRepository.CreateCalendarEvent(calendarEvent);
                    logger.LogInformation("Calendar Event created.");
                    PopUpNotification("Calendar Event has been created successfully");
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                PopUpNotification("Error occurred while creating Calendar Event", notificationType: NotificationType.error);
                logger.LogError($"Exception occurred: {ex.Message}");
                return Ok();
            }
        }

        // GET: CalendarEventController/Edit/5
        [HttpGet]
        [ResponseCache(Duration = 30, NoStore = true)]
        public ActionResult Edit(int id, string returnUrl = null)
        {
            TempData["ReturnUrl"] = returnUrl;
            SetupUserList();
            var model = _calendarEventRepository.GetCalendarEventById(id);
            logger.LogInformation($"Updating Calendar Event started.");
            return View("Edit", model);
        }

        //PUT: CalendarEventController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CalendarEvent calendarEvent)
        {
            try
            {
                SetupUserList();
                var validationResults = _calendarEventService.ValidateCalendarEvent(calendarEvent);
                if (validationResults.Any(x => x != ValidationResult.Success))
                {
                    validationResults.Where(x => x != ValidationResult.Success).ToList().ForEach(x => ModelState.AddModelError(nameof(calendarEvent.CalendarEventEndTime), x.ErrorMessage));
                    return View(calendarEvent);
                }
                _calendarEventRepository.UpdateCalendarEvent(id, calendarEvent);
                logger.LogInformation($"Modified Calendar Event.");
                PopUpNotification("Calendar event has been updated successfully");
                var returnUrl = TempData["ReturnUrl"] as string;

                return returnUrl != null ? Redirect(returnUrl) : RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        //POST: CalendarEventController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                CalendarEvent calendarEventToDelete = _calendarEventRepository.GetCalendarEventById(id);
                _calendarEventRepository.DeleteCalendarEvent(calendarEventToDelete);
                logger.LogInformation($"Deleted Calendar Event.");
                PopUpNotification("Calendar Event has been successfully deleted");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception occurred: {ex.Message}");
                PopUpNotification("Error occurred while deleting Calendar Event", notificationType: NotificationType.error);
                return View();
            }
        }
    }
}
