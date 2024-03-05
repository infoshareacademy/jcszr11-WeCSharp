using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Schedulist.App.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        public AdminController(ILogger logger) : base(logger)
        {

        }
    }
}
