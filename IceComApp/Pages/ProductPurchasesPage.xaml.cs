using IceComApp.Entities;
using IceComApp.Utils;
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
using Excel = Microsoft.Office.Interop.Excel;

namespace IceComApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductPurchasesPage.xaml
    /// </summary>
    public partial class ProductPurchasesPage : Page
    {
        public ProductPurchasesPage()
        {
            InitializeComponent();
        }

        private void UpdateGrid()
        {
            List<Entities.ProductPurchase> productPurchases = App.Context.ProductPurchases.ToList();

            if (DPickerStart.SelectedDate != null)
                productPurchases = productPurchases.Where(x => x.DateTime.Date >= DPickerStart.SelectedDate.Value.Date).ToList();

            if (DPickerEnd.SelectedDate != null)
                productPurchases = productPurchases.Where(x => x.DateTime.Date <= DPickerEnd.SelectedDate.Value.Date).ToList();

            if (!string.IsNullOrWhiteSpace(TBoxSearch.Text))
                productPurchases = productPurchases.Where(x => x.User.FullName.ToLower().Contains(TBoxSearch.Text.ToLower()) ||
                x.Product.Name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            DGridProductsPurchases.ItemsSource = productPurchases.OrderBy(x => x.DateTime).ToList();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateGrid();
        }

        private void BtnReport_Click(object sender, RoutedEventArgs e)
        {
            List<ProductPurchase> data = DGridProductsPurchases.ItemsSource as List<ProductPurchase>;
            using (ExcelWriter writer = new ExcelWriter())
            {
                writer.Sheet.Name = $"Отчет на {DateTime.Now.ToShortDateString()}";
                writer.CreateHeaders(new[] { "Менеджер", "Склад", "Дата", "Время", "Продукция", "Цена", "Количество", "Сумма" },
                    Excel.XlRgbColor.rgbOrangeRed, Excel.XlRgbColor.rgbWhite);
                foreach (var item in data)
                {
                    writer.CreateRow(new[] { item.User.FullName, item.Warehouse.Name,
                        item.DateTime.ToShortDateString(), item.DateTime.ToShortTimeString(),
                    item.Product.Name, item.Product.Price.ToString(),item.Amount.ToString(), $"=F{writer.RowIndex}*G{writer.RowIndex}" });
                }
                writer.CreateSum("ИТОГО:", 8, $"=SUM(H1:H{writer.RowIndex - 1})");
            }
        }


        private void BtnAddPurchase_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddProductPurchasePage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateGrid();
        }

        private void DPickerStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateGrid();
        }

        private void DPickerEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateGrid();
        }
    }
}
