using Schedulist.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.Business
{
    internal class AdminCommands
    {
        private List<User> _userlist = new CsvUserRepository("Users.csv").GetAllUsers();
        public User DiplayUsers()
        {
            Console.Clear();
            Console.WriteLine("====== List of users ======");
            Console.WriteLine("ID.  Name and surname");
            foreach (User user in _userlist)
            {
                Console.WriteLine($"{user.Id}.    {user.Name} {user.Surname}");
            }
            Console.WriteLine("===============================================================================");
            Console.WriteLine("Choose user from the list by ID or type x to choose yourself:");
            while (true)
            {
                string option = Console.ReadLine();
                if (option == "x") return CurrentUser.currentUser;
                else foreach (User user in _userlist)
                    {
                        if (user.Id.ToString() == option) return user;
                    }
            }
        }
    }
}
