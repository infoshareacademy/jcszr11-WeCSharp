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
        private List<User> users = new List<User> { new User("Basic user", "Login1"), new User("Admin user", "Login2", Rights.IsAdmin, Rights.DeleteUser, Rights.AddUser) };
         
        public List<User> GetUsers()
        {
            return users;   
        }
    }
}
