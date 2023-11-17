using Schedulist.Business.Actions;
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
            new("..\\..\\..\\WorkModes.csv");
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
            Console.Clear();
            Console.WriteLine("User ID:\tDate:\t\t Work mode name:");
            //foreach(var user in usersList)
            foreach(var workmode in workModesToUser)
                Console.WriteLine($"{workmode.UserID}:\t\t{workmode.DateOfWorkmode}:\t{workmode.WorkModeName}");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();         
            Console.Clear();
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
                else if (option.Key == ConsoleKey.Backspace) break;
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
            var dateOfWorkMode = DateOfWorkModeValidation();
            WorkModesToUser workModesToUser = new(id: workModeToUserID, name: workModeName, userid: (int)CurrentUser.currentUser.Id , dow: dateOfWorkMode);
            _csvWorkModesRepository.AddWorkModes(workModesToUser);
            Console.Clear();
            Console.WriteLine($"Work mode for {dateOfWorkMode} has been created successfully");
            Console.WriteLine("\nType any key to continue");
            Console.ReadKey();
        }

        public void ChangeOptionWorkMode()
        {
            Console.Clear();
            int? userID = (int)CurrentUser.currentUser.Id;
            var dateOfWorkMode = DateOfWorkModeValidation();

            WorkModesToUser workModeToChange = _csvWorkModesRepository.GetWorkModeByUserAndDate((int)userID, dateOfWorkMode);

            workModeToUserID = _csvWorkModesRepository.ListOfWorkModes.IndexOf(_csvWorkModesRepository.ListOfWorkModes.First(u=>u.DateOfWorkmode==dateOfWorkMode && u.UserID==userID));
            //int workModeToUserID;
            //var workModeUser =
            //    _workModesToUser.First(u => u.DateOfWorkmode == dateOfWorkMode && u.UserID == userID).ToString();
            //if (workModeUser != null && int.TryParse(workModeUser.ToString(), out workModeToUserID))
            //{

            //}
            
            //Console.WriteLine(workModeToChange);
            Console.Clear();
            Console.WriteLine("Data about work mode, you want to change:");
            Console.WriteLine($"ID work mode:   {workModeToChange.WorkModeToUserID}");
            Console.WriteLine($"Work mode name:  {workModeToChange.WorkModeName}");
            Console.WriteLine($"Your user ID:  {workModeToChange.UserID}");
            Console.WriteLine($"Date of work day:   {workModeToChange.DateOfWorkmode}");            
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
                        
            WorkModesToUser workModeModified = new(workModeToUserID, workModeName, (int)userID,dateOfWorkMode);
            _csvWorkModesRepository.ModifyWorkModes(workModeToUserID, workModeModified);
            Console.Clear();
            Console.WriteLine("Work Mode has been modified successfully");
            Console.WriteLine("\nType any key to continue");
            Console.ReadKey();
            
        }

        private static DateOnly DateOfWorkModeValidation()
        {
            DateOnly dateOfWorkMode;
            while (true)
            {
                string dateOfWorkModeInput =
                    Helper.ConsolHelper("\nProvide the date, you want to change your work mode in format DD.MM.YYYY:");
                if (DateOnly.TryParse(dateOfWorkModeInput, out dateOfWorkMode))
                {
                    break;
                }
                else Console.WriteLine("\nInvalid date format, please provide again in format DD.MM.YYYY!");
            }

            return dateOfWorkMode;
        }

        public void RemoveWorkMode()
        {
            Console.Clear();
            //int userID = (int)CurrentUser.currentUser.Id;
            Console.WriteLine("Provide the date, you want to change your work mode in format DD.MM.YYYY:");
            var dateOfWorkMode = DateOnly.Parse(Console.ReadLine());
            workModeToUserID = _csvWorkModesRepository.ListOfWorkModes.IndexOf(_csvWorkModesRepository.ListOfWorkModes.First(u => u.DateOfWorkmode == dateOfWorkMode && u.UserID == CurrentUser.currentUser.Id));
            WorkModesToUser workModeToDelete = _csvWorkModesRepository.GetWorkModeByUserAndDate((int)CurrentUser.currentUser.Id, dateOfWorkMode);
            Console.WriteLine("Are you sure to remove this work mode? Type y - to remove or n - to cancel");

            while (true)
            {
                var userAnswer = Console.ReadKey(intercept: true);
                if (userAnswer.Key == ConsoleKey.Y)
                {
                    _csvWorkModesRepository.DeleteWorkModes(workModeToUserID);
                    break;
                }
                else if (userAnswer.Key == ConsoleKey.N)
                {
                    break;
                }
                else Console.WriteLine("Invalid value, please provide again");
            }

            Console.Clear();
            Console.WriteLine("Type any key to continue");
            Console.ReadKey();
        }


    }
}
