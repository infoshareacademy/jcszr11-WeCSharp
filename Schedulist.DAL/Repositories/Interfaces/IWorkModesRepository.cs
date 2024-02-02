using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.DAL.Repositories.Interfaces
{
    public interface IWorkModesRepository
    {
        List<WorkModesToUser> GetAllWorkModes();
        void AddWorkModes(WorkModesToUser workMode);
        void ModifyWorkModes(int workModesID, WorkModesToUser workMode);
        void DeleteWorkModes(int workModesID);
        WorkModesToUser GetWorkModeByUserAndDate(int idUser, DateOnly dateWorkMode);

    }
}
