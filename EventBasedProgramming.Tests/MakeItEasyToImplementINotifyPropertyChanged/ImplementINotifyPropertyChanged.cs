using System.ComponentModel;
using EventBasedProgramming.Binding.Impl;
using EventBasedProgramming.Tests.zzTestSupportData;
using FluentAssertions;
using NUnit.Framework;

namespace EventBasedProgramming.Tests.MakeItEasyToImplementINotifyPropertyChanged
{
	[TestFixture]
	public class ImplementINotifyPropertyChanged
	{
		private string _propertyChanged;

		[Test]
		public void PropertyChangedShouldFireWhenClassFiresIt()
		{
			var testSubject = new _ObjWithPropertyChangeNotification();
			testSubject.MonitorEvents();
			testSubject.FireDescriptionChangedBecauseTestSaidTo();
			testSubject.ShouldRaisePropertyChangeFor(s => s.Description);
		}

		[Test]
		public void TrackablePropertyChangedShouldFireWhenClassFiresIt()
		{
			var testSubject = new _ObjWithTrackablePropertyChangeNotification();
			testSubject.MonitorEvents();
			testSubject.FireDescriptionChangedBecauseTestSaidTo();
			testSubject.ShouldRaisePropertyChangeFor(s => s.Description);
		}

		[Test]
		public void TrackablePropertyChangedShouldCorrectlyCallNonDynamicMethods()
		{
			var testSubject = new _ObjWithTrackablePropertyChangeNotification();
			_propertyChanged = null;
			testSubject.PropertyChanged += _GotNotified;
			testSubject.FireDescriptionChangedBecauseTestSaidTo();
			_propertyChanged.Should().Be(Extract.PropertyNameFrom(() => testSubject.Description));
		}

		private void _GotNotified(object sender, PropertyChangedEventArgs e)
		{
			_propertyChanged = e.PropertyName;
		}
	}
}
