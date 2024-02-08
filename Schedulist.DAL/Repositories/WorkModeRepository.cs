using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories.Interfaces;

namespace Schedulist.DAL.Repositories
{
    public class WorkModeRepository : BaseRepository, IWorkModesRepository
    {
        public WorkModeRepository(DBContact db, ILogger<BaseRepository> logger) : base(db, logger)
        {
        }
        public void AddWorkModes(WorkModesForUser workMode)
        {
            throw new NotImplementedException();
        }

        public void DeleteWorkModes(int workModesID)
        {
            throw new NotImplementedException();
        }

        public List<WorkModesForUser> GetAllWorkModes()
        {
            throw new NotImplementedException();
        }

        public WorkModesForUser GetWorkModeByUserAndDate(int idUser, DateOnly dateWorkMode)
        {
            throw new NotImplementedException();
        }

        public void ModifyWorkModes(int workModesID, WorkModesForUser workMode)
        {
            throw new NotImplementedException();
        }
    }
}
