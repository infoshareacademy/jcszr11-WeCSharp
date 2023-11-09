using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.DAL
{
    public class WorkModesToUser
    {
        public int WorkModeToUserID { get; set; }
        public int WorkModeIDName { get; set; }
        public string WorkModeName { get; set; }
        public User? UserID { get; set; }
        public DateOnly dateOfWorkmode { get; set; }
        
        public WorkModesToUser(int id, int idname, string name, User userid, DateOnly dow)
        {
            WorkModeToUserID = id;
            WorkModeIDName = idname;
            WorkModeName = name;
            UserID = userid;
            dateOfWorkmode = dow;
        }
        public WorkModesToUser() 
        { 
        
        }
    }
}
