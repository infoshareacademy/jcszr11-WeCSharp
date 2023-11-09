using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.DAL
{
    public class CSVWorkModesRepository : IWorkModesRepository
    {
        private readonly string FilePath;
        public List<WorkModesToUser> ListOfWorkModes;
        
        public CSVWorkModesRepository(string filePath)
        {
            FilePath=filePath;
        }
        public List<WorkModesToUser> GetAllWorkModes()
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                //Delimiter = "\t",
            };
            using (var reader = new StreamReader(FilePath))
            using (var csv = new CsvReader(reader, csvConfig))
            {
                ListOfWorkModes = csv.GetRecords<WorkModesToUser>().ToList();
                return ListOfWorkModes;
            }
        }
        public void AddWorkModes(WorkModesToUser workModes)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                //Delimiter = "\t",
            };
            ListOfWorkModes = GetAllWorkModes();

            // Oblicza lość użytkowników w liście i na tej podstawie nowemu użytkownikowi przydziela wartosć 
            // Id o jeden większą lub 1 w przypadku pustej listy. 
            int nextWorkmodeToUserId = ListOfWorkModes.Count > 0 ? ListOfWorkModes.Max(w => w.WorkModeToUserID) + 1 : 1;

            
            workModes.WorkModeToUserID = nextWorkmodeToUserId;
            try
            {
                ListOfWorkModes.Add(workModes);
                using (StreamWriter writer = new StreamWriter(FilePath))
                using (var csv = new CsvWriter(writer, csvConfig))
                {
                    csv.WriteRecords(ListOfWorkModes);
                    Console.Clear();
                    Console.WriteLine($" The Workmode {workModes.WorkModeToUserID}  Has been added to the list succesfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public void GetWorkModeByUserAndDate (WorkModesToUser workModes, User user)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                //Delimiter = "\t",
            };
            ListOfWorkModes = GetAllWorkModes();
            try
            {
                ListOfWorkModes.Take(CurrentUser.currentUser.Id);
                using (StreamReader reader = new StreamReader(FilePath));
                //tu nie wiem, jak zrobic odczytanie przez currentusera aktualnej daty i ID usera
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " +ex.Message);
            }
        }
    }
}
