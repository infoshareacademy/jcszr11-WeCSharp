using MailKit.Net.Smtp;
using MimeKit;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using System.IO;

namespace Schedulist.App.Services
{
    public class EmailReportService
    {
        public static void CreatePdf(DateTime date)
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            string month = date.ToString("M_yyyy");
            string filename = $"Raport_For_{month}.pdf";
            document.Save(filename);
        }
        public static void SendEmail(string emailAdress)
        {
            //checking if required pdf exists and generating it if it doesn't
            DateTime previousMonth = DateTime.Now.AddMonths(-1);
            string previousMonthString = previousMonth.ToString("M_yyyy");
            string filename = $"Raport_For_{previousMonthString}.pdf";
            if (!File.Exists(filename))
            {
                CreatePdf(previousMonth);
            }


            //generating email
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("ScheduList", "schedulistapp@gmail.com"));
            message.To.Add(new MailboxAddress("Customer", emailAdress));
            var multipart = new Multipart("mixed");
            message.Subject = "Schedulist Report";

            var body = new TextPart("plain")
            {
                Text = 
@"Hello!


The attachment includes a report for last month.
Thank you for using Schedulist."
            };
            multipart.Add(body);

            var attachment = new MimePart("document", "pdf")
            {
                Content = new MimeContent(File.OpenRead(filename), ContentEncoding.Default),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = Path.GetFileName(filename)
            };
            multipart.Add(attachment);

            message.Body = multipart;


            //connecting and sending
            using SmtpClient client = new();
            client.Connect("smtp.gmail.com", 465, true);
            client.Authenticate("schedulistapp@gmail.com", "lbax mujr mwuw wbhi");
            client.Send(message);
            client.Disconnect(true);
        }
    }
}
