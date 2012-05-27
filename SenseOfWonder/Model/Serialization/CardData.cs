using System;
using System.ComponentModel;
using System.Linq.Expressions;
using EventBasedProgramming.Binding;
using JetBrains.Annotations;

namespace SenseOfWonder.Model.Serialization
{
	public class CardData : IFirePropertyChanged
	{
		[NotNull] private readonly TrackingOnlyInitiallyNullProperty<string> _name;
		[NotNull] private readonly TestablePropertyChangedEvent _propertyChanged = new TestablePropertyChangedEvent();
		public event PropertyChangedEventHandler PropertyChanged
		{
			add { _propertyChanged.BindTo(value); }
			remove { _propertyChanged.UnbindFrom(value); }
		}

		public CardData()
		{
			_name = new TrackingOnlyInitiallyNullProperty<string>(this, () => Name);
		}

		[CanBeNull]
		public string Name
		{
			get { return _name.Value; }
			set { _name.Value = value; }
		}

		public void FirePropertyChanged(Expression<Func<object>> propertyThatChanged)
		{
			_propertyChanged.Call(this, propertyThatChanged);
		}
	}

	public class CardDataDesignData : CardData
	{
		public CardDataDesignData()
		{
			Name = "Right Here, Right Now";
		}
	}
}
