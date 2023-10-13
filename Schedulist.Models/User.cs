using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.Models
{
    public class User
    {

        public List<Rights> Uprawnienia { get; set; }

        public User(string name, string surname, string position, string department, string login, params Rights[] rights)
        {
            Name = name;
            Surname = surname;
            Login = login;
            Position = position;
            Department = department;
            if (rights.Length > 0)
            {
                Uprawnienia = rights.ToList();
            }
            else
            {
                Uprawnienia = new List<Rights> { Rights.ReadOnly };
            }
        }
        public int Id { get; private set; }
        public int CurrentId { get; set; } = 0;
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Position { get; private set; }
        public string Department { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public bool AdminPrivilege { get; set; }

        public void CreatePassword(User user)
        {
            while (true)
            {
                Console.WriteLine("Create password:");
                string newPassword = Console.ReadLine();
                Console.WriteLine("Enter your password again:");
                string repeatedNewPassoword = Console.ReadLine();
                if (newPassword == repeatedNewPassoword && repeatedNewPassoword != null)  //Sprawdza, czy hasła są takie same
                {
                    Password = newPassword;
                    Console.WriteLine("Password has been created");
                    Console.WriteLine("===============================================================================");
                    break;
                }
                else Console.WriteLine("Passwords do not match, enter your passwords again");
            }
        }
    }
}
