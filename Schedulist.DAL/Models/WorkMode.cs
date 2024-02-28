using System.ComponentModel.DataAnnotations;

namespace Schedulist.DAL.Models
{
    public class WorkMode
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
