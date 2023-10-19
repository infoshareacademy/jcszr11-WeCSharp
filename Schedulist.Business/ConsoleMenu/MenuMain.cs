using Schedulist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.Business
{
    public class MenuMain
    {
        public User Run(User user)
        {
            Console.WriteLine($"Welcome, {user.Name}");
            Console.WriteLine("Choose the option:");
            Console.WriteLine("1. Show calendar");
            Console.WriteLine("2. Manage tasks");
            if (user.AdminPrivilege == true)
            {
                Console.WriteLine("3. Manage work modes");
                Console.WriteLine("4. Manage users");
            }
            Console.WriteLine("Backspace. Log out");
            Console.WriteLine("===============================================================================");
            while (true)
            {
                var option = Console.ReadKey();
                if (option.Key == ConsoleKey.Backspace)
                {
                    return null;
                }
                else if (option.Key == ConsoleKey.D1) Console.WriteLine("*****calendar*****");
                else if (option.Key == ConsoleKey.D2) new MenuOptions().MenuTasks(user);
                else if (user.AdminPrivilege == true) // Admin options
                {
                    if (option.Key == ConsoleKey.D3) new MenuOptions().MenuWorkModes(user);
                    else if (option.Key == ConsoleKey.D4) new MenuOptions().MenuUsers(user);
                }
            }

        }
    }
}