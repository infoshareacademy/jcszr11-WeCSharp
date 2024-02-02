using Schedulist.App.Models;
using Schedulist.DAL;

namespace Schedulist.App.Services
{
    public class WorkModeService
    {
        private readonly CSVWorkModesRepository _repository;
        public WorkModesToUser GetWorkModeById(int id)
        {
            CSVWorkModesRepository repository = new CSVWorkModesRepository("..\\Schedulist\\WorkModes.csv");
            var workModes = repository.GetAllWorkModes();
            var workModeById = workModes
                .FirstOrDefault(m => m.WorkModeToUserID == id);
            return workModeById;
        }

        public WorkModesToUser Create(WorkModesToUser workModes)
        {
            CSVWorkModesRepository repository = new CSVWorkModesRepository("..\\Schedulist\\WorkModes.csv");
            repository.AddWorkModes(workModes);
            return workModes;
        }

        public int Delete (int id)
        {
            CSVWorkModesRepository repository = new CSVWorkModesRepository("..\\Schedulist\\WorkModes.csv");
            repository.DeleteWorkModes(id);
            return id;
        }

        public WorkModesToUser Edit (WorkModesToUser workModes)
        {
            CSVWorkModesRepository repository = new CSVWorkModesRepository("..\\Schedulist\\WorkModes.csv");
            var newWorkMode = GetWorkModeById(workModes.WorkModeToUserID);
            
            newWorkMode.UserID = workModes.UserID;
            newWorkMode.WorkModeName = workModes.WorkModeName;
            newWorkMode.DateOfWorkmode = workModes.DateOfWorkmode;

            repository.ModifyWorkModes(newWorkMode.WorkModeToUserID,newWorkMode);
            return workModes;
        }
    }
}
