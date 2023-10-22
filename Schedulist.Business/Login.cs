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
            bool isLoginCorrect = false;
            bool isPasswordCorrect = false;
            Console.WriteLine("Enter your login:");  //Login użytkownika
            string login = Console.ReadLine();
            //if (userRepository.GetAllUsers().Any(x => x.Login == login))
            //{

            //}
            //else
            //{
            //    Console.WriteLine("User not found, please try again");
            //}
            foreach (User user in userRepository.GetAllUsers())
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
                    if (user.Password == password)  //Sprawdza czy hasło jest poprawne
                    {
                        isPasswordCorrect = true;
                        return user;    //Loguje użytkownika
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