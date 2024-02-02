using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Schedulist.App.Helper;
using Schedulist.App.Models;
using Schedulist.App.Models.Domain_Models;
using Schedulist.App.Services;
using Schedulist.DAL;
using System.Diagnostics;

namespace Schedulist.App.Controllers
{
    public class WorkModeController : ControlerBase
    {
        private readonly CSVWorkModesRepository _repository;
        
        public WorkModeService _workModeService;
        public WorkModeController(ILogger<WorkModeController> logger, CSVWorkModesRepository repository) : base(logger) 
        {
            _repository = repository;
        }
        
        // GET: WorkModeController
        [Route("WorkModesToUser")]
        public ActionResult Index()
        {
            var workmode=_repository.GetAllWorkModes();
            return View(workmode);
        }

        // GET: WorkModeController/Details/5
        public IActionResult Details(int id)
        {
            var workmode = _repository.GetAllWorkModes()[id];
            return View(workmode);
        }

        //// GET: WorkModeController/Create
        //public ActionResult Create()
        //{
        //    Debug.WriteLine($"Creating Work Mode started!");
        //    return View();
        //}

        //// POST: WorkModeController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(WorkModeViewModel workModeView, WorkModesToUser workModesToUser)
        //{
        //    var workmodename = WorkModeNamesList.GetAll();
        //    workModeView.GetAllWorkModeNames = new List<SelectListItem>();
        //    try
        //    {
        //        if(!ModelState.IsValid)
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

        // GET: WorkModeController/Edit/5
        public ActionResult Edit(int id)
        {
            //WorkModeService workModeService = new WorkModeService();
            var model = _workModeService.GetWorkModeById(id);
            Debug.WriteLine($"Editing Work Mode started!");
            return View(model);
        }

        // POST: WorkModeController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, WorkModesToUser workModes)
        //{
        //    var namesWorkModes = WorkModeNamesList.GetAll();
        //    //var viewWorkMode = new WorkModeViewModel();
        //    //viewWorkMode.GetAllWorkModeNames=new List<SelectListItem>();
            
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return View(id);
        //        }
        //        foreach (var name in namesWorkModes)
        //        {
        //            viewWorkMode.GetAllWorkModeNames.Add(new SelectListItem { Text = name.Name, Value = name.Id.ToString() });
        //        }
        //        //WorkModeService workModeService = new WorkModeService();
        //        _workModeService.Edit(viewWorkMode,workModes);
        //        Debug.WriteLine("Modified Work Mode!");
        //        TempData["Success"] = "Work Mode has been modified successfully!";
        //        return RedirectToAction(nameof(Index));                
        //    }
        //    catch 
        //    {
        //        return View();
        //    }
        //}

        // GET: WorkModeController/Delete/5
        public ActionResult Delete(int id)
        {
            //WorkModeService workModeService = new WorkModeService();
            var model = _workModeService.GetWorkModeById(id);
            Debug.WriteLine($"Removing Work Mode started!");
            return View(model);
        }

        // POST: WorkModeController/Delete/5
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
                //WorkModeService workModeService = new WorkModeService();
                _workModeService.Delete(id);
                Debug.WriteLine("Removed Work Mode!");
                TempData["Success"] = "Work Mode has been removed successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Statistics()
        {
            return View();
        }
    }
}
