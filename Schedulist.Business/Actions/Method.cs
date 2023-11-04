using ConsoleApp1;
using Schedulist.DAL;
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
                Console.Clear();
                Console.WriteLine("===== Create password =====");
                Console.WriteLine("Password should contain between 5 - 15 letters, lowercase, uppercase and number");
                string newPassword = ConsolHelper("Type your password:");

                //todo - wip

                //var validator = new StringValidationBuilder().AddValidationOption(
                //    new LengthValidation(5, 15),
                //    new HasLowercaseValidation(),
                //    new HasUppercaseValidation(),
                //    new HasDigitValidation()
                //    );
                new StringValidationBuilder().Validate( newPassword );
                string repeatedNewPassoword = ConsolHelper("Enter your password again:");
                if (newPassword == repeatedNewPassoword && newPassword != null)  //Sprawdza, czy hasła są takie same lub czy hasło nie jest puste
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
