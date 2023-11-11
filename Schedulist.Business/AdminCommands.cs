using Schedulist.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.Business
{
    public class AdminCommands
    {
        private static List<User> _userlist = new CsvUserRepository("..\\..\\..\\Users.csv").GetAllUsers();
        public static User DisplayUsers()
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
        //public User DisplayUsersToDelete()
        //{
        //    List<User> users = new CsvUserRepository("..\\..\\..\\Users.csv").GetAllUsers();

        //    Console.WriteLine("Choose a number of User to delete:");

        //    for (int i = 0; i < users.Count; i++)
        //    {
        //        Console.WriteLine($"{i + 1}. {users[i].Name} {users[i].Surname}");
        //    }

        //    int userChoice;
        //    while (true)
        //    {
        //        if (int.TryParse(Console.ReadLine(), out userChoice) && userChoice >= 1 && userChoice <= users.Count)
        //        {
        //            return users[userChoice - 1];
        //        }
        //        else
        //        {
        //            Console.WriteLine("Invalid choice. Please enter a valid number.");
        //        }
        //    }
        //}
    }
}
