﻿using CsvHelper;
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
            listOfUsers = GetAllUsers();
            try
            {
                using (StreamWriter writer = new StreamWriter("C:\\Users\\Mariusz\\Desktop\\InfoShare\\#4 Projekt grupowy Work-Schedule\\jcszr11-WeCSharp\\Schedulist\\Users.csv", true))
                using (var csv = new CsvWriter(writer, csvConfig))
                {
                    csv.WriteRecord(user);
                    Console.Clear();        
                    Console.WriteLine($" The User {user.Name} {user.Surname} Has been added to the list succesfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
