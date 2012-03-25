using System;
using System.ComponentModel;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace EventBasedProgramming.Binding.Impl
{
	public class DependantProperty
	{
		private readonly IFirePropertyChanged _sender;
		private readonly Expression<Func<object>> _propertyExpression;

		public DependantProperty(IFirePropertyChanged sender, Expression<Func<object>> propertyExpression)
		{
			_sender = sender;
			_propertyExpression = propertyExpression;
		}

		public DependantProperty From<TSource>([NotNull] TSource originator, [NotNull] Expression<Func<TSource, object>> whenThisFiresExpression)
			where TSource : INotifyPropertyChanged
		{
			var propertyToPropagate = Extract.PropertyNameFrom(whenThisFiresExpression);
			originator.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == propertyToPropagate)
					_sender.FirePropertyChanged(_propertyExpression);
			};
			return this;
		}

		public DependantProperty OrFrom<TSource>([NotNull] TSource originator, [NotNull] Expression<Func<TSource, object>> whenThisFiresExpression)
			where TSource : INotifyPropertyChanged
		{
			return From(originator, whenThisFiresExpression);
		}
	}
}