using Microsoft.AspNetCore.Identity;
using Schedulist.DAL.Models;

namespace Schedulist.DAL
{
    public class DBSeed
    {
        private readonly UserManager<User> _userManager;
        public DBSeed(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task CreateAdmin()
        {

            var user = await _userManager.FindByNameAsync("kurs@gmail.com");
            if (user == null)
            {
                user = new()
                {
                    Email = "kurs@gmail.com",
                    UserName = "kurs@gmail.com",
                    Surname = "Andrzejewicz",
                    EmailConfirmed = true,
                    DepartmentId = 1,
                    PositionId = 1
                };
                await _userManager.CreateAsync(user, "Kurs1234!");
                await _userManager.AddToRoleAsync(user, "ADMIN");
            }
        }
    }
}
