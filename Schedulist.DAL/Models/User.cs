using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Schedulist.DAL.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }

        //EntityFramework Configuration Section
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; } 

        public int? PositionId { get; set; }
        public Position? Position { get; set; } 

        public List<WorkModeForUser>? WorkModesForUser { get; set; }

        public ICollection<CalendarEvent>? CalendarEvents { get; set; }
        //EntityFramework Configuration Section
    }
}
