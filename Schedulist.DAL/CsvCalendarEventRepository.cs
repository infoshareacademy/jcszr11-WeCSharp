using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            calendarEvents = csv.GetRecords<CalendarEvent>().ToList();
            return calendarEvents;
        }
        public void AddCalendarEvent(CalendarEvent calendarEvent)
        {
            var csvConfig = CsvConfiguration();
            //List<CalendarEvent> calendarEvents = GetAllCalendarEvents();
            calendarEvents = GetAllCalendarEvents();

            int nextCalendarEventId = calendarEvents.Count > 0 ? calendarEvents.Max(u => u.CalendarEventId) + 1 : 1;
            calendarEvent.CalendarEventId = nextCalendarEventId;

            try
            {
                calendarEvents.Add(calendarEvent);
                using StreamWriter writer = new(_pathToCsvFile, append: false);
                using var csv = new CsvWriter(writer, csvConfig);
                    //Console.Clear();
                    csv.WriteRecords(calendarEvents);
                    Console.WriteLine($"\nThe Calendar Event named: '{calendarEvent.CalendarEventName}' \nwith description: '{calendarEvent.CalendarEventDescription}' \non day {calendarEvent.CalendarEventDate} \nstarting at {calendarEvent.CalendarEventStartTime} \nending at {calendarEvent.CalendarEventEndTime} \nhas been added to the list successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
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
