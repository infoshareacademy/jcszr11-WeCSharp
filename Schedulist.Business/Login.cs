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
            var usersMemory = new UsersMemory();
            
                Console.WriteLine("Enter your login:");  //Login użytkownika
                string login = Console.ReadLine();             
                foreach (var user in usersMemory.GetUsers())
                {
                    if (user.Login == login)
                    {
                        isLoginCorrect = true;
                        /*if (user.Password == "" || user.Password == null)*/    //Prosi o stworzenie hasła dla użytkownika jeżeli on takowego nie posiada
                        if (string.IsNullOrEmpty(user.Password))    //Prosi o stworzenie hasła dla użytkownika jeżeli on takowego nie posiada
                        {
                            while (true)
                            {
                                Console.WriteLine("Create password:");
                                string newpassword = Console.ReadLine();
                                Console.WriteLine("Enter your password again:");
                                string repeatedNewPassoword = Console.ReadLine();
                                if (newpassword == repeatedNewPassoword && repeatedNewPassoword != null)  //Sprawdza, czy hasła są takie same
                                {
                                    user.CreatePassword(repeatedNewPassoword);
                                    Console.WriteLine("Password has been created");
                                    break;
                                }
                                else Console.WriteLine("Passwords do not match, enter your passwords again");
                            }
                        }
                        Console.WriteLine("Enter your password:");
                        string password = Console.ReadLine();
                        if (user.Password == password)  //Loguje użytkownika jeżeli hasło jest poprawne
                        {
                            isPasswordCorrect = true;
                            Console.WriteLine($"Welcome, {user.Name}");
                        Console.WriteLine("===============================================================================");
                        return user;              
                        }
                        if (!isPasswordCorrect) Console.WriteLine("Wrong password, please try again");
                    }
                }
                if (!isLoginCorrect)
            {
                Console.WriteLine("Login not found, please try again");
                
            }
            return null;

        }
    }
}