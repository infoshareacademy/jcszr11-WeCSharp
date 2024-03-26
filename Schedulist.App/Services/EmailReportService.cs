using MailKit.Net.Smtp;
using MimeKit;

namespace Schedulist.App.Services
{
    public class EmailReportService
    {
        public static void SendEmail(string emailAdress)
        {
            var message = new MimeMessage();

            //TODO fill from / / body
            message.From.Add(new MailboxAddress("ScheduList", "wecsharp@op.pl"));
            message.To.Add(new MailboxAddress("Customer", emailAdress));
            message.Subject = "Schedulist Report";
            message.Body = new TextPart("plain")
            {
                Text = "Witaj!"
            };

            using (SmtpClient client = new())
            {
                client.Connect("smtp.poczta.onet.pl", 587, false);
                client.Authenticate("wecsharp@op.pl", "hasloSharp123");
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
