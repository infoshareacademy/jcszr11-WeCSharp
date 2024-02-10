using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedulist.DAL.Models;

namespace Schedulist.DAL.Repositories.Interfaces
{
    public interface IWorkModesRepository
    {
        List<WorkModeForUser> GetAllWorkModes();
        WorkModeForUser CreateWorkMode(WorkModeForUser workMode);
        bool UpdateWorkModes(WorkModeForUser workMode);
        bool DeleteWorkMode(WorkModeForUser workModeToDelete);
        WorkModeForUser GetWorkModeByUserIdAndDateOfWorkMode(int idUser, DateOnly dateWorkMode);

    }
}
