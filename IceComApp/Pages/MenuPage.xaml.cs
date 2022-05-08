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
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
            
            BtnProducts.Visibility = Visibility.Collapsed;
            BtnShops.Visibility = Visibility.Collapsed;
            BtnProductSales.Visibility = Visibility.Collapsed;
            BtnProductPurchases.Visibility = Visibility.Collapsed;
            BtnCompanies.Visibility = Visibility.Collapsed;

            switch (App.AuthUser.RoleID)
            {
                case 1:
                    BtnProducts.Visibility = Visibility.Visible;
                    BtnShops.Visibility = Visibility.Visible;
                    BtnProductSales.Visibility = Visibility.Visible;
                    break;
                case 2:
                    BtnProducts.Visibility = Visibility.Visible;
                    BtnProductPurchases.Visibility= Visibility.Visible;
                    BtnCompanies.Visibility= Visibility.Visible;
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }

        private void BtnProducts_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductListPage());
        }

        private void BtnShops_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ShopListPage());
        }
        private void BtnCompanies_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CompanyListPage());
        }

        private void BtnProductSales_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductSalesPage());
        }

        private void BtnProductPurchases_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductPurchasesPage());
        }
    }
}
