using Schedulist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.DAL
{
    public class UsersMemory
    {
        //User  name  surname  position  department  login)
        public static List<User> listOfUsers = new List<User> {
            new User("Tomasz", "Tomaszewicz", "Driver", "Logistics", "LogU1", "passU1"),
            new User("Bartek", "Bartkowicz", "Salesmen", "Sales Department","LogU2", "passU2"),
            new User("Romek", "Romanowicz", "CEO", "Executive", "LogA1", "passA1")
            {
                AdminPrivilege = true,
            }
        };

        //public List<User> GetUsers()
        //{
        //    return listOfUsers;   
        //}
    }
}
