using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Schedulist.App.Models.Enum;

namespace Schedulist.App.Controllers
{
    public class ControllerBase : Controller
    {
        public readonly ILogger logger;
        public ControllerBase(ILogger logger)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            this.logger = logger;
        }
        protected void PopupNotification(string title, string message = "", NotificationType notificationType = NotificationType.success)
        {
            var msg = new
            {
                title = title,
                message = message,
                type = notificationType.ToString(),
            };

            TempData["Message"] = JsonConvert.SerializeObject(msg);
        }
        protected T PickTempDataValue<T>(string paramName)
        {
            var value = TempData.Peek(paramName);
            if (value == null)
                throw new ArgumentNullException(paramName,"Value has not been found");           

            return (T)value;
        }
        protected ActionResult HandleValueTempDataNotFound(string paramName)
        {
            logger.LogError($"TempData value not found for argument: {paramName}");
            return View();
        }
      
    }
}
