using Schedulist.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Schedulist.DAL
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        //public string Department { get; set; }
        //public string Position { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool AdminPrivilege { get; set; }

        //EntityFramework Configuration Section
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public int PositionId { get; set; }
        public Position? Position { get; set; }

        public List<WorkModesForUser>? WorkModesForUser { get; set; }

        public ICollection<CalendarEvent>? CalendarEvents { get; set; }
        //EntityFramework Configuration Section
    }
}
