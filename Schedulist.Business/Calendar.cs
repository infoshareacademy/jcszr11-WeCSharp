using System;
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
        private List<CalendarEvent> _calendarEvents =
            new CsvCalendarEventRepository("..\\..\\..\\CalendarEvents.csv").GetAllCalendarEvents();
        private CsvCalendarEventRepository _csvCalendarEventRepository =
            new("..\\..\\..\\CalendarEvents.csv");
        private ManageCalendarEvent manageCalendarEvent;
        private List<WorkModesToUser> _workModesToUser =
            new CSVWorkModesRepository("..\\..\\..\\WorkModes.csv").GetAllWorkModes();
        private CSVWorkModesRepository _csvWorkModesRepository =
            new CSVWorkModesRepository("..\\..\\..\\WorkModes.csv");
        
        public void ShowCalendar(int year, int month)
        {
            Console.Clear();
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
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
            Console.WriteLine($"\n\n========================================================");
            Console.WriteLine("\n\n\nPlease enter date to show your workmode and calendar events (DD/MM/YYYY).");
            string inputDate = Console.ReadLine();
            DateOnly.TryParse(inputDate, out DateOnly selectedDate);

            Console.Clear();
            var workModes = _csvWorkModesRepository.GetAllWorkModes();
            var userWorkModes = workModes
                .FirstOrDefault(c => c.UserID == CurrentUser.currentUser.Id && c.dateOfWorkmode == selectedDate);
            Console.WriteLine($"Your workmode for date {selectedDate} is {userWorkModes.WorkModeName}");
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
                Console.WriteLine($"\nCalendar Events on {selectedDate}:");
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
            Console.ReadKey();
        }
    }
}