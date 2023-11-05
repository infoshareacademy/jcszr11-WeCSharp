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
    internal class CSVWorkModesRepository : IWorkModesRepository
    {
        private readonly string FilePath;
        public List<WorkModes> ListOfWorkModes;
        
        public CSVWorkModesRepository(string filePath)
        {
            FilePath=filePath;
        }
        public List<WorkModes> GetAllWorkModes()
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                //Delimiter = "\t",
            };
            using (var reader = new StreamReader(FilePath))
            using (var csv = new CsvReader(reader, csvConfig))
            {
                ListOfWorkModes = csv.GetRecords<WorkModes>().ToList();
                return ListOfWorkModes;
            }
        }
        public void AddWorkModes(WorkModes workModes)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                //Delimiter = "\t",
            };
            ListOfWorkModes = GetAllWorkModes();

            // Oblicza lość użytkowników w liście i na tej podstawie nowemu użytkownikowi przydziela wartosć 
            // Id o jeden większą lub 1 w przypadku pustej listy. 
            int nextWorkmodeId = ListOfWorkModes.Count > 0 ? ListOfWorkModes.Max(w => w.WorkModeID) + 1 : 1;

            // przydzielenie Id nowego użytkownika do dobranej wartości.
            workModes.WorkModeID = nextWorkmodeId;
            try
            {
                ListOfWorkModes.Add(workModes);
                using (StreamWriter writer = new StreamWriter(FilePath))
                using (var csv = new CsvWriter(writer, csvConfig))
                {
                    csv.WriteRecords(ListOfWorkModes);
                    Console.Clear();
                    Console.WriteLine($" The Workmode {workModes.WorkModeName}  Has been added to the list succesfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
