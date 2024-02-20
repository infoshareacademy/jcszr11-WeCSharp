using Microsoft.Extensions.Logging;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.DAL.Repositories
{
    public class WorkModeRepository : BaseRepository, IWorkModeRepository
    {
        public WorkModeRepository(SchedulistDbContext db, ILogger<BaseRepository> logger) : base(db, logger)
        {
            
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
    }
}
