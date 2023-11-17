﻿using System;
using Schedulist.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using CsvHelper.TypeConversion;
using Schedulist.Business;
using System.Globalization;

namespace Schedulist.DAL
{

    public class Calendar
    {
        private CsvCalendarEventRepository _csvCalendarEventRepository =
            new("..\\..\\..\\CalendarEvents.csv");
        private CSVWorkModesRepository _csvWorkModesRepository =
            new("..\\..\\..\\WorkModes.csv");

        public void ShowCalendar()
        {
            Console.Clear();
            DateTime currentDate = DateTime.Today;
            int year = currentDate.Year;
            int month = currentDate.Month;
            DateTime firstDayOfMonth = new(year, month, 1);
            int dayOfWeek = (int)firstDayOfMonth.DayOfWeek;
            int daysInMonth = DateTime.DaysInMonth(year, month);
            CultureInfo englishCulture = new CultureInfo("en-GB");
            CultureInfo.DefaultThreadCurrentCulture = englishCulture;
            string monthName = firstDayOfMonth.ToString("MMMM");
            Console.WriteLine($"\n\t{monthName} {year}");
            Console.WriteLine($"\t\n========================================================");
            Console.WriteLine($" \nSun  Mon Tue Wed Thu Fri Sat");
            Console.WriteLine(" ");
            for (int i = 0; i < dayOfWeek; i++)
            {
                Console.Write("    ");
            }
            for (int day = 1; day <= daysInMonth; day++)
            {
                Console.Write(day.ToString().PadLeft(4));
                if (++dayOfWeek == 7)
                {
                    dayOfWeek = 0;
                    Console.WriteLine();
                }
            }
        }

        public void ShowUserCalendar(User user)
        {
            ShowCalendar();
            Console.WriteLine($"\n\n========================================================");
            Console.WriteLine("\n\n\nPlease enter date to show your work mode and calendar events (DD/MM/YYYY).");
            string inputDate = Console.ReadLine();
            if (DateOnly.TryParse(inputDate, out DateOnly selectedDate))
            {
                Console.Clear();
                Console.WriteLine($"Chosen date: {selectedDate}");
                var workModes = _csvWorkModesRepository.GetAllWorkModes();
                var userWorkModes = workModes.FirstOrDefault(c => c.UserID == CurrentUser.currentUser.Id && c.DateOfWorkmode == selectedDate);
                if (userWorkModes == null)
                {
                    Console.WriteLine("\nThere is no work mode existing for chosen date!");
                }
                else
                {
                    Console.WriteLine($"Your work mode is {userWorkModes.WorkModeName}");
                }
                var calendarEvents = _csvCalendarEventRepository.GetAllCalendarEvents();
                var userCalendarEvents = calendarEvents
                    .Where(c => c.AssignedToUser == CurrentUser.currentUser.Id && c.CalendarEventDate == selectedDate)
                    .OrderBy(c => c.CalendarEventStartTime)
                    .ToList();
                if (userCalendarEvents.Count == 0)
                {
                    Console.WriteLine("There is no Calendar Event existing for chosen date!");
                }
                else
                {
                    Console.WriteLine($"\nYour Calendar Events:");
                    Console.WriteLine($"\n========================================================");
                    Console.WriteLine($"\nStart - End time  \t Calendar Event Name");
                    Console.WriteLine(" ");
                    foreach (var calendarEvent in userCalendarEvents)
                    {
                        Console.WriteLine($"{calendarEvent.CalendarEventStartTime} -  {calendarEvent.CalendarEventEndTime} \t\t {calendarEvent.CalendarEventName}");
                    }
                }
                Console.WriteLine("\n========================================================");
                Console.WriteLine("\nType any key do return to Menu");
            }
            else
            {
                Console.WriteLine("The date is incorrect. Please try again.");
            }
            Console.ReadKey();
        }

        public void ShowUserCalendarAdmin(User user)
        {
            ShowCalendar();
            Console.WriteLine($"\n\n========================================================");
            Console.WriteLine($"\n\nShowing information for {user.Name} {user.Surname}");
            Console.WriteLine("\nPlease enter date to show work mode and calendar events in format DD/MM/YYYY.");
            string inputDate = Console.ReadLine();
            if (DateOnly.TryParse(inputDate, out DateOnly selectedDate))
            {
                Console.Clear();
                Console.WriteLine($"Showing information for {user.Name} {user.Surname}");
                Console.WriteLine($"\n\nChosen date: {selectedDate}");
                var workModes = _csvWorkModesRepository.GetAllWorkModes();
                var userWorkModes = workModes.FirstOrDefault(c => c.UserID == user.Id && c.DateOfWorkmode == selectedDate);
                if (userWorkModes == null)
                {
                    Console.WriteLine("\nThere is no work mode existing for chosen date!");
                }
                else
                {
                    Console.WriteLine($"Work mode is {userWorkModes.WorkModeName}");
                }
                var calendarEvents = _csvCalendarEventRepository.GetAllCalendarEvents();
                var userCalendarEvents = calendarEvents
                    .Where(c => c.AssignedToUser == user.Id && c.CalendarEventDate == selectedDate)
                    .OrderBy(c => c.CalendarEventStartTime)
                    .ToList();
                if (userCalendarEvents.Count == 0)
                {
                    Console.WriteLine("There is no Calendar Event existing for chosen date!");
                }
                else
                {
                    Console.WriteLine($"\nCalendar Events:");
                    Console.WriteLine($"\n========================================================");
                    Console.WriteLine($"\nStart - End time  \t Calendar Event Name");
                    Console.WriteLine(" ");

                    foreach (var calendarEvent in userCalendarEvents)
                    {
                        Console.WriteLine($"{calendarEvent.CalendarEventStartTime} -  {calendarEvent.CalendarEventEndTime} \t\t {calendarEvent.CalendarEventName}");
                    }
                }

                Console.WriteLine("\n========================================================");
                Console.WriteLine("\nType any key to return to Menu");
            }
            else
            {
                Console.WriteLine("The date is incorrect. Please try again.");
            }
            Console.ReadKey();
        }
    }
}