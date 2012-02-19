using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace PluginApi.Display.Helpers
{
	public class TrackingNonNullProperty<TProperty> : IEquatable<TrackingNonNullProperty<TProperty>>
		where TProperty : class
	{
		[NotNull] private readonly List<Expression<Func<object>>> _enclosingProperties;
		[NotNull] private readonly IFirePropertyChanged _owner;
		[NotNull] private TProperty _value;

		public TrackingNonNullProperty([NotNull] TProperty initialValue, [NotNull] IFirePropertyChanged owner,
			[NotNull] params Expression<Func<object>>[] enclosingProperties)
		{
			_value = initialValue;
			_owner = owner;
			_enclosingProperties = enclosingProperties.ToList();
		}

		public bool Equals(TrackingNonNullProperty<TProperty> other)
		{
			if (ReferenceEquals(null, other))
				return false;
			if (ReferenceEquals(this, other))
				return true;
			return Equals(other._value, _value);
		}

		[NotNull]
		public TProperty Value
		{
			get { return _value; }
			set
			{
				if (_value.Equals(value))
					return;
				if (value == null)
					throw new ArgumentException("This property is not allowed to be null.");
				_value = value;
				foreach (var property in _enclosingProperties)
					_owner.FirePropertyChanged(property);
			}
		}

		public void AddDependantProperty(Expression<Func<object>> enclosingProperty)
		{
			_enclosingProperties.Add(enclosingProperty);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as TrackingNonNullProperty<TProperty>);
		}

		public override int GetHashCode()
		{
			return _value.GetHashCode();
		}

		public static bool operator ==(TrackingNonNullProperty<TProperty> left, TrackingNonNullProperty<TProperty> right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(TrackingNonNullProperty<TProperty> left, TrackingNonNullProperty<TProperty> right)
		{
			return !Equals(left, right);
		}

		public override string ToString()
		{
			return _value.ToString();
		}
	}
}
