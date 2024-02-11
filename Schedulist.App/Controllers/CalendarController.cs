using Microsoft.AspNetCore.Mvc;
using Schedulist.App.ViewModels;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Schedulist.App.Controllers
{
    public class CalendarController : ControlerBase
    {
        private User _user;
        //private List<User> _userRepository;
        private readonly CalendarEvent _newCalendarEvent;
        private MonthViewModel? _monthViewModel;
        private readonly IWorkModeForUserRepository _workModesRepository;
        private readonly ICalendarEventRepository _calendarEventRepository;
        private readonly IUserRepository _userRepository;
        private Dictionary<string, int> _userDict = new Dictionary<string, int>();
        private MonthViewModel _calendarParams;
        public CalendarController(ILogger<CalendarController> logger, User user/*, List<User> users*/, IWorkModeForUserRepository workModesRepository, ICalendarEventRepository calendarEventRepository, IUserRepository userRepository) : base(logger)
        {
            _user = user;
            //_userRepository = users;
            _workModesRepository = workModesRepository;
            _calendarEventRepository = calendarEventRepository;
            _userRepository = userRepository;
            _newCalendarEvent = new CalendarEvent();
            foreach (User userToAdd in _userRepository.GetAllUsers())
            {
                _userDict.Add($"{userToAdd.Name} {userToAdd.Surname}", userToAdd.Id);
            }
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            int userToChangeId = _user.Id;
            List<CalendarEvent> allCalendarEvents = _calendarEventRepository.GetAllCalendarEvents();
            var calendarEventsToDraw = allCalendarEvents.Where(e => e.UserId == userToChangeId && e.CalendarEventDate.Month == DateTime.Now.Month).ToList();
            //foreach (CalendarEvent calendarEvent in allCalendarEvents)
            //{
            //    if (calendarEvent.UserId == _user.Id && calendarEvent.CalendarEventDate.Month == DateTime.Now.Month)
            //        allCalendarEvents.Add(calendarEvent);
            //}
            //_monthViewModel = new MonthViewModel(calendarEvents);
            //return View(_monthViewModel);
            Debug.WriteLine($"Drawing calendar for: {_monthViewModel.CurrentDate:y}");
            _calendarParams = new MonthViewModel(calendarEventsToDraw, _userDict, userToChangeId);
            return View(_calendarParams);
        }

        public IActionResult PreviousMonth(DateTime date, int userToEdit)
        {
            List<CalendarEvent> allCalendarEvents = _calendarEventRepository.GetAllCalendarEvents();
            _user = _userRepository.GetAllUsers().First(obj => obj.Id == userToEdit);
            var calendarEventsToDraw = allCalendarEvents.Where(e => e.UserId == _user.Id && e.CalendarEventDate.Month == date.AddMonths(-1).Month).ToList();
            //foreach (CalendarEvent calendarEvent in calendarEvents)
            //{
            //    if (calendarEvent.UserId == _user.Id && calendarEvent.CalendarEventDate.Month == date.AddMonths(-1).Month) calendarEventsToDraw.Add(calendarEvent);
            //}
            Debug.WriteLine($"Drawing calendar for: {_monthViewModel.CurrentDate:y}");
            _calendarParams = new MonthViewModel(date.AddMonths(-1), calendarEventsToDraw, _userDict, userToEdit);
            return View("Index", _calendarParams);
        }
        public IActionResult NextMonth(DateTime date, int userToEdit)
        {
            List<CalendarEvent> allCalendarEvents = _calendarEventRepository.GetAllCalendarEvents();
            _user = _userRepository.GetAllUsers().First(obj => obj.Id == userToEdit);
            var calendarEventsToDraw = allCalendarEvents.Where(e => e.UserId == _user.Id && e.CalendarEventDate.Month == date.AddMonths(+1).Month).ToList();
            Debug.WriteLine($"Drawing calendar for: {_monthViewModel.CurrentDate:y}");       
            _calendarParams = new MonthViewModel(date.AddMonths(1), calendarEventsToDraw, _userDict, userToEdit);
            return View("Index", _calendarParams);
        }

        public IActionResult ChangeUser(DateTime date, int userToEdit)
        {
            _user = _userRepository.GetAllUsers().First(obj => obj.Id == userToEdit);
            List<CalendarEvent> allCalendarEvents = _calendarEventRepository.GetAllCalendarEvents();
            var calendarEventsToDraw = allCalendarEvents.Where(e => e.UserId == _user.Id && e.CalendarEventDate.Month == date.Month).ToList();
            //foreach (CalendarEvent calendarEvent in allCalendarEvents)
            //{
            //    if (calendarEvent.UserId == _user.Id && calendarEvent.CalendarEventDate.Month == date.Month) calendarEventsToDraw.Add(calendarEvent);
            //}
            _calendarParams = new MonthViewModel(date, calendarEventsToDraw, _userDict, userToEdit);
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
            WorkModeForUser workMode = _workModesRepository.GetWorkModeByUserIdAndDateOfWorkMode((int)_user.Id, dateOnly);
            string workModeString;
            if (workMode != null) workModeString = workMode.WorkMode.Name;
            else workModeString = "No work mode";
            List<CalendarEvent> calendarEvents = _calendarEventRepository.GetAllCalendarEvents();
            var calendarEventsToDraw = calendarEvents.Where(c => c.UserId == _user.Id && c.CalendarEventDate == dateOnly).ToList();
            //foreach (CalendarEvent calendarEvent in calendarEvents)
            //{
            //    if (calendarEvent.UserId == _user.Id && calendarEvent.CalendarEventDate == dateOnly) calendarEventsToDraw.Add(calendarEvent);
            //}
            var dayViewModel = new DayViewModel(dateOnly, _user, workModeString, calendarEventsToDraw);
            Debug.WriteLine($"Drawing calendar day for: {dateOnly}");
            return View(dayViewModel);
        }

        //GET: CalendarController/Create
        public ActionResult Create(int id)
        {
            var calendarEvent = _newCalendarEvent;
            calendarEvent.UserId = id;

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
                if (!ModelState.IsValid)
                {
                    return View(calendarEvent);
                }
              
                DateTime selectedDate = (DateTime)TempData.Peek("SelectedDate");
                DateOnly parsedChosenDate = DateOnly.FromDateTime(selectedDate);

                calendarEvent.UserId = (int)TempData.Peek("UserId");
                calendarEvent.CalendarEventDate = parsedChosenDate;
                var validationResult = _calendarEventRepository.CalendarEventStartTimeOverlappingValidation(calendarEvent.CalendarEventDate, calendarEvent.CalendarEventStartTime, calendarEvent.CalendarEventEndTime, calendarEvent.UserId);
                if (validationResult != ValidationResult.Success)
                {
                    ModelState.AddModelError(nameof(calendarEvent.CalendarEventStartTime), validationResult.ErrorMessage);
                    return View(calendarEvent);
                }
                _calendarEventRepository.CreateCalendarEvent(calendarEvent);
                Debug.WriteLine($"Created Calendar Event.");
                PopupNotification("Calendar event has been created successfully");
                var returnUrl = TempData["ReturnUrl"] as string;
                return Redirect(returnUrl);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception occurred: {ex.Message}");
                return View();
            }
        }

        // GET: WorkModeController/Create
        public ActionResult CreateWM()
        {
            Debug.WriteLine($"Creating Work Mode started!");
            return View();
        }

        // POST: WorkModeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateWM(WorkModeForUser workModesToUser)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(workModesToUser);
                }

                DateTime selectedDate = (DateTime)TempData.Peek("SelectedDateForWM");
                DateOnly parsedChosenDate = DateOnly.FromDateTime(selectedDate);

                workModesToUser.UserId = (int)TempData.Peek("UserId");
                workModesToUser.DateOfWorkMode = parsedChosenDate;
                _workModesRepository.CreateWorkModeForUser(workModesToUser);
                Debug.WriteLine("Created new work mode!");
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
