﻿using System;
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
    /// Логика взаимодействия для ShopListPage.xaml
    /// </summary>
    public partial class ShopListPage : Page
    {
        public ShopListPage()
        {
            InitializeComponent();
            LViewShops.ItemsSource = App.Context.Shops.ToList();
        }
    }
}
