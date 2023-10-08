﻿using Schedulist.Models;
using Schedulist.Business;

using System;

namespace Schedulist
{
    internal class Program
    {
        static void Main(string[] args)
        {
           List<User> users = new List<User>();
            users.Add(new User("Basic user"));
            users.Add(new User("Admin user", Rights.IsAdmin, Rights.DeleteUser, Rights.AddUser));

            foreach(var user in users)
            {
                Console.WriteLine(user.Name);
                if (user.Uprawnienia.Contains(Rights.DeleteUser)) { Console.WriteLine("usuwanie użytkownia menu"); }
                if (user.Uprawnienia.Contains(Rights.AddUser)) { Console.WriteLine("dodawanie użytkownia menu"); }
                if (user.Uprawnienia.Contains(Rights.IsAdmin)) { Console.WriteLine("admin menu"); }
                if (user.Uprawnienia.Contains(Rights.ReadOnly)) { Console.WriteLine("readolny menu"); }

            }
            Console.WriteLine("Hello. This is Schedulist!");
           // new Login().Run();    //Metoda do logowania użytkownika // do dokończenia
            Console.ReadLine();

        }
    }
}