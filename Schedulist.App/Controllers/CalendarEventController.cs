﻿using Azure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Schedulist.App.Models.Enum;
using Schedulist.App.Services.Interfaces;
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

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<CalendarEvent>> GetAll([FromQuery] CalendarEventQuery query)
        {
            var calendarEvent = _calendarEventRepository.GetAll(query);
            return Ok(calendarEvent);
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
                    PopupNotification("Calendar event has been created successfully");
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
            SetupUserList();
            var model = _calendarEventRepository.GetCalendarEventById(id);
            logger.LogInformation($"Updating Calendar Event started.");
            return View("Edit", model);
        }

        //POST: CalendarEventController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CalendarEvent calendarEvent)
        {
            try
            {
                SetupUserList();
                //var validationResults = _calendarEventService.ValidateCalendarEvent(calendarEvent);
                //if (validationResults.Any(x => x != ValidationResult.Success))
                //{
                //    validationResults.Where(x => x != ValidationResult.Success).ToList().ForEach(x => ModelState.AddModelError(nameof(calendarEvent.CalendarEventEndTime), x.ErrorMessage));
                //    return View(calendarEvent);
                //}
                _calendarEventRepository.UpdateCalendarEvent(calendarEvent);
                logger.LogInformation($"Modified Calendar Event.");
                PopupNotification("Calendar event has been updated successfully");
                return RedirectToAction(nameof(Index));
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
