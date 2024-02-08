using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;
using Schedulist.DAL.Repositories.Interfaces;
using Schedulist.DAL.Models;

namespace Schedulist.DAL
{
    public class CSVWorkModesRepository : IWorkModesRepository
    {
        private readonly string FilePath;
        public List<WorkModesForUser> ListOfWorkModes;
        
        public CSVWorkModesRepository(string filePath)
        {
            FilePath=filePath;
        }
        public List<WorkModesForUser> GetAllWorkModes()
        {
            var csvConfig = CsvConfiguration();
            using var reader = new StreamReader(FilePath);
            using var csv = new CsvReader(reader, csvConfig);
            ListOfWorkModes = csv.GetRecords<WorkModesForUser>().ToList();
            return ListOfWorkModes;
        }
        public void AddWorkModes(WorkModesForUser workModes)
        {
            var csvConfig = CsvConfiguration();
            ListOfWorkModes = GetAllWorkModes();

            int nextWorkmodeToUserId = (int)(ListOfWorkModes.Count > 0 ? ListOfWorkModes.Max(w => w.Id) + 1 : 1);
                        
            workModes.Id = nextWorkmodeToUserId;
            try
            {
                ListOfWorkModes.Add(workModes);
                using StreamWriter writer = new(FilePath);
                using var csv = new CsvWriter(writer, csvConfig);
                csv.WriteHeader<WorkModesForUser>();
                csv.NextRecord();
                csv.WriteRecords(ListOfWorkModes);
                //Console.Clear();
                Console.WriteLine($" The Workmode {workModes.WorkMode.Name} has been added to the list successfully at the day {workModes.DateOfWorkMode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
        
        public void ModifyWorkModes(int workModeToUserID, WorkModesForUser workModesToModify)
        {
            var csvConfig = CsvConfiguration();
            ListOfWorkModes = GetAllWorkModes();
            
            if(ListOfWorkModes.Any(w=>w.Id==workModeToUserID)) 
            {                
                workModeToUserID = workModesToModify.Id;
                int indexToUpdate = workModeToUserID - 1;
                ListOfWorkModes[indexToUpdate]=workModesToModify;
                try
                {
                    using StreamWriter writer = new(FilePath);
                    using var csv = new CsvWriter(writer, csvConfig);
                    csv.WriteRecords(ListOfWorkModes);
                }
                catch (Exception exc)
                {

                    Console.WriteLine("An error occurred while writing to the CSV file: " + exc.Message);
                    if (exc.InnerException != null)
                    {
                        Console.WriteLine("Inner Exception: " + exc.InnerException.Message);
                    }
                }
            }                
                //WorkModesToUser workMode = ListOfWorkModes.FirstOrDefault(w=>w.WorkModeToUserID==workModeToUserID);
                //ListOfWorkModes.Remove(workMode);            
                //AddWorkModes(workModesToModify);
        }

        public void DeleteWorkModes(int workModesToDeleteID)
        {
            var csvConfig = CsvConfiguration();
            ListOfWorkModes = GetAllWorkModes();
            
            if(ListOfWorkModes.Any(w=>w.Id==workModesToDeleteID))
            {
                var removeWorkMode = ListOfWorkModes.FirstOrDefault(w=>w.Id==workModesToDeleteID);
                ListOfWorkModes.Remove(removeWorkMode);
                try
                {
                    using StreamWriter writer = new(FilePath, append: false);
                    using CsvWriter csv = new(writer, csvConfig);
                    csv.WriteRecords(ListOfWorkModes);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                    }
                }
            }
        }
        
        public WorkModesForUser? GetWorkModeByUserAndDate (int? idUser, DateOnly dateWorkMode)
        {
            ListOfWorkModes = GetAllWorkModes();
            var workModesReturn = ListOfWorkModes.FirstOrDefault(u=>u.UserId==idUser && u.DateOfWorkMode==dateWorkMode);
            return workModesReturn;
        }
        private static CsvConfiguration CsvConfiguration()
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ",",
            };
            return csvConfig;
        }

        public WorkModesForUser GetWorkModeByUserAndDate(int idUser, DateOnly dateWorkMode)
        {
            throw new NotImplementedException();
        }
    }
}
