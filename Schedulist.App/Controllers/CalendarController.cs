using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Schedulist.App.Services;
using Schedulist.App.Services.Interfaces;
using Schedulist.App.ViewModels;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.Principal;


namespace Schedulist.App.Controllers
{
    public class CalendarController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWorkModeForUserRepository _workModeForUserRepository;
        private readonly IWorkModeRepository _workModeRepository;
        private readonly ICalendarEventRepository _calendarEventRepository;
        private readonly ICalendarEventService _calendarEventService;
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<User> _signInManager;
        private Dictionary<string, string> _userDict = [];
        private MonthViewModel _calendarParams;
        public CalendarController(IHttpContextAccessor httpContextAccessor, ILogger<CalendarController> logger, IWorkModeForUserRepository workModeForUserRepository, IWorkModeRepository workModeRepository, ICalendarEventRepository calendarEventRepository, ICalendarEventService calendarEventService, IUserRepository userRepository) : base(logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _workModeForUserRepository = workModeForUserRepository;
            _workModeRepository = workModeRepository;
            _calendarEventRepository = calendarEventRepository;
            _calendarEventService = calendarEventService;
            _userRepository = userRepository;
            foreach (User userToAdd in _userRepository.GetAllUsers())
            {
                _userDict.Add(userToAdd.Id, $"{userToAdd.Name} {userToAdd.Surname}");
            }
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            var userManager = HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
            var user = userManager.GetUserAsync(HttpContext.User).Result;

            //var user = _httpContextAccessor.HttpContext?.User;

            string userToChangeId = user.Id;
            List<CalendarEvent> allCalendarEvents = _calendarEventRepository.GetAllCalendarEvents();
            var calendarEventsToDraw = allCalendarEvents.Where(calendarEvent => calendarEvent.UserId == userToChangeId && calendarEvent.CalendarEventDate.Month == DateTime.Now.Month).ToList();
            List<WorkModeForUser> workModesToDraw = _workModeForUserRepository.GetAllWorkModesForUser().Where(e => e.DateOfWorkMode.Month == DateTime.Now.Month && e.UserId == userToChangeId).ToList();
            var userNameDisplay = $"{user?.Name}";
            _calendarParams = new MonthViewModel(calendarEventsToDraw, _userDict, userToChangeId, userNameDisplay, _workModeRepository, workModesToDraw);
            return View(_calendarParams);
        }

        public IActionResult PreviousMonth(DateTime date, string userToEdit)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            var userManager = HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
            var user = userManager.GetUserAsync(HttpContext.User).Result;

