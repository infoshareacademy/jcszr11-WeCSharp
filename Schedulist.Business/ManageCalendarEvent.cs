using Schedulist.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using CsvHelper.TypeConversion;
using Schedulist.Business.Actions;

namespace Schedulist.Business
{
    public class ManageCalendarEvent  //IManageCalendarEvent
    {
        private List<CalendarEvent> _calendarEvents =
            new CsvCalendarEventRepository("..\\..\\..\\CalendarEvents.csv").GetAllCalendarEvents();
        private CsvCalendarEventRepository _csvCalendarEventRepository =
            new("..\\..\\..\\CalendarEvents.csv");
        private CSVWorkModesRepository _csvWorkModesRepository =
            new("..\\..\\..\\WorkModes.csv");
        DateOnly currentDate = DateOnly.FromDateTime(DateTime.Today);
        public void ShowCalendarEvent(User user, DateOnly date)
        {
            Console.WriteLine($"==========List of Calendar Events for {date}==========");
            Console.WriteLine("Start time \tEnd time \tName of Calendar Event");
            var calendarEvents = _csvCalendarEventRepository.GetAllCalendarEvents();
            var calendarEventSorted = calendarEvents
                .Where(c => c.AssignedToUser == user.Id && c.CalendarEventDate == date)
                .OrderBy(c => c.CalendarEventStartTime)
                .ToList();
            foreach (var calendarEvent in calendarEventSorted)
            {
                Console.WriteLine(
                    $"{calendarEvent.CalendarEventStartTime} \t{calendarEvent.CalendarEventEndTime} \t{calendarEvent.CalendarEventName}");
            }
            Console.WriteLine("========================================================");
        }
        public List<CalendarEvent> ShowUserCalendarEvent(User user, DateOnly providedDate)
        {
            Console.Clear();
            Console.WriteLine("==========List of Calendar Events==========");
            Console.WriteLine("Provide date for which you want to show Calendar Events using format DD.MM.YYYY");
            //string providedDate = Console.ReadLine();
            //providedDate = DateValueEmptinessValidation(providedDate);
            //DateOnly.TryParse(providedDate, out var specifiedDate);
            var calendarEvents = _csvCalendarEventRepository.GetAllCalendarEvents();
            var userCalendarEvents = calendarEvents
                .Where(c => c.AssignedToUser == user.Id && c.CalendarEventDate == providedDate)
                .OrderBy(c => c.CalendarEventStartTime)
                .ToList();
            if (userCalendarEvents.Count == 0)
            {
                Console.WriteLine("There is no Calendar Event existing for chosen date!");
            }
            else
            {
                Console.WriteLine($"\nCalendar Events on {providedDate}:");
                Console.WriteLine($"Start time \t End time \t Calendar Event Name");
                foreach (var calendarEvent in userCalendarEvents)
                {
                    Console.WriteLine(
                        $"{calendarEvent.CalendarEventStartTime} \t\t {calendarEvent.CalendarEventEndTime} \t\t {calendarEvent.CalendarEventName}");
                }
            }
            Console.WriteLine("========================================================");
            Console.WriteLine("\nType any key do return to Menu");
            return userCalendarEvents;
            //Console.ReadKey();
        }
        public void CreateCalendarEvent(User user)
        {
            Console.Clear();
            Console.WriteLine("==========Creating new Calendar Event==========");
            Console.WriteLine("You are creating new Calendar Event, please provide data as following:");
            int calendarEventId = 1;
            var dateValue = "";
            string calendarEventName = Helper.ConsolHelper("Name of Calendar Event:");
            string calendarEventDescription = Helper.ConsolHelper("Description of the Calendar Event:");
            Console.WriteLine("Date of Calendar Event using format DD.MM.YYYY");
            var calendarEventDate = CalendarEventDateAddValidation(out dateValue);
            calendarEventDate = CalendarEventDateMinMaxValidation(calendarEventDate);
            calendarEventDate = CalendarEventDateWeekendValidation(calendarEventDate);
            calendarEventDate = CalendarEventDateRelatedToWorkModeValidation(user, calendarEventDate);
            var calendarEvents = _csvCalendarEventRepository.GetAllCalendarEvents();
            var calendarEventAvailable = calendarEvents
                .FirstOrDefault(c => c.AssignedToUser == user.Id &&
                                     c.CalendarEventDate == calendarEventDate);
            CalendarEventAvailabilityCheck(user, calendarEventAvailable, calendarEventDate);
            Console.WriteLine("Start time of Calendar Event using format HH:MM");
            var calendarEventStartTime = CalendarEventStartTimeParseValidation();
            var validatedStartTime = calendarEvents
                .FirstOrDefault(c => c.AssignedToUser == user.Id &&
                                     c.CalendarEventDate == calendarEventDate &&
                                     (c.CalendarEventStartTime == calendarEventStartTime ||
                                      c.CalendarEventEndTime.CompareTo(calendarEventStartTime) > 0));
            calendarEventStartTime = CalendarEventStartTimeOverlappingValidation(validatedStartTime,
                calendarEventStartTime, calendarEvents, calendarEventDate, user);
            Console.WriteLine("End time of Calendar Event using format HH:MM");
            string endTime = Console.ReadLine();
            endTime = EndTimeEmptinessValidation(endTime);
            TimeOnly.TryParse(endTime, out var calendarEventEndTime);
            calendarEventEndTime = CalendarEventEndTimeValidation(calendarEventEndTime, calendarEventStartTime);
            CalendarEvent calendarEvent = new(calendarEventId, calendarEventName,
                calendarEventDescription, calendarEventDate, calendarEventStartTime, calendarEventEndTime,
                (int)user.Id);
            _csvCalendarEventRepository.AddCalendarEvent(calendarEvent);
            Console.WriteLine("\nType any key do return to Menu");
            Console.ReadKey();
        }
        
