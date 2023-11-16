
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.DAL
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        void AddUser(User user, int? ID);
        void ModifyUser(string userToModifyLogin, User modifiedUser);
        void DeleteUser(User userToDelete);
    }
}
