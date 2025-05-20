using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PharmaManagerUI.Data;
using PharmaManagerUI.Views;
using System.IO;
using System.Windows;

namespace PharmaManagerUI
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory) // более надёжно
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("PharmaManagerDB");

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionString); // вот тут точно подключаем!

            using (var context = new AppDbContext(optionsBuilder.Options))
            {
                context.Database.EnsureCreated();
            }

            var loginWindow = new LoginWindow();
            loginWindow.Show();
        }

    }
}