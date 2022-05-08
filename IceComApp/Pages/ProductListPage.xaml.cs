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
    /// Логика взаимодействия для ProductListPage.xaml
    /// </summary>
    public partial class ProductListPage : Page
    {
        public ProductListPage()
        {
            InitializeComponent();
            if(App.AuthUser.RoleID != 2)
            {
                BtnAdd.Visibility = Visibility.Collapsed;
                BtnDelete.Visibility = Visibility.Collapsed;
                BtnEdit.Visibility = Visibility.Collapsed;
            }
            CBoxSort.ItemsSource = new[] { "Без сортировки", "От А до Я", "От Я до А" };
            CBoxSort.SelectedIndex = 0;
            List<Entities.ProductType> productTypes = App.Context.ProductTypes.ToList();
            productTypes.Insert(0, new Entities.ProductType() { Name = "Все типы продуктов" });
            CBoxProductTypes.ItemsSource = productTypes;
            CBoxProductTypes.SelectedIndex = 0;
        }

        private void UpdateList()
        {
            List<Entities.Product> products = App.Context.Products.ToList();

            if(CBoxProductTypes.SelectedIndex != 0)
                products = products.Where(x => x.ProductType == CBoxProductTypes.SelectedItem).ToList();

            if (!string.IsNullOrWhiteSpace(TBoxSearch.Text))
                products = products.Where(x => x.Name.ToLower().Contains(TBoxSearch.Text.ToLower()) ||
                x.Company.FullName.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            switch (CBoxSort.SelectedIndex)
            {
                case 1:
                    products = products.OrderBy(x => x.Name).ToList();
                    break;
                case 2:
                    products = products.OrderByDescending(x => x.Name).ToList();
                    break;
                default:
                    break;
            }
            LViewProducts.ItemsSource = products;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList();
        }

        private void CBoxSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }

        private void LViewProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var value = LViewProducts.SelectedItem != null;
            BtnDelete.IsEnabled = value;
            BtnEdit.IsEnabled = value;
        }

        private void CBoxProductTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateList();
        }
    }
}
