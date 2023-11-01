using Microsoft.VisualBasic.FileIO;
using Schedulist.Business.Actions;
using Schedulist.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.Business
{
    internal class ManageUser
    {
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
            User user = new(name, surname, position, department, login, password)
            { AdminPrivilege = isAdmin };
            new CsvUserRepository("..\\..\\..\\Users.csv").AddUser(user);
        }

        internal void Modify()
        {
            Console.Clear();
            Console.WriteLine("====== Modify User ======");
            User userToModify = new AdminCommands().DiplayUsers();
            System.ConsoleKeyInfo option;
            Console.Clear();
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
                option = Console.ReadKey();
                if (option.Key == ConsoleKey.D1)
                {
                    Modify_Ask(userToModify.Name, "Name", false, userToModify);
                    break;
                }
                else if (option.Key == ConsoleKey.D2)
                {
                    Modify_Ask(userToModify.Surname, "Surname", false, userToModify);
                    break;
                }
                else if (option.Key == ConsoleKey.D3)
                {
                    Modify_Ask(userToModify.Position, "Position", false, userToModify);
                    break;
                }
                else if (option.Key == ConsoleKey.D4)
                {
                    Modify_Ask(userToModify.Department, "Department", false, userToModify);
                    break;
                }
                else if (option.Key == ConsoleKey.D5)
                {
                    Modify_Ask(userToModify.Login, "Login", false, userToModify);
                    break;
                }
                else if (option.Key == ConsoleKey.D6)
                {   
                    Modify_Ask(userToModify.Password, "Password", false, userToModify);
                    break;
                }
                else if (option.Key == ConsoleKey.D7)
                    Modify_Ask(userToModify.AdminPrivilege.ToString(), "admin privilage", true, userToModify);
                else if (option.Key == ConsoleKey.Backspace) break;
            }
        }
        internal static User Modify_Ask(string variableToModify, string variableName, bool variableToModify_bool, User userToModify)
        {
            Console.Clear();
            if (variableToModify != null)
            {
                Console.WriteLine($"Currnt {variableName.ToLower()} is {variableToModify} for {userToModify.Name} {userToModify.Surname}");
                Console.WriteLine($"Provide new {variableName.ToLower()}:");
                variableToModify = Console.ReadLine();
                if (userToModify.GetType().GetProperty(variableName) != null) userToModify.Name = variableToModify;
                else if (userToModify.GetType().GetProperty(variableName) != null) userToModify.Surname = variableToModify;
                else if (userToModify.GetType().GetProperty(variableName) != null) userToModify.Position = variableToModify;
                else if (userToModify.GetType().GetProperty(variableName) != null) userToModify.Department = variableToModify;
                else if (userToModify.GetType().GetProperty(variableName) != null) userToModify.Login = variableToModify;
                else if (userToModify.GetType().GetProperty(variableName) != null) userToModify.Password = variableToModify;
                //todo save to file
                Console.WriteLine($"{variableName} for {userToModify.Name} {userToModify.Surname} changed to {variableToModify}");
            }
            else if (variableToModify_bool)
            {
                Console.WriteLine($"Current Admin Privilege for {userToModify.Name} {userToModify.Surname} is {userToModify.AdminPrivilege}");
                Console.WriteLine("Do you want to change it? y/n");
                while (true)
                {
                    System.ConsoleKeyInfo option = Console.ReadKey();
                    if (option.Key == ConsoleKey.Y)
                    {
                        userToModify.AdminPrivilege = !userToModify.AdminPrivilege;
                        //todo save to file
                        break;
                    }
                    else if (option.Key == ConsoleKey.N) break;
                }
            }
            return userToModify;
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
