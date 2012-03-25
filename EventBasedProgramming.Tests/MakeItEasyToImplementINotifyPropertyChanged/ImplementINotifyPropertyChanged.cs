using EventBasedProgramming.Tests.zzTestSupportData;
using FluentAssertions.EventMonitoring;
using NUnit.Framework;

namespace EventBasedProgramming.Tests.MakeItEasyToImplementINotifyPropertyChanged
{
	[TestFixture]
	public class ImplementINotifyPropertyChanged
	{
		[Test]
		public void PropertyChangedShouldFireWhenClassFiresIt()
		{
			var testSubject = new _ObjWithPropertyChangeNotification();
			testSubject.MonitorEvents();
			testSubject.FireDescriptionChangedBecauseTestSaidTo();
			testSubject.ShouldRaisePropertyChangeFor(s => s.Description);
		}
	}
}
