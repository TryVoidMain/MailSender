using MailSender.Models;
using MailSender.Lib;
using System.Windows;
using System.Net.Mail;
using System.IO;

namespace MailSender
{
    public partial class MainWindow : Window
    {
        public MainWindow() { InitializeComponent(); }

        private void OnSendButtonClick(object Sender, RoutedEventArgs e)
        {            
            if (!(SendersList.SelectedItem is Sender sender)) return;
            if(!(RecipientsList.SelectedItem is Recipient recipient)) return;
            if (!(ServerList.SelectedItem is Server server)) return;
            if (!(MessagesList.SelectedItem is Message message)) return;

            var send_service = new MailSenderService
            {
                ServerAddress = server.Address,
                ServerPort = server.Port,
                UseSSL = server.UseSSL,
                Login = server.Login,
                Password = server.Password
            };

            try
            {
                send_service.SendMessage(sender.Address, recipient.Address, message.Subject, message.Body);
            }
            catch (SmtpException error)
            {
                MessageBox.Show(
                    "Ошибка при отправке почты "+ error.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
