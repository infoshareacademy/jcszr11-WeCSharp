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

        public int? Id { get;  set; }
        public string Name { get;  set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool AdminPrivilege { get; set; }
        public ICollection<CalendarEvent> CalendarEvents { get; set; }

        //public override string ToString()
        //{
        //    return ($"{Id}");
        //}
    }
}
