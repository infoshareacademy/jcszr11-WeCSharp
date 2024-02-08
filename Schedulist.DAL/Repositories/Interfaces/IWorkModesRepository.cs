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
        List<WorkModesForUser> GetAllWorkModes();
        void AddWorkModes(WorkModesForUser workMode);
        void ModifyWorkModes(int workModesID, WorkModesForUser workMode);
        void DeleteWorkModes(int workModesID);
        WorkModesForUser GetWorkModeByUserAndDate(int idUser, DateOnly dateWorkMode);

    }
}
