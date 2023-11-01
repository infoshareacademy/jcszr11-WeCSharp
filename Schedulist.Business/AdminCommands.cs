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
    }
}
