using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Schedulist.App.Controllers
{
    public class ControlerBase : Controller
    {
        public readonly ILogger logger;
        public ControlerBase(ILogger logger)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            this.logger = logger;
        }
    }
}
