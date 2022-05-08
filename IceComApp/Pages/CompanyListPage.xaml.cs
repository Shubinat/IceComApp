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
    /// Логика взаимодействия для CompanyListPage.xaml
    /// </summary>
    public partial class CompanyListPage : Page
    {
        public CompanyListPage()
        {
            InitializeComponent();
            CBoxSort.ItemsSource = new[] { "Без сортировки", "От А до Я", "От Я до А" };
            CBoxSort.SelectedIndex = 0;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить этого производителей?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Question)
                == MessageBoxResult.Yes)
            {
                var currShop = LViewCompanies.SelectedItem as Company;
                App.Context.Companies.Remove(currShop);
                App.Context.SaveChanges();
                UpdateList();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CompanyEditorPage(LViewCompanies.SelectedItem as Company));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CompanyEditorPage());
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList();
        }

        private void UpdateList()
        {
            List<Entities.Company> companies = App.Context.Companies.ToList();

            if (!string.IsNullOrWhiteSpace(TBoxSearch.Text))
                companies = companies.Where(x => x.FullName.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            switch (CBoxSort.SelectedIndex)
            {
                case 1:
                    companies = companies.OrderBy(x => x.Name).ToList();
                    break;
                case 2:
                    companies = companies.OrderByDescending(x => x.Name).ToList();
                    break;
                default:
                    break;
            }

            LViewCompanies.ItemsSource = companies;
        }

        private void CBoxSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }

        private void LViewCompanies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var value = LViewCompanies.SelectedItem != null;
            BtnEdit.IsEnabled = value;
            BtnDelete.IsEnabled = value;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateList();
        }
    }
}
