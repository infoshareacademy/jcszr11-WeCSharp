using System;
using Schedulist.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using CsvHelper.TypeConversion;

namespace Schedulist.DAL
{

    public class Calendar
    {
        //private ManageCalendarEvent manageCalendarEvent;
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
            DateTime.TryParse(inputDate, out DateTime selectedDate);
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

            //    new ManageCalendarEvent().ShowUserCalendarEvent
            
        }
    }
}
