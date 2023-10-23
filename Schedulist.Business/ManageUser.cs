using Schedulist.DAL;
using Schedulist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.Business
{
    internal class ManageUser
    {
        IUserRepository _userRepository;
        User _user;

        //public ManageUser(IUserRepository userRepository);
        //{
        //    _userRepository = userRepository;
        //}

        internal void Create()
        {
            Console.WriteLine("====== Create User Section ======");
            string name = ConsolHelper("Type User name");
            string surname = ConsolHelper("Type User surname");
            string position = ConsolHelper("Type User position");
            string department = ConsolHelper("Type User department");
            string login = ConsolHelper("Type User login");
            string password = ConsolHelper("Type User password");
            Console.WriteLine("Is created User Admin? type y/n");
            bool isAdmin = false;
            switch (Console.ReadLine())
            {
                case "y":
                    isAdmin = true;
                    break;
                case "n":
                    isAdmin = false;
                    break;
                default:
                    Console.WriteLine("Invalid Admin access");
                    break;
            }
            User user = new User(name, surname, position, department, login, password)
            { AdminPrivilege = isAdmin, Id = 4 };
            new CsvUserRepository("C:\\Users\\Mariusz\\Desktop\\InfoShare\\#4 Projekt grupowy Work-Schedule\\jcszr11-WeCSharp\\Schedulist\\Users.csv").AddUser(user);
        }

        internal void Modify()
        {
            Console.WriteLine();
            Console.WriteLine("===============================================================================");
            Console.WriteLine("Modify User");
            Console.WriteLine("===============================================================================");
        }
        internal void Delete()
        {
            Console.WriteLine();
            Console.WriteLine("===============================================================================");
            Console.WriteLine("Delete User");
            Console.WriteLine("===============================================================================");
        }

        internal string ConsolHelper(string message)
        {
            Console.WriteLine(message);
            string userInput = Console.ReadLine();
            return userInput;
        }
    }
}
