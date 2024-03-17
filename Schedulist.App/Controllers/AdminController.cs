using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Schedulist.App.ViewModels.Admin;
using Schedulist.DAL.Models;
using Schedulist.DAL.Repositories.Interfaces;

namespace Schedulist.App.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        public AdminController(ILogger<AdminController> logger, IUserRepository userRepository, UserManager<User> userManager) : base(logger)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
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
                    Email = user.Email,
                    Roles = string.Join(",", (await _userManager.GetRolesAsync(user)))
            });
            }

            return View(new IndexViewModel { Users = userListItems });
        }
        [HttpPost]
        public async Task<IActionResult> AddToAdministrator(string userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user != null && !await _userManager.IsInRoleAsync(user, "ADMIN"))
            {
                await _userManager.AddToRoleAsync(user, "ADMIN");
            }
            return RedirectToAction("Index");
        }
    }
}
