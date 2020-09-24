using System;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace WpfTest
{
    public partial class MainWindow : Window
    {
        public MainWindow() 
        {
            InitializeComponent(); 
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            var mailSender = new MailAddress("shcherbininki@yandex.ru", "Константин");
            var mailRecipient = new MailAddress("shcherbininki@gmail.com");

            using var message = new MailMessage(mailSender, mailRecipient)
            {
                Subject = "Hello, world",
                Body = $"Время отправки: {DateTime.Now}"
            };
            using (var client = new SmtpClient("smtp.yandex.ru", 25))
            {
                var login = LoginEdit.Text;
                var password = PasswordEdit.SecurePassword;

                client.Credentials = new NetworkCredential(login, password);
                client.EnableSsl = true;

                client.Send(message);
            }
            
            MessageBox.Show("Почта отправлена", "Отправка почты");
        }
    }
}
