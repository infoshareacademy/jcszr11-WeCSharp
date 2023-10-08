using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.Models
{
    enum Rights
    {
        IsAdmin = 10,
        DeleteUser = 20,
        AddUser = 30,
        ReadOnly = 40
    }
    public class User
    {
       
        public List<Rights> Uprawnienia { get; set; }

        public User(string name, params Rights[] rights)
        {
            this.Name= name;
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
     
    }
}
