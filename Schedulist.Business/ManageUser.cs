using Schedulist.Business.Actions;
using Schedulist.DAL;
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
            Console.Clear();
            Console.WriteLine("====== Create User Section ======");
            string name = Method.ConsolHelper("Type User name");
            string surname = Method.ConsolHelper("Type User surname");
            string position = Method.ConsolHelper("Type User position");
            string department = Method.ConsolHelper("Type User department");
            string login = Method.ConsolHelper("Type User login");
            string password = Method.ConsolHelper("Type User password");
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
            { AdminPrivilege = isAdmin };
            new CsvUserRepository("..\\..\\..\\Users.csv").AddUser(user);
        }

        internal void Modify(User currentUser)
        {
            Console.Clear();
            Console.WriteLine("====== Modify User ======");
            string userToModify_login = Method.ConsolHelper("Provide login of the user you want to modify:");
            if (new CsvUserRepository("Users.csv").GetAllUsers().Any(x => x.Login == userToModify_login))
            {
                User userToModify = new CsvUserRepository("Users.csv").GetAllUsers().First(x => x.Login == userToModify_login);
                Console.Clear();
                Console.WriteLine($"User {userToModify.Login} found");
                Console.WriteLine("Choose variable of user that you want to modify:");
                Console.WriteLine($"1. Name:            {userToModify.Name}");
                Console.WriteLine($"2. Surname:         {userToModify.Surname}");
                Console.WriteLine($"3. Position:        {userToModify.Position}");
                Console.WriteLine($"4. Department:      {userToModify.Department}");
                Console.WriteLine($"5. Login:           {userToModify.Login}");
                Console.WriteLine($"6. Password:        {userToModify.Password}");
                Console.WriteLine($"7. AdminPrivilege:  {userToModify.AdminPrivilege}");
                Console.WriteLine("Backspace. Go back");
                Console.WriteLine("===============================================================================");
                while (true)
                {
                    var option = Console.ReadKey();
                    if (option.Key == ConsoleKey.D1) Console.WriteLine("1");
                    else if (option.Key == ConsoleKey.D2) Console.WriteLine("2");
                    else if (option.Key == ConsoleKey.D3) Console.WriteLine("3");
                    else if (option.Key == ConsoleKey.D4) Console.WriteLine("4");
                    else if (option.Key == ConsoleKey.D5) Console.WriteLine("5");
                    else if (option.Key == ConsoleKey.D6) Console.WriteLine("6");
                    else if (option.Key == ConsoleKey.D7) Console.WriteLine("7");
                    else if (option.Key == ConsoleKey.Backspace) new MenuOptions().MenuUsers(currentUser);
                    break;
                }
            }
            else Console.WriteLine("User not found, moving back to menu...");
            Thread.Sleep(3000);
            new MenuOptions().MenuUsers(currentUser);
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
