using CsvHelper.Configuration.Attributes;
using Schedulist.DAL;

namespace Schedulist.Business.Actions
{
    public class Helper
    {
        public static string ConsolHelper(string message)
        {
            while (true)
            {
                Console.WriteLine(message);
                string userInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(userInput)) return userInput;
                else
                {
                    Console.WriteLine("Invalid input");
                    continue;
                }
            }
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
                    if (!string.IsNullOrEmpty(error)) Console.WriteLine(error);
                    Console.WriteLine("===== Create password =====");
                    Console.WriteLine("Password should contain between 5 - 15 letters, lowercase, uppercase and number");
                    newPassword = Helper.ConsolHelper("Provide new password or type x to leave");
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
                                    else error = "Password doesn't contain uppercase character";
                                }
                                else error = "Password doesn't contain lowercase character";
                            }
                            else error = "Password doesn't contain a number";
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
                    new CsvUserRepository("..\\..\\..\\Users.csv").ModifyUser(user.Login, user);
                    Console.WriteLine("Password has been created");
                    return user;
                }
                else Console.WriteLine("Passwords do not match, enter your passwords again or type x to leave");
            }
        }
        public static bool IsCalendarEmpty(List<CalendarEvent> currentUserCalendarEvents)
        {
            if (currentUserCalendarEvents.Count == 0)
            {
                Console.WriteLine("No Calendar Events to delete.");
                Console.WriteLine("Type any key to return to menu");
                Console.ReadKey();
                return true;
            }
            return false;
        }
        public static CalendarEvent GetCalendarEvent(List<CalendarEvent> UserCalendarEvents)
        {
            Console.WriteLine("====== List of Calendar Events ======");
            Console.WriteLine("ID:\tDate:\t\tName: ");
            foreach (var item in UserCalendarEvents)
            {
                Console.WriteLine($"{item.CalendarEventId + 1}\t{item.CalendarEventDate}\t{item.CalendarEventName}");
            }
            string input = Helper.ConsolHelper("Choose the ID of Calendar Event you want to modify (or 0 to cancel):");
            if (int.TryParse(input, out int calendarEventId) && calendarEventId > 0 && calendarEventId <= UserCalendarEvents.Count)
            {
                var calendarEventToModify = UserCalendarEvents.First(c => c.CalendarEventId == (calendarEventId - 1));
                return calendarEventToModify;
            }
            else if (input.ToLower() == "0")
            {
                Console.WriteLine("Operation canceled.");
                Console.WriteLine("Press any key to return to the menu.");
                Console.ReadKey();
                new MenuOptions();
                return null;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid choice");
                Console.WriteLine("Press any key to return to the menu.");
                Console.ReadKey();
                new MenuOptions();
                return null;
            }
        }
    }
}
