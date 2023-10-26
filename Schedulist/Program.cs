using Schedulist.Models;
using Schedulist.Business;
using System;
using Schedulist.DAL;

namespace Schedulist
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello. This is Schedulist!");
            while (true)
            {
                User currentUser = new Login(new CsvUserRepository("Users.csv")).Run();    //Metoda do logowania użytkownika
                if (currentUser != null) currentUser = new MenuMain().Run(currentUser);
            }
        }
    }
}