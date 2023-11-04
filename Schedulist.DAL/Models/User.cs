using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;


namespace Schedulist.DAL
{
    public class User
    {

        public User(string name, string surname, string position, string department, string login, string password)
        {
            Name = name;
            Surname = surname;
            Login = login;
            Position = position;
            Department = department;
            Password = password;
        }
        public User()
        {
            
        }
        //[Index(0)]
        public int Id { get;  set; }
        //[Index(1)]
        public string Name { get;  set; }
        //[Index(2)]
        public string Surname { get; set; }
        //[Index(3)]
        public string Position { get; set; }
        //[Index(4)]
        public string Department { get; set; }
        //[Index(5)]
        public string Login { get; set; }
        //[Index(6)]
        public string Password { get; set; }
        //[Index(7)]
        public bool AdminPrivilege { get; set; }

    }
}
