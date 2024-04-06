using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Schedulist.App.Models.Enum;
using Schedulist.App.Services.Interfaces;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories;
using Schedulist.DAL.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Schedulist.App.Controllers
{
    public class WorkModeForUserController : ControllerBase
    {
        private readonly IWorkModeForUserRepository _workModeForUserRepository;
        private readonly IWorkModeRepository _workModeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IWorkModeForUserService _workModeForUserService;
        public WorkModeForUserController(ILogger<WorkModeForUserController> logger, IWorkModeForUserRepository workModeForUserRepository, IWorkModeRepository workModeRepository, IUserRepository userRepository) : base(logger)
        {

            _workModeForUserRepository = workModeForUserRepository;
            _workModeRepository = workModeRepository;
            _userRepository = userRepository;

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var workModeToDelete = _workModeForUserRepository.GetWorkModeById(id);
                _workModeForUserRepository.DeleteWorkModeForUser(workModeToDelete);
                logger.LogInformation("Removed Work Mode!");
                PopUpNotification("Work mode has been successfully deleted");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogInformation($"Exception occurred: {ex.Message}");
                PopUpNotification("Error occurred while deleting calendar event", notificationType: NotificationType.error);
                return View();
            }
        }

        public void SetupWorkModeList()
        {
            var workModes = _workModeRepository.GetAllWorkModes();
            var workModesItems = workModes.Select(workMode => new SelectListItem { Text = $"{workMode.Name}", Value = workMode.Id.ToString() });
            ViewBag.WorkModes = new SelectList(workModesItems, "Value", "Text");
        }

        private void SetupUserList()
        {
            var users = _userRepository.GetAllUsers();
            var usersListItems = users.Select(user => new SelectListItem { Text = $"{user.Name} {user.Surname}", Value = user.Id.ToString() });
            ViewBag.Users = new SelectList(usersListItems, "Value", "Text");
        }

        //GET: WorkModeForUserController
        [Route("WorkModesToUser")]
        public ActionResult Index()
        {
            var workmodeForUser = _workModeForUserRepository.GetAllWorkModesForUser();
            return View(workmodeForUser);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            SetupUserList();
            SetupWorkModeList();
            var model = _workModeForUserRepository.GetWorkModeById(id);
            logger.LogInformation($"Editing Work Mode started!");
            return View("Edit", model);
        }

        //POST: WorkModeForUserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, WorkModeForUser workModeForUser)
        {
            try
            {
                SetupUserList();
                SetupWorkModeList();
                var validationResults = _workModeForUserRepository.WorkModeForUserValidation(workModeForUser);
                if (validationResults != ValidationResult.Success)
                {
                    return View(workModeForUser);
                }
                _workModeForUserRepository.UpdateWorkModeForUser(id, workModeForUser);
                logger.LogInformation("Work mode for user has been updated successfully");
                PopUpNotification("Work mode for user has been updated successfully");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //GET: WorkModeController/Create
        [HttpGet]
        public ActionResult Create()
        {
            SetupUserList();
            SetupWorkModeList();
            logger.LogInformation($"Creating Work Mode started!");
            return View();
        }

        // POST: WorkModeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WorkModeForUser workModeForUser)
        {
            try
            {
                SetupUserList();
                SetupWorkModeList();
                var validationResults = _workModeForUserRepository.WorkModeForUserValidation(workModeForUser);
                if (validationResults != ValidationResult.Success)
                {
                    return View(workModeForUser);
                }
                _workModeForUserRepository.CreateWorkModeForUser(workModeForUser);
                logger.LogInformation("Work mode created.");
                PopUpNotification("Work mode has been created successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                PopUpNotification("Error occurred while deleting Calendar Event", notificationType: NotificationType.error);
                logger.LogError($"Exception occurred: {ex.Message}");
                return Ok();
            }
        }
    }
}
