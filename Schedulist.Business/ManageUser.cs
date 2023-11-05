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
            Console.WriteLine("Type any key do return to MenuOptions");
            Console.ReadKey();
            MenuOptions.MenuUsers();
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
            {
                Console.Clear();
                Console.WriteLine("====== Delete User section ======");
                List<User> listOfUsers;
                listOfUsers = new CsvUserRepository("..\\..\\..\\Users.csv").GetAllUsers();
                Console.WriteLine("List of available Users to delete:");
                for (int i = 0; i < listOfUsers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {listOfUsers[i].Name} {listOfUsers[i].Surname}");
                }

                Console.WriteLine("0. Cancel");
                Console.WriteLine("===============================================================================");

                System.ConsoleKeyInfo option;
                while (true)
                {
                    option = Console.ReadKey();
                    if (option.Key == ConsoleKey.D0)
                    {
                        Console.Clear();
                        Console.WriteLine("Operation canceled. Type any key to return to MenuOptions");
                        Console.ReadKey();
                        MenuOptions.MenuUsers();
                    }
                    else if (option.Key >= ConsoleKey.D1 && option.Key <= ConsoleKey.D9)
                    {
                        // W kodzie ASCII klawisze od D0 do D9 są reprezentowane są jako liczby dziesiętne odpowiednio: D0=48, D1=49, D2=50 itd.
                        // Przykład po wyborze klawisza D3: userChoice = 51 -48 -1 = 3.
                        int userChoice = (int)option.Key - (int)ConsoleKey.D0 - 1;

                        // Validacja poprawności wprowadzonej wartości 
                        if (userChoice >= 0 && userChoice < listOfUsers.Count)
                        {
                            // Usunięcie wybranego użytkownika
                            User userToDelete = listOfUsers[userChoice];
                            listOfUsers.RemoveAt(userChoice);

                            // Zapisanie zmian w pliku CSV
                            var csvUserRepository = new CsvUserRepository("..\\..\\..\\Users.csv");
                            csvUserRepository.WriteAllUsers(listOfUsers);
                            Console.Clear();

                            Console.WriteLine($"User {userToDelete.Name} {userToDelete.Surname} has been deleted successfully.");
                            Console.WriteLine("Type any key do return to MenuOptions");
                            Console.ReadKey();
                            MenuOptions.MenuUsers();
                        }
                        else
                            Console.WriteLine("Invalid user choice. Please choose a valid user to delete.");
                    }
                    else
                        Console.WriteLine("Invalid option. Please choose a valid option.");
                }
            }
        }
    }
}
