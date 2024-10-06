using LunaSenatiConnection.Views;
using System.Windows.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using LunaSenatiConnection.Services;
using System.Threading.Tasks;
using LunaSenatiConnection.Models;

namespace LunaSenatiConnection.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private string _email;
        private string _password;

        private AuthService _authService;

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            _authService = new AuthService();

            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe completar los campos", "OK");
                return;
            }

            await Login();
            // Application.Current.Properties["token"] = "tokenfake";
            // await Application.Current.SavePropertiesAsync();

            // Navegar a la siguiente página (MainPage, por ejemplo)
            // App.Current.MainPage = new NavigationPage(new MainPage());
        }

        private async Task Login()
        {
            try
            {


                var authResponse = await _authService.LoginAsync(Email, Password);
                Console.Write("response_del_login: " + authResponse.ToString());
                if (authResponse != null)
                {
                    // Si hay un token, significa que el inicio de sesión fue exitoso
                    if (!string.IsNullOrEmpty(authResponse.token))
                    {
                        // Guardar el token en las propiedades de la aplicación
                        Application.Current.Properties["token"] = authResponse.token;
                        await Application.Current.SavePropertiesAsync();

                        // Navegar a la siguiente página (MainPage, por ejemplo)
                        App.Current.MainPage = new NavigationPage(new MainPage());
                    }
                    else
                    {

                        // Mostrar el mensaje de éxito o manejarlo según sea necesario
                        await Application.Current.MainPage.DisplayAlert("Éxito", authResponse.message, "OK");

                    }
                }
                else
                {
                    // Manejar errores (puedes usar un mensaje predeterminado o el error que te haya llegado)
                    await Application.Current.MainPage.DisplayAlert("Error", "Inicio de sesión fallido. Verifica tus credenciales.", "OK");
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("error_encontrado: " + err.ToString());
                await Application.Current.MainPage.DisplayAlert("Error", "Inicio de sesión fallido. Verifica tus credenciales." + err.ToString(), "OK");

            }
        }

    }
}
