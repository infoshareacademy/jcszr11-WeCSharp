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
                Console.WriteLine("Hello. This is Schedulist!");
                string login = Helper.ConsolHelper("\nEnter your login:");
                if (userRepository.GetAllUsers().Any(x => x.Login == login))
                {
                    User currentUser = userRepository.GetAllUsers().First(x => x.Login == login); // Sprawdzenie czy w bazie jest użytkownik o podanym loginie
                    while (true)
                    {
                        if (string.IsNullOrWhiteSpace(currentUser.Password))//Prosi o stworzenie hasła dla użytkownika jeżeli on takowego nie posiada
                        {
                            Console.Clear();
                            currentUser.Password = Helper.CreatePassword();//Metoda do tworzenia hasła
                            new CsvUserRepository("..\\..\\..\\Users.csv").ModifyUser(currentUser.Login, currentUser);
                            Console.WriteLine("Password has been created");
                            if (!string.IsNullOrWhiteSpace(currentUser.Password))
                            {
                                CurrentUser.currentUser = currentUser;
                                return currentUser;//Loguje użytkownika
                            }
                            else break;
                        }
                        string password = ReadPassword("Enter your password or type x to enter login again:\n");

                        if (password == "x")
                        {
                            return null;
                        }
                        else
                        {
                            if (currentUser.Password == password)//Sprawdza czy hasło jest poprawne
                            {
                                CurrentUser.currentUser = currentUser;
                                return currentUser; //Loguje użytkownika
                            }
                            else
                            {
                                Console.WriteLine("Wrong password, please try again");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Login not found, please try again.");
                }
            }
        }
        private string ReadPassword(string prompt) // odczytuje pojedyńcze znaki i zamienia je na chary "*"
        {
            Console.Write(prompt);
            StringBuilder password = new StringBuilder();
            ConsoleKeyInfo key;

            while (true)
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password.Remove(password.Length - 1, 1);
                    Console.Write("\b \b");
                }
                else if (key.KeyChar != '\u0000' && !char.IsControl(key.KeyChar))
                {
                    password.Append(key.KeyChar);
                    Console.Write("*");
                }
            }

            return password.ToString();
        }
    }
}