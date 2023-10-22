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
        public User Run()
        {
            bool isLoginCorrect = false;
            bool isPasswordCorrect = false;
            Console.WriteLine("Enter your login:");  //Login użytkownika
            string login = Console.ReadLine();
            if (UsersMemory.listOfUsers.Any(x => x.Login == login))
            {

            }
            else
            {
                Console.WriteLine("User not found, please try again");
            }
            foreach (User item in UsersMemory.listOfUsers)
            {
                if (item.Login == login)
                {
                    isLoginCorrect = true;
                    if (string.IsNullOrEmpty(item.Password))    //Prosi o stworzenie hasła dla użytkownika jeżeli on takowego nie posiada
                    {
                        item.CreatePassword(item);  //Metoda do tworzenia hasła
                        return item;
                    }
                    Console.WriteLine("Enter your password:");
                    string password = Console.ReadLine();
                    if (item.Password == password)  //Loguje użytkownika jeżeli hasło jest poprawne
                    {
                        isPasswordCorrect = true;
                        Console.WriteLine("Successful login");
                        Console.WriteLine("===============================================================================");
                        return item;
                    }
                    if (!isPasswordCorrect) Console.WriteLine("Wrong password, please try again");
                }
                else
                {
                    Console.WriteLine("Login not found, please try again");
                }
            }
            if (!isLoginCorrect) Console.WriteLine("Login not found, please try again");
            return null;
        }
    }
}