using MvvmCross.Platform.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace FoodPoint_Seller.Touch.Converters
{
    public class StringFormatValueConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            if (parameter == null)
                return value;

            var format = "{0:" + parameter.ToString() + "}";

            return string.Format(format, value);
        }
    }
}