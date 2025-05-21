using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PharmaManagerUI.Commands;
using PharmaManagerUI.Data;
using PharmaManagerUI.Models;
using PharmaManagerUI.Views;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PharmaManagerUI.Helpers;
using PharmaManagerUI.Services;

namespace PharmaManagerUI.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _login;
        private string _password;
        private readonly IConfiguration _configuration;

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
                ((RelayCommand)LoginCommand).RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                ((RelayCommand)LoginCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand LoginCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public LoginViewModel()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
        }

        private bool CanExecuteLogin(object parameter)
        {
            bool canExecute = !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password);
            System.Diagnostics.Debug.WriteLine($"CanExecuteLogin: {canExecute}, Login: '{Login}', Password: '{Password}'");
            return canExecute;
        }

        private async void ExecuteLogin(object parameter)
        {
            try
            {
                using var ctx = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
                    .UseSqlServer(_configuration.GetConnectionString("PharmaManagerDB")).Options);
                System.Diagnostics.Debug.WriteLine($"Attempting login with Login: '{Login}'");
                var user = await ctx.Users.FirstOrDefaultAsync(u => u.Login == Login);
                if (user != null && PasswordService.VerifyPassword(Password, user.PasswordHash, user.Salt))
                {
                    System.Diagnostics.Debug.WriteLine($"Login successful for user: {user.Login}");
                    var mainWindow = new MainWindow(user.Role);
                    mainWindow.Show();
                    Application.Current.Windows.OfType<LoginWindow>().First().Close();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Login failed: User not found or password incorrect.");
                    MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Login error: {ex.Message}");
                MessageBox.Show("Ошибка подключения к базе данных: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}