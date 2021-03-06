﻿using System;
using System.ComponentModel;
using System.Linq.Expressions;
using EventBasedProgramming.Binding.Impl;
using JetBrains.Annotations;

namespace EventBasedProgramming.Binding
{
	public static class PropertyChangedExtensions
	{
		public static void Raise(this PropertyChangedEventHandler handler, object sender,
			[NotNull] Expression<Func<object>> propertyExpression)
		{
			if (handler == null)
				return;
			var e = new PropertyChangedEventArgs(Extract.PropertyNameFrom(propertyExpression));
			handler(sender, e);
		}

		public static DependantProperty Propagate([NotNull] this IFirePropertyChanged sender,
			[NotNull] Expression<Func<object>> propertyExpression)
		{
			return new DependantProperty(sender, propertyExpression);
		}

		public static PropertyChangedEventHandler ForProperty<TSender>([NotNull] this TSender sender,
			[NotNull] Expression<Func<object>> propertyExpression, Action target) where TSender : INotifyPropertyChanged
		{
			var property = Extract.PropertyNameFrom(propertyExpression);
			return (o, e) =>
			{
				if (e.PropertyName == property)
				{
					target();
				}
			};
		}
	}
}
