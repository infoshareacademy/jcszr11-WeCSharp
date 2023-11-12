using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

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
                Delimiter = ",",
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
                Delimiter = ",",
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
                    csv.WriteHeader<WorkModesToUser>();
                    csv.NextRecord();
                    csv.WriteRecords(ListOfWorkModes);
                    //Console.Clear();
                    Console.WriteLine($" The Workmode {workModes.WorkModeName} has been added to the list succesfully at the day {workModes.dateOfWorkmode}");
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
        
        public void ModifyWorkModes(WorkModesToUser workModes)
        {
            //GetWorkModeByUserAndDate();
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ",",
            };
            ListOfWorkModes = GetAllWorkModes();
            
        }
        
        public WorkModesToUser GetWorkModeByUserAndDate (int idUser, DateOnly dateWorkMode)
        {
            var workModesReturn = ListOfWorkModes.Where(u=>u.UserID==idUser && u.dateOfWorkmode==dateWorkMode);
            return (WorkModesToUser)workModesReturn;
        }
    }
}
