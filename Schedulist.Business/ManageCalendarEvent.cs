﻿using Schedulist.DAL;
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
        ICalendarEventRepository _calendarEventRepository;
        private List<CalendarEvent> _calendarEvents =
            new CsvCalendarEventRepository("..\\..\\..\\CalendarEvents.csv").GetAllCalendarEvents();
        private List<User> _userlist = new CsvUserRepository("..\\..\\..\\Users.csv").GetAllUsers();
        private CsvCalendarEventRepository csvCalendarEventRepository =
            new CsvCalendarEventRepository("..\\..\\..\\CalendarEvents.csv");
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
                .Select(c => c.CalendarEventName).ToList();
            Console.WriteLine($"\nCalendar Event Names for user id {specifiedUserId} on {specifiedDate}:");
            foreach (var eventName in userCalendarEvents)
            {
                Console.WriteLine($"{eventName}");
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
            DateOnly.TryParse(dateValue, out var calendarEventDate);
            Console.WriteLine("Start time of Calendar Event using format HH:MM");
            string startTime = Console.ReadLine();
            TimeOnly.TryParse(startTime, out var calendarEventStartTime);
            Console.WriteLine("End time of Calendar Event using format HH:MM");
            string endTime = Console.ReadLine();
            TimeOnly.TryParse(endTime, out var calendarEventEndTime);
            CalendarEvent calendarEvent = new CalendarEvent(calendarEventId, calendarEventName,
                calendarEventDescription, calendarEventDate, calendarEventStartTime, calendarEventEndTime,
                CurrentUser.currentUser);
            csvCalendarEventRepository.AddCalendarEvent(calendarEvent);
            Console.WriteLine("\nType any key do return to Menu");
            Console.ReadKey();
            MenuOptions.MenuCalendarEvents();
        }
        private static string CalendarEventDescriptionValidation(string? calendarEventDescription)
        {
            while (true)
            {
                if (string.IsNullOrWhiteSpace(calendarEventDescription))
                {
                    Console.WriteLine("Calendar Event Description cannot be empty, please provide value!");
                    calendarEventDescription = Console.ReadLine();
                }
                else break;
            }
            return calendarEventDescription;
        }
        private static string CalendarEventNameValidation(string? calendarEventName)
        {
            while (true)
            {
                if (string.IsNullOrWhiteSpace(calendarEventName))
                {
                    Console.WriteLine("Calendar Event Name cannot be empty, please provide value!");
                    calendarEventName = Console.ReadLine();
                }
                else break;
            }
            return calendarEventName;
        }
    }
}
