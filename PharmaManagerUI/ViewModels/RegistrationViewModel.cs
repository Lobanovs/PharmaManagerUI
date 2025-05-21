using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PharmaManagerUI.Commands;
using PharmaManagerUI.Data;
using PharmaManagerUI.Models;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PharmaManagerUI.Helpers;
using PharmaManagerUI.Services;
using PharmaManagerUI.Views;

namespace PharmaManagerUI.ViewModels
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        private string _login;
        private string _password;
        private string _confirmPassword;
        private string _role;
        private readonly IConfiguration _configuration;

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
                ((RelayCommand)RegisterCommand)?.RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                ((RelayCommand)RegisterCommand)?.RaiseCanExecuteChanged();
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
                ((RelayCommand)RegisterCommand)?.RaiseCanExecuteChanged();
            }
        }

        public string Role
        {
            get => _role;
            set
            {
                _role = value;
                OnPropertyChanged(nameof(Role));
                ((RelayCommand)RegisterCommand)?.RaiseCanExecuteChanged();
            }
        }

        public ICommand RegisterCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public RegistrationViewModel()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            RegisterCommand = new RelayCommand(ExecuteRegister, CanExecuteRegister);
        }

        private bool CanExecuteRegister(object parameter)
        {
            bool canExecute = !string.IsNullOrWhiteSpace(Login) &&
                             !string.IsNullOrWhiteSpace(Password) &&
                             Password == ConfirmPassword &&
                             !string.IsNullOrWhiteSpace(Role);

            System.Diagnostics.Debug.WriteLine($"CanExecuteRegister: {canExecute}");
            System.Diagnostics.Debug.WriteLine($"Login: '{Login}', Password: '{Password}', ConfirmPassword: '{ConfirmPassword}', Role: '{Role}'");

            return canExecute;
        }

        private async void ExecuteRegister(object parameter)
        {
            try
            {
                using var ctx = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
                    .UseSqlServer(_configuration.GetConnectionString("PharmaManagerDB")).Options);
                if (await ctx.Users.AnyAsync(u => u.Login == Login))
                {
                    MessageBox.Show("Пользователь с таким логином уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var (hash, salt) = PasswordService.HashPassword(Password);
                var newUser = new User
                {
                    Login = Login,
                    PasswordHash = hash,
                    Salt = salt,
                    Role = Role
                };
                ctx.Users.Add(newUser);
                await ctx.SaveChangesAsync();
                MessageBox.Show("Регистрация прошла успешно.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                var loginWindow = new LoginWindow();
                loginWindow.Show();
                Application.Current.Windows.OfType<RegistrationWindow>().First().Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Registration error: {ex.Message}");
                MessageBox.Show("Ошибка при регистрации: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}