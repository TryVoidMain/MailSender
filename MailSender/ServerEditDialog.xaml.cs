using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;

namespace MailSender
{
    public partial class ServerEditDialog
    {
        private ServerEditDialog() => InitializeComponent();
        
        private void OnPortTextInput(object Sender, TextCompositionEventArgs E)
        {
            if (!(Sender is TextBox text_box) || text_box.Text == "") return;
            E.Handled = !int.TryParse(text_box.Text, out _);
        }

        private void OnButtonClick(object Sender, RoutedEventArgs E)
        {
            DialogResult = !((Button)E.OriginalSource).IsCancel;
            Close();
        }

        public static bool ShowDialog(
            string Title, ref string Name,
            ref string Address, ref int Port, ref bool UseSSL,
            ref string Description, 
            ref string Login, ref string Password)
        {
            var window = new ServerEditDialog
            {
                Title = Title,
                ServerName = { Text = Name },
                ServerAddress = { Text = Address },
                ServerPort = { Text = Port.ToString() },
                ServerSSL = { IsChecked = UseSSL },
                Login = { Text = Login },
                Password = { Password = Password },
                ServerDescription = { Text = Description },
                Owner = Application
                .Current
                .Windows
                .Cast<Window>()
                .FirstOrDefault(window => window.IsActive)
            };
            if (window.ShowDialog() != true) return false;

            Name = window.ServerName.Text;
            Address = window.ServerAddress.Text;
            Port = int.Parse(window.ServerPort.Text);
            Login = window.Login.Text;
            Password = window.Password.Password;
            return true;

        }

        public static bool Create(
            out string Name,
            out string Address,
            out int Port,
            out bool UseSSL,
            out string Description,
            out string Login,
            out string Password)
        {
            Name = null;
            Address = null;
            Port = 25;
            UseSSL = false;
            Description = null;
            Login = null;
            Password = null;

            return ShowDialog("Создать сервер",
                ref Name,
                ref Address,
                ref Port,
                ref UseSSL,
                ref Description,
                ref Login,
                ref Password);
        }   
        

            
        
    }
}
