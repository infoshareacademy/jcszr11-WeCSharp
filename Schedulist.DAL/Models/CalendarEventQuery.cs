namespace Schedulist.DAL.Models
{
    public class CalendarEventQuery
    {
        public string SearchPhrase { get; set; }
        public DateOnly SearchDate { get; set; }
        public int PageNumber { get; set;}
        public int PageSize { get; set;}
    }
}
