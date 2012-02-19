using System;
using System.ComponentModel;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace PluginApi.Display.Helpers
{
	public interface IFirePropertyChanged : INotifyPropertyChanged
	{
		void FirePropertyChanged([NotNull] Expression<Func<object>> propertyThatChanged);
	}
}