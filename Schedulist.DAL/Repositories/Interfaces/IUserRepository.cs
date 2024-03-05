
using Schedulist.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public List<User> GetAllUsers();
        public User GetUserById(string id);
        public bool CreateUser(User user);
        public bool UpdateUser(User user);
        public bool DeleteUser(User user);

    }
}
