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
		[Test]
		public void NonNullablePropertyShouldNotAllowBeingSetToNull()
		{
			var testSubject = new TrackingNonNullProperty<string>(string.Empty, _target);
			Assert.Throws<ArgumentNullException>(() => testSubject.Value = null);
		}

		[Test]
		public void NonNullablePropertyShouldNotAllowNullInitialValue()
		{
			Assert.Throws<ArgumentNullException>(() => new TrackingNonNullProperty<string>(null, _target));
		}

		[Test]
		public void InitiallyNullablePropertyShouldNotAllowBeingSetToNull()
		{
			var testSubject = new TrackingOnlyInitiallyNullProperty<string>(_target);
			Assert.Throws<ArgumentNullException>(() => testSubject.Value = null);
		}

		[Test]
		public void InitiallyNullablePropertyShouldDefaultToNull()
		{
			new TrackingOnlyInitiallyNullProperty<string>(_target).Value.Should().BeNull();
		}

		[Test]
		public void NullablePropertyShouldAllowBeingSetToNull()
		{
			var testSubject = new TrackingNullableProperty<string>(_target);
			Assert.DoesNotThrow(() => testSubject.Value = null);
		}

		[Test]
		public void NullablePropertyShouldDefaultToNull()
		{
			new TrackingOnlyInitiallyNullProperty<string>(_target).Value.Should().BeNull();
		}

		[Test]
		public void AllTrackingPropertiesShouldBeEqualityComparableBasedOnTheirValues()
		{
			const string thePropertyValue = "the property value";
			var nullableProperty = new TrackingNullableProperty<string>(_target);
			var initiallyNullProperty = new TrackingNullableProperty<string>(_target);
			var nonNullableProperty = new TrackingNonNullProperty<string>(thePropertyValue, _target);
			nullableProperty.Should().Be(initiallyNullProperty);
			nullableProperty.Should().NotBe(nonNullableProperty);
			nullableProperty.Value = thePropertyValue;
			nullableProperty.Should().Be(nonNullableProperty);
			nullableProperty.Should().NotBe(initiallyNullProperty);
		}

		[SetUp]
		public void Setup()
		{
			_target = new _ClassWithSomeProperties();
		}

		private _ClassWithSomeProperties _target;
	}
}
