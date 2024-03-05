using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Schedulist.DAL.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        //public string Login { get; set; }
        //public string Password { get; set; }
        //public bool AdminPrivilege { get; set; }

        //EntityFramework Configuration Section
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public int PositionId { get; set; }
        public Position? Position { get; set; }

        public List<WorkModeForUser>? WorkModesForUser { get; set; }

        public ICollection<CalendarEvent>? CalendarEvents { get; set; }
        //EntityFramework Configuration Section
    }
}
