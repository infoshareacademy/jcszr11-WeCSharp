using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schedulist.DAL.Models
{
    public class WorkModesForUser
    {
        [Name("Id")]
        [Key]
        public int Id { get; set; }

        [Name("DateOfWorkMode")]
        public DateOnly DateOfWorkMode { get; set; }

        //EntityFramework Configuration Section
        [Name("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Name("WorkModeId")]
        public int WorkModeId { get; set; }
        public WorkMode WorkMode { get; set; }
        //EntityFramework Configuration Section

    }
}
