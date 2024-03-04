using Microsoft.AspNetCore.Mvc;
using Schedulist.App.Services;
using Schedulist.App.Services.Interfaces;
using Schedulist.App.ViewModels;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Schedulist.App.Controllers
{
    public class CalendarController : ControllerBase
    {
        private User _user;
        private readonly IWorkModeForUserRepository _workModeForUserRepository;
        private readonly IWorkModeRepository _workModeRepository;
        private readonly ICalendarEventRepository _calendarEventRepository;
        private readonly ICalendarEventService _calendarEventService;
        private readonly IUserRepository _userRepository;
        private Dictionary<string, int> _userDict = [];
        private MonthViewModel _calendarParams;
        public CalendarController(ILogger<CalendarController> logger, User user, IWorkModeForUserRepository workModeForUserRepository, IWorkModeRepository workModeRepository, ICalendarEventRepository calendarEventRepository, ICalendarEventService calendarEventService, IUserRepository userRepository) : base(logger)
        {
            _user = user;
            _workModeForUserRepository = workModeForUserRepository;
            _workModeRepository = workModeRepository;
            _calendarEventRepository = calendarEventRepository;
            _calendarEventService = calendarEventService;
            _userRepository = userRepository;
            foreach (User userToAdd in _userRepository.GetAllUsers())
            {
                _userDict.Add($"{userToAdd.Name} {userToAdd.Surname}", userToAdd.Id);
            }
        }

        public IActionResult Index()
        {
            int userToChangeId = _user.Id;
            List<CalendarEvent> allCalendarEvents = _calendarEventRepository.GetAllCalendarEvents();
            var calendarEventsToDraw = allCalendarEvents.Where(calendarEvent => calendarEvent.UserId == userToChangeId && calendarEvent.CalendarEventDate.Month == DateTime.Now.Month).ToList();
            List<WorkModeForUser> workModesToDraw = _workModeForUserRepository.GetAllWorkModesForUser().Where(e => e.DateOfWorkMode.Month == DateTime.Now.Month && e.UserId == userToChangeId).ToList();
            _calendarParams = new MonthViewModel(calendarEventsToDraw, _userDict, userToChangeId, _workModeRepository, workModesToDraw);
            return View(_calendarParams);
        }

        public IActionResult PreviousMonth(DateTime date, int userToEdit)
        {
            List<CalendarEvent> allCalendarEvents = _calendarEventRepository.GetAllCalendarEvents();
            _user = _userRepository.GetAllUsers().First(obj => obj.Id == userToEdit);
            var calendarEventsToDraw = allCalendarEvents.Where(e => e.UserId == _user.Id && e.CalendarEventDate.Month == date.AddMonths(-1).Month).ToList();
            List<WorkModeForUser> workModesToDraw = _workModeForUserRepository.GetAllWorkModesForUser().Where(e => e.DateOfWorkMode.Month == date.AddMonths(-1).Month && e.UserId == userToEdit).ToList();
            _calendarParams = new MonthViewModel(date.AddMonths(-1), calendarEventsToDraw, _userDict, userToEdit, _workModeRepository, workModesToDraw);
            return View("Index", _calendarParams);
        }
        public IActionResult NextMonth(DateTime date, int userToEdit)
        {
            List<CalendarEvent> allCalendarEvents = _calendarEventRepository.GetAllCalendarEvents();
            _user = _userRepository.GetAllUsers().First(obj => obj.Id == userToEdit);
            var calendarEventsToDraw = allCalendarEvents.Where(e => e.UserId == _user.Id && e.CalendarEventDate.Month == date.AddMonths(+1).Month).ToList();
            List<WorkModeForUser> workModesToDraw = _workModeForUserRepository.GetAllWorkModesForUser().Where(e => e.DateOfWorkMode.Month == date.AddMonths(1).Month && e.UserId == userToEdit).ToList();
            _calendarParams = new MonthViewModel(date.AddMonths(1), calendarEventsToDraw, _userDict, userToEdit, _workModeRepository, workModesToDraw);
            return View("Index", _calendarParams);
        }

        public IActionResult ChangeUser(DateTime date, int userToEdit)
        {
            _user = _userRepository.GetAllUsers().First(obj => obj.Id == userToEdit);
            List<CalendarEvent> allCalendarEvents = _calendarEventRepository.GetAllCalendarEvents();
            var calendarEventsToDraw = allCalendarEvents.Where(e => e.UserId == _user.Id && e.CalendarEventDate.Month == date.Month).ToList();
            List<WorkModeForUser> workModesToDraw = _workModeForUserRepository.GetAllWorkModesForUser().Where(e => e.DateOfWorkMode.Month == date.Month && e.UserId == userToEdit).ToList();
            _calendarParams = new MonthViewModel(date, calendarEventsToDraw, _userDict, userToEdit, _workModeRepository, workModesToDraw);
            return View("Index", _calendarParams);
        }

        public IActionResult Day(DateTime date, int userToEdit)
        {
            if (userToEdit != 0)
            {
                _user = _userRepository.GetAllUsers().First(obj => obj.Id == userToEdit);
            }

            TempData["ReturnUrl"] = HttpContext.Request.Path + HttpContext.Request.QueryString;
            TempData["ReturnUrlWM"] = HttpContext.Request.Path + HttpContext.Request.QueryString;
            TempData["SelectedDate"] = date;
            TempData["SelectedDateForWM"] = date;
            TempData["DayDate"] = date;
            TempData["DayDateWM"] = date;
            TempData["UserId"] = userToEdit;
            TempData["UserIdForWM"] = userToEdit;
            TempData["UserDetails"] = $"{_user.Name} {_user.Surname}";
            TempData["UserDetailsWM"] = $"{_user.Name} {_user.Surname}";
            DateOnly dateOnly = DateOnly.FromDateTime(date);
            WorkModeForUser workMode = _workModeForUserRepository.GetWorkModeByUserIdAndDateOfWorkMode((int)_user.Id, dateOnly);
            string workModeString = "No work mode";
            //if (workMode != null) workModeString = _workModeRepository.GetAllWorkModes().Where(x => x.Id == workMode.WorkModeId).FirstOrDefault().Name;
            if (workMode != null) workModeString = _workModeRepository.GetWorkModeById(workMode.WorkModeId).Name;
            List<CalendarEvent> calendarEvents = _calendarEventRepository.GetAllCalendarEvents();
            var calendarEventsToDraw = calendarEvents.Where(calendarEvent => calendarEvent.UserId == _user.Id && calendarEvent.CalendarEventDate == dateOnly).ToList();
            var dayViewModel = new DayViewModel(dateOnly, _user, workModeString, calendarEventsToDraw);
            logger.LogInformation($"Drawing calendar day for: {dateOnly}");
            return View(dayViewModel);
        }

        public IActionResult Week(DateTime date, int userToEdit)
        {
            if (userToEdit != 0)
            {
                _user = _userRepository.GetAllUsers().First(obj => obj.Id == userToEdit);
            }

            //TempData["ReturnUrl"] = HttpContext.Request.Path + HttpContext.Request.QueryString;
            //TempData["ReturnUrlWM"] = HttpContext.Request.Path + HttpContext.Request.QueryString;
            //TempData["SelectedDate"] = date;
            //TempData["SelectedDateForWM"] = date;
            //TempData["DayDate"] = date;
            //TempData["DayDateWM"] = date;
            //TempData["UserId"] = userToEdit;
            //TempData["UserIdForWM"] = userToEdit;
            //TempData["UserDetails"] = $"{_user.Name} {_user.Surname}";
            //TempData["UserDetailsWM"] = $"{_user.Name} {_user.Surname}";

            DateOnly dateOnly = DateOnly.FromDateTime(date);
            WorkModeForUser workMode = _workModeForUserRepository.GetWorkModeByUserIdAndDateOfWorkMode((int)_user.Id, dateOnly);
            string workModeString = "No work mode";
            //if (workMode != null) workModeString = _workModeRepository.GetAllWorkModes().Where(x => x.Id == workMode.WorkModeId).FirstOrDefault().Name;
            if (workMode != null) workModeString = _workModeRepository.GetWorkModeById(workMode.WorkModeId).Name;
            List<CalendarEvent> calendarEvents = _calendarEventRepository.GetAllCalendarEvents();
            var calendarEventsToDraw = calendarEvents.Where(c => c.UserId == _user.Id && c.CalendarEventDate == dateOnly).ToList();
            var weekViewModel = new WeekViewModel(dateOnly, _user, workModeString, calendarEventsToDraw);
            Debug.WriteLine($"Drawing calendar day for: {dateOnly}");
            return View(weekViewModel);
        }

        //GET: CalendarController/Create
        public ActionResult Create(int id)
        {
            var calendarEvent = new CalendarEvent();
            calendarEvent.UserId = _user.Id;

            Debug.WriteLine($"Creating Calendar Event started.");
            return View(calendarEvent);
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

                calendarEvent.UserId = PickTempDataValue<int>("UserId"); 
                calendarEvent.CalendarEventDate = parsedChosenDate;
                var validationResults = _calendarEventService.ValidateCalendarEvent(calendarEvent);
                if (validationResults.Any(x => x != ValidationResult.Success))
                {
                    validationResults.Where(x => x != ValidationResult.Success).ToList().ForEach(x => ModelState.AddModelError(nameof(calendarEvent.CalendarEventEndTime), x.ErrorMessage));
                    return View(calendarEvent);
                }
                _calendarEventRepository.CreateCalendarEvent(calendarEvent);
                logger.LogInformation($"Created Calendar Event.");
                PopupNotification("Calendar event has been created successfully");
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

                workModesToUser.UserId = PickTempDataValue<int>("UserId");
                workModesToUser.DateOfWorkMode = parsedChosenDate;
                _workModeForUserRepository.CreateWorkModeForUser(workModesToUser);
                logger.LogInformation("Created new work mode!");
                PopupNotification("Work mode has been created successfully");
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
    }
}
