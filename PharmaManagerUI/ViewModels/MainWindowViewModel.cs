using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PharmaManagerUI.Data;
using PharmaManagerUI.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PharmaManagerUI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IConfiguration _configuration;
        public ObservableCollection<Client> Clients { get; set; }
        public ObservableCollection<Drug> Drugs { get; set; }
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<RawMaterial> RawMaterials { get; set; }
        public ObservableCollection<Equipment> Equipment { get; set; }
        public ObservableCollection<ProductionOrder> ProductionOrders { get; set; }
        public ObservableCollection<Staff> Staff { get; set; }
        public ObservableCollection<QualityControl> QualityControls { get; set; }
        public ObservableCollection<Warehouse> Warehouses { get; set; }
        public ObservableCollection<Logistic> Logistics { get; set; }
        public ObservableCollection<PharmacyNetwork> PharmacyNetworks { get; set; }
        public ObservableCollection<Sale> Sales { get; set; }

        private string _filterText;
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

        public ICollectionView ClientsView { get; set; }
        public ICollectionView DrugsView { get; set; }
        public ICollectionView UsersView { get; set; }
        public ICollectionView RawMaterialsView { get; set; }
        public ICollectionView EquipmentView { get; set; }
        public ICollectionView ProductionOrdersView { get; set; }
        public ICollectionView StaffView { get; set; }
        public ICollectionView QualityControlsView { get; set; }
        public ICollectionView WarehousesView { get; set; }
        public ICollectionView LogisticsView { get; set; }
        public ICollectionView PharmacyNetworksView { get; set; }
        public ICollectionView SalesView { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(System.AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            Clients = new ObservableCollection<Client>();
            Drugs = new ObservableCollection<Drug>();
            Users = new ObservableCollection<User>();
            RawMaterials = new ObservableCollection<RawMaterial>();
            Equipment = new ObservableCollection<Equipment>();
            ProductionOrders = new ObservableCollection<ProductionOrder>();
            Staff = new ObservableCollection<Staff>();
            QualityControls = new ObservableCollection<QualityControl>();
            Warehouses = new ObservableCollection<Warehouse>();
            Logistics = new ObservableCollection<Logistic>();
            PharmacyNetworks = new ObservableCollection<PharmacyNetwork>();
            Sales = new ObservableCollection<Sale>();

            ClientsView = CollectionViewSource.GetDefaultView(Clients);
            DrugsView = CollectionViewSource.GetDefaultView(Drugs);
            UsersView = CollectionViewSource.GetDefaultView(Users);
            RawMaterialsView = CollectionViewSource.GetDefaultView(RawMaterials);
            EquipmentView = CollectionViewSource.GetDefaultView(Equipment);
            ProductionOrdersView = CollectionViewSource.GetDefaultView(ProductionOrders);
            StaffView = CollectionViewSource.GetDefaultView(Staff);
            QualityControlsView = CollectionViewSource.GetDefaultView(QualityControls);
            WarehousesView = CollectionViewSource.GetDefaultView(Warehouses);
            LogisticsView = CollectionViewSource.GetDefaultView(Logistics);
            PharmacyNetworksView = CollectionViewSource.GetDefaultView(PharmacyNetworks);
            SalesView = CollectionViewSource.GetDefaultView(Sales);

            ClientsView.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));
            DrugsView.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));
            UsersView.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));
            RawMaterialsView.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));
            EquipmentView.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));
            ProductionOrdersView.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));
            StaffView.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));
            QualityControlsView.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));
            WarehousesView.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));
            LogisticsView.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));
            PharmacyNetworksView.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));
            SalesView.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));

            LoadDataAsync().ConfigureAwait(false);
        }

        private async Task LoadDataAsync()
        {
            try
            {
                using var ctx = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
                    .UseSqlServer(_configuration.GetConnectionString("PharmaManagerDB")).Options);

                var clients = await ctx.Clients
                    .Include(c => c.PharmacyNetwork)
                    .ToListAsync();
                var drugs = await ctx.Drugs
                    .Include(d => d.Manufacturer)
                    .ToListAsync();
                var users = await ctx.Users.ToListAsync();
                var rawMaterials = await ctx.RawMaterials.ToListAsync();
                var equipment = await ctx.Equipment.ToListAsync();
                var productionOrders = await ctx.ProductionOrders
                    .Include(po => po.Staff)
                    .Include(po => po.Equipment)
                    .ToListAsync();
                var staff = await ctx.Staff.ToListAsync();
                var qualityControls = await ctx.QualityControls.ToListAsync();
                var warehouses = await ctx.Warehouses.ToListAsync();
                var logistics = await ctx.Logistics.ToListAsync();
                var pharmacyNetworks = await ctx.PharmacyNetworks.ToListAsync();
                var sales = await ctx.Sales.ToListAsync();

                Clients.Clear();
                Drugs.Clear();
                Users.Clear();
                RawMaterials.Clear();
                Equipment.Clear();
                ProductionOrders.Clear();
                Staff.Clear();
                QualityControls.Clear();
                Warehouses.Clear();
                Logistics.Clear();
                PharmacyNetworks.Clear();
                Sales.Clear();

                foreach (var client in clients)
                {
                    client.PharmacyNetworkName = client.PharmacyNetwork?.Name ?? "Не указана";
                    Clients.Add(client);
                }
                foreach (var drug in drugs)
                {
                    drug.ManufacturerName = drug.Manufacturer?.Name ?? "Не указан";
                    Drugs.Add(drug);
                }
                foreach (var user in users) Users.Add(user);
                foreach (var rawMaterial in rawMaterials) RawMaterials.Add(rawMaterial);
                foreach (var equip in equipment) Equipment.Add(equip);
                foreach (var order in productionOrders)
                {
                    order.StaffName = order.Staff?.Name ?? "Не указан";
                    order.EquipmentName = order.Equipment?.Name ?? "Не указано";
                    ProductionOrders.Add(order);
                }
                foreach (var staffMember in staff) Staff.Add(staffMember);
                foreach (var qc in qualityControls) QualityControls.Add(qc);
                foreach (var warehouse in warehouses) Warehouses.Add(warehouse);
                foreach (var logistic in logistics) Logistics.Add(logistic);
                foreach (var network in pharmacyNetworks) PharmacyNetworks.Add(network);
                foreach (var sale in sales) Sales.Add(sale);

                System.Diagnostics.Debug.WriteLine($"Loaded: Clients={Clients.Count}, Drugs={Drugs.Count}, Users={Users.Count}, RawMaterials={RawMaterials.Count}, Equipment={Equipment.Count}, ProductionOrders={ProductionOrders.Count}, Staff={Staff.Count}, QualityControls={QualityControls.Count}, Warehouses={Warehouses.Count}, Logistics={Logistics.Count}, PharmacyNetworks={PharmacyNetworks.Count}, Sales={Sales.Count}");

                ApplyFilter();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading data: {ex.Message}");
            }
        }

        private void ApplyFilter()
        {
            if (string.IsNullOrWhiteSpace(FilterText))
            {
                ClientsView.Filter = null;
                DrugsView.Filter = null;
                UsersView.Filter = null;
                RawMaterialsView.Filter = null;
                EquipmentView.Filter = null;
                ProductionOrdersView.Filter = null;
                StaffView.Filter = null;
                QualityControlsView.Filter = null;
                WarehousesView.Filter = null;
                LogisticsView.Filter = null;
                PharmacyNetworksView.Filter = null;
                SalesView.Filter = null;
            }
            else
            {
                var filterText = FilterText.ToLower();
                ClientsView.Filter = item => (item as Client)?.Name?.ToLower().Contains(filterText) == true || (item as Client)?.PharmacyNetworkName?.ToLower().Contains(filterText) == true;
                DrugsView.Filter = item => (item as Drug)?.Name?.ToLower().Contains(filterText) == true || (item as Drug)?.ManufacturerName?.ToLower().Contains(filterText) == true;
                UsersView.Filter = item => (item as User)?.Login?.ToLower().Contains(filterText) == true || (item as User)?.Role?.ToLower().Contains(filterText) == true;
                RawMaterialsView.Filter = item => (item as RawMaterial)?.Name?.ToLower().Contains(filterText) == true;
                EquipmentView.Filter = item => (item as Equipment)?.Name?.ToLower().Contains(filterText) == true;
                ProductionOrdersView.Filter = item => (item as ProductionOrder)?.StaffName?.ToLower().Contains(filterText) == true || (item as ProductionOrder)?.EquipmentName?.ToLower().Contains(filterText) == true;
                StaffView.Filter = item => (item as Staff)?.Name?.ToLower().Contains(filterText) == true;
                QualityControlsView.Filter = item => (item as QualityControl)?.Result?.ToLower().Contains(filterText) == true; // Предполагаемое свойство
                WarehousesView.Filter = item => (item as Warehouse)?.Location?.ToLower().Contains(filterText) == true; // Предполагаемое свойство
                LogisticsView.Filter = item => (item as Logistic)?.Status?.ToLower().Contains(filterText) == true; // Предполагаемое свойство
                PharmacyNetworksView.Filter = item => (item as PharmacyNetwork)?.Name?.ToLower().Contains(filterText) == true;
                SalesView.Filter = item => (item as Sale)?.Amount?.ToString()?.ToLower().Contains(filterText) == true; // Предполагаемое свойство
            }
            ClientsView.Refresh();
            DrugsView.Refresh();
            UsersView.Refresh();
            RawMaterialsView.Refresh();
            EquipmentView.Refresh();
            ProductionOrdersView.Refresh();
            StaffView.Refresh();
            QualityControlsView.Refresh();
            WarehousesView.Refresh();
            LogisticsView.Refresh();
            PharmacyNetworksView.Refresh();
            SalesView.Refresh();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}