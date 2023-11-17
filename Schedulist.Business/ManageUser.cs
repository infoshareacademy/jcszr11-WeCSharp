using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.VisualBasic.FileIO;
using Schedulist.Business.Actions;
using Schedulist.DAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.Business
{
    public class ManageUser
    {
        internal static void Create()
        {
            Console.Clear();
            Console.WriteLine("====== Create User Section ======");
            string name = Helper.ConsolHelper("Type User name");
            string surname = Helper.ConsolHelper("Type User surname");
            string position = Helper.ConsolHelper("Type User position");
            string department = Helper.ConsolHelper("Type User department");
            string login = Helper.ConsolHelper("Type User login");
            string password = Helper.ConsolHelper("Type User password");
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
            new CsvUserRepository("..\\..\\..\\Users.csv").AddUser(user, null);
            Console.WriteLine("Type any key do return to MenuOptions");
            Console.ReadKey();
        }

        public static void Modify()
        {
            User userToModify = new AdminCommands().DisplayUsers("modify");
            System.ConsoleKeyInfo option;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("====== Modify User ======");
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
                option = Console.ReadKey();
                if (option.Key == ConsoleKey.D1)
                {
                    ModifyAsk(userToModify.Name, "Name", false, userToModify);
                    break;
                }
                else if (option.Key == ConsoleKey.D2)
                {
                    ModifyAsk(userToModify.Surname, "Surname", false, userToModify);
                    break;
                }
                else if (option.Key == ConsoleKey.D3)
                {
                    ModifyAsk(userToModify.Position, "Position", false, userToModify);
                    break;
                }
                else if (option.Key == ConsoleKey.D4)
                {
                    ModifyAsk(userToModify.Department, "Department", false, userToModify);
                    break;
                }
                else if (option.Key == ConsoleKey.D5)
                {
                    ModifyAsk(userToModify.Login, "Login", false, userToModify);
                    break;
                }
                else if (option.Key == ConsoleKey.D6)
                {
                    ModifyAsk(userToModify.Password, "Password", false, userToModify);
                    break;
                }
                else if (option.Key == ConsoleKey.D7)
                {
                    ModifyAsk(userToModify.AdminPrivilege.ToString(), "admin privilage", true, userToModify);
                    break;
                }
                else if (option.Key == ConsoleKey.Backspace) break;
            }
        }
        internal static void ModifyAsk(string variableToModify, string variableName, bool variableToModify_bool, User userToModify)
        {
            string userToModifyLogin = userToModify.Login;
            Console.Clear();
            if (variableToModify != null)
            {
                Console.WriteLine($"Currnt {variableName.ToLower()} is {variableToModify} for {userToModify.Name} {userToModify.Surname}");
                string modifiedVariable = Helper.ConsolHelper($"Provide new {variableName.ToLower()}:");
                if (userToModify.Name == variableToModify) userToModify.Name = modifiedVariable;
                else if (userToModify.Surname == variableToModify) userToModify.Surname = modifiedVariable;
                else if (userToModify.Position == variableToModify) userToModify.Position = modifiedVariable;
                else if (userToModify.Department == variableToModify) userToModify.Department = modifiedVariable;
                else if (userToModify.Login == variableToModify) userToModify.Login = modifiedVariable;
                else if (userToModify.Password == variableToModify) userToModify.Password = modifiedVariable;
                new CsvUserRepository("..\\..\\..\\Users.csv").ModifyUser(userToModifyLogin, userToModify);
                Console.Clear();
                Console.WriteLine($"{variableName} for {userToModify.Name} {userToModify.Surname} changed to {modifiedVariable}");
                Console.WriteLine("Type any key to continue");
                Console.ReadKey();
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
                        new CsvUserRepository("..\\..\\..\\Users.csv").ModifyUser(userToModifyLogin, userToModify);
                        break;
                    }
                    else if (option.Key == ConsoleKey.N) break;
                }
            }
        }
        public static void Delete()
        {
            {
                while (true)
                {
                    User userToDelete = new AdminCommands().DisplayUsers("delete");
                    {
                        new CsvUserRepository("..\\..\\..\\Users.csv").DeleteUser(userToDelete);
                        Console.Clear();
                        Console.WriteLine($"User {userToDelete.Name} {userToDelete.Surname} has been deleted successfully.");
                        Console.WriteLine("Type any key to continue");
                        Console.ReadKey();
                        break;
                    }
                }
            }
        }
    }
}

