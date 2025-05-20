using PharmaManagerUI.Commands;
using PharmaManagerUI.Helpers;
using PharmaManagerUI.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PharmaManagerUI.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private object _currentView;
        private TableConfig _selectedTable;

        public string WelcomeMessage { get; }
        public ObservableCollection<TableConfig> AvailableTables { get; }
        public ICommand NavigateCommand { get; }
        public ICommand LogoutCommand { get; }

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public TableConfig SelectedTable
        {
            get => _selectedTable;
            set
            {
                _selectedTable = value;
                OnPropertyChanged(nameof(SelectedTable));
                System.Diagnostics.Debug.WriteLine($"Selected table: {_selectedTable?.TableName}");
                if (_selectedTable != null)
                {
                    var tableViewModel = new GenericTableViewModel(); // Создаём экземпляр модели
                    tableViewModel.SelectedTable = _selectedTable;    // Устанавливаем выбранную таблицу
                    var tableView = new GenericTableView();
                    tableView.DataContext = tableViewModel;           // Устанавливаем DataContext
                    CurrentView = tableView;
                }
                else
                {
                    CurrentView = null;
                }
            }
        }

        public MainWindowViewModel(string role)
        {
            WelcomeMessage = $"Добро пожаловать! Ваша роль: {role}";
            AvailableTables = new ObservableCollection<TableConfig>(TableConfigs.GetAll());
            System.Diagnostics.Debug.WriteLine($"Loaded {AvailableTables.Count} tables");
            NavigateCommand = new RelayCommand(Navigate, CanNavigate);
            LogoutCommand = new RelayCommand(Logout, CanLogout);
        }

        private void Navigate(object parameter)
        {
            if (parameter is TableConfig table)
            {
                SelectedTable = table;
            }
        }

        private bool CanNavigate(object parameter) => true;

        private void Logout(object parameter)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            Application.Current.Windows.OfType<MainWindow>().First().Close();
        }

        private bool CanLogout(object parameter) => true;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}