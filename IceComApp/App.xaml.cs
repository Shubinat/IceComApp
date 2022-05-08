using IceComApp.Properties;
using IceComApp.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace IceComApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Entities.IceComBaseEntities Context { get; } = new Entities.IceComBaseEntities();
        public static Entities.User AuthUser => Context.Users.ToList().FirstOrDefault(x => x.ID == Settings.Default.UserId);

        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            Message.ShowError("Произошла непридвиденная ошибка");
        }
    }
}
