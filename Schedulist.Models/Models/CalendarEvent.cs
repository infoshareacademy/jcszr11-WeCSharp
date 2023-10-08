using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.Models.Models
{
    internal class CalendarEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int UserId { get; set; }
    }
}
