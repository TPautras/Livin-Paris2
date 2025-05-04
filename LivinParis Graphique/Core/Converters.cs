using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace LivinParis_Graphique.Core
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                // If the parameter is specified and is "invert", we invert the boolean value
                if (parameter is string param && param.ToString().ToLower() == "invert")
                {
                    boolValue = !boolValue;
                }

                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            }
            
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibility)
            {
                // If the parameter is specified and is "invert", we invert the result
                bool result = visibility == Visibility.Visible;
                
                if (parameter is string param && param.ToString().ToLower() == "invert")
                {
                    result = !result;
                }
                
                return result;
            }
            
            return false;
        }
    }
}