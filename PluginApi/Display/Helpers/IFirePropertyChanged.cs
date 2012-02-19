using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace PluginApi.Display.Helpers
{
	public interface IFirePropertyChanged
	{
		void FirePropertyChanged([NotNull] Expression<Func<object>> propertyThatChanged);
	}
}