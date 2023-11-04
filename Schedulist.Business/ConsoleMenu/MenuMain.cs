using Schedulist.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.Business
{
    public class MenuMain
    {
        public static User Run()
        {
            Console.Clear();
            Console.WriteLine($"Welcome, {CurrentUser.currentUser.Name}");
            Console.WriteLine("Choose the option:");
            Console.WriteLine("1. Show calendar");
            Console.WriteLine("2. Manage calendar events");
            if (CurrentUser.currentUser.AdminPrivilege == true)
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
                else if (option.Key == ConsoleKey.D2) MenuOptions.MenuCalendarEvents();
                else if (CurrentUser.currentUser.AdminPrivilege) // Admin options
                {
                    if (option.Key == ConsoleKey.D3) MenuOptions.MenuWorkModes();
                    else if (option.Key == ConsoleKey.D4) MenuOptions.MenuUsers();
                }
            }
        }
    }
}