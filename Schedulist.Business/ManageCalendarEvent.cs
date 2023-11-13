using Schedulist.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using CsvHelper.TypeConversion;
using CsvHelper.Configuration.Attributes;
using Schedulist.Business.Actions;

namespace Schedulist.Business
{
    public class ManageCalendarEvent
    {
        private List<CalendarEvent> _calendarEvents =
            new CsvCalendarEventRepository("..\\..\\..\\CalendarEvents.csv").GetAllCalendarEvents();

        private CsvCalendarEventRepository _csvCalendarEventRepository =
            new("..\\..\\..\\CalendarEvents.csv");
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

        public void ShowUserCalendarEvent(User user)
        {
            Console.Clear();
            Console.WriteLine("==========List of Calendar Events==========");
            Console.WriteLine("Provide date for which you want to show Calendar Events using format DD/MM/YYYY");
            string providedDate = Console.ReadLine();
            providedDate = DateValueEmptinessValidation(providedDate);
            DateOnly.TryParse(providedDate, out var specifiedDate);
            var calendarEvents = _csvCalendarEventRepository.GetAllCalendarEvents();
            var userCalendarEvents = calendarEvents
                .Where(c => c.AssignedToUser == user.Id && c.CalendarEventDate == specifiedDate)
                .OrderBy(c => c.CalendarEventStartTime)
                .ToList();
            if (userCalendarEvents.Count == 0)
            {
                Console.WriteLine("There is no Calendar Event existing for chosen date!");
            }
            else
            {
                Console.WriteLine($"\nCalendar Events on {specifiedDate}:");
                Console.WriteLine($"Start time \t End time \t Calendar Event Name");
                foreach (var calendarEvent in userCalendarEvents)
                {
                    Console.WriteLine(
                        $"{calendarEvent.CalendarEventStartTime} \t\t {calendarEvent.CalendarEventEndTime} \t\t {calendarEvent.CalendarEventName}");
                }
            }
            Console.WriteLine("========================================================");
            Console.WriteLine("\nType any key do return to Menu");
            Console.ReadKey();
        }

        public void CreateCalendarEvent(User user)
        {
            Console.Clear();
            Console.WriteLine("==========Creating new Calendar Event==========");
            Console.WriteLine("You are creating new Calendar Event, please provide data as following:");
            int calendarEventId = 1;
            Console.WriteLine("Name of Calendar Event:");
            string calendarEventName = Console.ReadLine();
            calendarEventName = CalendarEventNameValidation(calendarEventName);
            Console.WriteLine("Description of the Calendar Event:");
            string calendarEventDescription = Console.ReadLine();
            calendarEventDescription = CalendarEventDescriptionValidation(calendarEventDescription);
            Console.WriteLine("Date of Calendar Event using format DD/MM/YYYY");
            var calendarEventDate = CalendarEventDateAdding(out var dateValue);
            calendarEventDate = CalendarEventDateMinMaxValidation(calendarEventDate);
            var calendarEvents = _csvCalendarEventRepository.GetAllCalendarEvents();
            var calendarEventAvailable = calendarEvents
                .FirstOrDefault(c => c.AssignedToUser == user.Id &&
                                     c.CalendarEventDate == calendarEventDate);
            calendarEventAvailability(user, calendarEventAvailable, calendarEventDate);
            Console.WriteLine("Start time of Calendar Event using format HH:MM");
            string startTime = Console.ReadLine();
            startTime = StartTimeEmptinessValidation(startTime);
            TimeOnly.TryParse(startTime, out var calendarEventStartTime);
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
            CalendarEvent calendarEvent = new CalendarEvent(calendarEventId, calendarEventName,
                calendarEventDescription, calendarEventDate, calendarEventStartTime, calendarEventEndTime,
                user.Id);
            _csvCalendarEventRepository.AddCalendarEvent(calendarEvent);
            Console.WriteLine("\nType any key do return to Menu");
            Console.ReadKey();
        }

