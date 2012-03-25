using System;
using System.ComponentModel;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace EventBasedProgramming.Binding
{
	public interface IFirePropertyChanged : INotifyPropertyChanged
	{
		void FirePropertyChanged([NotNull] Expression<Func<object>> propertyThatChanged);
	}
}