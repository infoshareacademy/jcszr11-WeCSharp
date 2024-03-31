using Schedulist.DAL.Models;

namespace Schedulist.DAL.Repositories.Interfaces
{
    public interface IWorkModeRepository
    {
        List<WorkMode> GetAllWorkModes();
        WorkMode GetWorkModeById(int id);
        bool CreateWorkMode(WorkMode newModeMode);
    }
}
