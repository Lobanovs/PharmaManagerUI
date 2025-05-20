using PharmaManagerUI.ViewModels;
using System.Windows;

namespace PharmaManagerUI.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow(string role)
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(role);
        }
    }
}