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
    /// Логика взаимодействия для ShopEditorPage.xaml
    /// </summary>
    public partial class ShopEditorPage : Page
    {
        private Shop _shop;
        public ShopEditorPage(Shop shop)
        {
            InitializeComponent();
            _shop = shop;
            if(_shop != null)
            {
                TBoxName.Text = _shop.Name;
                TBoxINN.Text = _shop.INN;
                TBoxDescription.Text = _shop.Description;
                Title = "Редактирование магазина";
                TBoxTitle.Text = "Редактирование магазина";
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var err = "";
            if (string.IsNullOrWhiteSpace(TBoxName.Text)) err += "Заполните поле Название\n";
            if (string.IsNullOrWhiteSpace(TBoxINN.Text)) err += "Заполните поле ИНН";
            

            if(err == "")
            {
                if (_shop == null)
                    _shop = new Shop();

                _shop.Name = TBoxName.Text;
                _shop.INN = TBoxINN.Text;
                _shop.Description = string.IsNullOrWhiteSpace(TBoxDescription.Text) ? null : TBoxDescription.Text;

                if (_shop.ID == 0)
                    App.Context.Shops.Add(_shop);

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
