using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace LunaSenatiConnection.ViewModel
{
    public class UserToAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string fromUser)
            {
                // Aquí verifica si el mensaje es de "Tú" y devuelve "End" para la alineación a la derecha
                return fromUser == "Tú" ? LayoutOptions.End : LayoutOptions.Start;
            }

            return LayoutOptions.Start; // Valor por defecto
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}