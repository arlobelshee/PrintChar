using System;
using System.ComponentModel;
using System.Linq.Expressions;
using EventBasedProgramming.Binding.Impl;
using JetBrains.Annotations;

namespace EventBasedProgramming.Binding
{
	public class TestablePropertyChangedEvent : TestableEvent<object, PropertyChangedEventArgs>
	{
		public void Call([NotNull] IFirePropertyChanged sender, [NotNull] Expression<Func<object>> propertyExpression)
		{
			Call(sender, new PropertyChangedEventArgs(Extract.PropertyNameFrom(propertyExpression)));
		}

		public void BindTo(PropertyChangedEventHandler handler)
		{
			_BindTo(new BindingInfoForDelegate(handler));
		}

		public void UnbindFrom(PropertyChangedEventHandler handler)
		{
			_UnbindFrom(handler.Method, handler.Target);
		}
	}
}
