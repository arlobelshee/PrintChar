using System;
using System.ComponentModel;
using System.Linq.Expressions;
using EventBasedProgramming.Binding;
using JetBrains.Annotations;
using NUnit.Framework;
using FluentAssertions.EventMonitoring;

namespace EventBasedProgramming.Tests.MakeItEasyToImplementINotifyPropertyChanged
{
	[TestFixture]
	public class TrackingPropertiesAutomaticallyNotifyWhenValuesChange
	{
		[Test]
		public void ChangingAPropertyValueShouldFirePropertyChangedForAllAffectedProperties()
		{
			var testSubject = new _ClassShowingTypicalUsageOfTrackingProperties();
			testSubject.MonitorEvents();
			testSubject.Name = "just changing the property to trigger the event";
			testSubject.ShouldRaisePropertyChangeFor(s => s.Name);
			testSubject.ShouldRaisePropertyChangeFor(s => s.FullName);
		}

		private class _ClassShowingTypicalUsageOfTrackingProperties : IFirePropertyChanged
		{
			[NotNull] private readonly TrackingNonNullProperty<string> _name;

			public _ClassShowingTypicalUsageOfTrackingProperties()
			{
				_name = new TrackingNonNullProperty<string>(string.Empty, this, () => Name, () => FullName);
			}

			public event PropertyChangedEventHandler PropertyChanged;

			public void FirePropertyChanged(Expression<Func<object>> propertyThatChanged)
			{
				PropertyChanged.Raise(this, propertyThatChanged);
			}

			[NotNull]
			public string Name
			{
				get { return _name.Value; }
				set { _name.Value = value; }
			}

			[NotNull]
			public string FullName
			{
				get { return _name.Value; }
			}
		}
	}
}
