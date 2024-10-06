using LunaSenatiConnection.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LunaSenatiConnection
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            // si ya existe una sesión iniciada lo dirige al mainPage
            if (Application.Current.Properties.ContainsKey("token"))
            {
                App.Current.MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                App.Current.MainPage = new NavigationPage(new Login());
            }

            NavigationPage.SetHasNavigationBar(MainPage, false);
            //MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
