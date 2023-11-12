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

namespace Schedulist.DAL
{

    public class Calendar
    {
        private List<CalendarEvent> _calendarEvents =
            new CsvCalendarEventRepository("..\\..\\..\\CalendarEvents.csv").GetAllCalendarEvents();

        private CsvCalendarEventRepository _csvCalendarEventRepository =
            new("..\\..\\..\\CalendarEvents.csv");
        private ManageCalendarEvent manageCalendarEvent;
        //public void CurrentDate()
        //{
        //    DateTime currentDate = DateTime.Today;

        //    int year = currentDate.Year;
        //    int month = currentDate.Month;

        //    ShowCalendar(year, month);
        //}
        public void ShowCalendar(int year, int month)
        {
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            string monthName = firstDayOfMonth.ToString("MMMM");
            Console.WriteLine($"{monthName} {year}");
            Console.WriteLine(" Sun  Mon Tue Wed Thu Fri Sat");

            int dayOfWeek = (int)firstDayOfMonth.DayOfWeek;
            for (int i = 0; i < dayOfWeek; i++)
            {
                Console.WriteLine("    ");
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

            Console.WriteLine(" Please enter date to show your workmode and events (DD/MM/YYYY).");

            string inputDate = Console.ReadLine();
            DateOnly.TryParse(inputDate, out DateOnly selectedDate);
            Console.WriteLine($"Your workmode for date  is ");

            var work


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
                Console.WriteLine($"Start time \t End time \t Calendar Event Name");
                foreach (var calendarEvent in userCalendarEvents)
                {
                    Console.WriteLine(
                        $"{calendarEvent.CalendarEventStartTime} \t\t {calendarEvent.CalendarEventEndTime} \t\t {calendarEvent.CalendarEventName}");
                }
            }
            Console.WriteLine("========================================================");


            //if ()
            // {
            //     Show(selectedDate);
            // }

            //public static void ShowWorkMode(DateTime selectedDate)
            //    {
            //        if ((selectedDate))
            //        {
            //            Console.WriteLine(
            //                $" Your workmode for today is: {}"); // nie wiem czy to nie jest za duże uproszczenie?
            //        }
            //    }

            //    public static void ShowCalendarEvents(DateTime selectedDate)
            //    {
            //        if ((selectedDate))
            //        {
            //            Console.WriteLine({ ShowCalendarEvent }); //???
            //        }
            //        else
            //        {
            //            Console.WriteLine("No events for today");
            //        }
            //    }



        }
    }
}
