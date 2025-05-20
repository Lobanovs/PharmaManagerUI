using PharmaManagerUI.ViewModels;
using System.Windows;

namespace PharmaManagerUI.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var registrationWindow = new RegistrationWindow();
            registrationWindow.ShowDialog();
        }
    }
}