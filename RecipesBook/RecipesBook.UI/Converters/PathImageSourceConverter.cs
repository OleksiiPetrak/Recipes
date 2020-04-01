using System;
using System.Globalization;
using Xamarin.Forms;

namespace RecipesBook.UI.Converters
{
    public class PathImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is string)
            {
                var path = (string)value;
                var result = ImageSource.FromFile(path);
                return result;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
