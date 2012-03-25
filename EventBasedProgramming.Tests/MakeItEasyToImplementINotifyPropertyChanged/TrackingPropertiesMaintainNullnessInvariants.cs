using System;
using EventBasedProgramming.Binding;
using EventBasedProgramming.Tests.zzTestSupportData;
using NUnit.Framework;
using FluentAssertions;

namespace EventBasedProgramming.Tests.MakeItEasyToImplementINotifyPropertyChanged
{
	[TestFixture]
	public class TrackingPropertiesMaintainNullnessInvariants
	{
		private const string TheNonNullPropertyValue = "the property value";

		[Test]
		public void NonNullablePropertyShouldNotAllowBeingSetToNull()
		{
			Assert.Throws<ArgumentNullException>(() => _nonNullableProperty.Value = null);
		}

		[Test]
		public void NonNullablePropertyShouldNotAllowNullInitialValue()
		{
			Assert.Throws<ArgumentNullException>(() => new TrackingNonNullProperty<string>(null, _target));
		}

		[Test]
		public void InitiallyNullablePropertyShouldNotAllowBeingSetToNull()
		{
			Assert.Throws<ArgumentNullException>(() => _initiallyNullProperty.Value = null);
		}

		[Test]
		public void InitiallyNullablePropertyShouldDefaultToNull()
		{
			_initiallyNullProperty.Value.Should().BeNull();
		}

		[Test]
		public void NullablePropertyShouldAllowBeingSetToNull()
		{
			Assert.DoesNotThrow(() => _nullableProperty.Value = null);
		}

		[Test]
		public void NullablePropertyShouldDefaultToNull()
		{
			_nullableProperty.Value.Should().BeNull();
		}

		[Test]
		public void AllTrackingPropertiesShouldBeEqualityComparableBasedOnTheirValues()
		{
			_nullableProperty.Should().Be(_initiallyNullProperty);
			_nullableProperty.Should().NotBe(_nonNullableProperty);

			_nullableProperty.Value = TheNonNullPropertyValue;
			_nullableProperty.Should().Be(_nonNullableProperty);
			_nullableProperty.Should().NotBe(_initiallyNullProperty);
		}

		[SetUp]
		public void Setup()
		{
			_target = new _ClassWithSomeProperties();
			_nullableProperty = new TrackingNullableProperty<string>(_target);
			_initiallyNullProperty = new TrackingOnlyInitiallyNullProperty<string>(_target);
			_nonNullableProperty = new TrackingNonNullProperty<string>(TheNonNullPropertyValue, _target);
		}

		private _ClassWithSomeProperties _target;
		private TrackingNullableProperty<string> _nullableProperty;
		private TrackingOnlyInitiallyNullProperty<string> _initiallyNullProperty;
		private TrackingNonNullProperty<string> _nonNullableProperty;
	}
}
