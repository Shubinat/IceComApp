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
            BtnShopSales.Visibility = Visibility.Collapsed;

            switch (App.AuthUser.RoleID)
            {
                case 1:
                    BtnProducts.Visibility = Visibility.Visible;
                    BtnShops.Visibility = Visibility.Visible;
                    BtnShopSales.Visibility = Visibility.Visible;
                    break;
                case 2:
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

        private void BtnShopSales_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
