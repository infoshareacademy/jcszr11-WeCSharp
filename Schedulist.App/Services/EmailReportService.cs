using MailKit.Net.Smtp;
using MimeKit;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.Globalization;

namespace Schedulist.App.Services
{
    public class EmailReportService
    {
        public static void CreatePdf(DateTime date)
        {
            PdfDocument document = new();
            CultureInfo ci = new("en-GB");
            string month = date.ToString("MMMM_yyyy", ci);

            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont header = new("Verdana", 20, XFontStyle.Bold);
            gfx.DrawString(@$"Raport for {month}", header, XBrushes.Black,
            new XRect(0, 10, page.Width, 50),
            XStringFormats.Center);

            XFont font = new("Verdana", 14, XFontStyle.Bold);
            string text = "Hello world";
            gfx.DrawString(text, font, XBrushes.Black,
            new XRect(20, 70, page.Width, page.Height),
            XStringFormats.TopLeft);

            string filename = $"Raport_For_{month}.pdf";

            document.Save($"raports/{filename}");
        }
        public static bool SendEmail(string emailAdress, DateTime month)
        {
            //checking if required pdf exists and generating it if it doesn't
            CultureInfo ci = new("en-GB");
            string selectedMonth = month.ToString("MMMM_yyyy", ci);
            string filename = $"Raport_For_{selectedMonth}.pdf";
            if (!File.Exists($"raports/{filename}"))
            {
                CreatePdf(month);
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
@$"Hello!


The attachment includes a report for {selectedMonth.Replace("_", " ")}.
Thank you for using Schedulist."
            };
            multipart.Add(body);

            var attachment = new MimePart("document", "pdf")
            {
                Content = new MimeContent(File.OpenRead($"raports/{filename}"), ContentEncoding.Default),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = Path.GetFileName($"raports/{filename}")
            };
            multipart.Add(attachment);

            message.Body = multipart;


            //connecting and sending
            using SmtpClient client = new();
            client.Connect("smtp.gmail.com", 465, true);
            client.Authenticate("schedulistapp@gmail.com", "lbax mujr mwuw wbhi");
            client.Send(message);
            client.Disconnect(true);
            return true;
        }
    }
}
