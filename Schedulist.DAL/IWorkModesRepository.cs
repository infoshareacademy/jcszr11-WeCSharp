using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.DAL
{
    public interface IWorkModesRepository
    {
        List<WorkModesToUser> GetAllWorkModes();
        void AddWorkModes(WorkModesToUser workMode);
        void ModifyWorkModes(WorkModesToUser workMode);
        WorkModesToUser GetWorkModeByUserAndDate(int idUser, DateOnly dateWorkMode);
        
    }
}
