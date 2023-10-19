using Microsoft.VisualBasic.FileIO;
using Schedulist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.Business
{
    internal class MenuOptions
    {
        public void MenuTasks(User user)
        {
            Console.WriteLine("Choose the option:");
            Console.WriteLine("1. Create new task");
            Console.WriteLine("2. Modify existing task");
            Console.WriteLine("3. Delete existing task");
            Console.WriteLine("Backspace. Go back");
            Console.WriteLine("===============================================================================");
            while (true)
            {
                var option = Console.ReadKey();
                if (option.Key == ConsoleKey.D1) new Task().CreateNewTask();
                else if (option.Key == ConsoleKey.D2) Console.WriteLine("*****modify task*****");
                else if (option.Key == ConsoleKey.D3) Console.WriteLine("*****delete task*****");
                else if (option.Key == ConsoleKey.Backspace) new MenuMain().Run(user);
                break;
            }
        }
        public void MenuWorkModes(User user)
        {
            Console.WriteLine("Choose the option:");
            Console.WriteLine("1. Create new work mode");
            Console.WriteLine("2. Modify existing work mode");
            Console.WriteLine("3. Delete existing work mode");
            Console.WriteLine("Backspace. Go back");
            Console.WriteLine("===============================================================================");
            while (true)
            {
                var option = Console.ReadKey();
                if (option.Key == ConsoleKey.D1) Console.WriteLine("*****create work mode*****");
                else if (option.Key == ConsoleKey.D2) Console.WriteLine("*****modify work mode*****");
                else if (option.Key == ConsoleKey.D3) Console.WriteLine("*****delete work mode*****");
                else if (option.Key == ConsoleKey.Backspace) new MenuMain().Run(user);
                break;
            }
        }
        public void MenuUsers(User user)
        {
            Console.WriteLine("Choose the option:");
            Console.WriteLine("1. Create new user");
            Console.WriteLine("2. Modify existing user");
            Console.WriteLine("3. Delete existing user");
            Console.WriteLine("Backspace. Go back");
            Console.WriteLine("===============================================================================");
            while (true)
            {
                var option = Console.ReadKey();
                if (option.Key == ConsoleKey.D1) Console.WriteLine("*****create user*****");
                else if (option.Key == ConsoleKey.D2) Console.WriteLine("*****modify user*****");
                else if (option.Key == ConsoleKey.D3) Console.WriteLine("*****delete user*****");
                else if (option.Key == ConsoleKey.Backspace) new MenuMain().Run(user);
                break;
            }
        }
    }
}