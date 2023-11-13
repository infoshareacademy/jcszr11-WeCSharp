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
            Console.WriteLine("Type any key to continue");
            Console.ReadKey();
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
                if (option.Key == ConsoleKey.D1) CreateWorkMode(1);
                else if (option.Key == ConsoleKey.D2) CreateWorkMode(2);
                else if (option.Key == ConsoleKey.D3) CreateWorkMode(3);
                else if (option.Key == ConsoleKey.D4) CreateWorkMode(4);
                else if (option.Key == ConsoleKey.D5) CreateWorkMode(5);
                else if (option.Key == ConsoleKey.Backspace) MenuOptions.MenuWorkModes();
                break;
            }
        }

        public void CreateWorkMode(int workModeOption)
        {
            int workModeToUserID = 1;
            
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
                workModeName = "Another workmode";
            }
            
            Console.WriteLine("\nEnter the date: ");
            var dateOfWorkMode = DateOnly.Parse(Console.ReadLine());
            WorkModesToUser workModesToUser = new WorkModesToUser(id: workModeToUserID, idname: workModeNameID, name: workModeName, userid: CurrentUser.currentUser.Id, dow: dateOfWorkMode);
            _csvWorkModesRepository.AddWorkModes(workModesToUser);
        }

        public void ChangeOptionWorkMode()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Provide the date, you want to change your workmode:");
                var dateOfWorkMode = DateOnly.Parse(Console.ReadLine());

                Console.WriteLine($"Your workmode at {dateOfWorkMode} is ");
            }
        }

        


    }
}
