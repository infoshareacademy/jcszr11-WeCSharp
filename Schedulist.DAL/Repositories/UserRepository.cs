using Microsoft.Extensions.Logging;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories.Interfaces;

namespace Schedulist.DAL.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(SchedulistDbContext db, ILogger<BaseRepository> logger) : base(db, logger)
        {

        }
        public List<User> GetAllUsers()
        {
            try
            {
                return _db.Users.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving Users from the database.");
                return new List<User>();
            }
        }
        public User GetUserById(int id)
        {
            try
            {
                return _db.Users.FirstOrDefault(x => x.Id == id);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving User from the database.");
                return new User();
            }
        }
        public bool CreateUser(User user)
        {
            try
            {
                _db.Users.Add(user);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving User to the database.");
                return false;
            }
        }
        public bool UpdateUser(User user)
        {
            try
            {
                _db.Users.Update(user);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating User in database.");
                return false;
            }
        }
        public bool DeleteUser(User user)
        {
            try
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting Calendar Event in database.");
                return false;
            }
        }
    }
}
