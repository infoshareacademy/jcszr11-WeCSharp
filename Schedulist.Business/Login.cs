using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.Business
{
    public class Login
    {
        public void UserLogin()
        {
            bool gotLogin = false;
            bool gotPassword = false;
            while (true)
            {
                Console.WriteLine("Enter your login:");  //Login użytkownika
                string login = Console.ReadLine();
                foreach (var user in userList)
                {
                    if (user.Login == login)
                    {
                        gotLogin = true;
                        if (user.Password == "")    //Prosi o stworzenie hasła dla użytkownika jeżeli on takowego nie posiada
                        {
                            while (true)
                            {
                                Console.WriteLine("Create password:");
                                string newpassword = Console.ReadLine();
                                Console.WriteLine("Enter your password again:");
                                string newnewpassword = Console.ReadLine();
                                if (newpassword == newnewpassword)  //Sprawdza, czy hasła są takie same
                                {
                                    user.Password = newpassword;
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
                            gotPassword = true;
                            Console.WriteLine($"Welcome, {user.Name}");
                            Console.WriteLine("===============================================================================");
                            break;
                        }
                        if (!gotPassword) Console.WriteLine("Wrong password, please try again");
                    }
                }
                if (!gotLogin) Console.WriteLine("Login not found, please try again");
            }
        }
    }
}