using Microsoft.AspNetCore.Mvc;
using Schedulist.App.Controllers;
using Schedulist.App.Services.Interfaces;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;
using ControllerBase = Schedulist.App.Controllers.ControllerBase;

namespace Schedulist.App.Services
{
    public class WorkModeForUserService : ControllerBase, IWorkModeForUserService
    {
        private readonly IWorkModeForUserRepository _workModeForUserRepository;
        public WorkModeForUserService(ILogger<WorkModeForUserService> logger,  IWorkModeForUserRepository workModeForUserRepository) : base(logger)
        {
            _workModeForUserRepository = workModeForUserRepository;
        }
        public List<ValidationResult> ValidateWorkMode(WorkModeForUser workModeForUser)
        {
            var validationResult = _workModeForUserRepository.WorkModeForUserValidation(workModeForUser);

            return new List<ValidationResult>
            {
                validationResult
            };
        }
    }
}
