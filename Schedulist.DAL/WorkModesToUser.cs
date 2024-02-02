using CsvHelper.Configuration.Attributes;
using Schedulist.DAL.Models;
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
        [Key]
        public int WorkModeToUserID { get; set; }
        [Name("workModeId")]
        public int WorkModeId { get; set; }
        [Name("userId")]
        public int UserID { get; set; }
        [Name("dateOfWorkMode")]
        public DateOnly DateOfWorkMode { get; set; }
        public User User { get; set; }
        public WorkMode WorkMode { get; set; }

        //public WorkModesToUser(int id, /*string name,*/ int userid, DateOnly dow)
        //{
        //    WorkModeToUserID = id;            
        //    //WorkModeName = name;
        //    UserID = userid;
        //    DateOfWorkMode = dow;
        //}
    }
}
