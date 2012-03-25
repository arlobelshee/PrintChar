using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace EventBasedProgramming.Binding
{
	public class TrackingNullableProperty<TProperty> : IEquatable<TrackingNullableProperty<TProperty>>
		where TProperty : class
	{
		[NotNull] private readonly List<Expression<Func<object>>> _enclosingProperties;
		[NotNull] private readonly IFirePropertyChanged _owner;
		[CanBeNull] private TProperty _value;

		public TrackingNullableProperty([NotNull] IFirePropertyChanged owner,
			[NotNull] params Expression<Func<object>>[] enclosingProperties)
		{
			_owner = owner;
			_enclosingProperties = enclosingProperties.ToList();
		}

		public bool Equals(TrackingNullableProperty<TProperty> other)
		{
			if (ReferenceEquals(null, other))
				return false;
			if (ReferenceEquals(this, other))
				return true;
			return Equals(other._value, _value);
		}

		[CanBeNull]
		public TProperty Value
		{
			get { return _value; }
			set
			{
				if ((_value == null && value == null) || (_value != null && _value.Equals(value)))
					return;
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
			return Equals(obj as TrackingNullableProperty<TProperty>);
		}

		public override int GetHashCode()
		{
			return _value == null ? 0 : _value.GetHashCode();
		}

		public static bool operator ==(TrackingNullableProperty<TProperty> left, TrackingNullableProperty<TProperty> right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(TrackingNullableProperty<TProperty> left, TrackingNullableProperty<TProperty> right)
		{
			return !Equals(left, right);
		}

		public override string ToString()
		{
			return _value.IfValid(v => v.ToString());
		}
	}
}