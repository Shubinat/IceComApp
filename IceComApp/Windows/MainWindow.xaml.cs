using IceComApp.Pages;
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

namespace IceComApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if(App.AuthUser == null)
                FrameMain.Navigate(new Pages.AuthPage());
            else
                FrameMain.Navigate(new Pages.MenuPage());
        }

        private void BtnNavBack_Click(object sender, RoutedEventArgs e)
        {
            if(FrameMain.CanGoBack)
                FrameMain.GoBack();
        }

        private void FrameMain_ContentRendered(object sender, EventArgs e)
        {
            if (FrameMain.Content is AuthPage || FrameMain.Content is MenuPage)
            {
                BtnNavBack.Visibility = Visibility.Collapsed;
            }
            else
            {
                BtnNavBack.Visibility = Visibility.Visible;
            }

            if (FrameMain.Content is MenuPage)
            {
                BtnNavExit.Visibility = Visibility.Visible;
            }
            else
            {
                BtnNavExit.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnNavExit_Click(object sender, RoutedEventArgs e)
        {
            FrameMain.Navigate(new AuthPage());
        }
    }
}
