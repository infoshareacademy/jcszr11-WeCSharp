using Schedulist.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.Business
{
    internal class ManageWorkMode
    {
        IWorkModesRepository _workModesRepository;
        private List<WorkModesToUser> _workModesToUser = 
            new CSVWorkModesRepository("..\\..\\..\\WorkModes.csv").GetAllWorkModes();
        private List<User> userList = new CsvUserRepository("Users.csv").GetAllUsers();
        private CSVWorkModesRepository _csvWorkModesRepository = 
            new CSVWorkModesRepository("..\\..\\..\\WorkModes.csv");
        string workModeName;
        int workModeNameID;
        int workModeToUserID = 1;
        public ManageWorkMode (IWorkModesRepository workModesRepository)
        {
            _workModesRepository = workModesRepository;            
        }

        public ManageWorkMode()
        {

        }

        public void ShowAllWorkModes()
        {
            var workModesToUser = _csvWorkModesRepository.GetAllWorkModes();
            Console.WriteLine(workModesToUser);
        }

        public void ChooseOptionsWorkMode()
        {
            while (true)
            {                           
                Console.Clear();
                Console.WriteLine("Choose workmode option: ");
                Console.WriteLine("1. At office");
                Console.WriteLine("2. Home office");
                Console.WriteLine("3. Sick leave");
                Console.WriteLine("4. Holiday leave");
                Console.WriteLine("5. Other workmode");
                Console.WriteLine("Backspace. Go back");
                Console.WriteLine("=========================================");
                var option = Console.ReadKey();
                if (option.Key == ConsoleKey.D1) AssignWorkMode(1);
                else if (option.Key == ConsoleKey.D2) AssignWorkMode(2);
                else if (option.Key == ConsoleKey.D3) AssignWorkMode(3);
                else if (option.Key == ConsoleKey.D4) AssignWorkMode(4);
                else if (option.Key == ConsoleKey.D5) AssignWorkMode(5);
                else if (option.Key == ConsoleKey.Backspace) MenuOptions.MenuWorkModes();
                break;
            }
        }

        public void AssignWorkMode(int workModeOption)
        {
            
            
            if(workModeOption==1)
            {
                workModeNameID = workModeOption;
                workModeName = "Office";                
            }
            else if(workModeOption==2)
            {
                workModeNameID = workModeOption;
                workModeName = "Home office";
            }
            else if(workModeOption==3)
            {
                workModeNameID = workModeOption;
                workModeName = "Sick leave";
            }
            else if(workModeOption==4)
            {
                workModeNameID = workModeOption;
                workModeName = "Holiday leave";
            }
            else if(workModeOption==5)
            {
                workModeNameID = workModeOption;
                workModeName = "Another work mode";
            }
            
            Console.WriteLine("\nEnter the date of your work day: ");
            var dateOfWorkMode = DateOnly.Parse(Console.ReadLine());
            WorkModesToUser workModesToUser = new WorkModesToUser(id: workModeToUserID, idname: workModeNameID, name: workModeName, userid: CurrentUser.currentUser.Id, dow: dateOfWorkMode);
            _csvWorkModesRepository.AddWorkModes(workModesToUser);
        }

        public void ChangeOptionWorkMode(int userID)
        {
            Console.Clear();
            Console.WriteLine("Provide the date, you want to change your work mode:");
            var dateOfWorkMode = DateOnly.Parse(Console.ReadLine());
            
            WorkModesToUser workModeToChange = _csvWorkModesRepository.GetWorkModeByUserAndDate(userID, dateOfWorkMode);
            Console.WriteLine(workModeToChange);
            Console.Clear();
            Console.WriteLine("Datas about work mode, you want to change:");
            Console.WriteLine($"ID work mode:   {workModeToChange.WorkModeToUserID}");
            Console.WriteLine($"Work mode name:  {workModeToChange.WorkModeName}");
            Console.WriteLine($"Your users ID:  {workModeToChange.UserID}");
            Console.WriteLine($"Date of work day:   {workModeToChange.dateOfWorkmode}");            
            Console.WriteLine("===========================================");
            Console.WriteLine("Choose the new work mode option: ");
            Console.WriteLine("1. At office");
            Console.WriteLine("2. Home office");
            Console.WriteLine("3. Sick leave");
            Console.WriteLine("4. Holiday leave");
            Console.WriteLine("5. Other workmode");

            var option = Console.ReadKey();
            
            if (option.Key == ConsoleKey.D1)
            {
                workModeNameID = (int)option.Key;
                workModeName = "Office";
            }
            else if (option.Key == ConsoleKey.D2)
            {
                workModeNameID = (int)option.Key;
                workModeName = "Home office";
            }
            else if (option.Key == ConsoleKey.D3)
            {
                workModeNameID = (int)option.Key;
                workModeName = "Sick leave";
            }
            else if (option.Key == ConsoleKey.D4)
            {
                workModeNameID = (int)option.Key;
                workModeName = "Holiday leave";
            }
            else if (option.Key == ConsoleKey.D5)
            {
                workModeNameID = (int)option.Key;
                workModeName = "Another work mode";
            }
            WorkModesToUser workModeModified = new WorkModesToUser(workModeToUserID, workModeNameID,workModeName,userID,dateOfWorkMode);

        }

        


    }
}
