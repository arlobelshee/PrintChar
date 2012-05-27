using System;
using System.ComponentModel;
using System.Linq.Expressions;
using EventBasedProgramming.Binding;

namespace EventBasedProgramming.Tests.zzTestSupportData
{
	internal class _ObjWithPropertyChangeNotification : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public string Description { get; set; }

		public void FireDescriptionChangedBecauseTestSaidTo()
		{
			PropertyChanged.Raise(this, () => Description);
		}
	}

	internal class _ObjWithTrackablePropertyChangeNotification : IFirePropertyChanged
	{
		private readonly TestablePropertyChangedEvent _propertyChanged = new TestablePropertyChangedEvent();

		public event PropertyChangedEventHandler PropertyChanged
		{
			add { _propertyChanged.BindTo(value); }
			remove { _propertyChanged.UnbindFrom(value); }
		}

		public void FirePropertyChanged(Expression<Func<object>> propertyThatChanged)
		{
			_propertyChanged.Call(this, propertyThatChanged);
		}

		public string Description { get; set; }

		public void FireDescriptionChangedBecauseTestSaidTo()
		{
			FirePropertyChanged(() => Description);
		}
	}
}
