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
        ICalendarEventRepository _calendarEventRepository;
        // public User currentUser;

        private List<CalendarEvent> _calendarEvents =
            new CsvCalendarEventRepository("..\\..\\..\\CalendarEvents.csv").GetAllCalendarEvents();

        private List<User> _userlist = new CsvUserRepository("Users.csv").GetAllUsers();

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
            Console.WriteLine("List of Calendar Events");
            Console.WriteLine("ID \t| Calendar Event Name \t\t|Date");
            for (int i = 0; i < _calendarEvents.Count; i++)
            {
                Console.WriteLine(
                    $"{i} \t{_calendarEvents[i].CalendarEventName} \t\t\t {_calendarEvents[i].CalendarEventDate}");
            }

            return null;
        }

        public CalendarEvent ShowUserCalendarEvent()
        {

            Console.WriteLine("List of Calendar Events");
            Console.WriteLine("Choose User ID for which you want to show Calendar Events");
            int specifiedUserId = int.Parse(Console.ReadLine());
            Console.WriteLine("Provide date for which you want to show Calendar Events");
            string providedDate = Console.ReadLine();
            DateOnly.TryParse(providedDate, out var specifiedDate);
            Console.WriteLine("ID \t| Calendar Event Name \t\t|Date");

            var calendarEvents = csvCalendarEventRepository.GetAllCalendarEvents();
            var userCalendarEvents = calendarEvents
                .Where(c => c.AssignedToUser.Id == specifiedUserId && c.CalendarEventDate == specifiedDate)
                .Select(c => c.CalendarEventName).ToList();

            Console.WriteLine($"Calendar Event Names for user id {specifiedUserId} on {specifiedDate}:");
            foreach (var eventName in userCalendarEvents)
            {
                Console.WriteLine($"{eventName}");
            }

            //for (int i = 1; i <= userCalendarEvents.Count; i++)
            //{
            //    Console.WriteLine($"Name {calendarEvents[i].CalendarEventName} starting at {calendarEvents[i].CalendarEventStartTime} and ending at {calendarEvents[i].CalendarEventEndTime}");
            //}
            return null;
        }

        public void CreateCalendarEvent()
        {
           // var finishCreatingTask = false;

            //while (!finishCreatingTask)
            //{
                Console.WriteLine("\nYou are creating new Calendar Event, please provide following data:");
                int calendarEventId = 1;
                Console.WriteLine("Name of Calendar Event:");
                string calendarEventName = Console.ReadLine();
                Console.WriteLine("Description of the Calendar Event:");
                string calendarEventDescription = Console.ReadLine();
                Console.WriteLine("Date of Calendar Event using format DD/MM/YYYY");
                string dateValue = Console.ReadLine();
                DateOnly.TryParse(dateValue, out var calendarEventDate);
                //{
                //    Console.WriteLine("You entered a valid date: " + dateValue);
                //}
                //else
                //{
                //    Console.WriteLine("Invalid date format. Please enter a date in the format 'dd/MM/yyyy'.");
                //}
                Console.WriteLine("Start time of Calendar Event using format HH:MM");
                string startTime = Console.ReadLine();
                TimeOnly.TryParse(startTime, out var calendarEventStartTime);
                Console.WriteLine("End time of Calendar Event using format HH:MM");
                string endTime = Console.ReadLine();
                TimeOnly.TryParse(endTime, out var calendarEventEndTime);

                //Console.WriteLine("You created new task as following:");
                //    Console.WriteLine(
                //        $"Calendar Event Name:           |{calendarEventName}    \n Calendar Event Description:    |{calendarEventDescription}    \nCalendar Event Date: | {calendarEventDate} \n Start Time:   | {calendarEventStartTime} \nEnd Time:   | {calendarEventEndTime}");
                //

                CalendarEvent calendarEvent = new CalendarEvent(calendarEventId, calendarEventName,
                    calendarEventDescription, calendarEventDate, calendarEventStartTime, calendarEventEndTime,
                    CurrentUser.currentUser);
                csvCalendarEventRepository.AddCalendarEvent(calendarEvent);
              
                //Console.Write("Press 'x' and Enter to close the app, or press any other key and Enter to continue creating tasks: ");
                //if (Console.ReadLine() == "x") 
                //{finishCreatingTask = true;}
                //else if (Console.ReadLine() == "y") //read menu
                //    Console.WriteLine("\n");
           // }
        }
    }
}
