using Schedulist.DAL.Models;

namespace Schedulist.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public List<User> GetAllUsers();
        public User GetUserById(string id);
        public bool CreateUser(User user);
        public bool DeleteUser(User user);
        public bool UpdateUser(User user, Department department, Position position);

    }
}
