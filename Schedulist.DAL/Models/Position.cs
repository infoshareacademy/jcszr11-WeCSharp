using System.ComponentModel.DataAnnotations;

namespace Schedulist.DAL.Models
{
    public class Position
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User>? User { get; set; }
    }
}
