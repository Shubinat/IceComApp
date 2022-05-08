using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IceComApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для WarehousesHistoryPage.xaml
    /// </summary>
    public partial class WarehousesHistoryPage : Page
    {
        public WarehousesHistoryPage()
        {
            InitializeComponent();
            List<Entities.Warehouse> warehouses = App.Context.Warehouses.ToList();
            warehouses.Insert(0, new Entities.Warehouse() { Name = "Все склады" });
            CBoxWarehouses.ItemsSource = warehouses;
            CBoxWarehouses.SelectedIndex = 0;
            if(App.AuthUser.RoleID == 1)
                BtnAddPurchase.Visibility = Visibility.Collapsed;
            else if (App.AuthUser.RoleID == 2)
                BtnAddSale.Visibility = Visibility.Collapsed;
            else
                BtnStatistics.Visibility = Visibility.Visible;
        }

        private void UpdateGrid()
        {
            var warehouseProducts = App.Context.ProductHistories.ToList();

            if (CBoxWarehouses.SelectedIndex != 0)
                warehouseProducts = warehouseProducts.Where(x => x.Warehouse == CBoxWarehouses.SelectedItem).ToList();


            if (!string.IsNullOrWhiteSpace(TBoxSearch.Text))
                warehouseProducts = warehouseProducts.Where(x => x.Product.Name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            warehouseProducts = warehouseProducts
                .Where(x => x.DateTime == warehouseProducts.Where(y => y.Product == x.Product && y.Warehouse == x.Warehouse).OrderBy(y => y.DateTime).LastOrDefault().DateTime)
                .OrderByDescending(x => x.Amount).ToList();
            DGridWarehouseProducts.ItemsSource = warehouseProducts;
        }

        private void BtnAddSale_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddProductSalePage());
        }

        private void CBoxWarehouses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateGrid();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateGrid();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateGrid();
        }

        private void BtnAddPurchase_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddProductPurchasePage());
        }

        private void BtnStatistics_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
