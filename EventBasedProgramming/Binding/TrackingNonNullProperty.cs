using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace EventBasedProgramming.Binding
{
	public class TrackingNonNullProperty<TProperty> : TrackingProperty<TProperty>
		where TProperty : class
	{
		public TrackingNonNullProperty([NotNull] TProperty initialValue, [NotNull] IFirePropertyChanged owner,
			[NotNull] params Expression<Func<object>>[] enclosingProperties)
			: base(owner, enclosingProperties, initialValue)
		{
			ValidateNewValue(initialValue);
		}

		[NotNull]
		public TProperty Value
		{
			get { return GetValue(); }
			set { UpdateValue(value); }
		}

		protected override void ValidateNewValue(TProperty value)
		{
			if (value == null)
				throw new ArgumentNullException("Value", "This property is not allowed to be null.");
		}
	}
}
