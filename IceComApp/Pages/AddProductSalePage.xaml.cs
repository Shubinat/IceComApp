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
    /// Логика взаимодействия для AddProductSalePage.xaml
    /// </summary>
    public partial class AddProductSalePage : Page
    {
        public AddProductSalePage()
        {
            InitializeComponent();
            CBoxProduct.ItemsSource = App.Context.Products.ToList();
            CBoxWarehouse.ItemsSource = App.Context.Warehouses.ToList();
            CBoxShop.ItemsSource = App.Context.Shops.ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var err = "";
            if (CBoxProduct.SelectedItem == null) err += "Выберите продукцию\n";
            if (CBoxWarehouse.SelectedItem == null) err += "Выберите склад\n";
            if (CBoxShop.SelectedItem == null) err += "Выберите магазин\n";
            if (!double.TryParse(TBoxAmount.Text, out double amount)) err += "Проверьте правильность заполнения количества";

            if (err == "")
            {
                var lastAmount = App.Context.ProductHistories.ToList().Where(x => x.Product == CBoxProduct.SelectedItem && x.Warehouse == CBoxWarehouse.SelectedItem)?.OrderBy(x => x.DateTime).LastOrDefault()?.Amount ?? 0;
                var newAmount = lastAmount - amount;
                if(newAmount >= 0)
                {
                    App.Context.ProductHistories.Add(new Entities.ProductHistory()
                    {
                        Amount = lastAmount - amount,
                        Product = CBoxProduct.SelectedItem as Entities.Product,
                        DateTime = DateTime.Now,
                        Warehouse = CBoxWarehouse.SelectedItem as Entities.Warehouse,
                    });
                    App.Context.ProductSales.Add(new Entities.ProductSale()
                    {
                        Amount = amount,
                        Product = CBoxProduct.SelectedItem as Entities.Product,
                        DateTime = DateTime.Now,
                        User = App.AuthUser,
                        Shop = CBoxShop.SelectedItem as Entities.Shop,
                        Warehouse = CBoxWarehouse.SelectedItem as Entities.Warehouse,
                    });
                    App.Context.SaveChanges();
                    NavigationService.GoBack();
                }
                else
                {
                    Message.ShowError($"На данном складе нет столько продукции.\nНе хватает {Math.Abs(newAmount)} {(CBoxProduct.SelectedItem as Product).Unit.ShortName}");
                }
            }
            else
            {
                Message.ShowError(err);
            }
        }
    }
}
