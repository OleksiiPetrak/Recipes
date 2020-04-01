using System;
using System.Globalization;
using Xamarin.Forms;

namespace RecipesBook.UI.Converters
{
    public class DescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                var cookingSteps = (string)value;
                if (cookingSteps.Length > 20)
                    return cookingSteps.Substring(0, 20);
                else
                    return cookingSteps;
            }

            return (string)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
