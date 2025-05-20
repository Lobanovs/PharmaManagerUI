using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using PharmaManagerUI.ViewModels;

namespace PharmaManagerUI.Views
{
    public partial class GenericTableView : UserControl
    {
        public GenericTableView()
        {
            InitializeComponent();
            if (DataContext is GenericTableViewModel vm)
            {
                vm.PropertyChanged += Vm_PropertyChanged;
                System.Diagnostics.Debug.WriteLine("GenericTableView initialized with ViewModel");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("GenericTableView: DataContext is not GenericTableViewModel");
            }
        }

        private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(GenericTableViewModel.SelectedTable))
            {
                UpdateColumns();
            }
        }

        private void UpdateColumns()
        {
            if (DataContext is GenericTableViewModel vm && vm.SelectedTable != null)
            {
                dataGrid.Columns.Clear();
                System.Diagnostics.Debug.WriteLine($"Creating columns for {vm.SelectedTable.TableName}");
                foreach (var column in vm.SelectedTable.ColumnMappings)
                {
                    var binding = new Binding($"[{column.Key}]");
                    dataGrid.Columns.Add(new DataGridTextColumn
                    {
                        Header = column.Key,
                        Binding = binding
                    });
                    System.Diagnostics.Debug.WriteLine($"Added column: {column.Key}");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("UpdateColumns: No selected table or invalid DataContext");
            }
        }
    }
}