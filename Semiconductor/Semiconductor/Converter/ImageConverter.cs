using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Semiconductor
{
    public sealed class ImageConverter : IValueConverter
    {
        bool margin = false;
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                margin = (bool)value;
            }

            return margin ? "0,94,349,34" : "0,120,676,34";
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            return (visibility == Visibility.Visible);
        }
    }
}