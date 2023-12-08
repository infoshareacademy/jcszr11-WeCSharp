using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Schedulist.App.Controllers
{
    public class CalendarEventController : ControlerBase
    {
        public CalendarEventController(ILogger<CalendarEventController> logger) : base(logger) { }
        // GET: CalendarEventController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CalendarEventController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CalendarEventController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CalendarEventController/Create
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

        // GET: CalendarEventController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CalendarEventController/Edit/5
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

        // GET: CalendarEventController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CalendarEventController/Delete/5
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
