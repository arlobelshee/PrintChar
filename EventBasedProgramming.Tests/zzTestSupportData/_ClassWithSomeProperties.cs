using System;
using System.ComponentModel;
using System.Linq.Expressions;
using EventBasedProgramming.Binding;
using JetBrains.Annotations;

namespace EventBasedProgramming.Tests.zzTestSupportData
{
	internal class _ClassWithSomeProperties : IFirePropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public void FirePropertyChanged(Expression<Func<object>> propertyThatChanged)
		{
			PropertyChanged.Raise(this, propertyThatChanged);
		}

		[NotNull]
		public string Name { get; set; }

		[NotNull]
		public string FullName
		{
			get { return Name; }
		}
	}
}