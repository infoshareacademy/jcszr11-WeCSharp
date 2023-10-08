using Schedulist.Models;
using Schedulist.Business;

namespace Schedulist
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello. This is Schedulist!");
            new Login().Run();    //Metoda do logowania użytkownika
        }
    }
}