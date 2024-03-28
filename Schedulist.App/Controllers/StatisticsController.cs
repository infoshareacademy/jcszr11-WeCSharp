using Microsoft.AspNetCore.Mvc;
using Schedulist.DAL.Models;

namespace Schedulist.App.Controllers
{
    public class StatisticsController : ControllerBase
    {
        public StatisticsController(ILogger logger) : base(logger)
        {
        }

        public IActionResult Index()
        {

            var statystyki = new List<Statistics>
        {
            new Statistics { Nazwa = "Statystyka 1", Wartosc = 10 },
            new Statistics{ Nazwa = "Statystyka 2", Wartosc = 20 },

        };

            return View();
        }
    }
}
