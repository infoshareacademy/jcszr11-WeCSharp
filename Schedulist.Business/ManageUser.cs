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

        //public ManageUser(/*IUserRepository userRepository*/);
        ////{
        ////    _userRepository = userRepository;
        ////}
        ////public void ConsolHelper(string message)
        ////{
        ////    message;
        ////    Console.ReadLine(message);
        ////}
        internal void Create()
        {
            Console.WriteLine();
            Console.WriteLine("===============================================================================");
            Console.WriteLine("Create User");
            Console.WriteLine("===============================================================================");
            Console.WriteLine("Type User name");
            string name = Console.ReadLine();
            Console.WriteLine("Type User surname");
            string surname = Console.ReadLine();
            Console.WriteLine("Type User position");
            string position = Console.ReadLine();
            Console.WriteLine("Type User department");
            string department = Console.ReadLine();
            Console.WriteLine("Type User login");
            string login = Console.ReadLine();
            Console.WriteLine("Type User password");
            string password = Console.ReadLine();
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
                    Console.WriteLine("Error");
                    break;
            }
            User user = new User(name, surname, position, department, login, password) 
            {AdminPrivilege = isAdmin, Id=1};
            new CsvUserRepository("Users.csv").AddUser(user);
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
    }
}
