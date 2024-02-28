using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Schedulist.App.Models.Enum;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories;
using Schedulist.DAL.Repositories.Interfaces;
using System.Diagnostics;

namespace Schedulist.App.Controllers
{
    public class WorkModeForUserController : ControllerBase
    {
        private readonly IWorkModeForUserRepository _workModeForUserRepository;
        public WorkModeForUserController(ILogger<WorkModeForUserController> logger, IWorkModeForUserRepository workModeForUserRepository) : base(logger)
        {
            _workModeForUserRepository = workModeForUserRepository;
        }

        //GET: WorkModeForUserController
       [Route("WorkModesToUser")]
        public ActionResult Index()
        {
            var workmode = _workModeForUserRepository.GetAllWorkModesForUser();
            return View(workmode);
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

        //POST: WorkModeForUserController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(WorkModeForUser workMode)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return View();
        //        }
                
        //        foreach (var name in namesWorkModes)
        //        {
        //            viewWorkMode.GetAllWorkModeNames.Add(new SelectListItem { Text = name.Name, Value = name.Id.ToString() });
        //        }
        //        //WorkModeService workModeService = new WorkModeService();
        //        _workModeService.Edit(viewWorkMode, workModes);
        //        Debug.WriteLine("Modified Work Mode!");
        //        TempData["Success"] = "Work Mode has been modified successfully!";
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: WorkModeController/Details/5
        //public IActionResult Details(int id)
        //{
        //    var workmode = _repository.GetAllWorkModes()[id];
        //    return View(workmode);
        //}


        // GET: WorkModeController/Create
        //public ActionResult Create()
        //{
        //    Debug.WriteLine($"Creating Work Mode started!");
        //    return View();
        //}

        // POST: WorkModeController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(WorkModeViewModel workModeView, WorkModesToUser workModesToUser)
        //{
        //    var workmodename = WorkModeNamesList.GetAll();
        //    workModeView.GetAllWorkModeNames = new List<SelectListItem>();
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return View(workModesToUser);
        //        }
        //        //foreach (var name in workmodename)
        //        //{
        //        //    workModeView.GetAllWorkModeNames.Add(new SelectListItem { Text = name.Name, Value = name.Id.ToString() });
        //        //}
        //        //DateTime selectedDate = (DateTime)TempData.Peek("SelectedDate");
        //        //DateOnly parsedChosenDate = DateOnly.FromDateTime(selectedDate);
        //        workModeView.WorkModeName = WorkModeNamesList.GetAll().FirstOrDefault(w => w.Id == workModeView.SelectedWorkModeId)?.Name;

        //        _workModeService.Create(workModeView, workModesToUser);
        //        Debug.WriteLine("Created new work mode!");
        //        TempData["Success"] = "Work mode has been created successfully";
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }

        //    //var model = new WorkModeViewModel();
        //}

        //GET: WorkModeForUserController/Delete/5
    }
}
