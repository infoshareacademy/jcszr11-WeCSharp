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
            Console.WriteLine("You are creating new Calendar Event, please provide following data");
            Console.WriteLine("TasCalendar Event Name");
            string calendarEventName = Console.ReadLine();
            Console.WriteLine("Task Description");
            string calendarEventDescription = Console.ReadLine();
            Console.WriteLine("Start date and time of task using format DD/MM/YYYY HH:MM");
            string calendarEventStartDateTime = Console.ReadLine();
            DateTime parsedStartDate;
            var isValidStartDate = DateTime.TryParse(calendarEventStartDateTime, out parsedStartDate);
            if (!isValidStartDate) Console.WriteLine($"Incorrect date/time format, unable to parse date: {calendarEventStartDateTime}");                

            Console.WriteLine("End date and time of task using format DD/MM/YYYY HH:MM");
            string calendarEventEndDateTime = Console.ReadLine();
            DateTime parsedEndDateTime;
            bool isValidEndDate = DateTime.TryParse(calendarEventEndDateTime, out parsedEndDateTime);
            if (!isValidEndDate) Console.WriteLine($"Incorrect date/time format, unable to parse date: {calendarEventEndDateTime}");
           
                
        }
    }
}
