using Schedulist.Business;
using System;
using Schedulist.DAL;
using Schedulist.DAL.Models;

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