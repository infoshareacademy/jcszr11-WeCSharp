using MailKit.Net.Smtp;
using MimeKit;

namespace Schedulist.App.Services
{
    public class EmailReportService
    {
        public static void SendEmail(string emailAdress)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("ScheduList", "schedulistapp@gmail.com"));
            message.To.Add(new MailboxAddress("Customer", emailAdress));
            message.Subject = "Schedulist Report";
            message.Body = new TextPart("plain")
            {
                Text = "Witaj!"
            };

            using SmtpClient client = new();
            client.Connect("smtp.gmail.com", 465, true);
            client.Authenticate("schedulistapp@gmail.com", "lbax mujr mwuw wbhi");
            client.Send(message);
            client.Disconnect(true);
        }
    }
}