            List<CalendarEvent> allCalendarEvents = _calendarEventRepository.GetAllCalendarEvents();
            user = _userRepository.GetAllUsers().First(obj => obj.Id == userToEdit);
            var userNameDisplay = $"{user?.Name}";
            var calendarEventsToDraw = allCalendarEvents.Where(e => e.UserId == user.Id && e.CalendarEventDate.Month == date.AddMonths(-1).Month).ToList();
            List<WorkModeForUser> workModesToDraw = _workModeForUserRepository.GetAllWorkModesForUser().Where(e => e.DateOfWorkMode.Month == date.AddMonths(-1).Month && e.UserId == userToEdit).ToList();
            _calendarParams = new MonthViewModel(date.AddMonths(-1), calendarEventsToDraw, _userDict, userToEdit, _workModeRepository, workModesToDraw, userNameDisplay);
            return View("Index", _calendarParams);
        }
        public IActionResult NextMonth(DateTime date, string userToEdit)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            var userManager = HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
            var user = userManager.GetUserAsync(HttpContext.User).Result;

            List<CalendarEvent> allCalendarEvents = _calendarEventRepository.GetAllCalendarEvents();
            user = _userRepository.GetAllUsers().First(obj => obj.Id == userToEdit);
            var userNameDisplay = $"{user?.Name}";
            var calendarEventsToDraw = allCalendarEvents.Where(e => e.UserId == user.Id && e.CalendarEventDate.Month == date.AddMonths(+1).Month).ToList();
            List<WorkModeForUser> workModesToDraw = _workModeForUserRepository.GetAllWorkModesForUser().Where(e => e.DateOfWorkMode.Month == date.AddMonths(1).Month && e.UserId == userToEdit).ToList();
            _calendarParams = new MonthViewModel(date.AddMonths(1), calendarEventsToDraw, _userDict, userToEdit, _workModeRepository, workModesToDraw, userNameDisplay);
            return View("Index", _calendarParams);
        }

        public IActionResult ChangeUser(DateTime date, string userToEdit)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            var userManager = HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
            var user = userManager.GetUserAsync(HttpContext.User).Result;

            user = _userRepository.GetAllUsers().First(obj => obj.Id == userToEdit);
            List<CalendarEvent> allCalendarEvents = _calendarEventRepository.GetAllCalendarEvents();
            var calendarEventsToDraw = allCalendarEvents.Where(e => e.UserId == user.Id && e.CalendarEventDate.Month == date.Month).ToList();
            var userNameDisplay = $"{user?.Name}";
            List<WorkModeForUser> workModesToDraw = _workModeForUserRepository.GetAllWorkModesForUser().Where(e => e.DateOfWorkMode.Month == date.Month && e.UserId == userToEdit).ToList();
            _calendarParams = new MonthViewModel(date, calendarEventsToDraw, _userDict, userToEdit, _workModeRepository, workModesToDraw, userNameDisplay);
            return View("Index", _calendarParams);
        }

        public IActionResult UpdateWorkMode(DateTime date, string userToEdit, int workModeId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            var userManager = HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
            var user = userManager.GetUserAsync(HttpContext.User).Result;

            List<CalendarEvent> allCalendarEvents = _calendarEventRepository.GetAllCalendarEvents();
            user = _userRepository.GetAllUsers().First(obj => obj.Id == userToEdit);
            var userNameDisplay = $"{user?.Name}";
            var calendarEventsToDraw = allCalendarEvents.Where(e => e.UserId == user.Id && e.CalendarEventDate.Month == date.Month).ToList();

            var allWorkModesForUser = _workModeForUserRepository.GetAllWorkModesForUser();
            bool workModeIsUpdated = false;
            foreach (var workModeForUser in allWorkModesForUser)
            {
                if (workModeForUser.UserId == userToEdit && workModeForUser.DateOfWorkMode == DateOnly.FromDateTime(date))
                {
                    _workModeForUserRepository.UpdateWorkModeForUser(workModeForUser.Id ,new WorkModeForUser()
                    {
                        DateOfWorkMode = DateOnly.FromDateTime(date),
                        UserId = userToEdit,
                        WorkModeId = workModeId
                    });
                    workModeIsUpdated = true;
                }
            }
            if (!workModeIsUpdated)
            {
                _workModeForUserRepository.CreateWorkModeForUser(new WorkModeForUser()
                {
                    DateOfWorkMode = DateOnly.FromDateTime(date),
                    UserId = userToEdit,
                    WorkModeId = workModeId
                });
            }

            List<WorkModeForUser> workModesToDraw = _workModeForUserRepository.GetAllWorkModesForUser().Where(e => e.DateOfWorkMode.Month == date.Month && e.UserId == userToEdit).ToList();
            _calendarParams = new MonthViewModel(date, calendarEventsToDraw, _userDict, userToEdit, _workModeRepository, workModesToDraw, userNameDisplay);

            return View("Index", _calendarParams);
        }

        public IActionResult Day (DateTime date, string userToEdit)
        {
            var userManager = HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
            var user = userManager.GetUserAsync(HttpContext.User).Result;

            if (userToEdit != string.Empty)
            {
                user = _userRepository.GetAllUsers().First(obj => obj.Id == userToEdit);
            }

            TempData["ReturnUrl"] = HttpContext.Request.Path + HttpContext.Request.QueryString;
            TempData["ReturnUrlWM"] = HttpContext.Request.Path + HttpContext.Request.QueryString;
            TempData["SelectedDate"] = date;
            TempData["SelectedDateForWM"] = date;
            TempData["DayDate"] = date;
            TempData["DayDateWM"] = date;
            TempData["UserId"] = userToEdit;
            TempData["UserIdForWM"] = userToEdit;
            TempData["UserDetails"] = $"{user.Name} {user.Surname}";
            TempData["UserDetailsWM"] = $"{user.Name} {user.Surname}";
            DateOnly dateOnly = DateOnly.FromDateTime(date);
            WorkModeForUser workMode = _workModeForUserRepository.GetWorkModeByUserIdAndDateOfWorkMode(user.Id, dateOnly);
            string workModeString = "No work mode";
            if (workMode != null) workModeString = _workModeRepository.GetAllWorkModes().Where(x => x.Id == workMode.WorkModeId).FirstOrDefault().Name;
            //if (workMode != null) workModeString = _workModeRepository.GetWorkModeById(workMode.WorkModeId).Name;
            List<CalendarEvent> calendarEvents = _calendarEventRepository.GetAllCalendarEvents();
            var calendarEventsToDraw = calendarEvents.Where(calendarEvent => calendarEvent.UserId == user.Id && calendarEvent.CalendarEventDate == dateOnly).ToList();
            var dayViewModel = new DayViewModel(dateOnly, user, workModeString, calendarEventsToDraw, HttpContext.Request.Path + HttpContext.Request.QueryString);
            logger.LogInformation($"Drawing calendar day for: {dateOnly}");
            return View(dayViewModel);
        }

        public IActionResult Week(DateTime date, string userToEdit)
        {
            var userManager = HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
            var user = userManager.GetUserAsync(HttpContext.User).Result;

            if (userToEdit != string.Empty)
            {
                user = _userRepository.GetAllUsers().First(obj => obj.Id == userToEdit);
            }
            DateOnly dateOnly = DateOnly.FromDateTime(date);
            List<CalendarEvent> calendarEvents = _calendarEventRepository.GetAllCalendarEvents();
            DateOnly startOfWeek = DateOnly.FromDateTime(date);
            while (true)
            {
                if (startOfWeek.DayOfWeek == DayOfWeek.Monday)
                {
                    break;
                }
                else
                {
                    startOfWeek = startOfWeek.AddDays(-1);
                }
            }
            DateOnly endOfWeek = DateOnly.FromDateTime(date);
            while (true)
            {
                if (endOfWeek.DayOfWeek == DayOfWeek.Sunday)
                {
                    break;
                }
                else
                {
                    endOfWeek = endOfWeek.AddDays(1);
                }
            }
            List<WorkModeForUser> workModesToDraw = _workModeForUserRepository.GetAllWorkModesForUser().Where(p => p.UserId == userToEdit && p.DateOfWorkMode >= startOfWeek && p.DateOfWorkMode <= endOfWeek).ToList();
            List<WorkMode> workModes = _workModeRepository.GetAllWorkModes().ToList();
            var calendarEventsToDraw = calendarEvents.Where(c => c.UserId == user.Id && c.CalendarEventDate > startOfWeek && c.CalendarEventDate < endOfWeek).ToList();
            var weekViewModel = new WeekViewModel(dateOnly, user, workModesToDraw, workModes, calendarEventsToDraw);
            Debug.WriteLine($"Drawing calendar week for: {weekViewModel.StartOfWeek} - {weekViewModel.EndOfWeek}");
            return View(weekViewModel);
        }

        public IActionResult WeekUpdateWorkMode(DateTime date, string userToEdit, int workModeId)
        {
            var userManager = HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
            var user = userManager.GetUserAsync(HttpContext.User).Result;

            if (userToEdit != string.Empty)
            {
                user = _userRepository.GetAllUsers().First(obj => obj.Id == userToEdit);
            }
            DateOnly dateOnly = DateOnly.FromDateTime(date);
            List<CalendarEvent> calendarEvents = _calendarEventRepository.GetAllCalendarEvents();
            DateOnly startOfWeek = DateOnly.FromDateTime(date);
            while (true)
            {
                if (startOfWeek.DayOfWeek == DayOfWeek.Monday)
                {
                    break;
                }
                else
                {
                    startOfWeek = startOfWeek.AddDays(-1);
                }
            }
            DateOnly endOfWeek = DateOnly.FromDateTime(date);
            while (true)
            {
                if (endOfWeek.DayOfWeek == DayOfWeek.Sunday)
                {
                    break;
                }
                else
                {
                    endOfWeek = endOfWeek.AddDays(1);
                }
            }
            var allWorkModesForUser = _workModeForUserRepository.GetAllWorkModesForUser();
            bool workModeIsUpdated = false;
            foreach (var workModeForUser in allWorkModesForUser)
            {
                if (workModeForUser.UserId == userToEdit && workModeForUser.DateOfWorkMode == DateOnly.FromDateTime(date))
                {
                    _workModeForUserRepository.UpdateWorkModeForUser(workModeForUser.Id, new WorkModeForUser()
                    {
                        DateOfWorkMode = DateOnly.FromDateTime(date),
                        UserId = userToEdit,
                        WorkModeId = workModeId
                    });
                    workModeIsUpdated = true;
                }
            }
            if (!workModeIsUpdated)
            {
                _workModeForUserRepository.CreateWorkModeForUser(new WorkModeForUser()
                {
                    DateOfWorkMode = DateOnly.FromDateTime(date),
                    UserId = userToEdit,
                    WorkModeId = workModeId
                });
            }
            List<WorkModeForUser> workModesToDraw = _workModeForUserRepository.GetAllWorkModesForUser().Where(p => p.UserId == userToEdit && p.DateOfWorkMode >= startOfWeek && p.DateOfWorkMode <= endOfWeek).ToList();
            List<WorkMode> workModes = _workModeRepository.GetAllWorkModes().ToList();
            var calendarEventsToDraw = calendarEvents.Where(c => c.UserId == user.Id && c.CalendarEventDate > startOfWeek && c.CalendarEventDate < endOfWeek).ToList();
            var weekViewModel = new WeekViewModel(dateOnly, user, workModesToDraw, workModes, calendarEventsToDraw);
            Debug.WriteLine($"Drawing calendar week for: {weekViewModel.StartOfWeek} - {weekViewModel.EndOfWeek}");
            return View("Week", weekViewModel);
        }

        //GET: CalendarController/Create
        public ActionResult Create(int id)
        {
            var userManager = HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
            var user = userManager.GetUserAsync(HttpContext.User).Result;

            //var calendarEvent = new CalendarEvent();
            //calendarEvent.UserId = user.Id;
            //var calendarEvent = new CalendarEvent();
            //calendarEvent.UserId = _user.Id;

            Debug.WriteLine($"Creating Calendar Event started.");
            return View();
        }

        // POST: CalendarController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CalendarEvent calendarEvent)
        {
            try
            {
                DateTime selectedDate = PickTempDataValue<DateTime>("SelectedDate");
                DateOnly parsedChosenDate = DateOnly.FromDateTime(selectedDate);

                calendarEvent.UserId = TempData.Peek("UserId").ToString();
                calendarEvent.CalendarEventDate = parsedChosenDate;
                var validationResults = _calendarEventService.ValidateCalendarEvent(calendarEvent);
                if (validationResults.Any(x => x != ValidationResult.Success))
                {
                    validationResults.Where(x => x != ValidationResult.Success).ToList().ForEach(x => ModelState.AddModelError(nameof(calendarEvent.CalendarEventEndTime), x.ErrorMessage));
                    return View(calendarEvent);
                }
                _calendarEventRepository.CreateCalendarEvent(calendarEvent);
                logger.LogInformation($"Created Calendar Event.");
                PopUpNotification("Calendar event has been created successfully");
                var returnUrl = TempData["ReturnUrl"] as string;
                return Redirect(returnUrl);
            }
            catch (ArgumentNullException ex)
            {
                return HandleValueTempDataNotFound(ex.ParamName!);
            }
            catch (Exception ex)
            {
                logger.LogInformation($"Exception occurred: {ex.Message}");
                return View();
            }
        }

        // GET: WorkModeController/Create
        public ActionResult CreateWM()
        {
            logger.LogInformation($"Creating Work Mode started!");
            return View();
        }

        // POST: WorkModeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateWM(WorkModeForUser workModesToUser)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return View(workModesToUser);
                //}
                DateTime selectedDate = PickTempDataValue<DateTime>("SelectedDateForWM");
                DateOnly parsedChosenDate = DateOnly.FromDateTime(selectedDate);

                workModesToUser.UserId = (string)TempData.Peek("UserId")!;
                workModesToUser.DateOfWorkMode = parsedChosenDate;
                _workModeForUserRepository.CreateWorkModeForUser(workModesToUser);
                logger.LogInformation("Created new work mode!");
                PopUpNotification("Work mode has been created successfully");
                var returnUrl = TempData["ReturnUrlWM"] as string;
                return Redirect(returnUrl);
            }
            catch
            {
                return View();
            }
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

        // GET: CalendarController/Edit/5
        [HttpGet]
        [ResponseCache(Duration = 30, NoStore = true)]
        public ActionResult Edit(int id)
        {
            var model = _calendarEventRepository.GetCalendarEventById(id);
            logger.LogInformation($"Updating Calendar Event started.");
            return View("Edit", model);
        }

        //PUT: CalendarController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CalendarEvent calendarEvent)
        {
            try
            {
                var validationResults = _calendarEventService.ValidateCalendarEvent(calendarEvent);
                if (validationResults.Any(x => x != ValidationResult.Success))
                {
                    validationResults.Where(x => x != ValidationResult.Success).ToList().ForEach(x => ModelState.AddModelError(nameof(calendarEvent.CalendarEventEndTime), x.ErrorMessage));
                    return View(calendarEvent);
                }
                _calendarEventRepository.UpdateCalendarEvent(id, calendarEvent);
                logger.LogInformation($"Modified Calendar Event.");
                PopUpNotification("Calendar event has been updated successfully");
                return RedirectToAction(nameof(Index));

                //var returnUrl = TempData["ReturnUrl"] as string;
                //return Redirect(returnUrl);
            }
            catch
            {
                return View();
            }
        }
    }
}
