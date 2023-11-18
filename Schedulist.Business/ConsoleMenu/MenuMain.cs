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
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Welcome, {CurrentUser.currentUser.Name}");
                Console.WriteLine("Choose the option:");
                Console.WriteLine("1. Show calendar");
                Console.WriteLine("2. Manage work modes");
                Console.WriteLine("3. Manage calendar events");
                if (CurrentUser.currentUser.AdminPrivilege == true)
                {
                    Console.WriteLine("4. Manage calendar events for other users");
                    Console.WriteLine("5. Manage users");
                }
                Console.WriteLine("Backspace. Log out");
                Console.WriteLine("===============================================================================");
                var option = Console.ReadKey();

                if (option.Key == ConsoleKey.Backspace)
                {
                    CurrentUser.currentUser = null;
                    return null;
                }
                else if (option.Key == ConsoleKey.D1) new MenuOptions().MenuCalendar();
                else if (option.Key == ConsoleKey.D2) new MenuOptions().MenuWorkModes();
                else if (option.Key == ConsoleKey.D3) MenuOptions.MenuCalendarEvents();
                else if (CurrentUser.currentUser.AdminPrivilege) // Admin options
                {
                    if (option.Key == ConsoleKey.D4) new MenuOptions().MenuAdminCalendarEvents();
                    else if (option.Key == ConsoleKey.D5) MenuOptions.MenuUsers();
                }
            }
        }
    }
}