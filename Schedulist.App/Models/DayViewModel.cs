using Schedulist.DAL;

namespace Schedulist.App.Models
{
    public class DayViewModel
    {
        public DateOnly Date { get; set; }
        public User User { get; set; }
        public string WorkMode { get; set; }
        public List<CalendarEvent> CalendarEvents { get; set; }
        public DayViewModel(DateOnly date, User user)
        {
            Date = date;
            User = user;
        }
    }
}