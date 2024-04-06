using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Schedulist.App.Exceptions;
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
                var workModesList = _db.WorkModesToUsers.Include(e => e.User).Include(w => w.WorkMode).ToList();
                return workModesList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving Work Modes from the database.");
                return new List<WorkModeForUser>();
            }
        }
        public WorkModeForUser CreateWorkModeForUser(WorkModeForUser workMode)
        {
            _db.WorkModesToUsers.Add(workMode);
            _db.SaveChanges();
            return workMode;
        }
        public WorkModeForUser GetWorkModeByUserIdAndDateOfWorkMode(string userId, DateOnly dateOfWorkMode)
        {
            try
            {
                var workModeByUserAndDate = _db.WorkModesToUsers.Where(w => w.UserId == userId && w.DateOfWorkMode == dateOfWorkMode).FirstOrDefault();
                return workModeByUserAndDate;
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
                var workModeById = _db.WorkModesToUsers.Include(e => e.User).Include(w => w.WorkMode).FirstOrDefault(w => w.Id == id);
                return workModeById;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving Work Mode from the database.");
                return new WorkModeForUser();
            }
        }
        public void UpdateWorkModeForUser(int id, WorkModeForUser workModeToUpdate)
        {
            try
            {
                var workMode = GetWorkModeById(id);
                workMode.DateOfWorkMode = workModeToUpdate.DateOfWorkMode;
                workMode.WorkModeId = workModeToUpdate.WorkModeId;
                workMode.UserId = workModeToUpdate.UserId;
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating Work Mode from the database.");
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
            var providedDateOfWorkMode = allWorkModes.FirstOrDefault(wm=>wm.UserId==workMode.UserId && wm.DateOfWorkMode==workMode.DateOfWorkMode);
            if (providedDateOfWorkMode != null)
            {
                return new ValidationResult("There is already a work mode for that day. Please provide different values.");
            }
            return ValidationResult.Success;
        }
    }
}
