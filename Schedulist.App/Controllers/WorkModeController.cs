using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Schedulist.DAL;

namespace Schedulist.App.Controllers
{
    public class WorkModeController : ControlerBase
    {
        public WorkModeController(ILogger<WorkModeController> logger/*, CSVWorkModesRepository repository*/) : base(logger) 
        {
            //this.repository = repository;
        }
        //private readonly CSVWorkModesRepository repository;
        //// GET: WorkModeController
        //[Route("WorkModesToUser")]
        //public IActionResult Index()
        //{
        //    var model = repository.GetAllWorkModes();
        //    return View(model);
        //}

        //// GET: WorkModeController/Details/5
        //public IActionResult Details(int id)
        //{
        //    var workmode = repository.GetAllWorkModes()[id];
        //    return View(workmode);
        //}

        //// GET: WorkModeController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: WorkModeController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: WorkModeController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: WorkModeController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: WorkModeController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: WorkModeController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
