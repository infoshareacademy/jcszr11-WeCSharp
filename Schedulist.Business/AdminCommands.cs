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
            Console.WriteLine("====== List of users ======");
            Console.WriteLine("ID.  Name and surname");
            for (int i = 0; i < _userlist.Count; i++)
            {
                Console.WriteLine($"{i}.    {_userlist[i].Name} {_userlist[i].Surname}");
            }
            Console.WriteLine("===============================================================================");
            Console.WriteLine("Choose user from the list by ID or click Backspace to choose yourself:");
            int ID;
            while (true)
            {
                var option = Console.ReadKey();
                if (option.Key == ConsoleKey.Backspace) return CurrentUser.currentUser;
                else if (char.IsDigit(option.KeyChar))
                    if ((int.Parse(option.KeyChar.ToString()) <= _userlist.Count))
                    {
                        ID = (int.Parse(option.KeyChar.ToString()));
                        return _userlist[ID];
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
