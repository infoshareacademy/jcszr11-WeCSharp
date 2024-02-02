using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Schedulist.DAL.Repositories.Interfaces;

namespace Schedulist.DAL
{
    public class CsvCalendarEventRepository : ICalendarEventRepository
    {
        private string _pathToCsvFile;
        public List<CalendarEvent> calendarEvents;
        public CsvCalendarEventRepository(string pathToCsvFile)
        {
            _pathToCsvFile = pathToCsvFile;
        }
        public List<CalendarEvent> GetAllCalendarEvents()
        {
            var csvConfig = CsvConfiguration();
            using var reader = new StreamReader(_pathToCsvFile);
            using var csv = new CsvReader(reader, csvConfig);
            calendarEvents = csv.GetRecords<CalendarEvent>().OrderByDescending(c => c.CalendarEventDate).ToList();
            return calendarEvents;
        }
        public void AddCalendarEvent(CalendarEvent calendarEvent)
        {
            var csvConfig = CsvConfiguration();
            calendarEvents = GetAllCalendarEvents();
            int nextCalendarEventId = calendarEvents.Count > 0 ? calendarEvents.Max(u => u.CalendarEventId) + 1 : 1;
            calendarEvent.CalendarEventId = nextCalendarEventId;
            try
            {
                calendarEvents.Add(calendarEvent);
                using StreamWriter writer = new(_pathToCsvFile, append: false);
                using var csv = new CsvWriter(writer, csvConfig);
                csv.WriteRecords(calendarEvents);
                //Console.Clear();
                Console.WriteLine($"The Calendar Event name: '{calendarEvent.CalendarEventName}' \nwith description: '{calendarEvent.CalendarEventDescription}' \non day {calendarEvent.CalendarEventDate} \nstarting at {calendarEvent.CalendarEventStartTime} \nending at {calendarEvent.CalendarEventEndTime} \nhas been added to the list successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
        public void DeleteCalendarEvent(int calendarEventId)
        {
                calendarEvents = GetAllCalendarEvents(); 

            var eventToDelete = calendarEvents.FirstOrDefault(e => e.CalendarEventId == calendarEventId);
            if (eventToDelete != null)
            {
                calendarEvents.Remove(eventToDelete);

                try
                {
                    using StreamWriter writer = new(_pathToCsvFile, append: false);
                    var csvConfig = CsvConfiguration();
                    using CsvWriter csv = new(writer, csvConfig);
                    csv.WriteRecords(calendarEvents);
                    //Console.Clear();                   
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                    }
                }
            }
        }
        public void ModifyCalendarEvent(CalendarEvent calendarEventToModify)
        {
            var csvConfig = CsvConfiguration();
            var calendarEventsList = GetAllCalendarEvents();

            int indexToUpade = calendarEventToModify.CalendarEventId;
            calendarEventsList[indexToUpade-1] = calendarEventToModify;

                try
                {
                    using StreamWriter writer = new(_pathToCsvFile, append: false);
                    using var csv = new CsvWriter(writer, csvConfig);
                    csv.WriteRecords(calendarEventsList);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while writing to the CSV file: " + ex.Message);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                    }
                }
        }
        private static CsvConfiguration CsvConfiguration()
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ",",
            };
            return csvConfig;
        }
    }
}
