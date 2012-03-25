using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace EventBasedProgramming.Binding
{
	public class TrackingNullableProperty<TProperty> : TrackingProperty<TProperty>
		where TProperty : class
	{
		public TrackingNullableProperty([NotNull] IFirePropertyChanged owner,
			[NotNull] params Expression<Func<object>>[] enclosingProperties)
			: base(owner, enclosingProperties, null) {}

		[CanBeNull]
		public TProperty Value
		{
			get { return GetValue(); }
			set { UpdateValue(value); }
		}

		protected override void ValidateNewValue(TProperty value) {}
	}
}
