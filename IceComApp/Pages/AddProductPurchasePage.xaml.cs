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
    /// Логика взаимодействия для AddProductPurchasePage.xaml
    /// </summary>
    public partial class AddProductPurchasePage : Page
    {
        public AddProductPurchasePage()
        {
            InitializeComponent();
            CBoxProduct.ItemsSource = App.Context.Products.ToList();
            CBoxWarehouse.ItemsSource = App.Context.Warehouses.ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var err = "";
            if (CBoxProduct.SelectedItem == null) err += "Выберите продукцию\n";
            if (CBoxWarehouse.SelectedItem == null) err += "Выберите склад\n";
            if (!double.TryParse(TBoxAmount.Text, out double amount)) err += "Проверьте правильность заполнения количества";

            if(err == "")
            {
                App.Context.ProductPurchases.Add(new Entities.ProductPurchase()
                {
                    Amount = amount,
                    Product = CBoxProduct.SelectedItem as Entities.Product,
                    DateTime = DateTime.Now,
                    User = App.AuthUser,
                    Warehouse = CBoxWarehouse.SelectedItem as Entities.Warehouse,
                });
                var lastAmount = App.Context.ProductHistories.ToList().Where(x => x.Product == CBoxProduct.SelectedItem && x.Warehouse == CBoxWarehouse.SelectedItem)?.OrderBy(x => x.DateTime).LastOrDefault()?.Amount ?? 0;
                App.Context.ProductHistories.Add(new Entities.ProductHistory()
                {
                    Amount = lastAmount + amount,
                    Product = CBoxProduct.SelectedItem as Entities.Product,
                    DateTime = DateTime.Now,
                    Warehouse = CBoxWarehouse.SelectedItem as Entities.Warehouse,
                });
                App.Context.SaveChanges();
                NavigationService.GoBack();
            }
            else
            {
                Message.ShowError(err);
            }
        }
    }
}
