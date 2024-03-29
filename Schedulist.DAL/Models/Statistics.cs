using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration.Attributes;
using Schedulist.DAL;



namespace Schedulist.DAL.Models
{
    public class Statistics
    {
        [Key]
        public int Id { get; set; }
        public string? Name {  get; set; }
        public string? WorkMode { get; set; }
        public string? CalendarEvent { get; set; }
    }
}
