using Schedulist.Business.Actions;
using Schedulist.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.Business
{
    public class Login
    {
        private readonly IUserRepository userRepository;

        public Login(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public User Run()
        {
            Console.Clear();
            while (true)
            {
                string login = Method.ConsolHelper("Enter your login:");
                if (userRepository.GetAllUsers().Any(x => x.Login == login))
                {
                    User currentUser = userRepository.GetAllUsers().First(x => x.Login == login); // Sprawdzenie czy w bazie jest użytkownik o podanym loginie
                    string password = Method.ConsolHelper("Enter your password:");
                    while (true)
                    {
                        if (currentUser.Password == password) // Sprawdzanie czy podane hasło jest zgodne z tym wpisanym w konsoli
                        {  
                            CurrentUser.currentUser = currentUser; //Loguje użytkownika
                            return null;
                        }
                        else if (currentUser.Password != password)
                        {
                            Console.Clear();
                            Console.WriteLine("Your password has not been found.");
                            Console.WriteLine("Do you want to create new password? Choose option:");
                            Console.WriteLine("1.Create new password");
                            Console.WriteLine("2.Retry typing");
                            string userAnswer = Method.ConsolHelper("3.Exit");
                            switch (userAnswer)
                            {
                                case "1":
                                    Console.Clear();
                                    Method.CreatePassword(currentUser);
                                    CurrentUser.currentUser = currentUser;
                                    break;
                                case "2":
                                    Console.Clear();
                                    password = Method.ConsolHelper("Enter your password:");
                                    break;
                                case "3":
                                    Environment.Exit(0);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                else Console.WriteLine("Login not found, please try again.");
            }
        }
    }
}

