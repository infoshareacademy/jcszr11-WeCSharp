﻿using Schedulist.Business.Actions;
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
                    while (true)
                    {
                        if (string.IsNullOrEmpty(currentUser.Password))    //Prosi o stworzenie hasła dla użytkownika jeżeli on takowego nie posiada
                        {
                            Method.CreatePassword(currentUser);  //Metoda do tworzenia hasła
                            if (!string.IsNullOrEmpty(currentUser.Password))
                            {
                                CurrentUser.currentUser = currentUser;
                                return currentUser;    //Loguje użytkownika
                            }
                            else break;
                        }
                        string password = Method.ConsolHelper("Enter your password or type x to enter login again:");
                        if (password == "x") return null;
                        else
                        {
                            if (currentUser.Password == password)  //Sprawdza czy hasło jest poprawne
                            {
                                CurrentUser.currentUser = currentUser;
                                return currentUser;    //Loguje użytkownika
                            }
                            else Console.WriteLine("Wrong password, please try again");
                        }
                    }
                }
                else Console.WriteLine("Login not found, please try again.");
            }
        }
    }
}