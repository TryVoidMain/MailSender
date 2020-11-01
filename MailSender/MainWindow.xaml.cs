using MailSender.Models;
using MailSender.Lib;
using System.Windows;
using System.Net.Mail;
using System.IO;
using MailSender.Data;
using System.Linq;

namespace MailSender
{
    public partial class MainWindow : Window
    {
        public MainWindow() { InitializeComponent(); }

        private void OnAddServerButton(object Sender, RoutedEventArgs E)
        {
            if (!ServerEditDialog.Create(
                out var name,
                out var address,
                out var port,
                out var useSSL,
                out var description,
                out var login,
                out var password)) return;

            var server = new Server
            {
                Name = name,
                Address = address,
                Port = port,
                UseSSL = useSSL,
                Description = description,
                Login = login,
                Password = password
            };
            TestData.Servers.Add(server);

            ServerList.ItemsSource = null;
            ServerList.ItemsSource = TestData.Servers;
            ServerList.SelectedItem = server;
        }

        private void OnEditServerButton(object Sender, RoutedEventArgs E)
        {
            if (!(ServerList.SelectedItem is Server server)) return;
            var name = server.Name;
            var address = server.Address;
            var port = server.Port;
            var useSSL = server.UseSSL;
            var description = server.Description;
            var login = server.Login;
            var password = server.Password;

            if (!ServerEditDialog.ShowDialog("Редактирование сервера",
                ref name,
                ref address,
                ref port,
                ref useSSL,
                ref description, 
                ref login, 
                ref password)) return;

            server.Name = name;
            server.Address = address;
            server.Port = port;
            server.UseSSL = useSSL;
            server.Description = description;
            server.Login = login;
            server.Password = password;

            ServerList.SelectedItem = null;
            ServerList.SelectedItem = TestData.Servers;
        }

       private void OnDeleteServerButton(object Sender, RoutedEventArgs E)
        {
            if (!(ServerList.SelectedItem is Server server)) return;

            TestData.Servers.Remove(server);

            ServerList.SelectedItem = null;
            ServerList.SelectedItem = TestData.Servers.FirstOrDefault();
        }

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
