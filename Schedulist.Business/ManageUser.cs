﻿using CsvHelper.Configuration;
using CsvHelper;
using Schedulist.Business.Actions;
using Schedulist.DAL;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                if (option.Key == ConsoleKey.D1) break;
                else if (option.Key == ConsoleKey.D2) break;
                else if (option.Key == ConsoleKey.D3) break;
                else if (option.Key == ConsoleKey.D4) break;
                else if (option.Key == ConsoleKey.D5) break;
                else if (option.Key == ConsoleKey.D6) break;
                else if (option.Key == ConsoleKey.D7) break;
                else if (option.Key == ConsoleKey.Backspace) MenuOptions.MenuUsers();
            }
            //todo
            Console.WriteLine("Option: " + option.Key);
            MenuOptions.MenuUsers();
        }
        internal void Delete()
        {
            Console.Clear();
            Console.WriteLine("====== Delete User section ======");
            User userToDelete = new AdminCommands().DisplayUsersToDelete();

            Console.WriteLine($"Are you sure you want to delete {userToDelete.Name} {userToDelete.Surname}? (y/n)");
            string confirmation = Console.ReadLine();

            if (confirmation.ToLower() == "y")
            {
                List<User> users = new CsvUserRepository("..\\..\\..\\Users.csv").GetAllUsers();
                users.Remove(userToDelete);

                // Zapisanie zaktualizowanej listy użytkowników do pliku CSV.
                UpdateUserList(users);
                Console.Clear();
                Console.WriteLine($"User {userToDelete.Name} {userToDelete.Surname} has been deleted.");
            }
            else
            {
                Console.WriteLine("Deletion canceled.");
            }
        }

        private void UpdateUserList(List<User> users)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };

            using StreamWriter writer = new StreamWriter("..\\..\\..\\Users.csv");
            using var csv = new CsvWriter(writer, csvConfig);
            csv.WriteRecords(users);
        }   
    }
}
