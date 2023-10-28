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
        private List<CalendarEvent> _calendars;
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
                _calendars = csv.GetRecords<CalendarEvent>().ToList();
                return _calendars;
            }
        }

        public CalendarEvent GetById(int id)
        {
            return _calendars.Single(c => c.CalendarEventId == id);
        }

        public void Add(CalendarEvent item)
        {
            _calendars.Add(item);
        }
    }
}
