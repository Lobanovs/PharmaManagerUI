using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using PharmaManagerUI.Data;
using PharmaManagerUI.Helpers;

namespace PharmaManagerUI.ViewModels
{
    public class GenericTableViewModel : INotifyPropertyChanged
    {
        private TableConfig _selectedTable;
        private string _filterText;
        private readonly IConfiguration _configuration;

        public ObservableCollection<TableConfig> AvailableTables { get; }
        public ObservableCollection<Dictionary<string, object>> DisplayRows { get; }
        public ICollectionView DisplayRowsView { get; }

        public TableConfig SelectedTable
        {
            get => _selectedTable;
            set
            {
                _selectedTable = value;
                System.Diagnostics.Debug.WriteLine($"SelectedTable set to: {_selectedTable?.TableName}");
                // Асинхронный вызов без блокировки UI-потока
                _ = LoadDataAsync(); // Запускаем асинхронно, не ждём результат здесь
                OnPropertyChanged(nameof(SelectedTable));
            }
        }

        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                OnPropertyChanged(nameof(FilterText));
                ApplyFilter();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public GenericTableViewModel()
        {
            try
            {
                _configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();
                System.Diagnostics.Debug.WriteLine("Configuration loaded successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to load configuration: {ex.Message}");
            }

            try
            {
                AvailableTables = new ObservableCollection<TableConfig>(TableConfigs.GetAll());
                System.Diagnostics.Debug.WriteLine($"Loaded {AvailableTables.Count} table configs");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to load table configs: {ex.Message}");
            }

            DisplayRows = new ObservableCollection<Dictionary<string, object>>();
            DisplayRowsView = CollectionViewSource.GetDefaultView(DisplayRows);
            DisplayRowsView.Filter = FilterPredicate;
            System.Diagnostics.Debug.WriteLine("DisplayRows and DisplayRowsView initialized");
        }

        private async Task LoadDataAsync()
        {
            DisplayRows.Clear();
            System.Diagnostics.Debug.WriteLine("Cleared DisplayRows");

            if (SelectedTable == null)
            {
                System.Diagnostics.Debug.WriteLine("No table selected");
                return;
            }

            try
            {
                System.Diagnostics.Debug.WriteLine($"Loading data for table: {SelectedTable.TableName}");
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                var connectionString = _configuration.GetConnectionString("PharmaManagerDB");
                System.Diagnostics.Debug.WriteLine($"Connection string: {connectionString}");
                optionsBuilder.UseSqlServer(connectionString);

                using (var ctx = new AppDbContext(optionsBuilder.Options))
                {
                    System.Diagnostics.Debug.WriteLine("AppDbContext created");
                    var data = await SelectedTable.DataSelector(ctx).ToListAsync();
                    System.Diagnostics.Debug.WriteLine($"Loaded {data.Count} rows for {SelectedTable.TableName}");

                    if (data.Count == 0)
                    {
                        System.Diagnostics.Debug.WriteLine("No data returned from database");
                    }

                    foreach (var row in data)
                    {
                        var displayRow = new Dictionary<string, object>();
                        foreach (var mapping in SelectedTable.ColumnMappings)
                        {
                            try
                            {
                                var value = mapping.Value(row);
                                displayRow[mapping.Key] = value;
                                System.Diagnostics.Debug.WriteLine($"Mapped column '{mapping.Key}' with value: {value}");
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"Error mapping column '{mapping.Key}': {ex.Message}");
                            }
                        }
                        DisplayRows.Add(displayRow);
                        System.Diagnostics.Debug.WriteLine($"Added row to DisplayRows: {string.Join(", ", displayRow.Select(kv => $"{kv.Key}: {kv.Value}"))}");
                    }

                    System.Diagnostics.Debug.WriteLine($"Total rows in DisplayRows: {DisplayRows.Count}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading data: {ex.Message}");
            }
        }

        private void ApplyFilter()
        {
            DisplayRowsView.Refresh();
            System.Diagnostics.Debug.WriteLine($"Filter applied: {FilterText}");
        }

        private bool FilterPredicate(object item)
        {
            if (string.IsNullOrEmpty(FilterText)) return true;
            var dict = item as Dictionary<string, object>;
            return dict?.Values.Any(v => v?.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) == true) ?? false;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            System.Diagnostics.Debug.WriteLine($"Property changed: {propertyName}");
        }
    }
}