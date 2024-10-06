using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace LunaSenatiConnection
{
    public static class ApiConfig
    {
        // Cada vez que se acceda a esta propiedad, se obtendrá la URL actualizada desde Preferences
        public static string ApiBaseUrl => Preferences.Get("ApiBaseUrl", "https://senati-back-connection.centralus.cloudapp.azure.com/");
    }
}