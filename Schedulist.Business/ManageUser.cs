using Schedulist.Business.Actions;
using Schedulist.DAL;
using Schedulist.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.Business
{
    internal class ManageUser
    {
        //IUserRepository _userRepository;
        //User _user;

        //public ManageUser(IUserRepository userRepository);
        //{
        //    _userRepository = userRepository;
        //}
        CsvUserRepository _csvUserRepository;
        public ManageUser(CsvUserRepository _csvUserRepository)
        {
            _csvUserRepository = new CsvUserRepository("..\\..\\..\\Users.csv");
        }

        internal void CreateUser()
        {
            Console.Clear();
            Console.WriteLine("====== Create User Section ======");
            string name = Method.ConsolHelper("Type User name");
            string surname = Method.ConsolHelper("Type User surname");
            string position = Method.ConsolHelper("Type User position");
            string department = Method.ConsolHelper("Type User department");
            string login = Method.ConsolHelper("Type User login");
            string password = Method.ConsolHelper("Type User password");
            Console.WriteLine("Is created User Admin? type y/n");
            bool isAdmin = false;
            switch (Console.ReadLine())
            {
                case "y":
                    isAdmin = true;
                    break;
                case "n":
                    isAdmin = false;
                    break;
                default:
                    Console.WriteLine("Invalid Admin access");
                    break;
            }
            User user = new User(name, surname, position, department, login, password)
            { AdminPrivilege = isAdmin };
            new CsvUserRepository("..\\..\\..\\Users.csv").AddUser(user);
        }

        internal void Modify()
        {
            Console.WriteLine();
            Console.WriteLine("===============================================================================");
            Console.WriteLine("Modify User");
            Console.WriteLine("===============================================================================");
        }
        public bool Delete(User user)
        {
            try
            {
                
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }
        //internal void Delete(User user)
        //{
        //    Console.Clear();
        //    Console.WriteLine("====== Delete User section ======");
        //    Console.WriteLine("Type Id od user to delete");

        //    Console.WriteLine("===============================================================================");

        //    Console.WriteLine("===============================================================================");
        //}
    }
}
