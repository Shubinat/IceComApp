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
    /// Логика взаимодействия для ProductStatisticsPage.xaml
    /// </summary>
    public partial class ProductStatisticsPage : Page
    {
        public ProductStatisticsPage()
        {
            InitializeComponent();

            CBoxWarehouses.ItemsSource = App.Context.Warehouses.ToList();
            CBoxWarehouses.SelectedIndex = 0;
        }

        private void UpdateStats()
        {
            var historyByProducts = App.Context.ProductHistories.ToList().Where(x => x.Warehouse == CBoxWarehouses.SelectedItem).GroupBy(x => x.Product);
            Statistics.Series.Clear();
            foreach (var historyByProduct in historyByProducts)
            {
                var currSeries = Statistics.Series.Add(historyByProduct.Key.Name);
                currSeries.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                currSeries.BorderWidth = 5;
                foreach (var historyElementByProduct in historyByProduct.OrderBy(x => x.DateTime))
                {
                    currSeries.Points.AddXY(historyElementByProduct.DateTime, historyElementByProduct.Amount);
                }
            }
        }

        private void CBoxWarehouses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateStats();
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog dialog = new PrintDialog();
            if(dialog.ShowDialog() == true)
            {
                dialog.PrintVisual(GridStats, $"Статистика по складу {(CBoxWarehouses.SelectedItem as Warehouse).Name}");
                NavigationService.GoBack();
            }
        }
    }
}
