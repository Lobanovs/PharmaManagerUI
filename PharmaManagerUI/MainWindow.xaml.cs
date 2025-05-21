using PharmaManagerUI.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace PharmaManagerUI.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow(string role)
        {
            InitializeComponent();
            DataContext = new MainViewModel(); // Устанавливаем DataContext здесь
            Title = $"PharmaManagerUI - {role}";
            MainTabControl.SelectedIndex = 0; // По умолчанию открываем вкладку "Клиенты"
        }

        private void Clients_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 0;
        }

        private void Drugs_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 1;
        }

        private void Users_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 2;
        }

        private void RawMaterials_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 3;
        }

        private void Equipment_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 4;
        }

        private void ProductionOrders_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 5;
        }

        private void Staff_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 6;
        }

        private void QualityControls_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 7;
        }

        private void Warehouses_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 8;
        }

        private void Logistics_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 9;
        }

        private void PharmacyNetworks_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 10;
        }

        private void Sales_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 11;
        }
    }
}