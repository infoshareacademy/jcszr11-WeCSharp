using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedulist.DAL.Models;

namespace Schedulist.DAL.Repositories.Interfaces
{
    public interface IWorkModeForUserRepository
    {
        List<WorkModeForUser> GetAllWorkModesForUser();
        WorkModeForUser CreateWorkModeForUser(WorkModeForUser workMode);
        bool UpdateWorkModeForUser(WorkModeForUser workMode);
        bool DeleteWorkModeForUser(WorkModeForUser workModeToDelete);
        WorkModeForUser GetWorkModeByUserIdAndDateOfWorkMode(int idUser, DateOnly dateWorkMode);
        public WorkModeForUser GetWorkModeById(int id);
        public ValidationResult WorkModeForUserValidation(WorkModeForUser workMode);
        //public ValidationResult WorkModeForUserDateValidation(WorkModeForUser workMode);

    }
}
