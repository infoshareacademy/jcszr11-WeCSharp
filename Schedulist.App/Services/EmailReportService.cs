using MailKit.Net.Smtp;
using MimeKit;
using System.Net.Mail;

namespace Schedulist.App.Services
{
    public class EmailReportService
    {
        public static async Task SendEmailAsync(string emailAdress)
        {
            var email = new MimeMessage();

            //TODO fill from / subject / body
            email.From.Add(new MailboxAddress("name", "adress"));
            email.To.Add(new MailboxAddress("Customer", emailAdress));
            email.Subject = "";
            email.Body = new TextPart("plain")
            {
                Text = "Witaj! \n" //+
                    //"Dziękujemy za rezerwację samochodu w CodeDrivers. Poniżej przesyłamy szczegóły Twojej rezerwacji. \n" +
                    //$"Auto: {reservation.Brand} {reservation.Model} \n" +
                    //$"Data odbioru: {reservation.ReservationFrom.ToShortDateString()} \n" +
                    //$"Data zwrotu: {reservation.ReservationTo.ToShortDateString()} \n" +
                    //$"Cena całkowita: {reservation.TotalReservationPrice} zł \n" +
                    //"W razie pytań jesteśmy do Twoje dyspozycji pod numerem 999 666 222 lub e-mailem codedrivers@o2.pl. \n" +
                    //"Pozdrawiamy \n" +
                    //"Ekipa CodeDrivers";
        };

        //using (var client = new SmtpClient())
        //{
        //    await client.ConnectAsync("smtp.poczta.onet.pl", 587, false); //???
        //    await client.AuthenticateAsync("email", "password");
        //    await client.SendAsync(emailMessage);

        //    await client.DisconnectAsync(true);
        //}


    }
}
}
