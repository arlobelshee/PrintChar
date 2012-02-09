using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace PrintChar
{
	public interface IFirePropertyChanged
	{
		void FirePropertyChanged([NotNull] Expression<Func<object>> propertyThatChanged);
	}
}