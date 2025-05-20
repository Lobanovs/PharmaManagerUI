using PharmaManagerUI.ViewModels;
using System.Windows;

namespace PharmaManagerUI.Views
{
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
            DataContext = new RegistrationViewModel();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}