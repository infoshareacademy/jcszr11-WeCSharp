using CsvHelper.Configuration.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.DAL
{
    public class WorkModesToUser
    {
        [Name("id")]
        [Display(Name = "Work Mode ID")]
        public int WorkModeToUserID { get; set; }

        [Name("name")]
        [Display(Name = "Work Mode Name")]
        public string WorkModeName { get; set; }

        [Name("userid")]
        [Display(Name = "User ID")]
        public int UserID { get; set; }

        [Name("dow")]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateOnly DateOfWorkmode { get; set; }

        public WorkModesToUser()
        { }
        public WorkModesToUser(int id, string name, int userid, DateOnly dow)
        {
            WorkModeToUserID = id;            
            WorkModeName = name;
            UserID = userid;
            DateOfWorkmode = dow;
        }
    }
}
