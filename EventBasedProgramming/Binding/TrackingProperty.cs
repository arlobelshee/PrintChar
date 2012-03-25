using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace EventBasedProgramming.Binding
{
	public abstract class TrackingProperty<TProperty> : IEquatable<TrackingProperty<TProperty>>
		where TProperty : class
	{
		private TProperty _value;
		[NotNull] private List<Expression<Func<object>>> _enclosingProperties;
		[NotNull] private IFirePropertyChanged _owner;

		protected TrackingProperty(IFirePropertyChanged owner, IEnumerable<Expression<Func<object>>> enclosingProperties, TProperty initialValue)
		{
			_value = initialValue;
			_owner = owner;
			_enclosingProperties = enclosingProperties.ToList();
		}

		public bool Equals(TrackingProperty<TProperty> other)
		{
			if (ReferenceEquals(null, other))
				return false;
			if (ReferenceEquals(this, other))
				return true;
			return Equals(other._value, _value);
		}

		protected void UpdateValue(TProperty value)
		{
			ValidateNewValue(value);
			if ((_value == null && value == null) || (_value != null && _value.Equals(value)))
				return;
			_value = value;
			foreach (Expression<Func<object>> property in _enclosingProperties)
				_owner.FirePropertyChanged(property);
		}

		protected TProperty GetValue()
		{
			return _value;
		}

		protected abstract void ValidateNewValue(TProperty value);

		public void AddDependantProperty(Expression<Func<object>> enclosingProperty)
		{
			_enclosingProperties.Add(enclosingProperty);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as TrackingProperty<TProperty>);
		}

		public override int GetHashCode()
		{
			return _value.GetHashCode();
		}

		public static bool operator ==(TrackingProperty<TProperty> left, TrackingProperty<TProperty> right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(TrackingProperty<TProperty> left, TrackingProperty<TProperty> right)
		{
			return !Equals(left, right);
		}

		public override string ToString()
		{
			return _value.ToString();
		}
	}
}
