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

namespace IceComApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductSalesPage.xaml
    /// </summary>
    public partial class ProductSalesPage : Page
    {
        public ProductSalesPage()
        {
            InitializeComponent();
        }

        private void UpdateGrid()
        {
            List<Entities.ProductSale> productSales = App.Context.ProductSales.ToList();

            if (DPickerStart.SelectedDate != null)
                productSales = productSales.Where(x => x.DateTime.Date >= DPickerStart.SelectedDate.Value.Date).ToList();

            if (DPickerEnd.SelectedDate != null)
                productSales = productSales.Where(x => x.DateTime.Date <= DPickerEnd.SelectedDate.Value.Date).ToList();

            if (!string.IsNullOrWhiteSpace(TBoxSearch.Text))
                productSales = productSales.Where(x => x.User.FullName.ToLower().Contains(TBoxSearch.Text.ToLower()) ||
                x.Product.Name.ToLower().Contains(TBoxSearch.Text.ToLower()) ||
                x.Shop.Name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            DGridProductsSales.ItemsSource = productSales.OrderBy(x => x.DateTime).ToList();
        }
        private void BtnReport_Click(object sender, RoutedEventArgs e)
        {
            List<ProductSale> data = DGridProductsSales.ItemsSource as List<ProductSale>;
            using (ExcelWriter writer = new ExcelWriter())
            {
                writer.Sheet.Name = $"Отчет на {DateTime.Now.ToShortDateString()}";
                writer.CreateHeaders(new[] { "Менеджер", "Магазин", "Дата", "Время", "Продукция", "Цена", "Количество", "Сумма" },
                    Microsoft.Office.Interop.Excel.XlRgbColor.rgbOrangeRed, Microsoft.Office.Interop.Excel.XlRgbColor.rgbWhite);
                foreach (var item in data)
                {
                    writer.CreateRow(new[] { item.User.FullName, item.Shop.Name,
                        item.DateTime.ToShortDateString(), item.DateTime.ToShortTimeString(),
                    item.Product.Name, item.Product.Price.ToString(),item.Amount.ToString(), $"=F{writer.RowIndex}*G{writer.RowIndex}" });
                }
                writer.CreateSum("ИТОГО:", 8, $"=SUM(H1:H{writer.RowIndex - 1})");
            }
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

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateGrid();
        }

        private void BtnAddSale_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
