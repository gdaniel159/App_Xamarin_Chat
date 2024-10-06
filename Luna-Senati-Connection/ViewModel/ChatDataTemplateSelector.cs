using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace LunaSenatiConnection.ViewModel
{
    public class ChatDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate FromTemplate { get; set; }
        public DataTemplate ToTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is ChatViewModel message) // Coincidencia de patrones
            {
                // Ajusta la condición según sea necesario (por ejemplo, "Sent" vs "Received")
                return message.Status == "Sent" ? FromTemplate : ToTemplate;
            }

            return ToTemplate; // O devuelve un DataTemplate predeterminado si es necesario
        }


    }
}