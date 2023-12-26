using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Schedulist.App.Controllers
{
    public class WorkModeController : ControlerBase
    {
        public WorkModeController(ILogger<WorkModeController> logger) : base(logger) { }
        // GET: WorkModeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: WorkModeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WorkModeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkModeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WorkModeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WorkModeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
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
            return View();
        }

        // POST: WorkModeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
