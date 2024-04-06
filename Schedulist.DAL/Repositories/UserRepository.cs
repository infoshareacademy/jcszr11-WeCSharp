using Microsoft.AspNetCore.Identity;
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
                return _db.Users.ToList();
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
        //public async Task<bool> UpdateUser(User user)
        //{
        //    try
        //    {
        //        var abd = _db.Users.FirstOrDefault(u => u.Id == user.Id );
        //        var existingUser = await _userManager.FindByIdAsync(user.Id);
        //        if (existingUser != null)
        //        {
        //            existingUser.Name = user.Name;
        //            existingUser.Surname = user.Surname;
        //            existingUser.Email = user.Email;


        //            var result = await _userManager.UpdateAsync(existingUser);
        //            await _db.SaveChangesAsync();
        //            if (result.Succeeded)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                foreach (var error in result.Errors)
        //                {
        //                    _logger.LogError(error.Description);
        //                }
        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            _logger.LogError($"User with id {user.Id} not found.");
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while updating User in database.");
        //        return false;
        //    }
        //}
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
        public bool UpdateUser(User user)
        {
            try
            {
                var userFromDB = _db.Users.FirstOrDefault(u => u.Id == user.Id) ?? 
                    throw new Exception("User was not found in database");
                userFromDB.Name = user.Name;
                userFromDB.Surname = user.Surname;
                userFromDB.Email = user.Email;
                //_db.Users.Update(userFromDB);
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
