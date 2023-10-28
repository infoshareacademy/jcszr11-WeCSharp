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
    public class CsvCalendarEventRepository : IRepository<CalendarEvent>
    {
        private string _pathToCsvFile;
        private List<CalendarEvent> _calendarEvents;
        public CsvCalendarEventRepository(string pathToCsvFile)
        {
            _pathToCsvFile = pathToCsvFile;
        }

        public List<CalendarEvent> GetAll()
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                //Delimiter = "\t",
            };
            using (var reader = new StreamReader(_pathToCsvFile))
            using (var csv = new CsvReader(reader, csvConfig))
            {
                _calendarEvents = csv.GetRecords<CalendarEvent>().ToList();
                return _calendarEvents;
            }
        }

        public CalendarEvent GetById(int id)
        {
            return _calendarEvents.Single(c => c.CalendarEventId == id);
        }

        public void Add(CalendarEvent item)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                //Delimiter = "\t",
            };
            _calendarEvents = GetAll();

            try
            {
                _calendarEvents.Add(item);
                using (StreamWriter writer = new StreamWriter(_pathToCsvFile))
                using (var csv = new CsvWriter(writer, csvConfig))
                {
                    csv.WriteRecords(_calendarEvents);
                    Console.Clear();
                    Console.WriteLine($" The Calendar Event named '{item.CalendarEventName}' /n staring {item.CalendarEventStartDateTime} /n and ending {item.CalendarEventEndDateTime} as been added to the list successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            // _calendarEvents.Add(item);
        }
    }
}
