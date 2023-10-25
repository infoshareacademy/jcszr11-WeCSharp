using Schedulist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.Business.Actions
{
    internal class Method
    {
        public static string ConsolHelper(string message)
        {
            Console.WriteLine(message);
            string userInput = Console.ReadLine();
            return userInput;
        }
        public static User CreatePassword(User user)
        {
            while (true)
            {
                Console.WriteLine("===== Create password section =====");
                string newPassword = ConsolHelper("Type your password:");
                string repeatedNewPassoword = ConsolHelper("Enter your password again:");
                if (newPassword == repeatedNewPassoword && repeatedNewPassoword != null)  //Sprawdza, czy hasła są takie same lub czy hasło nie jest puste
                {
                    user.Password = newPassword;
                    Console.WriteLine("Password has been created.");
                    return user;
                }
                else Console.WriteLine("Passwords do not match or is empty, enter your passwords again.");
            }
        }
    }
}
