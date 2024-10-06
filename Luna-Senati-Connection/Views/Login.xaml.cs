using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System;
using System.Threading.Tasks;

namespace LunaSenatiConnection.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {

            InitializeComponent();

        }

        // Método que se ejecuta cuando se toca el Label
        private async void OnLabelTapped(object sender, EventArgs e)
        {
            await ShowUrlEditPrompt();
        }

        // Método que se ejecuta cuando se toca la Imagen
        private async void OnImageTapped(object sender, EventArgs e)
        {
            await ShowUrlEditPrompt();
        }

        private async Task ShowUrlEditPrompt()
        {
            string currentUrl = Preferences.Get("ApiBaseUrl", "https://senati-back-connection.centralus.cloudapp.azure.com/api/");

            string newUrl = await DisplayPromptAsync("Modificar URL de la API",
                                                     "Ingresa la nueva URL base para la API:",
                                                     initialValue: currentUrl,
                                                     maxLength: 200,
                                                     keyboard: Keyboard.Url);

            if (!string.IsNullOrEmpty(newUrl))
            {
                Preferences.Set("ApiBaseUrl", newUrl);

                await DisplayAlert("Éxito", "La URL base de la API ha sido actualizada a: " + newUrl, "OK");
            }
        }
    }
}