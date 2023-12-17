using Schedulist.DAL;

namespace Schedulist.App.Models
{
    public class DayViewModel
    {
        public DateTime Date { get; set; }
        public User User { get; set; }
        public string WorkMode { get; set; }
        public List<CalendarEvent> CalendarEvents { get; set; }
        public DayViewModel(DateTime date)
        {
            Date = date;
        }
    }
}
