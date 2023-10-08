using Schedulist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.Business
{
    public class ConsoleMenu
    {
        public void Run(User user)
        {
            Console.WriteLine($"Welcome, {user.Name}");
            while (true)
            {
                Console.WriteLine("Choose the option number:");
                Console.WriteLine("1. Show calendar");
                Console.WriteLine("2. Manage tasks");
                if (user.AdminPrivilege == true)
                {
                    Console.WriteLine("3. Manage work modes");
                    Console.WriteLine("4. Manage users");
                }
                Console.WriteLine("x. Log out");
                Console.WriteLine("===============================================================================");
                string option = Console.ReadLine();
                if (option == "x")
                {
                    while (true)
                    {
                        var currentUser = new Login().Run();    // Metoda do logowania użytkownika
                        if (currentUser != null) new ConsoleMenu().Run(currentUser);
                    }
                }
                else if (option == "1") Console.WriteLine("*calendar*"); // calendar
                else if (option == "2") // tasks
                {
                    while (true)
                    {
                        Console.WriteLine("Choose the option number:");
                        Console.WriteLine("1. Create new task");
                        Console.WriteLine("2. Modify existing task");
                        Console.WriteLine("3. Delete existing task");
                        Console.WriteLine("x. Go back");
                        option = Console.ReadLine();
                        if (option == "1") Console.WriteLine("*create task*");
                        else if (option == "2") Console.WriteLine("*modify task*");
                        else if (option == "3") Console.WriteLine("*delete task*");
                        else if (option == "x") Run(user);
                        else Console.WriteLine("Error, choose the option number again");
                    }
                }
                else if (user.AdminPrivilege == true) // funkcje dla admina
                {
                    if (option == "3") // work modes
                    {
                        while (true)
                        {
                            Console.WriteLine("Choose the option number:");
                            Console.WriteLine("1. Create new work mode");
                            Console.WriteLine("2. Modify existing work mode");
                            Console.WriteLine("3. Delete existing work mode");
                            Console.WriteLine("x. Go back");
                            option = Console.ReadLine();
                            if (option == "1") Console.WriteLine("*create work mode*");
                            else if (option == "2") Console.WriteLine("*modify work mode*");
                            else if (option == "3") Console.WriteLine("*delete work mode*");
                            else if (option == "x") Run(user);
                            else Console.WriteLine("Error, choose the option number again");
                        }
                    }
                    else if (option == "4") // users
                    {
                        while (true)
                        {
                            Console.WriteLine("Choose the option number:");
                            Console.WriteLine("1. Create new user");
                            Console.WriteLine("2. Modify existing user");
                            Console.WriteLine("3. Delete existing user");
                            Console.WriteLine("x. Go back");
                            option = Console.ReadLine();
                            if (option == "1") Console.WriteLine("*create user*");
                            else if (option == "2") Console.WriteLine("*modify user*");
                            else if (option == "3") Console.WriteLine("*delete user*");
                            else if (option == "x") Run(user);
                            else Console.WriteLine("Error, choose the option number again");
                        }
                    }
                }
                else Console.WriteLine("Error, choose the option number again");
            }
        }
    }
}