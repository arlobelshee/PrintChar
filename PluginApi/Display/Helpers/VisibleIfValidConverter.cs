using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PluginApi.Display.Helpers
{
	public class VisibleIfValidConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var valid = value != null;
			return valid ? Visibility.Visible : Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException("This conversion loses data, so is valid for one way bindings only.");
		}
	}
}
