using Schedulist.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using CsvHelper.TypeConversion;

namespace Schedulist.Business
{
    public class ManageCalendarEvent
    {
        //not used \|/
        ICalendarEventRepository _calendarEventRepository;
        private List<CalendarEvent> _calendarEvents =
            new CsvCalendarEventRepository("..\\..\\..\\CalendarEvents.csv").GetAllCalendarEvents();
        //not used \|/
        private List<User> _userlist = new CsvUserRepository("..\\..\\..\\Users.csv").GetAllUsers();
        private CsvCalendarEventRepository csvCalendarEventRepository = new("..\\..\\..\\CalendarEvents.csv");
        public ManageCalendarEvent(ICalendarEventRepository calendarEventRepository)
        {
            _calendarEventRepository = calendarEventRepository;
        }
        public ManageCalendarEvent()
        {
        }
        public CalendarEvent ShowCalendarEvent()
        {
            Console.Clear();
            Console.WriteLine("======List of Calendar Events======");
            Console.WriteLine("ID \t| Calendar Event Name \t\t|Date");
            for (int i = 0; i < _calendarEvents.Count; i++)
            {
                Console.WriteLine(
                    $"{i} \t{_calendarEvents[i].CalendarEventName} \t\t\t {_calendarEvents[i].CalendarEventDate}");
            }
            Console.WriteLine("\nType any key do return to Menu");
            Console.ReadKey();
            MenuOptions.MenuCalendarEvents();
            return null;
        }
        public CalendarEvent ShowUserCalendarEvent()
        {
            Console.Clear();
            Console.WriteLine("======List of Calendar Events=======");
            Console.WriteLine("Choose User ID for which you want to show Calendar Events");
            int specifiedUserId = int.Parse(Console.ReadLine());
            Console.WriteLine("Provide date for which you want to show Calendar Events");
            string providedDate = Console.ReadLine();
            DateOnly.TryParse(providedDate, out var specifiedDate);
            var calendarEvents = csvCalendarEventRepository.GetAllCalendarEvents();
            var userCalendarEvents = calendarEvents
                .Where(c => c.AssignedToUser.Id == specifiedUserId && c.CalendarEventDate == specifiedDate)
               .ToList();
            Console.WriteLine($"\nCalendar Event Names for user id {specifiedUserId} on {specifiedDate}:");
            foreach (var calendarEvent in userCalendarEvents)
            {
                Console.WriteLine($"{calendarEvent.CalendarEventName} {calendarEvent.CalendarEventStartTime} {calendarEvent.CalendarEventEndTime}");
            }
            //for (int i = 1; i <= userCalendarEvents.Count; i++)
            //{
            //    Console.WriteLine($"Name {calendarEvents[i].CalendarEventName} starting at {calendarEvents[i].CalendarEventStartTime} and ending at {calendarEvents[i].CalendarEventEndTime}");
            //}
            Console.WriteLine("\nType any key do return to Menu");
            Console.ReadKey();
            MenuOptions.MenuCalendarEvents();
            return null;
        }

        public void CreateCalendarEvent()
        {
            Console.Clear();
            Console.WriteLine("You are creating new Calendar Event, please provide data as following:");
            int calendarEventId = 1;
            Console.WriteLine("Name of Calendar Event:");
            string calendarEventName = Console.ReadLine();
            calendarEventName = CalendarEventNameValidation(calendarEventName);
            Console.WriteLine("Description of the Calendar Event:");
            string calendarEventDescription = Console.ReadLine();
            calendarEventDescription = CalendarEventDescriptionValidation(calendarEventDescription);
            Console.WriteLine("Date of Calendar Event using format DD/MM/YYYY");
            string dateValue = Console.ReadLine();
            dateValue = DateValueEmptinessValidation(dateValue);
            DateOnly.TryParse(dateValue, out var calendarEventDate);
            Console.WriteLine("Start time of Calendar Event using format HH:MM");
            string startTime = Console.ReadLine();
            startTime = StartTimeEmptinessValidation(startTime);
            TimeOnly.TryParse(startTime, out var calendarEventStartTime);
            var calendarEvents = csvCalendarEventRepository.GetAllCalendarEvents();
            var validatedStartTime = calendarEvents
                .FirstOrDefault(c => c.AssignedToUser.Id == CurrentUser.currentUser.Id &&
                            c.CalendarEventDate == calendarEventDate && (c.CalendarEventStartTime == calendarEventStartTime || c.CalendarEventEndTime.CompareTo(calendarEventStartTime) > 0));
            calendarEventStartTime = CalendarEventStartTimeOverlappingValidation(validatedStartTime, calendarEventStartTime, calendarEvents, calendarEventDate);
            Console.WriteLine("End time of Calendar Event using format HH:MM");
            string endTime = Console.ReadLine();
            endTime = EndTimeEmptinessValidation(endTime);
            TimeOnly.TryParse(endTime, out var calendarEventEndTime);
            calendarEventEndTime = CalendarEventEndTimeValidation(calendarEventEndTime, calendarEventStartTime);
            CalendarEvent calendarEvent = new(calendarEventId, calendarEventName,
                calendarEventDescription, calendarEventDate, calendarEventStartTime, calendarEventEndTime,
                CurrentUser.currentUser);
            csvCalendarEventRepository.AddCalendarEvent(calendarEvent);
            Console.WriteLine("\nType any key do return to Menu");
            Console.ReadKey();
            MenuOptions.MenuCalendarEvents();
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
            TimeOnly calendarEventStartTime, List<CalendarEvent> calendarEvents, DateOnly calendarEventDate)
        {
            string? startTime;
            while (validatedStartTime != null)
            {
                Console.WriteLine(
                    "Calendar Event with provided Start time already exists for that day, please provide different value!");
                startTime = Console.ReadLine();
                TimeOnly.TryParse(startTime, out calendarEventStartTime);
                validatedStartTime = calendarEvents
                    .FirstOrDefault(c => c.AssignedToUser.Id == CurrentUser.currentUser.Id &&
                                         c.CalendarEventDate == calendarEventDate && (c.CalendarEventStartTime == calendarEventStartTime || c.CalendarEventEndTime.CompareTo(calendarEventStartTime) > 0));
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
        private static TimeOnly CalendarEventEndTimeValidation(TimeOnly calendarEventEndTime, TimeOnly calendarEventStartTime)
        {
            string? endTime;
            while (calendarEventEndTime.CompareTo(calendarEventStartTime) <= 0)
            {
                Console.WriteLine(
                    "Calendar Event End Time cannot be earlier or at the same time as Start Time, adjust the value!");
                endTime = Console.ReadLine();
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
    }
}