        #region CalendarEvent - Delete Section
        public void DeleteYourCalendarEvent(User user)
        {
            Console.Clear();
            List<CalendarEvent> currentUserCalendarEvents = _calendarEvents.Where(u => u.AssignedToUser == user.Id).ToList();

            if (Helper.IsCalendarEmpty(currentUserCalendarEvents)) return;

            DisplayCalendarEvents(currentUserCalendarEvents);
            CurrentUserCalendarEventsSelection(user);
            
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
            MenuOptions.MenuCalendarEvents();
        }
        public void DeleteCalendarEventAdmin()
        {
            Console.Clear();
            if (Helper.IsCalendarEmpty(_calendarEvents)) return;

            DisplayCalendarEvents(_calendarEvents);
            CalendarEventsSelection();

            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
            MenuOptions.MenuAdminCalendarEvents();
        }
        private void DisplayCalendarEvents(List<CalendarEvent> calendarEvents)
        {
            Console.WriteLine("ID \t| Calendar Event Name \t\t|Date");
            for (int i = 0; i < calendarEvents.Count; i++)
            {
                Console.WriteLine
                ($"{i + 1} \t {calendarEvents[i].CalendarEventName} \t\t\t {calendarEvents[i].CalendarEventDate}");
            }
        }
        private void CurrentUserCalendarEventsSelection(User user)
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
        private void CalendarEventsSelection()
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
        private DateOnly CalendarEventDateMinMaxValidation(DateOnly calendarEventDate)
        {
            string dateValue;
            while (true)
            {
                if (calendarEventDate < currentDate.AddDays(-30))
                {
                    Console.WriteLine(
                        "You are trying to add date that is more than 30 days in the past from today or value is incorrect, adjust the value!");
                    calendarEventDate = CalendarEventDateAdding(out dateValue);
                }
                else if (calendarEventDate > currentDate.AddDays(60))
                {
                    Console.WriteLine(
                        "You are trying to add date that is more than 60 days in the future from today or value is incorrect, adjust the value!");
                    calendarEventDate = CalendarEventDateAdding(out dateValue);
                }
                else break;
            }

            return calendarEventDate;
        }
        private DateOnly CalendarEventDateAdding(out string dateValue)
        {
            dateValue = Console.ReadLine();
            dateValue = DateValueEmptinessValidation(dateValue);
            DateOnly.TryParse(dateValue, out var calendarEventDate);
            return calendarEventDate;
        }
        private void calendarEventAvailability(User user, CalendarEvent? calendarEventAvailable, DateOnly calendarEventDate)
        {
            if (calendarEventAvailable != null)
            {
                Console.WriteLine(
                    $"There is already at least one Calendar Event created for date {calendarEventDate}. Do you want to display it? \nProvide y - to show and n - to proceed further");
                while (true)
                {
                    var keyInfo = Console.ReadKey(intercept: true);
                    char userAnswer = keyInfo.KeyChar;
                    if (userAnswer == 'y' || userAnswer == 'Y')
                    {
                        ShowCalendarEvent(user, calendarEventDate);
                        break;
                    }
                    else if (userAnswer == 'n' || userAnswer == 'N')
                    {
                        break;
                    }
                    else Console.WriteLine("Invalid value, please provide again");
                }
            }
        }
        private static string EndTimeEmptinessValidation(string? endTime)
        {
            while (string.IsNullOrWhiteSpace(endTime))
            {
                Console.WriteLine("Calendar Event Start Time cannot be empty, please provide value!");
                endTime = Console.ReadLine();
            }
            return endTime;
        }
        private static string StartTimeEmptinessValidation(string? startTime)
        {
            while (string.IsNullOrWhiteSpace(startTime))
            {
                Console.WriteLine("Calendar Event Start Time cannot be empty, please provide value!");
                startTime = Console.ReadLine();
            }
            return startTime;
        }
        private static TimeOnly CalendarEventStartTimeOverlappingValidation(CalendarEvent? validatedStartTime,
            TimeOnly calendarEventStartTime, List<CalendarEvent> calendarEvents, DateOnly calendarEventDate,
            User user)
        {
            string? startTime;
            while (validatedStartTime != null)
            {
                Console.WriteLine(
                    "There is already a Calendar Event with provided Start time or that takes place on the same time, please provide different value!");
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
        private static string DateValueEmptinessValidation(string? dateValue)
        {
            while (string.IsNullOrWhiteSpace(dateValue))
            {
                Console.WriteLine("Calendar Event Date cannot be empty, please provide value!");
                dateValue = Console.ReadLine();
            }
            return dateValue;
        }
        private static TimeOnly CalendarEventEndTimeValidation(TimeOnly calendarEventEndTime,
            TimeOnly calendarEventStartTime)
        {
            string? endTime;
            while (calendarEventEndTime.CompareTo(calendarEventStartTime) <= 0)
            {
                Console.WriteLine(
                    "Calendar Event End Time cannot be earlier or at the same time as Start Time, adjust the value!");
                endTime = Console.ReadLine();
                endTime = EndTimeEmptinessValidation(endTime);
                TimeOnly.TryParse(endTime, out calendarEventEndTime);
            }

            return calendarEventEndTime;
        }
        private static string CalendarEventDescriptionValidation(string? calendarEventDescription)
        {
            while (string.IsNullOrWhiteSpace(calendarEventDescription))
            {
                Console.WriteLine("Calendar Event Description cannot be empty, please provide value!");
                calendarEventDescription = Console.ReadLine();
            }

            return calendarEventDescription;
        }
        private static string CalendarEventNameValidation(string? calendarEventName)
        {
            while (string.IsNullOrWhiteSpace(calendarEventName))
            {
                Console.WriteLine("Calendar Event Name cannot be empty, please provide value!");
                calendarEventName = Console.ReadLine();
            }

            return calendarEventName;
        }
        #endregion
    }

}