        #region CalendarEvent - Modify Section
        public CalendarEvent GetCurrentCalendarEvent(User user)
        {
            List<CalendarEvent> currentUserCalendarEvents =
            _calendarEvents.Where(c => c.AssignedToUser == user.Id).ToList();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("====== List of Calendar Events ======");
                Console.WriteLine("ID:\tUserID:\tDate:\t\tName:\t ");
                foreach (var item in currentUserCalendarEvents)
                {
                    Console.WriteLine($"{item.CalendarEventId + 1}\t{item.AssignedToUser}\t{item.CalendarEventDate}\t{item.CalendarEventName}");
                }
                string input = Helper.ConsolHelper("Choose the ID of Calendar Event you want to modify (or 0 to cancel):");
                if (int.TryParse(input, out int calendarEventId) && calendarEventId > 0 && calendarEventId <= _calendarEvents.Count)
                {
                    var calendarEventToModify = currentUserCalendarEvents.First(c => c.CalendarEventId == (calendarEventId - 1));
                    return calendarEventToModify;
                }
                else if (input.ToLower() == "0")
                {
                    Console.WriteLine("Operation canceled.");
                    MenuOptions.MenuCalendarEvents();
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid choice");
                    break;
                }
            }
            string readKey = Helper.ConsolHelper("Press any key to return to the menu.");
            MenuOptions.MenuCalendarEvents();
            return null;
        }
        public void ModifyCurrentCalendarEvent(User user)
        {
            CalendarEvent calendarEventToModify = GetCurrentCalendarEvent(user);
            Console.Clear();
            Console.WriteLine("====== Modify Calendar Event ======");
            Console.WriteLine("Choose variable ID of Calendar Event that you want to modify:");
            Console.WriteLine($"1. Name:            {calendarEventToModify.CalendarEventName}");
            Console.WriteLine($"2. Description:     {calendarEventToModify.CalendarEventDescription}");
            Console.WriteLine($"3. Date:            {calendarEventToModify.CalendarEventDate}");
            Console.WriteLine($"4. Start time:       {calendarEventToModify.CalendarEventStartTime}");
            Console.WriteLine($"5. End time:         {calendarEventToModify.CalendarEventEndTime}");
            Console.WriteLine("Backspace. Go back");
            Console.WriteLine("===============================================================================");

            ConsoleKeyInfo option = Console.ReadKey();
            string modifiedVariable;
            try
            {
                switch (option.Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        Console.WriteLine($"Current Name is: {calendarEventToModify.CalendarEventName}");
                        modifiedVariable = Helper.ConsolHelper("Provide new Name:");
                        calendarEventToModify.CalendarEventName = modifiedVariable;
                        break;
                    case ConsoleKey.D2:
                        Console.Clear();
                        Console.WriteLine($"Current Name is: {calendarEventToModify.CalendarEventDescription}");
                        modifiedVariable = Helper.ConsolHelper("Provide new Description:");
                        calendarEventToModify.CalendarEventDescription = modifiedVariable;
                        break;
                    case ConsoleKey.D3:
                        Console.Clear();
                        Console.WriteLine($"Current Name is: {calendarEventToModify.CalendarEventDate}");
                        modifiedVariable = Helper.ConsolHelper("Provide new Date (DD-MM-YYYY):");
                        if (DateOnly.TryParse(modifiedVariable, out DateOnly newDate))
                        {
                            CalendarEventDateMinMaxValidation(newDate);
                            calendarEventToModify.CalendarEventDate = newDate;
                        }
                        else
                            Console.WriteLine("Invalid format. Please enter a valid date.");
                        break;
                    case ConsoleKey.D4:
                        Console.Clear();
                        Console.WriteLine($"Current Name is: {calendarEventToModify.CalendarEventStartTime}");
                        modifiedVariable = Helper.ConsolHelper("Provide new Start Time (HH:mm):");
                        if (TimeOnly.TryParse(modifiedVariable, out TimeOnly newStartTime))
                            calendarEventToModify.CalendarEventStartTime = newStartTime;
                        else
                            Console.WriteLine("Invalid format. Please enter a valid date.");
                        break;
                    case ConsoleKey.D5:
                        Console.Clear();
                        Console.WriteLine($"Current Name is: {calendarEventToModify.CalendarEventEndTime}");
                        modifiedVariable = Helper.ConsolHelper("Provide new End Time (HH:mm):");
                        if (TimeOnly.TryParse(modifiedVariable, out TimeOnly NewEndTime))
                        {
                            NewEndTime = CalendarEventEndTimeValidation(NewEndTime, calendarEventToModify.CalendarEventStartTime);
                            calendarEventToModify.CalendarEventEndTime = NewEndTime;
                        }
                        break;
                    case ConsoleKey.Backspace:
                        MenuOptions.MenuCalendarEvents();
                        break;
                    default:
                        Console.WriteLine("Press any key to return to the menu.");
                        Console.ReadKey();
                        MenuOptions.MenuCalendarEvents();
                        break;
                }
                Console.Clear();
                new CsvCalendarEventRepository("..\\..\\..\\CalendarEvents.csv").ModifyCalendarEvent(calendarEventToModify);
                Console.WriteLine($"CalendarEvent with ID:{calendarEventToModify.CalendarEventId + 1} has been successfully modified");
                Console.WriteLine("Press any key to return to the menu.");
                Console.ReadKey();
                MenuOptions.MenuCalendarEvents();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while writing to the CSV file: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }
            }
        }
        public void ModifyCalendarEventsAdmin()
        {
            Console.Clear();
            CalendarEvent calendarEventToModify;
            while (true)
            {
                Console.WriteLine("====== List of Calendar Events ======");
                Console.WriteLine("ID:\tUserID:\tDate:\t\tName: ");
                foreach (var item in _calendarEvents)
                {
                    Console.WriteLine($"{item.CalendarEventId + 1}\t{item.AssignedToUser}\t{item.CalendarEventDate}\t{item.CalendarEventName}");
                }
                string input = Helper.ConsolHelper("Choose the ID of Calendar Event you want to modify (or 0 to cancel):");
                if (int.TryParse(input, out int calendarEventId) && calendarEventId > 0 && calendarEventId <= _calendarEvents.Count)
                {
                    calendarEventToModify = _calendarEvents.First(c => c.CalendarEventId == (calendarEventId - 1));
                    //return calendarEventToModify;
                    break;
                }
                else if (input.ToLower() == "0")
                {
                    Console.WriteLine("Operation canceled.");
                    Console.WriteLine("Press any key to return to continue.");
                    Console.ReadKey();
                    new MenuOptions().MenuAdminCalendarEvents();
                    //break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid choice");
                    Console.WriteLine("Press any key to return to continue.");
                    Console.ReadKey();
                    //new MenuOptions().MenuAdminCalendarEvents();
                    //break;
                }
            }
                Console.Clear();
            Console.WriteLine("====== Modify Calendar Event ======");
            Console.WriteLine("Choose variable ID of Calendar Event that you want to modify:");
            Console.WriteLine($"1. Name:            {calendarEventToModify.CalendarEventName}");
            Console.WriteLine($"2. Description:     {calendarEventToModify.CalendarEventDescription}");
            Console.WriteLine($"3. Date:            {calendarEventToModify.CalendarEventDate}");
            Console.WriteLine($"4. StartTIme:       {calendarEventToModify.CalendarEventStartTime}");
            Console.WriteLine($"5. EndTime:         {calendarEventToModify.CalendarEventEndTime}");
            Console.WriteLine("Backspace. Go back");
            Console.WriteLine("===============================================================================");
            ConsoleKeyInfo option = Console.ReadKey();
            string modifiedVariable;
            try
            {
                Console.Clear();
                switch (option.Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        Console.WriteLine($"Current Name is: {calendarEventToModify.CalendarEventName}");
                        modifiedVariable = Helper.ConsolHelper("Provide new Name:");
                        calendarEventToModify.CalendarEventName = modifiedVariable;
                        break;
                    case ConsoleKey.D2:
                        Console.Clear();
                        Console.WriteLine($"Current Name is: {calendarEventToModify.CalendarEventDescription}");
                        modifiedVariable = Helper.ConsolHelper("Provide new Description:");
                        calendarEventToModify.CalendarEventDescription = modifiedVariable;
                        break;
                    case ConsoleKey.D3:
                        Console.Clear();
                        Console.WriteLine($"Current Name is: {calendarEventToModify.CalendarEventDate}");
                        modifiedVariable = Helper.ConsolHelper("Provide new Date (DD-MM-YYYY):");
                        if (DateOnly.TryParse(modifiedVariable, out DateOnly newDate))
                        {
                            CalendarEventDateMinMaxValidation(newDate);
                            calendarEventToModify.CalendarEventDate = newDate;
                        }
                        else
                            Console.WriteLine("Invalid format. Please enter a valid date.");
                        break;
                    case ConsoleKey.D4:
                        Console.Clear();
                        Console.WriteLine($"Current Name is: {calendarEventToModify.CalendarEventStartTime}");
                        modifiedVariable = Helper.ConsolHelper("Provide new Start Time (HH:mm):");
                        if (TimeOnly.TryParse(modifiedVariable, out TimeOnly newStartTime))
                            calendarEventToModify.CalendarEventStartTime = newStartTime;
                        else
                            Console.WriteLine("Invalid format. Please enter a valid date.");
                        break;
                    case ConsoleKey.D5:
                        Console.Clear();
                        Console.WriteLine($"Current Name is: {calendarEventToModify.CalendarEventEndTime}");
                        modifiedVariable = Helper.ConsolHelper("Provide new End Time (HH:mm):");
                        if (TimeOnly.TryParse(modifiedVariable, out TimeOnly NewEndTime))
                        {
                            NewEndTime = CalendarEventEndTimeValidation(NewEndTime, calendarEventToModify.CalendarEventStartTime);
                            calendarEventToModify.CalendarEventEndTime = NewEndTime;
                        }
                        break;
                    case ConsoleKey.Backspace:
                        new MenuOptions().MenuAdminCalendarEvents();
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please choose a valid option.");
                        Console.WriteLine("Press any key to return to the menu.");
                        Console.ReadKey();
                        new MenuOptions().MenuAdminCalendarEvents();
                        break;
                }
                Console.Clear();
                new CsvCalendarEventRepository("..\\..\\..\\CalendarEvents.csv").ModifyCalendarEvent(calendarEventToModify);
                Console.WriteLine($"CalendarEvent with ID:{calendarEventToModify.CalendarEventId + 1} has been succesfully modified");
                Console.WriteLine("Press any key to return to the menu.");
                Console.ReadKey();
                new MenuOptions();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while writing to the CSV file: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }
            }
        }
        #endregion

        #region CalendarEvent - Delete Section
        public void DeleteCurrentCalendarEvent(User user)
        {
            Console.Clear();
            List<CalendarEvent> currentUserCalendarEvents = _calendarEvents.Where(u => u.AssignedToUser == user.Id).ToList();

            if (Helper.IsCalendarEmpty(currentUserCalendarEvents)) return;

            DisplayCalendarEvents(currentUserCalendarEvents);
            CurrentUserCalendarEventsSelection(user);

            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }
        public void DeleteCalendarEventAdmin()
        {
            Console.Clear();
            if (Helper.IsCalendarEmpty(_calendarEvents)) return;

            DisplayCalendarEvents(_calendarEvents);
            CalendarEventsSelection();

            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }
        public void DisplayCalendarEvents(List<CalendarEvent> calendarEvents)
        {
            Console.WriteLine("====== List of Calendar Events ======");
            Console.WriteLine("ID:\tUserID:\t\tDate:\t\tCalendar Event Name:");
            for (int i = 0; i < calendarEvents.Count; i++)
            {
                Console.WriteLine
                ($"{i + 1}\t{calendarEvents[i].AssignedToUser}\t\t{calendarEvents[i].CalendarEventDate}\t\t{calendarEvents[i].CalendarEventName}");
            }
        }
        public void CurrentUserCalendarEventsSelection(User user)
        {
            List<CalendarEvent> currentUserCalendarEvents = _calendarEvents.Where(u => u.AssignedToUser == user.Id).ToList();

            while (true)
            {
                Console.WriteLine("\n Choose the ID from the list of Calendar Events above that you want to delete (or 0 to cancel):");
                string input = Console.ReadLine();
                if (input.ToLower() == "0")
                {
                    Console.WriteLine("Canceled deletion.");
                    break;
                }
                else if (int.TryParse(input, out int calendarEventId) && calendarEventId > 0 && calendarEventId <= currentUserCalendarEvents.Count)
                {
                    var selectedEvent = currentUserCalendarEvents[calendarEventId - 1];
                    _csvCalendarEventRepository.DeleteCalendarEvent(selectedEvent.CalendarEventId);
                    Console.WriteLine($"Calendar Event from your list with ID: {calendarEventId} has been successfully deleted.");
                    break;
                }
                else
                    Console.WriteLine
                    ($"Invalid choice. Please enter a valid Calendar Event ID or 0 to cancel.");
            }
        }
        public void CalendarEventsSelection()
        {
            while (true)
            {
                Console.WriteLine("\n Choose the ID from the list of Calendar Events above that you want to delete (or 0 to cancel):");
                string input = Console.ReadLine();
                if (input.ToLower() == "0")
                {
                    Console.WriteLine("Canceled deletion.");
                    break;
                }
                else if (int.TryParse(input, out int calendarEventId) && calendarEventId > 0 && calendarEventId <= _calendarEvents.Count)
                {
                    var selectedEvent = _calendarEvents[calendarEventId - 1];
                    _csvCalendarEventRepository.DeleteCalendarEvent(selectedEvent.CalendarEventId);
                    Console.WriteLine($"Calendar Event from your list with ID: {calendarEventId} has been successfully deleted.");
                    break;
                }
                else
                    Console.WriteLine
                    ($"Invalid choice. Please enter a valid Calendar Event ID or 0 to cancel.");
            }
        }
        #endregion

        #region CalendarEvent - Validation Section
        public WorkModesToUser? GetWorkModesToUserForDate(User user, DateOnly calendarEventDate)
        {
            var workModes = _csvWorkModesRepository.GetAllWorkModes();
            var userWorkModes = workModes.FirstOrDefault(c => c.UserID == user.Id && c.DateOfWorkmode == calendarEventDate);
            return userWorkModes;
        }
        public DateOnly CalendarEventDateRelatedToWorkModeValidation(User user, DateOnly calendarEventDate)
        {
            string dateValue;
            while (true)
            {
                var userWorkModes = GetWorkModesToUserForDate(user, calendarEventDate);
                if (userWorkModes == null)
                {
                    Console.WriteLine($"Work mode for {calendarEventDate} is empty");
                    break;
                }
                else if (userWorkModes.WorkModeName.Equals("Sick leave") ||
                         userWorkModes.WorkModeName.Equals("Holiday leave"))
                {
                    Console.WriteLine(
                        $"\nWork mode for {calendarEventDate} equals sick or holiday leave - you cannot create Calendar Event for that date!");
                    Console.WriteLine($"Please provide other date using format DD.MM.YYYY");
                    calendarEventDate = CalendarEventDateAddValidation(out dateValue);
                    calendarEventDate = CalendarEventDateMinMaxValidation(calendarEventDate);
                    calendarEventDate = CalendarEventDateWeekendValidation(calendarEventDate);
                }
                else
                {
                    Console.WriteLine($"Work mode is {userWorkModes.WorkModeName}");
                    break;
                }
            }
            return calendarEventDate;
        }
        public DateOnly CalendarEventDateWeekendValidation(DateOnly calendarEventDate)
        {
            string dateValue;
            if (calendarEventDate.DayOfWeek == DayOfWeek.Saturday || calendarEventDate.DayOfWeek == DayOfWeek.Sunday)
            {
                Console.WriteLine(
                    "You are trying to add calendar event for Saturday or Sunday, which is incorrect, please adjust the value in format DD.MM.YYYY!");
                calendarEventDate = CalendarEventDateAddValidation(out dateValue);
                calendarEventDate = CalendarEventDateMinMaxValidation(calendarEventDate);
            }
            return calendarEventDate;
        }
        public DateOnly CalendarEventDateMinMaxValidation(DateOnly calendarEventDate)
        {
            string dateValue;
            while (true)
            {
                if (calendarEventDate < currentDate.AddDays(-30))
                {
                    Console.WriteLine(
                        "You are trying to add date that is more than 30 days in the past from today or value is incorrect, adjust the value using format DD.MM.YYYY!");
                    calendarEventDate = CalendarEventDateAddValidation(out dateValue);
                }
                else if (calendarEventDate > currentDate.AddDays(60))
                {
                    Console.WriteLine(
                        "You are trying to add date that is more than 60 days in the future from today or value is incorrect, adjust the value using format DD.MM.YYYY!");
                    calendarEventDate = CalendarEventDateAddValidation(out dateValue);
                }
                else break;
            }

            return calendarEventDate;
        }
        public DateOnly CalendarEventDateAddValidation(out string dateValue)
        {
            while (true)
            {
                dateValue = Console.ReadLine();
                dateValue = DateValueEmptinessValidation(dateValue);
                if (DateOnly.TryParse(dateValue, out var calendarEventDate))
                {
                    return calendarEventDate;
                }
                else
                {
                    Console.WriteLine("Invalid value, please provide again using format DD.MM.YYYY");
                }
            }
        }
        public void CalendarEventAvailabilityCheck(User user, CalendarEvent? calendarEventAvailable, DateOnly calendarEventDate)
        {
            if (calendarEventAvailable != null)
            {
                Console.WriteLine(
                    $"There is already at least one Calendar Event created for date {calendarEventDate}. Do you want to display it? \nProvide y - to show and n - to proceed further");
                while (true)
                {
                    var userAnswer = Console.ReadKey(intercept: true);
                    if (userAnswer.Key == ConsoleKey.Y)
                    {
                        ShowCalendarEvent(user, calendarEventDate);
                        break;
                    }
                    else if (userAnswer.Key == ConsoleKey.N)
                    {
                        break;
                    }
                    else Console.WriteLine("Invalid value, please provide again");
                }
            }
        }
        public string EndTimeEmptinessValidation(string? endTime)
        {
            while (string.IsNullOrWhiteSpace(endTime))
            {
                Console.WriteLine("Calendar Event Start Time cannot be empty, please provide value in format HH:MM!");
                endTime = Console.ReadLine();
            }
            return endTime;
        }
        public string StartTimeEmptinessValidation(string? startTime)
        {
            while (string.IsNullOrWhiteSpace(startTime))
            {
                Console.WriteLine("Calendar Event Start Time cannot be empty, please provide value in format HH:MM!");
                startTime = Console.ReadLine();
            }
            return startTime;
        }
        public TimeOnly CalendarEventStartTimeOverlappingValidation(CalendarEvent? validatedStartTime,
            TimeOnly calendarEventStartTime, List<CalendarEvent> calendarEvents, DateOnly calendarEventDate,
            User user)
        {
            string? startTime;
            while (validatedStartTime != null)
            {
                Console.WriteLine(
                    "There is already a Calendar Event with provided Start time or that takes place on the same time, please provide different value in format HH:MM!");
                startTime = Console.ReadLine();
                startTime = StartTimeEmptinessValidation(startTime);
                TimeOnly.TryParse(startTime, out calendarEventStartTime);
                validatedStartTime = calendarEvents
                    .FirstOrDefault(c => c.AssignedToUser == user.Id &&
                                         c.CalendarEventDate == calendarEventDate &&
                                         (c.CalendarEventStartTime == calendarEventStartTime ||
                                          c.CalendarEventEndTime.CompareTo(calendarEventStartTime) > 0));
            }
            return calendarEventStartTime;
        }
        public string DateValueEmptinessValidation(string? dateValue)
        {
            while (string.IsNullOrWhiteSpace(dateValue))
            {
                Console.WriteLine("Calendar Event Date cannot be empty, please provide value using format DD.MM.YYYY!");
               // dateValue = Console.ReadLine();
            }
            return dateValue;
        }
        public TimeOnly CalendarEventEndTimeValidation(TimeOnly calendarEventEndTime,
            TimeOnly calendarEventStartTime)
        {
            string? endTime;
            while (calendarEventEndTime.CompareTo(calendarEventStartTime) <= 0)
            {
                Console.WriteLine(
                    "Calendar Event End Time cannot be earlier or at the same time as Start Time, please adjust the value using format HH:MM!");
                endTime = Console.ReadLine();
                endTime = EndTimeEmptinessValidation(endTime);
                TimeOnly.TryParse(endTime, out calendarEventEndTime);
            }

            return calendarEventEndTime;
        }
        public  TimeOnly CalendarEventStartTimeParseValidation()
        {
            while (true)
            {
                string startTime = Console.ReadLine();
                startTime = StartTimeEmptinessValidation(startTime);
                if (TimeOnly.TryParse(startTime, out var calendarEventStartTime))
                {
                    return calendarEventStartTime;
                }
                else
                {
                    Console.WriteLine("Invalid value, please provide again using format HH:MM");
                }
            }
        }
        #endregion
    }
}

