using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Schedulist.App.Services;
using Schedulist.App.Services.Interfaces;
using Schedulist.App.ViewModels;
using Schedulist.DAL;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories;
using Schedulist.DAL.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
namespace Schedulist.App.Controllers
{
    public class StatisticsController : ControllerBase
    {
        public readonly SchedulistDbContext _context;

        public StatisticsController(SchedulistDbContext context, ILogger<StatisticsController> logger) : base(logger)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var workModesCurrentMonthByUser = _context.WorkModesToUsers.Include(w => w.User)
                .Where(wm => wm.DateOfWorkMode.Month == DateTime.Now.Month)
                .GroupBy(wm => wm.User.Name)
                .Select(group => new { Name = group.Key, Count = group.Count() })
                .ToList();

            var labels = workModesCurrentMonthByUser.Select(wm => wm.Name).ToArray();
            var data = workModesCurrentMonthByUser.Select(wm => wm.Count).ToArray();

            ViewBag.Labels = labels;
            ViewBag.Data = data;


            var homeOfficePerUser = _context.WorkModesToUsers
                .Where(wm => wm.DateOfWorkMode.Month == DateTime.Now.Month && wm.WorkModeId == 2)
                .GroupBy(wm => wm.DateOfWorkMode)
                .Select(group => new { Date = group.Key, Count = group.Count() })
                .ToList();

            var labels2 = homeOfficePerUser.Select(wm => wm.Date).ToArray();
            var data2 = homeOfficePerUser.Select(wm => wm.Count).ToArray();

            ViewBag.Labels2 = labels2;
            ViewBag.Data2 = data2;

            var sickLeavesCurrentMonth = _context.WorkModesToUsers
                .Where(wm => wm.DateOfWorkMode.Month == DateTime.Now.Month && wm.WorkModeId == 3)
                .GroupBy(wm => wm.DateOfWorkMode)
                .Select(group => new { Date = group.Key, Count = group.Count() })
                .ToList();

            var labels3 = sickLeavesCurrentMonth.Select(wm => wm.Date).ToArray();
            var data3 = sickLeavesCurrentMonth.Select(wm => wm.Count).ToArray();

            ViewBag.Labels3 = labels3;
            ViewBag.Data3 = data3;

            var HolidayCurrentMonth = _context.WorkModesToUsers
                .Where(wm => wm.DateOfWorkMode.Month == DateTime.Now.Month && wm.WorkModeId == 5)
                .GroupBy(wm => wm.DateOfWorkMode)
                .Select(group => new { Date = group.Key, Count = group.Count() })
                .ToList();

            var labels4 = HolidayCurrentMonth.Select(wm => wm.Date).ToArray();
            var data4 = HolidayCurrentMonth.Select(wm => wm.Count).ToArray();

            ViewBag.Labels4 = labels4;
            ViewBag.Data4 = data4;

            return View();
        }
        public IActionResult DataFromDatabase()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
