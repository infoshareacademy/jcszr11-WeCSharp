using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories.Interfaces;

namespace Schedulist.DAL.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly UserManager<User> _userManager;
        public UserRepository(SchedulistDbContext db, ILogger<BaseRepository> logger, UserManager<User> userManager) : base(db, logger)
        {
            _userManager = userManager;
        }
        public List<User> GetAllUsers()
        {
            try
            {
                return _db.Users.Include(u => u.Department).Include(u =>u.Position).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving Users from the database.");
                return new List<User>();
            }
        }
        public User GetUserById(string id)
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
        public bool UpdateUser(User user, Department department, Position position)
        {
            try
            {
                var userFromDB = _db.Users.FirstOrDefault(u => u.Id == user.Id) ?? 
                    throw new Exception("User was not found in database");

                userFromDB.Name = user.Name;
                userFromDB.Surname = user.Surname;
                userFromDB.Email = user.Email;
                userFromDB.Department = _db.Departments.FirstOrDefault(d => d.Id == department.Id);
                userFromDB.Position = _db.Positions.FirstOrDefault(d => d.Id == position.Id);

                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the User in database.");
                return false;
            }
        }
    }
}
