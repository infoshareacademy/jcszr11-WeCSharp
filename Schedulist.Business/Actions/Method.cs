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
                string newPassword;
                string error = "";
                while (true)
                {
                    Console.Clear();
                    if (!String.IsNullOrEmpty(error)) Console.WriteLine(error);
                    Console.WriteLine("===== Create password =====");
                    Console.WriteLine("Password should contain between 5 - 15 letters, lowercase, uppercase and number");
                    newPassword = Method.ConsolHelper("Provide new password or type x to leave");
                    if (newPassword == "x") return null;
                    if (newPassword.Length >= 5)
                    {
                        if (newPassword.Length <= 15)
                        {
                            if (newPassword.Any(char.IsDigit))
                            {
                                if (newPassword.Any(char.IsLower))
                                {
                                    if (newPassword.Any(char.IsUpper))
                                    {
                                        break;
                                    }
                                    else error = "Password doesnt contain uppercase character";
                                }
                                else error = "Password doesnt contain lowercase character";
                            }
                            else error = "Password doesnt contain number";
                        }
                        else error = "Password too long";
                    }
                    else error = "Password too short";
                }
                string repeatedNewPassoword = ConsolHelper("Enter your password again:");
                if (repeatedNewPassoword == "x") return null;
                if (newPassword == repeatedNewPassoword && newPassword != null)
                {
                    user.Password = newPassword;
                    //todo save to file
                    Console.WriteLine("Password has been created.");
                    return user;
                }
                else Console.WriteLine("Passwords do not match, enter your passwords again or type x to leave");
            }
        }
    }
}
