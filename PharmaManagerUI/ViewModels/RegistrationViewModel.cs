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
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        private string _name;
        private string _address;
        private string _contactInfo;
        private readonly IConfiguration _configuration;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        public string ContactInfo
        {
            get => _contactInfo;
            set
            {
                _contactInfo = value;
                OnPropertyChanged(nameof(ContactInfo));
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
            RegisterCommand = new RelayCommand(async _ => await ExecuteRegisterAsync(), _ => CanExecuteRegister());
        }

        private bool CanExecuteRegister()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Address) && !string.IsNullOrWhiteSpace(ContactInfo);
        }

        private async Task ExecuteRegisterAsync()
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("PharmaManagerDB"));
                using var ctx = new AppDbContext(optionsBuilder.Options);

                var newClient = new Client
                {
                    Name = Name,
                    Address = Address,
                    ContactInfo = ContactInfo // Здесь можно добавить логику для пароля, если нужно
                };
                ctx.Clients.Add(newClient);
                await ctx.SaveChangesAsync();
                MessageBox.Show("Регистрация прошла успешно.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                var loginWindow = new LoginWindow();
                loginWindow.Show();
                Application.Current.Windows.OfType<RegistrationWindow>().First().Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Registration error: {ex.Message}");
                MessageBox.Show("Ошибка при регистрации.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}