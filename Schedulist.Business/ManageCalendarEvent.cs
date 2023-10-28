using Schedulist.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.Business
{
    public class ManageCalendarEvent
    {
        IRepository<CalendarEvent> _calendarEventRepository;

        public ManageCalendarEvent(IRepository<CalendarEvent> calendarEventRepository)
        {
            _calendarEventRepository = calendarEventRepository;
        }

        public ManageCalendarEvent()
        {
        }

        public void ShowCalendarEvent()
        {
            var calendarEvents = _calendarEventRepository.GetAll();
        }
        public void CreateCalendarEvent()
        {
            var finishCreatingTask = false;

            while (!finishCreatingTask)
            {
                Console.WriteLine("You are creating new Calendar Event, please provide following data:");
                Console.WriteLine("Calendar Event ID:");
                int calendarEventId = int.Parse(Console.ReadLine());
                Console.WriteLine("Calendar Event Name:");
                string calendarEventName = Console.ReadLine();
                Console.WriteLine("Calendar Event Description");
                string calendarEventDescription = Console.ReadLine();
                Console.WriteLine("Start date and time of Calendar Event using format DD/MM/YYYY HH:MM");
                DateTime calendarEventStartDateTime = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("End date and time of Calendar Event using format DD/MM/YYYY HH:MM");
                DateTime calendarEventEndDateTime = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("You created new task as following:");
                Console.WriteLine(
                    $"Task ID:             |{calendarEventId}    \nTask Name:           |{calendarEventName}    \nTask Description:    |{calendarEventDescription}    \nStart Date and Time: | {calendarEventStartDateTime} \nEnd Date and Time:   | {calendarEventEndDateTime}");
                Console.Write(
                    "Press 'x' and Enter to close the app, or press any other key and Enter to continue creating tasks: ");
                if (Console.ReadLine() == "x") finishCreatingTask = true;
                else if (Console.ReadLine() == "y") //read menu
                    Console.WriteLine("\n");
            }
        }
    }
}
