using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Schedulist.App.Services.Interfaces
{
    public interface IWorkModeForUserService
    {
        public List<ValidationResult> ValidateWorkMode(WorkModeForUser workModeForUser);
    }
}
