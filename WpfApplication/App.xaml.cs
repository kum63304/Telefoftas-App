using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TelesoftasApp.Models;
using TelesoftasApp.Views;
using Unity;


namespace TelesoftasApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IMainView, MainWindow>();
            container.RegisterType<IFileProcessingService, FileProcessingService>();


            IMainView mainView = container.Resolve<IMainView>();
            mainView.Show();
        }
    }
}
