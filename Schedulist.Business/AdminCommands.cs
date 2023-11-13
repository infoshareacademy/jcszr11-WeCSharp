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
        private List<User> _userlist = new CsvUserRepository("..\\..\\..\\Users.csv").GetAllUsers();
        public User DisplayUsers(string methodName)
        {
            Console.Clear();
            Console.WriteLine($"====== List of users to {methodName}======");
            Console.WriteLine("ID.  Name and surname");
            for (int i = 0; i < _userlist.Count; i++)
            {
                Console.WriteLine($"{i+1}.    {_userlist[i].Name} {_userlist[i].Surname}");
            }
            Console.WriteLine("===============================================================================");
            Console.WriteLine("Choose user from the list by ID or type x to choose yourself:");
            while (true)
            {
                string option = Console.ReadLine();
                if (option == "x") return CurrentUser.currentUser;
                //else for (int i = 0; i < _userlist.Count; i++)
                else if (int.TryParse(option, out int i))
                {
                    if (i <= _userlist.Count) return _userlist[i-1];
                }
                else Console.WriteLine("error, not a number, please try again:");
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
