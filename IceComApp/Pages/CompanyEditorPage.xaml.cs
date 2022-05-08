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
    /// Логика взаимодействия для CompanyEditorPage.xaml
    /// </summary>
    public partial class CompanyEditorPage : Page
    {
        private Company _company;
        public CompanyEditorPage(Company company = null)
        {
            InitializeComponent();
            _company = company;
            CBoxCompanyType.ItemsSource = App.Context.CompanyTypes.ToList();
            if (_company != null)
            {
                TBoxName.Text = _company.Name;
                TBoxINN.Text = _company.INN;
                TBoxAddress.Text = _company.Address;
                CBoxCompanyType.SelectedItem = _company.CompanyType;
                Title = "Редактирование компании";
                TBoxTitle.Text = "Редактирование компании";
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var err = "";
            if (string.IsNullOrWhiteSpace(TBoxName.Text)) err += "Заполните поле Название\n";
            if (string.IsNullOrWhiteSpace(TBoxINN.Text)) err += "Заполните поле ИНН\n";
            if (string.IsNullOrWhiteSpace(TBoxAddress.Text)) err += "Заполните поле Адрес\n";
            if (string.IsNullOrWhiteSpace(CBoxCompanyType.Text)) err += "Выберите тип компании";


            if (err == "")
            {
                if (_company == null)
                    _company = new Company();

                _company.Name = TBoxName.Text;
                _company.INN = TBoxINN.Text;
                _company.Address = TBoxAddress.Text;
                _company.CompanyType = CBoxCompanyType.SelectedItem as CompanyType;
                if (_company.ID == 0)
                    App.Context.Companies.Add(_company);

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
