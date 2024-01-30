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
        [Route("WorkModeViewModel")]
        public ActionResult Index()
        {
            var workmodename = WorkModeNamesList.GetAll();
            var model = new WorkModeViewModel();
            model.GetAllWorkModeNames = new List<SelectListItem>();

            foreach(var name in workmodename)
            {
                model.GetAllWorkModeNames.Add(new SelectListItem { Text = name.Name });
            }

            return View(model);
        }

        // GET: WorkModeController/Details/5
        public IActionResult Details(int id)
        {
            var workmode = _repository.GetAllWorkModes()[id];
            return View(workmode);
        }

        // GET: WorkModeController/Create
        public ActionResult Create()
        {
            Debug.WriteLine($"Creating Work Mode started!");
            return View();
        }

        // POST: WorkModeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WorkModesToUser workModes)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(workModes);
                }
                WorkModeService workModeService = new WorkModeService();
                workModeService.Create(workModes);
                Debug.WriteLine("Created Work Mode!");
                TempData["Success"] = "Work Mode has been created successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception exc)
            {
                Debug.WriteLine($"Exception occured {exc.Message}");
                return View();
            }
        }

        // GET: WorkModeController/Edit/5
        public ActionResult Edit(int id)
        {
            //WorkModeService workModeService = new WorkModeService();
            var model = _workModeService.GetWorkModeById(id);
            Debug.WriteLine($"Deleting Work Mode started!");
            return View(model);
        }

        // POST: WorkModeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, WorkModesToUser workModes)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(id);
                }
                //WorkModeService workModeService = new WorkModeService();
                _workModeService.Edit(workModes);
                Debug.WriteLine("Modified Work Mode!");
                TempData["Success"] = "Work Mode has been modified successfully!";
                return RedirectToAction(nameof(Index));                
            }
            catch 
            {
                return View();
            }
        }

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
    }
}
