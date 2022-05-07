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
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void BtnLogIn_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(TBoxLogin.Text) && !string.IsNullOrWhiteSpace(PBoxPassword.Password))
            {
                if (App.Context.Users.ToList().FirstOrDefault(x => x.Login == TBoxLogin.Text && PBoxPassword.Password == x.Password) is User user)
                {
                    Properties.Settings.Default.UserId = user.ID;
                    NavigationService.Navigate(new MenuPage());
                    if(ChBoxRememberMe.IsChecked == true)
                        Properties.Settings.Default.Save();
                }
                else
                {
                    Message.ShowError("Невернй логин или пароль.");
                }
            }
            else
            {
                Message.ShowError("Заполните оба поля.");
            }
        }
    }
}
