using Schedulist.DAL.Models;

namespace Schedulist.App.ViewModels
{
    public class CalendarEventViewModel
    {
        public List<CalendarEvent> CalendarEvents { get; set; }
        public string SearchPhrase { get; set; }
        public DateOnly SearchDate { get; set; }
        public int TotalItems { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
