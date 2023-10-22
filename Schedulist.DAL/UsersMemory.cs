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
        private List<User> users = new List<User> {
            new User("T", "G", "e", "a", "User", null),
            new User("T", "G", "e", "a", "Admin", null)
            {
                AdminPrivilege = true,
            }
        };
        public List<User> GetUsers()
        {
            return users;   
        }
    }
}
