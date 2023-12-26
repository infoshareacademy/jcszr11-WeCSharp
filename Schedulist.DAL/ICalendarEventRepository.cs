using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.DAL
{
    public interface ICalendarEventRepository
    {
        List<CalendarEvent> GetAllCalendarEvents();
        void AddCalendarEvent(CalendarEvent calendarEvent);
    }
}
