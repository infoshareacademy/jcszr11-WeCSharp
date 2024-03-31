using Microsoft.Extensions.Logging;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories.Interfaces;

namespace Schedulist.DAL.Repositories
{
    public class WorkModeRepository : BaseRepository, IWorkModeRepository
    {
        public WorkModeRepository(SchedulistDbContext db, ILogger<BaseRepository> logger) : base(db, logger)
        {
            
        }

        public WorkMode GetWorkModeById(int id)
        {
            return GetAllWorkModes().Where(e => e.Id == id).FirstOrDefault();
        }

        public List<WorkMode> GetAllWorkModes()
        {
            try
            {
                return _db.WorkModes.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving from database");
                return new List<WorkMode>();
            }
        }
        public bool CreateWorkMode(WorkMode newModeMode)
        {
            try
            {
                _db.WorkModes.Add(newModeMode);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving new Work Mode to DB");
                return false;
            }
        }
    }
}
