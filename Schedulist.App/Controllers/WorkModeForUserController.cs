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

        //GET: WorkModeForUserController
        [Route("WorkModesToUser")]
        public ActionResult Index()
        {
            var workmodeForUser = _workModeForUserRepository.GetAllWorkModesForUser();
            return View(workmodeForUser);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var workModeToDelete = _workModeForUserRepository.GetWorkModeById(id);
                _workModeForUserRepository.DeleteWorkModeForUser(workModeToDelete);
                Debug.WriteLine("Removed Work Mode!");
                PopupNotification("Work mode has been successfully deleted");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception occurred: {ex.Message}");
                PopupNotification("Error occurred while deleting calendar event", notificationType: NotificationType.error);
                return View();
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _workModeForUserRepository.GetWorkModeById(id);
            Debug.WriteLine($"Editing Work Mode started!");
            return View(model);
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

        //POST: WorkModeForUserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WorkModeForUser workModeForUser)
        {
            try
            {
                SetupUserList();
                SetupWorkModeList();
                _workModeForUserRepository.UpdateWorkModeForUser(workModeForUser);
                logger.LogInformation("Work mode for user has been updated successfully");
                PopupNotification("Work mode for user has been updated successfully");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WorkModeController/Details/5
        //public IActionResult Details(int id)
        //{
        //    var workmode = _repository.GetAllWorkModes()[id];
        //    return View(workmode);
        //}


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
                //var validationResults = _workModeForUserService.ValidateWorkMode(workModeForUser);
                //if (validationResults.Any(x => x != ValidationResult.Success))
                //{
                //    validationResults.Where(x => x != ValidationResult.Success).ToList().ForEach(x => ModelState.AddModelError(nameof(workModeForUser.DateOfWorkMode), x.ErrorMessage));
                //    return View(workModeForUser);
                //}
                //else
                //{
                //    return View();
                //}
                _workModeForUserRepository.CreateWorkModeForUser(workModeForUser);
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return Ok();
            }

            //var model = new WorkModeViewModel();
            //}

            //GET: WorkModeForUserController/Delete/5
        }
    }
}
