using Schedulist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.DAL
{
    public class InMemoryUserRepository : IUserRepository
    {
        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllUsers()
        {
            return new List<User> 
            {
            new User("Tomasz", "Tomaszewicz", "Driver", "Logistics", "LogU1", "passU1"),
            new User("Bartek", "Bartkowicz", "Salesmen", "Sales Department","LogU2", "passU2"),
            new User("Romek", "Romanowicz", "CEO", "Executive", "LogA1", "passA1")
                {
                    AdminPrivilege = true,
                }
            };

        }
    }
}
