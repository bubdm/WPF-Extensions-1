using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace WPF_Extension.Converter
{
    public abstract class BaseValueConverter<TConverter> : MarkupExtension, IValueConverter
        where TConverter: class, new()
        
    {
        private static TConverter _converter = null;


        public override object ProvideValue(IServiceProvider serviceProvider)
            => _converter ??= new TConverter();


        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

    }
}
