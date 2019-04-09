using System;
using System.Globalization;
using Xamarin.Forms;

namespace CompucareWard.Converters
{
	public class WebNavigationEventArgsConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var eventArgs = value as WebNavigationEventArgs;
			if (eventArgs == null)
				throw new ArgumentException("Expected WebNavigationEventArgs as value", "value");

			return eventArgs.Url;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
