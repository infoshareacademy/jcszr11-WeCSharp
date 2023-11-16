using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Schedulist.DAL
{
    public class CsvUserRepository : IUserRepository
    {
        private readonly string FilePath;
        public List<User> listOfUsers;
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
            };
            using var reader = new StreamReader(FilePath);
            using var csv = new CsvReader(reader, csvConfig);
            listOfUsers = csv.GetRecords<User>().ToList();
            return listOfUsers;
        }
        public void AddUser(User user, int? ID)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };
            listOfUsers = GetAllUsers();

            // Oblicza loœæ u¿ytkowników w liœcie i na tej podstawie nowemu u¿ytkownikowi przydziela wartosæ 
            // Id o jeden wiêksz¹ lub 1 w przypadku pustej listy. 
            if (ID == null) user.Id = listOfUsers.Count > 0 ? listOfUsers.Max(u => u.Id) + 1 : 1;
                
            try
            {
                listOfUsers.Add(user);

                //listOfUsers = listOfUsers.OrderByDescending(q => q.listOfUsers.Id).ToList();
                listOfUsers.Sort((p, q) => p.Id.ToString().CompareTo(q.Id.ToString()));

                using StreamWriter writer = new(FilePath);
                using var csv = new CsvWriter(writer, csvConfig);
                csv.WriteRecords(listOfUsers);
                Console.Clear();
                Console.WriteLine($"The User {user.Name} {user.Surname} Has been added to the list succesfully");
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
        public void WriteAllUsers(List<User> users)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);
            try
            {
                using StreamWriter writer = new StreamWriter(FilePath);
                using var csv = new CsvWriter(writer, csvConfig);
                csv.WriteRecords(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while writing to the CSV file: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }
            }
        }
        public void ModifyUser(string userToModifyLogin, User modifiedUser)
        {
            listOfUsers = GetAllUsers();
            if (listOfUsers.Any(user => user.Login == userToModifyLogin))
            {
                User user = listOfUsers.FirstOrDefault(user => user.Login == userToModifyLogin);
                DeleteUser(user);
                AddUser(modifiedUser, user.Id);
            }
        }
        public void DeleteUser(User userToDelete)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true, };
            listOfUsers = GetAllUsers();
            using StreamWriter writer = new(FilePath);
            using var csv = new CsvWriter(writer, csvConfig);
            User user = listOfUsers.FirstOrDefault(user => user.Login == userToDelete.Login);
            listOfUsers.Remove(user);
            csv.WriteRecords(listOfUsers);
        }
    }
}
