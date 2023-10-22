using CsvHelper;
using CsvHelper.Configuration;
using Schedulist.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Schedulist.DAL
{
    public class CsvUserRepository : IUserRepository
    {
        private readonly string filePath;
        public List<User> listOfUsers;
        public CsvUserRepository(string filePath)
        {
            this.filePath = filePath;
        }

        public List<User> GetAllUsers()
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                //Delimiter = "\t",
            };
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, csvConfig))
            {
                listOfUsers = csv.GetRecords<User>().ToList();
                return listOfUsers;
            }
        }
        public void AddUser(User user)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                //Delimiter = "\t",
            };
            try
            {
                // Create a new StreamWriter to write to the CSV file.
                //using (StreamWriter writer = new StreamWriter(filePath))
                //{
                    //foreach (User record in listOfUsers)
                    //{
                    //    // Join the values in the row with commas and write to the file.
                    //    writer.WriteLine(record);
                    //}
                    //writer.WriteRecord(user);
                    //Console.WriteLine("Succes");
                    using (var writer = new StreamWriter(filePath))
                    using (var csv = new CsvWriter(writer))
                    {
                        csv.WriteRecord(user);
                    }
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
