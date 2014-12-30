using System;
using System.ComponentModel;
using System.Linq.Expressions;
using EventBasedProgramming.Binding;
using EventBasedProgramming.Tests.zzTestSupportData;
using JetBrains.Annotations;
using NUnit.Framework;
using FluentAssertions;

namespace EventBasedProgramming.Tests.MakeItEasyToChainDependantProperties
{
	[TestFixture]
	public class ExtensionMethodsAllowDeclaringChangeDependencies
	{
		[Test]
		public void ShouldFirePropertyChangedWhenDependencyChanges()
		{
			var source = new _ObjWithPropertyChangeNotification();
			var listener = new _ObjWithPropagation(source);
			listener.MonitorEvents();
			source.FireDescriptionChangedBecauseTestSaidTo();
			listener.ShouldRaisePropertyChangeFor(l => l.DependsOnDescription);
		}

		[Test]
		public void ForPropertyShouldWrapAnActionAndCallItOnlyIfCalledWithPropertyChangeForTheRightProperty()
		{
			var source = new _ObjWithPropertyChangeNotification();
			bool wasCalled = false;
			var testSubject = source.ForProperty(() => source.Description, () => wasCalled = true);
			testSubject(null, new PropertyChangedEventArgs("NotDescription"));
			wasCalled.Should().BeFalse();
			testSubject(null, new PropertyChangedEventArgs("Description"));
			wasCalled.Should().BeTrue();
		}

		private class _ObjWithPropagation : IFirePropertyChanged
		{
			private readonly _ObjWithPropertyChangeNotification _source;

			public _ObjWithPropagation([NotNull] _ObjWithPropertyChangeNotification source)
			{
				_source = source;
				this.Propagate(() => DependsOnDescription).From(source, src => src.Description);
			}

			public void FirePropertyChanged(Expression<Func<object>> propertyThatChanged)
			{
				PropertyChanged.Raise(this, propertyThatChanged);
			}

			public event PropertyChangedEventHandler PropertyChanged;

			public string DependsOnDescription
			{
				get { return _source.Description; }
			}
		}
	}
}
