using IceComApp.Entities;
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
    /// Логика взаимодействия для ShopListPage.xaml
    /// </summary>
    public partial class ShopListPage : Page
    {
        public ShopListPage()
        {
            InitializeComponent();
            CBoxSort.ItemsSource = new[] { "Без сортировки", "От А до Я", "От Я до А"};
            CBoxSort.SelectedIndex = 0;
        }

        private void UpdateList()
        {
            List<Entities.Shop> shops = App.Context.Shops.ToList();
            
            if(!string.IsNullOrWhiteSpace(TBoxSearch.Text))
                shops = shops.Where(x => x.Name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            switch (CBoxSort.SelectedIndex)
            {
                case 1:
                    shops = shops.OrderBy(x => x.Name).ToList();
                    break;
                case 2:
                    shops = shops.OrderByDescending(x => x.Name).ToList();
                    break;
                default:
                    break;
            }

            LViewShops.ItemsSource = shops;
        }

        private void LViewShops_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var value = LViewShops.SelectedItem != null;
            BtnEdit.IsEnabled = value;
            BtnDelete.IsEnabled = value;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Вы действительно хотите удалить этот магазин?", "Сообщение", MessageBoxButton.YesNo,MessageBoxImage.Question)
                == MessageBoxResult.Yes)
            {
                var currShop = LViewShops.SelectedItem as Shop;
                App.Context.Shops.Remove(currShop);
                App.Context.SaveChanges();
                UpdateList();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ShopEditorPage(LViewShops.SelectedItem as Shop));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ShopEditorPage(null));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateList();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList();
        }

        private void CBoxSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }
    }
}
