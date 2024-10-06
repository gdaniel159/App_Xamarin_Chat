using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace LunaSenatiConnection.Models
{
    public class AuthResponse
    {
        public string message { get; set; } // Para el mensaje de éxito
        public string token { get; set; } // Para el token
        public string name { get; set; } // Para el nombre del usuario
    }
}