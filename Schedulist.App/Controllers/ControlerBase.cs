using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Schedulist.App.Models.Enum;

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
        public void Notify(string title, string message = "", NotificationType notificationType = NotificationType.success)
        {
            var msg = new
            {
                title = title,
                message = message,
                type = notificationType.ToString(),
            };

            TempData["Message"] = JsonConvert.SerializeObject(msg);
        }
    }
}
