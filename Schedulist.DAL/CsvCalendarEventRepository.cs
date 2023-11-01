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
        public CalendarEvent GetById(int id)
        {
            return calendarEvents.Single(c => c.CalendarEventId == id);
        }
        public List<CalendarEvent> GetAllCalendarEvents()
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ",",
            };
            using (var reader = new StreamReader(_pathToCsvFile))
            using (var csv = new CsvReader(reader, csvConfig))
            {
                calendarEvents = csv.GetRecords<CalendarEvent>().ToList();
                return calendarEvents;
            }
        }
        public void AddCalendarEvent(CalendarEvent calendarEvent)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ",",
            };
            List<CalendarEvent> calendarEvents = GetAllCalendarEvents();

            int nextCalendarEventId = calendarEvents.Count > 0 ? calendarEvents.Max(u => u.CalendarEventId) + 1 : 1;
            calendarEvent.CalendarEventId = nextCalendarEventId;

            try
            {
                calendarEvents.Add(calendarEvent);
                using (var writer = new StreamWriter(_pathToCsvFile, append: false))
                using (var csv = new CsvWriter(writer, csvConfig))
                {
                    csv.WriteHeader<CalendarEvent>();
                    csv.NextRecord();
                    //Console.Clear();
                    csv.WriteRecords(calendarEvents);
                    Console.WriteLine($" The Calendar Event named '{calendarEvent.CalendarEventName}' \n on day {calendarEvent.CalendarEventDate} \n starting at {calendarEvent.CalendarEventStartTime} \n and ending {calendarEvent.CalendarEventEndTime} as been added to the list successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
