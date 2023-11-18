using Microsoft.VisualBasic.FileIO;
using Schedulist.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.Business
{
    
    public class MenuOptions
    {
        public static void MenuCalendarEvents()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("====== Calendar Events ======");
                Console.WriteLine("Choose the option:");
                Console.WriteLine("1. Create new calendar event");
                Console.WriteLine("2. Show calendar events for chosen date");
                Console.WriteLine("3. Modify calendar event");
                Console.WriteLine("4. Delete calendar event");
                Console.WriteLine("Backspace. Go back");
                Console.WriteLine("===============================================================================");
                var option = Console.ReadKey();
                if (option.Key == ConsoleKey.D1) new ManageCalendarEvent().CreateCalendarEvent(CurrentUser.currentUser);
                else if (option.Key == ConsoleKey.D2) new ManageCalendarEvent().ShowUserCalendarEvent(CurrentUser.currentUser);
                else if (option.Key == ConsoleKey.D3) new ManageCalendarEvent().ModifyCurrentCalendarEvent(CurrentUser.currentUser);
                else if (option.Key == ConsoleKey.D4) new ManageCalendarEvent().DeleteCurrentCalendarEvent(CurrentUser.currentUser);
                else if (option.Key == ConsoleKey.Backspace) break;
            }
        }
        public void MenuAdminCalendarEvents()     
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("====== Calendar Events for other Users ======");
                Console.WriteLine("Choose the option:");
                Console.WriteLine("1. Create new calendar event for chosen user");
                Console.WriteLine("2. Show user and date related calendar events");
                Console.WriteLine("3. Modify existing calendar event for chosen user");
                Console.WriteLine("4. Delete existing calendar event for chosen user");
                Console.WriteLine("Backspace. Go back");
                Console.WriteLine("===============================================================================");
                var option = Console.ReadKey();
                if (option.Key == ConsoleKey.D1)
                {
                    var actAsUser = SetActAsUser();
                    new ManageCalendarEvent().CreateCalendarEvent(actAsUser);
                }
                else if (option.Key == ConsoleKey.D2)
                {
                    var actAsUser = SetActAsUser();
                    new ManageCalendarEvent().ShowUserCalendarEvent(actAsUser);
                }
                else if (option.Key == ConsoleKey.D3) new ManageCalendarEvent().ModifyCalendarEventsAdmin();
                else if (option.Key == ConsoleKey.D4) new ManageCalendarEvent().DeleteCalendarEventAdmin();
                else if (option.Key == ConsoleKey.Backspace) break;
            }

        }
        //metoda pomocnicza, żeby nie powtarzać tej samej akcji w różnych miejscach menu    
        private User SetActAsUser() 
        {
            User actAsUser = new AdminCommands().DisplayUsers("display");
            return actAsUser;
        }
        public void MenuWorkModes()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("====== Work Modes ======");
                Console.WriteLine("Choose the option:");
                Console.WriteLine("1. Create new work mode");
                Console.WriteLine("2. Modify existing work mode");
                Console.WriteLine("3. Delete existing work mode");
                Console.WriteLine("4. Show all work modes");
                if (CurrentUser.currentUser.AdminPrivilege == true)
                {
                    Console.WriteLine("5. Create new work mode for chosen user");
                    Console.WriteLine("6. Delete existing work mode for chosen user");
                }
                Console.WriteLine("Backspace. Go back");
                Console.WriteLine("===============================================================================");
                var option = Console.ReadKey();
                if (option.Key == ConsoleKey.D1) new ManageWorkMode().ChooseOptionsWorkMode();
                else if (option.Key == ConsoleKey.D2) new ManageWorkMode().ChangeOptionWorkMode();
                else if (option.Key == ConsoleKey.D3) new ManageWorkMode().RemoveCurrentWorkMode();
                else if (option.Key == ConsoleKey.D4) new ManageWorkMode().ShowAllWorkModes();
                else if (option.Key == ConsoleKey.D5)
                {
                    var actAsUser = SetActAsUser();
                    int workModeOption = 1;
                    new ManageWorkMode().ChooseOptionsWorkModeAdmin(workModeOption, actAsUser);
                } 
                else if (option.Key == ConsoleKey.D6)
                {
                    var actAsUser = SetActAsUser();
                    new ManageWorkMode().RemoveCurrentWorkModeAdmin(actAsUser);
                }
                else if (option.Key == ConsoleKey.Backspace) break;
            }
        }
        public static void MenuUsers()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("====== Users ======");
                Console.WriteLine("Choose the option:");
                Console.WriteLine("1. Create new user");
                Console.WriteLine("2. Modify existing user");
                Console.WriteLine("3. Delete existing user");
                Console.WriteLine("Backspace. Go back");
                Console.WriteLine("===============================================================================");
                var option = Console.ReadKey();
                if (option.Key == ConsoleKey.D1) ManageUser.Create();
                else if (option.Key == ConsoleKey.D2) ManageUser.Modify();
                else if (option.Key == ConsoleKey.D3) ManageUser.Delete();
                else if (option.Key == ConsoleKey.Backspace) break;
            }
        }
        public void MenuCalendar()
        {
            
            while (true)
            {
                Console.Clear();
                DateTime currentDate = DateTime.Today;
                int year = currentDate.Year;
                int month = currentDate.Month;
                Console.WriteLine("====== Calendar ======");
                Console.WriteLine("Choose the option:");
                Console.WriteLine($"1. Show {CurrentUser.currentUser.Name} monthly calendar");
                if (CurrentUser.currentUser.AdminPrivilege == true)
                {
                    Console.WriteLine($"2. Show monthly calendar for chosen user");
                }
                Console.WriteLine("Backspace. Go back");
                Console.WriteLine("===============================================================================");
                var option = Console.ReadKey();
                if (option.Key == ConsoleKey.D1) new Calendar().ShowUserCalendar(CurrentUser.currentUser);
                else if (option.Key == ConsoleKey.D2)
                {
                    var actAsUser = SetActAsUser();
                    new Calendar().ShowUserCalendarAdmin(actAsUser);
                }
                else if (option.Key == ConsoleKey.Backspace) break;
            }

        }
    }
}
    