using Schedulist.Business;
using System;
using Schedulist.DAL;

namespace Schedulist
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello. This is Schedulist!");
            while (true)
            {
                new Login(new CsvUserRepository("Users.csv")).Run();    //Metoda do logowania użytkownika
                if (CurrentUser.currentUser != null) MenuMain.Run();
            }
        }
    }
}