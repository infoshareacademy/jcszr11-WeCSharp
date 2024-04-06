using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Schedulist.DAL.Models
{
    public class WorkModeForUser
    {
        [Name("Id")]
        [Key]
        public int Id { get; set; }

        [Name("Date Of Work Mode")]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Required]
        public DateOnly DateOfWorkMode { get; set; }

        //EntityFramework Configuration Section
        [Name("UserId")]
        [Display(Name = "Assigned to User")]
        public string UserId { get; set; }
        public User User { get; set; }

        [Name("WorkModeId")]
        public int WorkModeId { get; set; }
        public WorkMode WorkMode { get; set; }
        //EntityFramework Configuration Section

    }
}
