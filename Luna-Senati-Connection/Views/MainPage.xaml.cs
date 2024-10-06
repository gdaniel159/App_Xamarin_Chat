using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LunaSenatiConnection.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnLogoutClicked(object sender, EventArgs e)
        {
            Logout(); // Llama a tu método logout
        }

        public async void Logout()
        {
            var confirm = await DisplayAlert("Cerrar sesión", "¿Seguro que deseas cerrar sesión?", "Sí", "No");
            if (confirm)
            {
                // Aquí va tu lógica para eliminar el token y navegar a la LoginPage
                if (Application.Current.Properties.ContainsKey("token"))
                {
                    Application.Current.Properties.Remove("token");
                    await Application.Current.SavePropertiesAsync();
                }

                // Navega a la LoginPage
                App.Current.MainPage = new NavigationPage(new Login());
            }
        }

    }
}