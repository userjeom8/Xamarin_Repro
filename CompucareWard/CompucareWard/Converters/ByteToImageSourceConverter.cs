using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace CompucareWard.Converters
{
    class ByteToImageSourceConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(ImageSource))
                throw new InvalidOperationException("The target must be a image source");

            var image = (byte[])value;
            return ImageSource.FromStream(() => new MemoryStream(image));
        }       

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }   
}
