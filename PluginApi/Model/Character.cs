using System;
using System.ComponentModel;
using System.Linq.Expressions;
using JetBrains.Annotations;
using PluginApi.Display.Helpers;

namespace PluginApi.Model
{
	public class Character : INotifyPropertyChanged, IFirePropertyChanged, IEquatable<Character>
	{
		[NotNull] protected readonly TrackingNonNullProperty<string> _gender;
		[NotNull] protected readonly TrackingNonNullProperty<string> _name;
		[NotNull] protected readonly EqualityFingerprint EqualityFields;

		public virtual event PropertyChangedEventHandler PropertyChanged;

		public Character()
		{
			_gender = new TrackingNonNullProperty<string>(string.Empty, this, () => Gender);
			_name = new TrackingNonNullProperty<string>(string.Empty, this, () => Name);
			EqualityFields = new EqualityFingerprint(_gender, _name);
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

		public bool Equals(Character other)
		{
			if (ReferenceEquals(null, other))
				return false;
			if (ReferenceEquals(this, other))
				return true;
			return Equals(other.EqualityFields, EqualityFields);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as Character);
		}

		public override int GetHashCode()
		{
			return EqualityFields.GetHashCode();
		}
	}
}
