using System;
using System.ComponentModel;
using System.Linq.Expressions;
using EventBasedProgramming.Binding;
using EventBasedProgramming.Tests.zzTestSupportData;
using JetBrains.Annotations;
using NUnit.Framework;
using FluentAssertions.EventMonitoring;

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
