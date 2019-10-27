using LaZeroDayCore.Controller;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace LaZeroDayCore.Converter
{
    public class IConverterImageProdect : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (byte[])value;
            if ((v == null) || (v.Length == 0)) v = F_Image.Image2Bytes(Properties.Resources.DefaultProduct);
            return F_Image.Bytes2BitmapImage(v, 100, 100);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return F_Image.BitmapImage2Bytes((BitmapImage)value);
        }
    }
}
