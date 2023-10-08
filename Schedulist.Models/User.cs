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

        public User(string name, string login, params Rights[] rights)
        {
            this.Name = name;
            this.Login = login;
            if (rights.Length > 0) {
                Uprawnienia = rights.ToList();
            }

            else 
            {
                Uprawnienia = new List<Rights> {Rights.ReadOnly };
            }

        }


        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Position { get; private set; }
        public string Department { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
     
        public void CreatePassword(string newPassword)
        {
            Password = newPassword;
        }
    }
}
