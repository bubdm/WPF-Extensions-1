using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WPF_Extension.Converter
{
    [ValueConversion(typeof(Color), typeof(SolidColorBrush))]
    public class ColorToSolidBrushConverter : BaseValueConverter<ColorToSolidBrushConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value != null ? new SolidColorBrush((Color)value) : null;
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
