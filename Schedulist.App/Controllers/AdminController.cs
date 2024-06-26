﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Schedulist.App.Models.Enum;
using Schedulist.App.Services;
using Schedulist.App.ViewModels.Admin;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories.Interfaces;

namespace Schedulist.App.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IWorkModeRepository _workModeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IPositionRepository _positiontRepository;
        private readonly UserManager<User> _userManager;
        public AdminController(ILogger<AdminController> logger, IUserRepository userRepository, IWorkModeRepository workModeRepository, IDepartmentRepository departmentRepository, IPositionRepository positionRepository, UserManager<User> userManager) : base(logger)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _workModeRepository = workModeRepository;
            _departmentRepository = departmentRepository;
            _positiontRepository = positionRepository;
        }
        public async Task<IActionResult> Management()
        {
            var userListItems = new List<UserListItemModel>();
            var allUsers = _userRepository.GetAllUsers().ToList();
            foreach (var user in allUsers)
            {
                userListItems.Add(new UserListItemModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Department = user.Department,
                    Position = user.Position,
                    Roles = string.Join(",", (await _userManager.GetRolesAsync(user)))
                });
            }
            var listOfWorkModes = _workModeRepository.GetAllWorkModes().ToList();

            return View(new AdminViewModel { Users = userListItems, ListOfWorkModes = listOfWorkModes });
        }
        public async Task<IActionResult> SendEmail(string email)
        {
            var isReportSent = EmailReportService.SendEmail(email);
            if (isReportSent)
            {
                logger.LogInformation($"Email successfully sent to {email}");
            }
            else
            {
                logger.LogError($"Email unsuccessfully sent to {email}");
            }
            var userListItems = new List<UserListItemModel>();
            var allUsers = _userRepository.GetAllUsers().ToList();
            foreach (var user in allUsers)
            {
                userListItems.Add(new UserListItemModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Roles = string.Join(",", (await _userManager.GetRolesAsync(user)))
                });
            }
            var listOfWorkModes = _workModeRepository.GetAllWorkModes().ToList();

            return View("Management", new AdminViewModel { Users = userListItems, ListOfWorkModes = listOfWorkModes });
        }
        [HttpPost]
        public async Task<IActionResult> AddToAdministrator(string userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    await _userManager.RemoveFromRoleAsync(user, role);
                }
                await _userManager.AddToRoleAsync(user, "ADMIN");
                PopUpNotification("Permissions have been changed");
            }
            return RedirectToAction("Management");
        }
        [HttpPost]
        public async Task<IActionResult> AddToStandardRole(string userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    await _userManager.RemoveFromRoleAsync(user, role);
                }
                PopUpNotification("Permissions have been changed");
                await _userManager.AddToRoleAsync(user, "USER");
            }
            return RedirectToAction("Management");
        }

        [HttpPost]
        public IActionResult CreateNewWorkMode(AdminViewModel adminViewModel)
        {
            try
            {
                var newWorkmode = adminViewModel.WorkMode;
                _workModeRepository.CreateWorkMode(newWorkmode);

                PopUpNotification("New WorkMode has been created successfully!");

                return RedirectToAction(nameof(Management));
            }
            catch (Exception)
            {
                PopUpNotification("Error while saving WorkMode", notificationType: NotificationType.error);
            }

            return RedirectToAction(nameof(Management));
        }
        [HttpPost]
        public IActionResult DeleteWorkMode(AdminViewModel model)
        {
            try
            {
                WorkMode workModeToDelete = _workModeRepository.GetWorkModeById(model.WorkMode.Id);
                _workModeRepository.DeleteWorkMode(workModeToDelete);
                PopUpNotification("Work Mode has been deleted successfully!");
                logger.LogInformation($"Work Mode deleted.");

                return RedirectToAction(nameof(Management));
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception occurred: {ex.Message}");
                PopUpNotification("Error occurred while deleting User", notificationType: NotificationType.error);

                return RedirectToAction(nameof(Management));
            }
        }
        [HttpPost]
        public IActionResult DeleteUser(string Id)
        {
            try
            {
                var userToDelete = _userRepository.GetUserById(Id);
                _userRepository.DeleteUser(userToDelete);

                return RedirectToAction(nameof(Management));
            }
            catch (Exception)
            {
                PopUpNotification("Error occurred while deleting the User", notificationType: NotificationType.error);
            }
            return RedirectToAction(nameof(Management));
        }

        [HttpGet]
        public IActionResult UpdateUser(string Id)
        {
            var userToUpdate = _userRepository.GetUserById(Id);
            AdminViewModel adminViewModel = new AdminViewModel();
            adminViewModel.User = userToUpdate;
            adminViewModel.Departments = _departmentRepository.GetAllDepartments();
            adminViewModel.Positions = _positiontRepository.GetAllPositions();

            return View(adminViewModel);
        }

        [HttpPost]
        public IActionResult UpdateUser(AdminViewModel adminViewModel)
        {
            var newDepartment = adminViewModel.Department;
            var newPosition =  adminViewModel.Position;
            try
            {
                if (ModelState.IsValid)
                {
                    _userRepository.UpdateUser(adminViewModel.User, newDepartment, newPosition);

                    PopUpNotification("User has been updated successfully");

                    return RedirectToAction(nameof(Management));
                }
            }
            catch (Exception)
            {
                PopUpNotification("Error occurred while updating User", notificationType: NotificationType.error);
            }
            return RedirectToAction(nameof(Management));
        }
    }
}
