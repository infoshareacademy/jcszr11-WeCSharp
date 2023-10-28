using Microsoft.VisualBasic.FileIO;
using Schedulist.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.Business
{
    internal class MenuOptions
    {
        public void MenuCalendarEvents(User currentUser)
        {
            Console.Clear();
            Console.WriteLine("Choose the option:");
            Console.WriteLine("1. Create new calendar event");
            Console.WriteLine("2. Show calendar events");
            Console.WriteLine("3. Modify existing calendar event");
            Console.WriteLine("4. Delete existing calendar event");
            Console.WriteLine("Backspace. Go back");
            Console.WriteLine("===============================================================================");
            while (true)
            {
                var option = Console.ReadKey();
                if (option.Key == ConsoleKey.D1) new ManageCalendarEvent().CreateCalendarEvent();
                else if (option.Key == ConsoleKey.D2) Console.WriteLine("*****show tasks*****");
                else if (option.Key == ConsoleKey.D3) Console.WriteLine("*****modify task*****");
                else if (option.Key == ConsoleKey.D4) Console.WriteLine("*****delete task*****");
                else if (option.Key == ConsoleKey.Backspace) new MenuMain().Run(currentUser);
                break;
            }
        }
        public void MenuWorkModes(User currentUser)
        {
            Console.Clear();
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
                else if (option.Key == ConsoleKey.Backspace) new MenuMain().Run(currentUser);
                break;
            }
        }
        public void MenuUsers(User currentUser)
        {
            Console.Clear();
            Console.WriteLine("Choose the option:");
            Console.WriteLine("1. Create new user");
            Console.WriteLine("2. Modify existing user");
            Console.WriteLine("3. Delete existing user");
            Console.WriteLine("Backspace. Go back");
            Console.WriteLine("===============================================================================");
            while (true)
            {
                var option = Console.ReadKey();
                if (option.Key == ConsoleKey.D1) new ManageUser().Create();
                else if (option.Key == ConsoleKey.D2) new ManageUser().Modify(currentUser);
                else if (option.Key == ConsoleKey.D3) new ManageUser().Delete();
                else if (option.Key == ConsoleKey.Backspace) new MenuMain().Run(currentUser);
                break;
            }
        }
    }
}