using Microsoft.Extensions.Logging;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Schedulist.DAL.Repositories
{
    public class WorkModeForUserRepository : BaseRepository, IWorkModeForUserRepository
    {
        public WorkModeForUserRepository(SchedulistDbContext db, ILogger<BaseRepository> logger) : base(db, logger)
        {
        }
        public List<WorkModeForUser> GetAllWorkModesForUser()
        {
            try
            {
                return _db.WorkModesToUsers.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving Work Modes from the database.");
                return new List<WorkModeForUser>();
            }
        }
        public WorkModeForUser CreateWorkModeForUser(WorkModeForUser workMode)
        {
            throw new NotImplementedException();
        }
        public WorkModeForUser GetWorkModeByUserIdAndDateOfWorkMode(int userId, DateOnly dateOfWorkMode)
        {
            try
            {
                return _db.WorkModesToUsers.Where(w => w.UserId == userId && w.DateOfWorkMode == dateOfWorkMode).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving Work Mode from the database.");
                return new WorkModeForUser();
            }
        }
        public WorkModeForUser GetWorkModeById(int id)
        {
            try
            {
                return _db.WorkModesToUsers.FirstOrDefault(w => w.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving Work Mode from the database.");
                return new WorkModeForUser();
            }
        }
        public bool UpdateWorkModeForUser(WorkModeForUser workModeToUpdate)
        {
            try
            {
                _db.WorkModesToUsers.Update(workModeToUpdate);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating Work Mode from the database.");
                return false;
            }
        }
        public bool DeleteWorkModeForUser(WorkModeForUser workModeToDelete)
        {
            try
            {
                _db.WorkModesToUsers.Remove(workModeToDelete);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting Work Mode from the database.");
                return false;
            }
        }

        public ValidationResult WorkModeForUserValidation(WorkModeForUser workMode)
        {
            List<WorkModeForUser> allWorkModes = GetAllWorkModesForUser();
            var providedDateOfWorkMode = allWorkModes.FirstOrDefault(wm=>wm.UserId==workMode.UserId && wm.Id==workMode.Id && wm.DateOfWorkMode==workMode.DateOfWorkMode);
            if (providedDateOfWorkMode != null)
            {
                return new ValidationResult("There is already a work mode with the provided date or that takes place at the same day. Please provide different values.");
            }
            return ValidationResult.Success;
        }

        /*public ValidationResult WorkModeForUserDateValidation(WorkModeForUser workMode)
        {
            return ValidationResult.Success();
        }*/
    }
}
