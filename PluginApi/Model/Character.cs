using System;
using System.ComponentModel;
using System.Linq.Expressions;
using JetBrains.Annotations;
using PrintChar;

namespace PluginApi.Model
{
	public abstract class Character : INotifyPropertyChanged, IFirePropertyChanged
	{
		[NotNull] protected TrackingNonNullProperty<string> _gender;
		[NotNull] protected TrackingNonNullProperty<string> _name;
		public virtual event PropertyChangedEventHandler PropertyChanged;

		protected Character()
		{
			_gender = new TrackingNonNullProperty<string>(string.Empty, this, () => Gender);
			_name = new TrackingNonNullProperty<string>(string.Empty, this, () => Name);
		}

		[NotNull]
		public string Name
		{
			get { return _name.Value; }
			set { _name.Value = value; }
		}

		[NotNull]
		public string Gender
		{
			get { return _gender.Value; }
			set { _gender.Value = value; }
		}

		public void FirePropertyChanged(Expression<Func<object>> propertyThatChanged)
		{
			PropertyChanged.Raise(this, propertyThatChanged);
		}
	}
}