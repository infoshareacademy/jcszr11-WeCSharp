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
                //Delimiter = "\t",
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
                //Delimiter = "\t",
            };
            calendarEvents = GetAllCalendarEvents();
            try
            {
                calendarEvents.Add(calendarEvent);
                using (StreamWriter writer = new StreamWriter(_pathToCsvFile))
                using (var csv = new CsvWriter(writer, csvConfig))
                {
                    csv.WriteRecords(calendarEvents);
                    //Console.Clear();
                    Console.WriteLine($" The Calendar Event named '{calendarEvent.CalendarEventName}' /n staring {calendarEvent.CalendarEventStartDateTime} /n and ending {calendarEvent.CalendarEventEndDateTime} as been added to the list successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            // calendarEvents.Add(item);
        }
    }
}
