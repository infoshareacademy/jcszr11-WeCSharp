using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Schedulist.App.Models.Enum;
using Schedulist.App.ViewModels.Admin;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories;
using Schedulist.DAL.Repositories.Interfaces;

namespace Schedulist.App.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IWorkModeRepository _workModeRepository;
        private readonly UserManager<User> _userManager;
        public AdminController(ILogger<AdminController> logger, IUserRepository userRepository, IWorkModeRepository workModeRepository, UserManager<User> userManager) : base(logger)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _workModeRepository = workModeRepository;
        }
        public async Task<IActionResult> Management()
        {
            //await _userManager.GetRolesAsync();
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

            return View(new AdminViewModel { Users = userListItems, ListOfWorkModes = listOfWorkModes });
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
            }
            return RedirectToAction("ManageUsers");
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

                await _userManager.AddToRoleAsync(user, "USER");
            }
            return RedirectToAction("ManageUsers");
        }
        //[HttpGet]
        //public IActionResult Update(string userId) 
        //{

        //}
        //[HttpPost]
        //public IActionResult Update(User userToUpdate)
        //{

        //}
        [HttpPost]
        public IActionResult Delete(string id)
        {
            try
            {
                User userToDelete = _userRepository.GetUserById(id);
                _userRepository.DeleteUser(userToDelete);
                logger.LogInformation($"User deleted.");
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception occurred: {ex.Message}");
                PopUpNotification("Error occurred while deleting User", notificationType: NotificationType.error);
                return View();
            }
        }
        [HttpPost]
        public IActionResult CreateNewWorkMode(AdminViewModel model)
        {
            try
            {
                var newCategory = model.WorkMode;
                _workModeRepository.CreateWorkMode(newCategory);

                PopUpNotification("New WorkMode has been created successfully!");

                return Ok();
            }
            catch (Exception)
            {
                PopUpNotification("Error while saving WorkMode", notificationType: NotificationType.error);
            }

            return RedirectToAction(nameof(Management));
        }
    }
}
