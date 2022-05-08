using IceComApp.Entities;
using IceComApp.Utils;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для ProductEditorPage.xaml
    /// </summary>
    public partial class ProductEditorPage : Page
    {
        private Product _product;
        private byte[] _photo;
        public ProductEditorPage(Product product = null)
        {
            InitializeComponent();
            _product = product;
            CBoxCompany.ItemsSource = App.Context.Companies.ToList();
            CBoxProductType.ItemsSource = App.Context.ProductTypes.ToList();
            CBoxUnit.ItemsSource = App.Context.Units.ToList();
            if (product != null)
            {
                _photo = _product.Picture;
                ImgProfilePhoto.DataContext = _photo;
                TBoxName.Text = _product.Name;
                TBoxDescription.Text = _product.Description;
                TBoxPrice.Text = _product.Price.ToString();
                CBoxCompany.SelectedItem = _product.Company;
                CBoxProductType.SelectedItem = _product.ProductType;
                CBoxUnit.SelectedItem = _product.Unit;
                Title = "Редактирование продукции";
                TBlockTitle.Text = "Редактирование продукции";
            }
        }

        private void BtnSelectPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файлы изображений|*.png;*.jpg";
            if (dialog.ShowDialog() == true)
            {
                _photo = File.ReadAllBytes(dialog.FileName);
                ImgProfilePhoto.DataContext = _photo;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var err = "";
            if (string.IsNullOrWhiteSpace(TBoxName.Text)) err += "Заполните поле Название\n";
            if (string.IsNullOrWhiteSpace(CBoxCompany.Text)) err += "Выберите производителя\n";
            if (string.IsNullOrWhiteSpace(CBoxProductType.Text)) err += "Выберите тип продукта\n";
            if (string.IsNullOrWhiteSpace(CBoxUnit.Text)) err += "Выберите единицу измерения\n";
            if (!decimal.TryParse(TBoxPrice.Text, out decimal d)) err += "Заполните правильность заполнения поля Цена\n";
            
            if (err == "")
            {
                if (_product == null)
                    _product = new Product();

                _product.Name = TBoxName.Text;
                _product.Price = decimal.Parse(TBoxPrice.Text);
                _product.Company = CBoxCompany.SelectedItem as Company;
                _product.ProductType = CBoxProductType.SelectedItem as ProductType;
                _product.Unit = CBoxUnit.SelectedItem as Unit;
                _product.Picture = _photo;
                _product.Description = string.IsNullOrWhiteSpace(TBoxDescription.Text) ? null : TBoxDescription.Text;

                if (_product.ID == 0)
                    App.Context.Products.Add(_product);

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
