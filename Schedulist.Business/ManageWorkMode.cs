using Schedulist.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.Business
{
    internal class ManageWorkMode
    {
        private MenuOptions menuOptions;
        IWorkModesRepository _workModesRepository;
        private List<WorkModesToUser> _workModesToUser = 
            new CSVWorkModesRepository("..\\..\\..\\WorkModes.csv").GetAllWorkModes();
        private List<User> userList = new CsvUserRepository("Users.csv").GetAllUsers();
        private CSVWorkModesRepository _csvWorkModesRepository = 
            new CSVWorkModesRepository("..\\..\\..\\WorkModes.csv");
        string workModeName;
        int workModeToUserID = 1;
        
        //int workModeToUserID = .OrderBy(x => x.WorkModeToUserID).Last().WorkModeToUserID + 1;
        public ManageWorkMode (IWorkModesRepository workModesRepository)
        {
            _workModesRepository = workModesRepository;            
        }

        public ManageWorkMode()
        {

        }

        public void ShowAllWorkModes()
        {
            //var usersList = userList.ToList();
            var workModesToUser = _csvWorkModesRepository.GetAllWorkModes().ToList();
            Console.WriteLine();
            Console.WriteLine("\tID User :\t name work mode :\t date");
            //foreach(var user in usersList)
            foreach(var workmode in workModesToUser)
                Console.WriteLine($"\t{workmode.UserID} :\t {workmode.WorkModeName} :\t {workmode.dateOfWorkmode}");
            Console.WriteLine("Press any key to back the previous page - Menu Work Mode");
            Console.ReadKey();         
            Console.Clear();
            MenuOptions.MenuWorkModes();    
                        
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
                workModeName = "Office";        
            else if(workModeOption==2)            
                workModeName = "Home office";
            else if(workModeOption==3)            
                workModeName = "Sick leave";            
            else if(workModeOption==4)            
                workModeName = "Holiday leave";            
            else if(workModeOption==5)            
                workModeName = "Another work mode";
            
            //workModeToUserID = _csvWorkModesRepository.ListOfWorkModes.OrderBy(x => x.WorkModeToUserID).Last().WorkModeToUserID + 1;
            Console.WriteLine("\nEnter the date of your work day: ");
            var dateOfWorkMode = DateOnly.Parse(Console.ReadLine());
            WorkModesToUser workModesToUser = new WorkModesToUser(id: workModeToUserID, name: workModeName, userid: CurrentUser.currentUser.Id, dow: dateOfWorkMode);
            _csvWorkModesRepository.AddWorkModes(workModesToUser);
            Console.Clear();
            MenuOptions.MenuWorkModes();
        }

        public void ChangeOptionWorkMode()
        {
            Console.Clear();
            int userID = CurrentUser.currentUser.Id;
            Console.WriteLine("Provide the date, you want to change your work mode:");
            var dateOfWorkMode = DateOnly.Parse(Console.ReadLine());
            workModeToUserID = _csvWorkModesRepository.ListOfWorkModes.IndexOf(_csvWorkModesRepository.ListOfWorkModes.First(u=>u.dateOfWorkmode==dateOfWorkMode && u.UserID==userID));
            WorkModesToUser workModeToChange = _csvWorkModesRepository.GetWorkModeByUserAndDate(userID, dateOfWorkMode);
            //Console.WriteLine(workModeToChange);
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
                workModeName = "Office";
            else if (option.Key == ConsoleKey.D2)
                workModeName = "Home office";
            else if (option.Key == ConsoleKey.D3)
                workModeName = "Sick leave";
            else if (option.Key == ConsoleKey.D4)
                workModeName = "Holiday leave";
            else if (option.Key == ConsoleKey.D5)
                workModeName = "Another work mode";
            else if (option.Key == ConsoleKey.Backspace) MenuOptions.MenuWorkModes();
                        
            WorkModesToUser workModeModified = new WorkModesToUser(workModeToUserID, workModeName,userID,dateOfWorkMode);
            _csvWorkModesRepository.ModifyWorkModes(workModeToUserID,workModeModified);
            MenuOptions.MenuWorkModes();
        }

        public void RemoveWorkMode()
        {
            Console.Clear();
            int userID = CurrentUser.currentUser.Id;
            Console.WriteLine("Provide the date, you want to change your work mode:");
            var dateOfWorkMode = DateOnly.Parse(Console.ReadLine());
            workModeToUserID = _csvWorkModesRepository.ListOfWorkModes.IndexOf(_csvWorkModesRepository.ListOfWorkModes.First(u => u.dateOfWorkmode == dateOfWorkMode && u.UserID == userID));
            WorkModesToUser workModeToDelete = _csvWorkModesRepository.GetWorkModeByUserAndDate(userID, dateOfWorkMode);
            Console.WriteLine("Are you sure to remove this work mode?");

            _csvWorkModesRepository.DeleteWorkModes(workModeToUserID);
            
            Console.Clear();
            MenuOptions.MenuWorkModes();
        }


    }
}
