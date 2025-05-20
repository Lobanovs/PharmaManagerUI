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
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
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
            LoginCommand = new RelayCommand(async _ => await ExecuteLoginAsync(), _ => CanExecuteLogin());
        }

        private bool CanExecuteLogin()
        {
            return !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password);
        }

        private async Task ExecuteLoginAsync()
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("PharmaManagerDB"));
                using var ctx = new AppDbContext(optionsBuilder.Options);

                // Предполагаем, что contact_info содержит логин:пароль (нужно уточнить реальную логику)
                var client = await ctx.Clients.FirstOrDefaultAsync(c => c.ContactInfo.Contains(Login + ":" + Password));
                if (client != null)
                {
                    var mainWindow = new MainWindow(client.Name); // Передаём роль или имя клиента
                    mainWindow.Show();
                    Application.Current.Windows.OfType<LoginWindow>().First().Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Login error: {ex.Message}");
                MessageBox.Show("Ошибка подключения к базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}