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
    /// Логика взаимодействия для MyProfilePage.xaml
    /// </summary>
    public partial class MyProfilePage : Page
    {
        private User _user;
        private byte[] _photo;
        public MyProfilePage(User user)
        {
            InitializeComponent();
            _user = user;
            if (user != null)
            {
                _photo = _user.Picture;
                ImgProfilePhoto.DataContext = _photo;
                TBoxLogin.Text = user.Login;
                TBoxPassword.Text = user.Password;
                TBoxSurname.Text = user.Surname;
                TBoxName.Text = user.Name;
                TBoxPatronymic.Text = user.Patronymic;
                TBoxTelephone.Text = user.Telephone;
                Title = "Редактирование профиля";
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
            var errors = "";
            if (string.IsNullOrWhiteSpace(TBoxLogin.Text)) errors += "Заполните поле логин!\n";
            if (string.IsNullOrWhiteSpace(TBoxPassword.Text)) errors += "Заполните поле пароль!\n";
            if (string.IsNullOrWhiteSpace(TBoxSurname.Text)) errors += "Заполните поле фамилия!\n";
            if (string.IsNullOrWhiteSpace(TBoxName.Text)) errors += "Заполните поле имя!\n";
            if (!TBoxTelephone.MaskFull) errors += "Заполните поле номер телефона!\n";

            if (errors == "")
            {
                if (_user == null)
                    _user = new User();

                _user.Password = TBoxPassword.Text;
                _user.Login = TBoxLogin.Text;
                _user.Surname = TBoxSurname.Text;
                _user.Name = TBoxName.Text;
                _user.Patronymic = TBoxPatronymic.Text;
                _user.Telephone = TBoxTelephone.Text;
                _user.Picture = _photo;

                if (_user.ID == 0)
                    App.Context.Users.Add(_user);

                App.Context.SaveChanges();
                NavigationService.GoBack();
            }
            else
            {
                Message.ShowError(errors);
            }

        }
    }
}
