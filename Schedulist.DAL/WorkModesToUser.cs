using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.DAL
{
    public class WorkModesToUser
    {
        [Name("id")]
        public int WorkModeToUserID { get; set; }
        [Name("name")]
        public string WorkModeName { get; set; }
        [Name("userid")]
        public int UserID { get; set; }
        [Name("dow")]
        public DateOnly dateOfWorkmode { get; set; }
        
        public WorkModesToUser(int id, string name, int userid, DateOnly dow)
        {
            WorkModeToUserID = id;            
            WorkModeName = name;
            UserID = userid;
            dateOfWorkmode = dow;
        }
        public WorkModesToUser(int id) 
        { 
        
        }
    }
}
