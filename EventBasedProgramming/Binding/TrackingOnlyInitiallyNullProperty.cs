using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace EventBasedProgramming.Binding
{
	public class TrackingOnlyInitiallyNullProperty<TProperty> : TrackingProperty<TProperty>
		where TProperty : class
	{
		public TrackingOnlyInitiallyNullProperty([NotNull] IFirePropertyChanged owner,
			[NotNull] params Expression<Func<object>>[] enclosingProperties)
			: base(owner, enclosingProperties, null) {}

		public TProperty Value
		{
			[CanBeNull]
			get { return GetValue(); }
			[NotNull]
			set { UpdateValue(value); }
		}

		protected override void ValidateNewValue(TProperty value)
		{
			if (value == null)
				throw new ArgumentNullException("Value", "This property is not allowed to be null.");
		}
	}
}
