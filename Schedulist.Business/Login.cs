using Schedulist.Business.Actions;
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
        private readonly IUserRepository userRepository;

        public Login(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public User Run()
        {
            while (true)
            {
                string login = Method.ConsolHelper("Enter your login:");
                if (userRepository.GetAllUsers().Any(x => x.Login == login))
                {
                    User currentUser = userRepository.GetAllUsers().First(x => x.Login == login); // Sprawdzenie czy w bazie jest użytkownik o podanym loginie
                    string password = Method.ConsolHelper("Enter your password:");
                    if (currentUser.Password == password) // Sprawdzanie czy podane hasło jest zgodne z tym wpisanym w konsoli
                    {
                        return currentUser; //Loguje użytkownika
                    }
                    else
                    {
                        Console.WriteLine("Your password has not been found.");
                        Method.CreatePassword(currentUser);  //Metoda do tworzenia hasła
                        return currentUser; //Loguje użytkownika
                    }
                }
                else
                    Console.WriteLine("Login not found, please try again.");
            }
        }
    }
}

