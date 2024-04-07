using Schedulist.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Schedulist.DAL.Repositories.Interfaces
{
    public interface IWorkModeForUserRepository
    {
        List<WorkModeForUser> GetAllWorkModesForUser();
        WorkModeForUser CreateWorkModeForUser(WorkModeForUser workMode);
        public void UpdateWorkModeForUser(int id, WorkModeForUser workMode);
        bool DeleteWorkModeForUser(WorkModeForUser workModeToDelete);
        WorkModeForUser GetWorkModeByUserIdAndDateOfWorkMode(string idUser, DateOnly dateWorkMode);
        public WorkModeForUser GetWorkModeById(int id);
        public ValidationResult WorkModeForUserValidation(WorkModeForUser workMode);
        //public ValidationResult WorkModeForUserDateValidation(WorkModeForUser workMode);

    }
}
