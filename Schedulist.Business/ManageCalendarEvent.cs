﻿using Schedulist.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.TypeConversion;

namespace Schedulist.Business
{
    public class ManageCalendarEvent
    {
        ICalendarEventRepository _calendarEventRepository;
        public User currentUser;

        public ManageCalendarEvent(ICalendarEventRepository calendarEventRepository)
        {
            _calendarEventRepository = calendarEventRepository;
        }

        
        public ManageCalendarEvent()
        {
        }

        public void ShowCalendarEvent()
        {
            var calendarEvents = _calendarEventRepository.GetAllCalendarEvents();
           // Console.WriteLine(calendarEvents);
        }
        public void CreateCalendarEvent()
        {
            //var finishCreatingTask = false;

            //while (!finishCreatingTask)
            //{
                Console.WriteLine("\nYou are creating new Calendar Event, please provide following data:");
                //Console.WriteLine("ID");
                int calendarEventId = 1;
                Console.WriteLine("Name of Calendar Event:");
                string calendarEventName = Console.ReadLine();
                Console.WriteLine("Description of the Calendar Event:");
                string calendarEventDescription = Console.ReadLine();
                Console.WriteLine("Date of Calendar Event using format DD/MM/YYYY");
                string dateValue = Console.ReadLine();
                DateOnly.TryParse(dateValue, out DateOnly calendarEventDate);
                //{
                //    Console.WriteLine("You entered a valid date: " + dateValue);
                //}
                //else
                //{
                //    Console.WriteLine("Invalid date format. Please enter a date in the format 'dd/MM/yyyy'.");
                //}
            Console.WriteLine("Start time of Calendar Event using format  HH:MM");
                string startTime = Console.ReadLine();
                TimeOnly.TryParse(startTime, out TimeOnly calendarEventStartTime);
            Console.WriteLine("End time of Calendar Event using format HH:MM");
                string endTime = Console.ReadLine();
                TimeOnly.TryParse(endTime, out TimeOnly calendarEventEndTime);
             
            //Console.WriteLine("You created new task as following:");
            //    Console.WriteLine(
            //        $"Calendar Event Name:           |{calendarEventName}    \n Calendar Event Description:    |{calendarEventDescription}    \nCalendar Event Date: | {calendarEventDate} \n Start Time:   | {calendarEventStartTime} \nEnd Time:   | {calendarEventEndTime}");
            //Console.Write(
            //    "Press 'x' and Enter to close the app, or press any other key and Enter to continue creating tasks: ");
            //if (Console.ReadLine() == "x") finishCreatingTask = true;
            //else if (Console.ReadLine() == "y") //read menu
            //    Console.WriteLine("\n");
            //int userId = currentUser.Id;
            //User assignedUser = userId;
            int assignedUser = 1;

            CalendarEvent calendarEvent = new CalendarEvent(calendarEventId, calendarEventName,
                calendarEventDescription, calendarEventDate, calendarEventStartTime, calendarEventEndTime);
                // { AssignedToUser = assignedUser};
              //  { UserId = assignedUser };
            new CsvCalendarEventRepository("..\\..\\..\\CalendarEvents.csv").AddCalendarEvent(calendarEvent);

                //TODO - to update headers/CSV file to save correctly
            //}

        }
    }
}
