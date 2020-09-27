using System;
using System.Net;
using System.Net.Mail;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var sender = new MailAddress("shcherbininki@yandex.ru", "Константин");
            var recipient = new MailAddress("shcherbininki@gmail.com");

            using var message = new MailMessage(sender, recipient)
            {
                Subject = "Hello, world",
                Body = $"Время отправки: {DateTime.Now}"
            };
            using (var client = new SmtpClient("smtp.yandex.ru", 25))
            {
                const string login = "shcherbininki@yandex.ru";
                string password = Convert.ToString(Console.ReadLine());

                client.Credentials = new NetworkCredential(login, password);
                client.EnableSsl = true;

                client.Send(message);
            }

            Console.WriteLine("Почта отправлена");
            Console.ReadLine();
        }
    }
}
