using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Semiconductor
{
    public sealed class BooleanToVisibilityConverter : IValueConverter
    {
        bool visibility = false;
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            
            if (value is bool)
            {
                visibility = (bool)value;
            }

            return visibility ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            return (visibility == Visibility.Visible);
        }
    }
}