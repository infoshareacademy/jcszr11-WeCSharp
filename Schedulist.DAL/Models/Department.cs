using System.ComponentModel.DataAnnotations;

namespace Schedulist.DAL.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User>? User { get; set; }
    }
}
