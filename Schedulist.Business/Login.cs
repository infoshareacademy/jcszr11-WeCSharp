using Schedulist.DAL;
using Schedulist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.Business
{
    public class Login
    {
        public User Run(UsersMemory usersMemory)
        {
            bool isLoginCorrect = false;
            bool isPasswordCorrect = false;
            Console.WriteLine("Enter your login:");  //Login użytkownika
            string login = Console.ReadLine();
            foreach (var user in usersMemory.GetUsers())
            {
                if (user.Login == login)
                {
                    isLoginCorrect = true;
                    if (string.IsNullOrEmpty(user.Password))    //Prosi o stworzenie hasła dla użytkownika jeżeli on takowego nie posiada
                    {
                        user.CreatePassword(user);  //Metoda do tworzenia hasła
                        return user;
                    }
                    Console.WriteLine("Enter your password:");
                    string password = Console.ReadLine();
                    if (user.Password == password)  //Loguje użytkownika jeżeli hasło jest poprawne
                    {
                        isPasswordCorrect = true;
                        Console.WriteLine("Successful login");
                        Console.WriteLine("===============================================================================");
                        return user;
                    }
                    if (!isPasswordCorrect) Console.WriteLine("Wrong password, please try again");
                }
            }
            if (!isLoginCorrect) Console.WriteLine("Login not found, please try again");
            return null;
        }
    }
}