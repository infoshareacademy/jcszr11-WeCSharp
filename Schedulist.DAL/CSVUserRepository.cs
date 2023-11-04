using CsvHelper;
using CsvHelper.Configuration;
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
        private readonly string FilePath;
        public List<User> ListOfUsers;
        public CsvUserRepository(string filePath)
        {
            this.FilePath = filePath;
        }

        public CsvUserRepository()
        {
        }

        public List<User> GetAllUsers()
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                //Delimiter = "\t",
            };
            using var reader = new StreamReader(FilePath);
            using var csv = new CsvReader(reader, csvConfig);
            ListOfUsers = csv.GetRecords<User>().ToList();
            return ListOfUsers;
        }
        public void AddUser(User user)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                //Delimiter = "\t",
            };
            ListOfUsers = GetAllUsers();

            // Oblicza lość użytkowników w liście i na tej podstawie nowemu użytkownikowi przydziela wartosć 
            // Id o jeden większą lub 1 w przypadku pustej listy. 
            int nextUserId = ListOfUsers.Count > 0 ? ListOfUsers.Max(u => u.Id) + 1 : 1;

            // przydzielenie Id nowego użytkownika do dobranej wartości.
            user.Id = nextUserId;
            try
            {
                ListOfUsers.Add(user);
                using StreamWriter writer = new (FilePath);
                using var csv = new CsvWriter(writer, csvConfig);
                csv.WriteRecords(ListOfUsers);
                Console.Clear();
                Console.WriteLine($" The User {user.Name} {user.Surname} Has been added to the list succesfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
